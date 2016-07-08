using BHao.Business.Entities;
using BHao.Data;
using BHao.Data.Common;
using BHao.Data.Contracts;
using BHao.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BHao.Common.Exceptions;
using System.ServiceModel;
using BHao.Business.Entities.DTOs;

namespace BHao.Business.Service.Business_Engine
{
    public static class BHaoBusinessEngine
    {
        //private static BHaoDataContext _context;

        static BHaoBusinessEngine()
        {
            //_context = new BHaoDataContext();
        }

        public static AukcijaDTO Licitiraj(Ponuda ponuda, BHaoDataContext _context)
        {
            IAukcijaRepository aukcijaRepo = new AukcijaRepository(_context);
            IInkrementRepository inkrementRepo = new InkrementRepository(_context);
            IDataRepository<Ponuda> ponudaRepo = new DataRepositoryBase<Ponuda>(_context);
            KorisnikRepository korisnikRepo = new KorisnikRepository(_context);

            AukcijaDTO aukcijaDTO = new AukcijaDTO();

            Aukcija aukcija = aukcijaRepo.Get(ponuda.AukcijaId);

            if (aukcija.ProdavacId == ponuda.KorisnikId)
            {
                throw new PonudaCreateException("Prodavac ne može licitirati na vlastitu aukciju!");
            }

            if (ponuda.MaksimalanIznos <= aukcija.MinimalnaCijena || ponuda.MaksimalanIznos <= aukcija.NajvecaPonuda)
            {
                throw new PonudaCreateException("Maksimalna ponuda mora biti veća od početne cijene ili trenutne najveće ponude!");

            }

            if (aukcija.Zavrsena || aukcija.Zavrsetak < DateTime.Now)
            {
                throw new PonudaCreateException("Vrijeme aukcije isteklo, aukcija završena!");
            }

            List<Ponuda> topDvijePonude = ponudaRepo.GetAll().Where(x => x.AukcijaId == ponuda.AukcijaId)
                .OrderByDescending(x => x.MaksimalanIznos).ThenByDescending(x=>x.Id).Take(2).ToList();
            
            Ponuda trenutnaNajveca = new Ponuda();

            if (topDvijePonude.Count>0)
            {
                trenutnaNajveca = topDvijePonude[0];
            }

            if (aukcija.NajvecaPonuda == null)
            {
                aukcija.NajvecaPonuda = aukcija.MinimalnaCijena > 0 ? aukcija.MinimalnaCijena : 1;
                ponuda.Iznos = (decimal)aukcija.NajvecaPonuda;

                ponudaRepo.Insert(ponuda);

                aukcija.NajvecaPonuda = ponuda.Iznos;
                aukcija.NajveciPonudjacId = ponuda.KorisnikId;
                aukcijaRepo.Update(aukcija);

                aukcijaDTO.NajvecaPonuda = ponuda;
            }
            else 
            {
                if (aukcija.NajveciPonudjacId == ponuda.KorisnikId)
                    throw new PonudaCreateException("Već imate najveću ponudu!");

                //Inkrement inkrement = inkrementRepo.GetInkrementByIznosPonude((decimal)aukcija.NajvecaPonuda);

                if (ponuda.MaksimalanIznos > trenutnaNajveca.MaksimalanIznos)
                {
                    Inkrement inkrement = inkrementRepo.GetInkrementByIznosPonude(trenutnaNajveca.MaksimalanIznos);
                    if ((trenutnaNajveca.MaksimalanIznos + inkrement.IznosInkrementa) <= ponuda.MaksimalanIznos)
                    {
                        ponuda.Iznos = trenutnaNajveca.MaksimalanIznos + inkrement.IznosInkrementa;
                    }
                    else
                    {
                        ponuda.Iznos = ponuda.MaksimalanIznos;
                    }

                    

                    ponudaRepo.Insert(ponuda);

                    if (trenutnaNajveca.Iznos != trenutnaNajveca.MaksimalanIznos)
                    {
                        trenutnaNajveca.Iznos = trenutnaNajveca.MaksimalanIznos;
                        ponudaRepo.Insert(trenutnaNajveca);
                    }
                    

                    aukcija.NajvecaPonuda = ponuda.Iznos;
                    aukcija.NajveciPonudjacId = ponuda.KorisnikId;
                    aukcijaRepo.Update(aukcija);

                    aukcijaDTO.NajvecaPonuda = ponuda;

                }
                else
                {
                    Ponuda novaNajveca = new Ponuda();
                    novaNajveca.AukcijaId = trenutnaNajveca.AukcijaId;
                    novaNajveca.KorisnikId = trenutnaNajveca.KorisnikId;
                    novaNajveca.MaksimalanIznos = trenutnaNajveca.MaksimalanIznos;
                    
                    Inkrement inkrement = inkrementRepo.GetInkrementByIznosPonude(ponuda.MaksimalanIznos);
                    if (trenutnaNajveca.MaksimalanIznos > (ponuda.MaksimalanIznos+inkrement.IznosInkrementa))
                    {
                        novaNajveca.Iznos = ponuda.MaksimalanIznos + inkrement.IznosInkrementa;
                        novaNajveca.Vrijeme = DateTime.Now;
                    }
                    else
                    {
                        novaNajveca.Iznos = trenutnaNajveca.MaksimalanIznos;
                        novaNajveca.Vrijeme = DateTime.Now;
                    }
                    
                    ponuda.Iznos = ponuda.MaksimalanIznos;

                    ponudaRepo.Insert(ponuda);
                    ponudaRepo.Insert(novaNajveca);

                    aukcija.NajvecaPonuda = novaNajveca.Iznos;
                    aukcija.NajveciPonudjacId = novaNajveca.KorisnikId;
                    aukcijaRepo.Update(aukcija);

                    aukcijaDTO.NajvecaPonuda = novaNajveca;

                }
                
                //ponuda.Iznos = (decimal)aukcija.NajvecaPonuda + inkrement.IznosInkrementa < ponuda.MaksimalanIznos 
                //    ? (decimal)aukcija.NajvecaPonuda + inkrement.IznosInkrementa : ponuda.MaksimalanIznos; 
            }

            
            

            //if (topDvijePonude.Count > 1)
            //{
            //    Ponuda ponuda1 = topDvijePonude.First();
            //    Ponuda ponuda2 = topDvijePonude.Last();

            //    if (ponuda1.KorisnikId != ponuda2.KorisnikId)
            //    {

            //        Inkrement inkrement = inkrementRepo.GetInkrementByIznosPonude(ponuda2.MaksimalanIznos);

            //        ponuda1.Iznos = (ponuda2.MaksimalanIznos + inkrement.IznosInkrementa) > ponuda1.MaksimalanIznos ? ponuda1.MaksimalanIznos : ponuda2.MaksimalanIznos + inkrement.IznosInkrementa;

            //        ponudaRepo.Insert(ponuda1);


            //        aukcija.NajvecaPonuda = ponuda1.Iznos;
            //        aukcija.NajveciPonudjacId = ponuda1.KorisnikId;
            //        aukcijaRepo.Update(aukcija);

            //        aukcijaDTO.NajveciPonudjacId = aukcija.NajveciPonudjacId;
            //        aukcijaDTO.NajvecaPonuda = ponuda1;
            //        aukcijaDTO.NajveciPonudjac = korisnikRepo.GetById(aukcija.NajveciPonudjacId);
            //        aukcijaDTO.Prodavac = korisnikRepo.GetById(aukcija.ProdavacId);
            //        return aukcijaDTO;
            //    }


            //    aukcijaDTO.NajveciPonudjacId = ponuda1.KorisnikId;
            //    aukcijaDTO.NajvecaPonuda = ponuda1;
            //    aukcijaDTO.NajveciPonudjac = korisnikRepo.GetById(ponuda1.KorisnikId);
                
            //    return aukcijaDTO;
            //}

            //aukcija.NajvecaPonuda = ponuda.Iznos;
            //aukcija.NajveciPonudjacId = ponuda.KorisnikId;
            //aukcijaRepo.Update(aukcija);

            //aukcijaDTO.NajvecaPonuda = ponuda;
            aukcijaDTO.NajveciPonudjacId = aukcija.NajveciPonudjacId;
            aukcijaDTO.NajveciPonudjac = korisnikRepo.GetById(aukcija.NajveciPonudjacId);
            aukcijaDTO.Prodavac = korisnikRepo.GetById(aukcija.ProdavacId);
            return aukcijaDTO;
        }

        public static List<Aukcija> ProvjeriZavrsene()
        {

            List<Aukcija> OkoncaneAukcije = new List<Aukcija>();
            using (BHaoDataContext context = new BHaoDataContext())
            {
                IAukcijaRepository aukcijaRepo = new AukcijaRepository(context);
                IEnumerable<Aukcija> aukcije = aukcijaRepo.GetAllNezavrsene();

                foreach (var aukcija in aukcije)
                {
                    aukcija.Zavrsena = true;
                    aukcija.Provizija = ObracunProvizije(aukcija);
                    if(aukcija.NajveciPonudjacId > 0)
                        aukcija.KupacId = aukcija.NajveciPonudjacId;
                    aukcijaRepo.Update(aukcija);
                    OkoncaneAukcije.Add(aukcija);
                }
            }

            return OkoncaneAukcije;
        }

        public static void KupiOdmah(int aukcijaId, int korisnikId, BHaoDataContext context)
        {
            IAukcijaRepository aukcijaRepo = new AukcijaRepository(context);
            IPonudaRepository ponudaRepo = new PonudaRepository(context);

            Aukcija aukcija = aukcijaRepo.Get(aukcijaId);

            if (!aukcija.Zavrsena && aukcija.Zavrsetak > DateTime.Now)
            {
                Ponuda ponuda = new Ponuda();
                ponuda.Vrijeme = DateTime.Now;
                ponuda.Iznos = (decimal)aukcija.KupiOdmahCijena;
                ponuda.MaksimalanIznos = (decimal)aukcija.KupiOdmahCijena;
                ponuda.KorisnikId = korisnikId;
                ponuda.AukcijaId = aukcija.Id;

                ponudaRepo.Insert(ponuda);
                
                aukcija.Zavrsetak = ponuda.Vrijeme;
                aukcija.KupacId = korisnikId;
                aukcija.Zavrsena = true;
                aukcija.NajveciPonudjacId = korisnikId;
                aukcija.NajvecaPonuda = aukcija.KupiOdmahCijena;
                aukcija.Provizija = ObracunProvizije(aukcija);

                aukcijaRepo.Update(aukcija);
            }
            else
            {
                throw new Exception("Aukcija već zavrsena!");
            }
        }

        public static decimal ObracunProvizije(Aukcija aukcija)
        {
            decimal procent = 0.05M;

            return aukcija.NajvecaPonuda > 0 ? (decimal)aukcija.NajvecaPonuda * procent : 0;
        }
    }
}
