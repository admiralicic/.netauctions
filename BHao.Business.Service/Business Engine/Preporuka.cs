using BHao.Business.Entities;
using BHao.Business.Entities.DTOs;
using BHao.Data;
using BHao.Data.Contracts;
using BHao.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BHao.Business.Service.Business_Engine
{
    // preuzeto iz materijala predmeta Web Tehnologije - FIT

    public class Preporuka
    {
        BHaoDataContext context;

        public Preporuka(BHaoDataContext _context)
        {
            context = _context;
        }

        Dictionary<int, List<OcjenaArtikla>> artikli = new Dictionary<int, List<OcjenaArtikla>>();
        int currentUserID;
        #region Item-based preporuka
        
        //Funkcija koja se poziva iz web dijela aplikacije
        public List<AukcijaDTO> GetSlicneArtikle(int artikalId, int userID)
        {
            logiraniUserID = userID;  

            UcitajArtikle(artikalId);

            List<OcjenaArtikla> ocjene = context.OcjeneArtikala.Where(x => x.ArtikalId == artikalId).OrderBy(x => x.OcijenioId).ToList();

            List<OcjenaArtikla> zajednickeOcjene1 = new List<OcjenaArtikla>();
            List<OcjenaArtikla> zajednickeOcjene2 = new List<OcjenaArtikla>();

            //List<Artikli> preporuceno = new List<Artikli>();
            List<AukcijaDTO> preporuceno = new List<AukcijaDTO>();

            //Petlja svih proizvoda (ne uključujući onaj koji je proslijeđen u funkciju)
            foreach (var item in artikli)
            {
                foreach (OcjenaArtikla ocjena1 in ocjene) //Sve ocjene aktivnog proizvoda
                {
                    //Provjeriti da li je naredni proizvod (iz liste proizvodi) ocijenio isti kupac
                    if (item.Value.Where(x => x.OcijenioId == ocjena1.OcijenioId).Count() != 0)
                    {
                        zajednickeOcjene1.Add(ocjena1);
                        zajednickeOcjene2.Add(item.Value.Where(x => x.OcijenioId == ocjena1.OcijenioId).First());
                    }
                }

                //Za računanje sličnosti se uzimaju samo zajedničke ocjene, odnosno ocjene istih kupaca za oba proizvoda
                double slicnost = GetSlicnost(zajednickeOcjene1, zajednickeOcjene2);
                if (slicnost > 0.6)//Granična vrijednost (treshold)
                {                    
                   
                    List<AukcijaDTO> aukcije = new List<AukcijaDTO>();
                    List<OcjenaArtikla> ocjeneSelectByKupacArtikal = new List<OcjenaArtikla>();

                    IAukcijaRepository aukcijeRepo = new AukcijaRepository(context);

                    aukcije = aukcijeRepo.GetAukcijeAktivneByArtikal(item.Key).ToList();
                    
                    if (aukcije.Count > 0)
                    {
                        foreach (var a in aukcije)
                        {
                            ocjeneSelectByKupacArtikal = item.Value.Where(x => x.OcijenioId == logiraniUserID).ToList();
                            if (ocjeneSelectByKupacArtikal.Count < 1)
                            {
                                preporuceno.Add(a);
                            }
                            
                        }
                    }

                }
                zajednickeOcjene1.Clear();
                zajednickeOcjene2.Clear();

            }

            //Lista preporučenih proizvoda
            return preporuceno;
        }

        //Učitava se kolekcija proizvoda i njihovih ocjena (osim onog koji se trenutno pregleda)
        private void UcitajArtikle(int artikalId)
        {
            List<Artikal> aktivniArtikli = context.Artikli.Where(x => x.Id != artikalId).ToList();
            List<OcjenaArtikla> ocjene = new List<OcjenaArtikla>();
            foreach (Artikal item in aktivniArtikli)
            {
                ocjene = context.OcjeneArtikala.Where(x => x.ArtikalId == item.Id).OrderBy(x => x.OcijenioId).ToList();

                if (ocjene.Count > 0)
                    artikli.Add(item.Id, ocjene);
            }
        }

        //Cosine similarity
        double GetSlicnost(List<OcjenaArtikla> ocjene1, List<OcjenaArtikla> ocjene2)
        {
            if (ocjene1.Count != ocjene2.Count)
                return 0;

            int brojnik = 0;
            double int1 = 0;
            double int2 = 0;

            for(int i=0; i<ocjene1.Count; i++)
            {
                brojnik += ocjene1[i].Ocjena * ocjene2[i].Ocjena;
                int1 += ocjene1[i].Ocjena * ocjene1[i].Ocjena;
                int2 += ocjene2[i].Ocjena * ocjene2[i].Ocjena;
            }

            int1 = Math.Sqrt(int1);
            int2 = Math.Sqrt(int2);

            double nazivnik = int1 * int2;

            if (nazivnik != 0)
                return brojnik / nazivnik;

            return 0;

        }

        #endregion

        public int logiraniUserID { get; set; }
    }
}
