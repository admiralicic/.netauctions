using BHao.Business.Service.Contracts;
using BHao.Data.Contracts;
using BHao.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BHao.Data;
using System.ServiceModel;
using BHao.Business.Entities;
using BHao.Data.Common;
using BHao.Business.Entities.DTOs;
using BHao.Business.Service.Business_Engine;
using BHao.Common.Exceptions;

namespace BHao.Business.Service.Managers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
        ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class AukcijeManager : ManagerBase, IAukcijeService
    {
        public AukcijeManager()
        {

        }


        public Entities.Aukcija GetAukcija(int id)
        {
            try
            {
                using (BHaoDataContext context = new BHaoDataContext())
                {
                    IAukcijaRepository repo = new AukcijaRepository(context);

                    return repo.Get(id);
                }

            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        public Entities.Aukcija[] GetAllAukcije()
        {
            using (BHaoDataContext context = new BHaoDataContext())
            {
                IAukcijaRepository repo = new AukcijaRepository(context);

                return repo.GetAll().ToArray();
            }
        }

        public Aukcija KreirajAukciju(AukcijaKreirajDTO aukcijaModel, int prijavljeniKorisnik)
        {
            Aukcija aukcija = new Aukcija();

            try
            {
                using (BHaoDataContext context = new BHaoDataContext())
                {
                    Artikal artikal = new Artikal();

                    DateTime pocetakAukcije = DateTime.Now;

                    IArtikalRepository artikalRepo = new ArtikalRepository(context);
                    IAukcijaRepository aukcijaRepo = new AukcijaRepository(context);
                    IDataRepository<Slika> slikeRepo = new DataRepositoryBase<Slika>(context);

                    aukcija.ArtikalId = artikalRepo.KreirajIzAukcije(aukcijaModel.Naziv, aukcijaModel.Model, aukcijaModel.Proizvodjac);
                    aukcija.Pocetak = pocetakAukcije;
                    aukcija.Zavrsetak = pocetakAukcije.AddDays(aukcijaModel.Trajanje);
                    aukcija.MinimalnaCijena = aukcijaModel.MinimalnaCijena;
                    aukcija.KupiOdmahCijena = aukcijaModel.KupiOdmahCijena;
                    aukcija.Napomena = aukcijaModel.Napomena;
                    aukcija.DetaljanOpis = aukcijaModel.DetaljanOpis;
                    aukcija.NacinPlacanjaId = aukcijaModel.NacinPlacanjaId;
                    aukcija.KategorijaId = aukcijaModel.KategorijaId;
                    aukcija.ProdavacId = prijavljeniKorisnik;

                    aukcija = aukcijaRepo.Insert(aukcija);

                    foreach (var slika in aukcijaModel.Slike)
                    {
                        slika.AukcijaId = aukcija.Id;
                        slikeRepo.Insert(slika);
                    }

                }
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }

            return aukcija;
        }

        public void OkoncajAukciju(int aukcijaId)
        {
            throw new NotImplementedException();
        }


        public AukcijaDetailDTO[] GetAllAktivne(int kategorijaId, string stringZaPretragu)
        {
            try
            {
                using (BHaoDataContext context = new BHaoDataContext())
                {
                    IAukcijaRepository repo = new AukcijaRepository(context);

                    return repo.GetAllAktivne(kategorijaId, stringZaPretragu).ToArray();
                  
                };
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        public AukcijaDetailDTO[] GetZadnjeUspjesne(int kategorijaId, string stringZaPretragu)
        {
            try
            {
                using (BHaoDataContext context = new BHaoDataContext())
                {
                    IAukcijaRepository repo = new AukcijaRepository(context);

                    AukcijaDetailDTO[] zadnjeUspjesne = repo.GetAll(true).Where(x=>x.Aukcija.KupacId > 0).ToArray();

                    if (kategorijaId > 0)
                    {
                        zadnjeUspjesne = zadnjeUspjesne.Where(x => x.Aukcija.KategorijaId == kategorijaId).ToArray();
                    }

                    if (!String.IsNullOrEmpty(stringZaPretragu))
                    {
                        zadnjeUspjesne = zadnjeUspjesne.Where(x => x.Artikal.Naziv.ToLower().Contains(stringZaPretragu) 
                            || x.Artikal.Model.ToLower().Contains(stringZaPretragu)
                            || x.Artikal.Proizvodjac.ToLower().Contains(stringZaPretragu)).ToArray();
                    }

                    zadnjeUspjesne = zadnjeUspjesne.OrderByDescending(x => x.Aukcija.Zavrsetak)
                            .Take(8)
                            .ToArray();

                    return zadnjeUspjesne;
                };
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        public AukcijaDTO GetAukcijaDetail(int aukcijaId, int korisnikId)
        {
            try
            {
                using (BHaoDataContext context = new BHaoDataContext())
                {
                    IAukcijaRepository aukcijaRepo = new AukcijaRepository(context);
                    KorisnikRepository korisnikRepo = new KorisnikRepository(context);

                    AukcijaDTO aukcija = new AukcijaDTO();
                    aukcija = aukcijaRepo.GetAukcijaDetail(aukcijaId, korisnikId);

                    return aukcija;
                };
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        public AukcijaDTO AukcijaKreirajPonudu(int aukcijaId, decimal iznosPonude, int prijavljeniKorisnik)
        {
            try
            {
                using (BHaoDataContext context = new BHaoDataContext())
                {
                    AukcijaDTO aukcijaDTO = new AukcijaDTO();

                    Ponuda ponuda = new Ponuda()
                    {
                        AukcijaId = aukcijaId,
                        MaksimalanIznos = iznosPonude,
                        KorisnikId = prijavljeniKorisnik,
                        Vrijeme = DateTime.Now
                    };

                    try
                    {
                        aukcijaDTO = BHaoBusinessEngine.Licitiraj(ponuda, context);

                        return aukcijaDTO;

                    }
                    catch (PonudaCreateException ex)
                    {

                        throw ex;
                    }


                }
            }
            catch (FaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }


        public void KupiOdmah(int aukcijaId, int korisnikId)
        {
            try
            {
                using (BHaoDataContext context = new BHaoDataContext())
                {
                    BHaoBusinessEngine.KupiOdmah(aukcijaId, korisnikId, context);
                }
            }
            catch (FaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public void OcijeniKorisnika(OcjenaKorisnika ocjena)
        {
            try
            {
                if (ocjena.Ocjena < 1)
                    throw new FaultException("Morate odabrati ocjenu!");

                using (BHaoDataContext context = new BHaoDataContext())
                {
                    IDataRepository<OcjenaKorisnika> ocjenaRepo = new DataRepositoryBase<OcjenaKorisnika>(context);
                    AukcijaRepository aukcijaRepo = new AukcijaRepository(context);

                    Aukcija aukcija = aukcijaRepo.Get(ocjena.AukcijaId);

                    if ((ocjena.OcjenjivacId == aukcija.ProdavacId || ocjena.OcjenjivacId == aukcija.KupacId) &&
                        (ocjena.OcijenjeniKorisnikId == aukcija.ProdavacId || ocjena.OcijenjeniKorisnikId == aukcija.KupacId))
                    {
                        if (ocjenaRepo.GetAll().Where(x => x.OcjenjivacId == ocjena.OcjenjivacId &&
                            x.OcijenjeniKorisnikId == ocjena.OcijenjeniKorisnikId && x.AukcijaId == ocjena.AukcijaId).FirstOrDefault() != null)
                            throw new FaultException("Možete ocijeniti korisnika samo jednom!");
                        else
                            ocjena.Datum = DateTime.Now;
                        ocjenaRepo.Insert(ocjena);
                    }
                    else
                    {
                        throw new FaultException("Ocjenjivać i ocijenjeni korisnik moraju biti kupac ili prodavac!");
                    }


                }
            }
            catch (FaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public void OcijeniArtikal(OcjenaArtikla ocjena)
        {
            try
            {
                if (ocjena.Ocjena < 1)
                    throw new FaultException("Morate odabrati ocjenu!");

                using (BHaoDataContext context = new BHaoDataContext())
                {

                    IDataRepository<OcjenaArtikla> ocjenaRepo = new DataRepositoryBase<OcjenaArtikla>(context);
                    AukcijaRepository aukcijaRepo = new AukcijaRepository(context);

                    Aukcija aukcija = aukcijaRepo.Get(ocjena.AukcijaId);

                    if (aukcija.KupacId == ocjena.OcijenioId)
                    {
                        if (ocjenaRepo.GetAll().Where(x => x.ArtikalId == ocjena.ArtikalId && x.OcijenioId == aukcija.KupacId && x.AukcijaId == ocjena.AukcijaId).FirstOrDefault() != null)
                        {
                            throw new FaultException("Možete ocijeniti artikal samo jednom!");
                        }
                        else
                        {
                            ocjena.Datum = DateTime.Now;
                            ocjenaRepo.Insert(ocjena);
                        }
                    }
                    else
                    {
                        throw new FaultException("Morate biti kupac da bi mogli ocijeniti artikal!");
                    }
                }
            }
            catch (FaultException ex)
            {

                throw ex;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }

        }

        public void DodajKomentar(KomentarKorisnika komentar)
        {
            try
            {
                if (komentar.TextKomentara == null || String.IsNullOrEmpty(komentar.TextKomentara))
                    throw new FaultException("Morate unijeti tekst komentara!");

                using (BHaoDataContext context = new BHaoDataContext())
                {
                    IDataRepository<KomentarKorisnika> repo = new DataRepositoryBase<KomentarKorisnika>(context);
                    if (repo.GetAll().Where(x => x.AukcijaId == komentar.AukcijaId && x.KomentatorId == komentar.KomentatorId).FirstOrDefault() != null)
                        throw new FaultException("Već ste jednom komentirali!");

                    komentar.Datum = DateTime.Now;
                    repo.Insert(komentar);
                }
            }
            catch (FaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public void DodajKomentarArtikla(KomentarArtikla komentar)
        {
            try
            {
                if (komentar.TextKomentara == null || string.IsNullOrEmpty(komentar.TextKomentara))
                    throw new FaultException("Morate unijeti tekst komentara!");

                using (BHaoDataContext context = new BHaoDataContext())
                {
                    IDataRepository<KomentarArtikla> repo = new DataRepositoryBase<KomentarArtikla>(context);
                    if (repo.GetAll().Where(x => x.ArtikalId == komentar.ArtikalId && x.KomentatorId == komentar.KomentatorId).FirstOrDefault() != null)
                        throw new FaultException("Već ste jednom komentirali artikal!");

                    komentar.Datum = DateTime.Now;
                    repo.Insert(komentar);
                }
            }
            catch (FaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public AukcijaDetailDTO[] GetAukcijeByKorisnik(int korisnikId, string kriterij)
        {

            try
            {
                using (BHaoDataContext context = new BHaoDataContext())
                {
                    AukcijaRepository repo = new AukcijaRepository(context);

                    return repo.GetAllZaKorisnika(korisnikId, kriterij).ToArray();
                }
            }
            catch (FaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }

        }


        public AukcijaDTO[] GetPreporuke(int artikalId, int prijavljeniKorisnikId)
        {
            try
            {
                using (BHaoDataContext context = new BHaoDataContext())
                {
                    Preporuka preporuka = new Preporuka(context);

                    return preporuka.GetSlicneArtikle(artikalId, prijavljeniKorisnikId).ToArray();
                }
            }
            catch (FaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }

        }


        public AukcijaDetailDTO[] GetAllZavrsene(string filterPlacanja, string stringZaPretragu)
        {
            try
            {
                using (BHaoDataContext context = new BHaoDataContext())
                {
                    AukcijaRepository repo = new AukcijaRepository(context);

                    AukcijaDetailDTO[] aukcije = repo.GetAllZavrsene().ToArray();

                    if (filterPlacanja == "placene")
                    {
                        aukcije = aukcije.Where(x => x.Aukcija.DatumPlacanja != null).ToArray();
                    }

                    if (filterPlacanja == "neplacene")
                    {
                        aukcije = aukcije.Where(x => x.Aukcija.DatumPlacanja == null).ToArray();
                    }

                    if (!String.IsNullOrEmpty(stringZaPretragu))
                    {
                        aukcije = aukcije.Where(x => x.Aukcija.Id.ToString() == stringZaPretragu ||
                                                    x.Artikal.Naziv.ToLower().Contains(stringZaPretragu) ||
                                                    x.Artikal.Proizvodjac.ToLower().Contains(stringZaPretragu) ||
                                                    x.Artikal.Model.ToLower().Contains(stringZaPretragu)).ToArray();
                    }
                    return aukcije;
                }
            }
            catch (FaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }




        public void EvidentirajPlacanje(int aukcijaId, DateTime datumPlacanja)
        {
            using (BHaoDataContext context = new BHaoDataContext())
            {
                try
                {
                    AukcijaRepository repo = new AukcijaRepository(context);
                    Aukcija aukcija = repo.Get(aukcijaId);
                    aukcija.DatumPlacanja = datumPlacanja;
                    repo.Update(aukcija);
                }
                catch (FaultException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new FaultException(ex.Message);
                }
            }
        }


        public AukcijaDetailDTO[] GetAll(bool zavrsena)
        {
            using (BHaoDataContext context = new BHaoDataContext())
            {
                try
                {
                    AukcijaRepository repo = new AukcijaRepository(context);
                    AukcijaDetailDTO[] aukcije = repo.GetAll(zavrsena).ToArray();
                    return aukcije;
                }
                catch (FaultException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw new FaultException(ex.Message);
                }
            }
        }


        public void Ukloni(int aukcijaId)
        {

            try
            {
                using (BHaoDataContext context = new BHaoDataContext())
                {
                    AukcijaRepository repo = new AukcijaRepository(context);
                    Aukcija aukcija = repo.Get(aukcijaId);
                    aukcija.isDeleted = true;
                    aukcija.Zavrsena = true;
                    aukcija.Zavrsetak = DateTime.Now;
                    repo.Update(aukcija);

                    Artikal artikal = context.Artikli.Find(aukcija.ArtikalId);

                    Poruka poruka = new Poruka()
                    {
                        Datum = DateTime.Now,
                        PosiljalacId = 14,
                        PrimalacId = aukcija.ProdavacId,
                        TextPoruke = "Vaša aukcija za " + artikal.Naziv + " " + artikal.Proizvodjac + " " + artikal.Model + " je uklonjena!",
                        AukcijaId = aukcija.Id
                    };

                    PorukaRepository porukaRepo = new PorukaRepository(context);
                    porukaRepo.Insert(poruka);
                }
            }
            catch (FaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public StatistikaDTO GetStatistika(int kategorijaId, DateTime? prikazOd, DateTime? prikazDo)
        {

            try
            {
                using (BHaoDataContext context = new BHaoDataContext())
                {
                    AukcijaRepository repo = new AukcijaRepository(context);

                    IEnumerable<AukcijaDetailDTO> aukcije = repo.GetAll(null);

                    if (kategorijaId > 0)
                        aukcije = aukcije.Where(x => x.Aukcija.KategorijaId == kategorijaId).ToList();

                    if (prikazOd != null && prikazDo != null)
                        aukcije = aukcije.Where(x => (x.Aukcija.Pocetak >= prikazOd && x.Aukcija.Pocetak <= prikazDo) || (x.Aukcija.Zavrsetak >= prikazOd && x.Aukcija.Zavrsetak <= prikazDo)).ToList();

                    StatistikaDTO statistika = new StatistikaDTO();
                    if (aukcije.Count() > 0)
                    {
                        statistika.BrojAukcija = aukcije.Count();
                        statistika.ListaAukcija = aukcije.OrderByDescending(x=>x.Aukcija.Pocetak);
                        statistika.ProsjecnaVrijednostAukcija = (decimal)aukcije.Where(x => x.Aukcija.Zavrsena && x.Aukcija.NajvecaPonuda > 0)
                            .Average(x => x.Aukcija.NajvecaPonuda);

                        statistika.NajcesceProdavaniArtikli = aukcije.GroupBy(x => x.Artikal).Select(group => new NajcesciArtikliDTO
                                                {
                                                    Artikal = group.Key.Proizvodjac + " " + group.Key.Model + " - " + group.Key.Naziv,
                                                    Count = group.Count()
                                                }).OrderByDescending(x => x.Count).Take(10);
                    }
                    else
                    {
                        statistika.BrojAukcija = 0;
                        statistika.ProsjecnaVrijednostAukcija = 0;
                    }



                    return statistika;

                }
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
