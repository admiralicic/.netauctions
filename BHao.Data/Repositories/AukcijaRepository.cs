using BHao.Business.Entities;
using BHao.Data.Common;
using BHao.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BHao.Business.Entities.DTOs;

namespace BHao.Data.Repositories
{
    public class AukcijaRepository : DataRepositoryBase<Aukcija>, IAukcijaRepository
    {
        public AukcijaRepository( BHaoDataContext context )
            : base( context )
        {

        }

        public IEnumerable<AukcijaDetailDTO> GetAll(bool? zavrsena)
        {
            var query = from a in _context.Aukcije
                        join ar in _context.Artikli on a.ArtikalId equals ar.Id
                        join pr in _context.Users on a.ProdavacId equals pr.Id
                        join np in _context.NaciniPlacanja on a.NacinPlacanjaId equals np.Id
                        join ka in _context.Kategorije on a.KategorijaId equals ka.Id
                        select new AukcijaDetailDTO()
                        {
                            Aukcija = a,
                            Artikal = ar,
                            NacinPlacanja = np,
                            Kategorija = ka,
                            Slike = a.Slike
                        };

            if (zavrsena != null)
                query = query.Where(x => x.Aukcija.Zavrsena == zavrsena && x.Aukcija.isDeleted == false);

            IEnumerable<AukcijaDetailDTO> aukcije = query.ToList().OrderBy(x => x.Aukcija.Zavrsetak);

            KorisnikRepository korisnikRepo = new KorisnikRepository(_context);

            foreach (var item in aukcije)
            {
                item.Prodavac = korisnikRepo.GetById(item.Aukcija.ProdavacId);
                item.Kupac = item.Aukcija.KupacId != null ? korisnikRepo.GetById((int)item.Aukcija.KupacId) : null;
            }

            return aukcije;
        }


        public AukcijaDTO GetAukcijaDetail(int aukcijaId, int korisnikId)
        {
            
            var query = from a in _context.Aukcije.Include(x => x.Slike)
                        where a.Id == aukcijaId
                        join ar in _context.Artikli on a.ArtikalId equals ar.Id
                        join n in _context.NaciniPlacanja on a.NacinPlacanjaId equals n.Id
                        select new AukcijaDTO()
                        {
                               Id = a.Id,
                               Pocetak = a.Pocetak,
                               Zavrsetak = a.Zavrsetak,
                               NazivArtikla = ar.Naziv,
                               Proizvodjac = ar.Proizvodjac,
                               Model = ar.Model,
                               PocetnaCijena = a.MinimalnaCijena,
                               KupiOdmahCijena = a.KupiOdmahCijena,
                               DetaljanOpis  = a.DetaljanOpis,
                               ProdavacId  = a.ProdavacId,
                               NacinPlacanja  = n.Opis,
                               Napomena = a.Napomena,
                               Slike = a.Slike,
                               NajveciPonudjacId = a.NajveciPonudjacId,
                               Zavrsena = a.Zavrsena,
                               ArtikalId = (int)a.ArtikalId
                        };

            AukcijaDTO aukcijaDTO =  query.FirstOrDefault();

            KorisnikRepository korisnikRepo = new KorisnikRepository(_context);

            if (aukcijaDTO.NajveciPonudjacId != 0)
            {
                Ponuda najvecaPonuda = _context.Ponude
                    .Where(x => x.KorisnikId == aukcijaDTO.NajveciPonudjacId && x.AukcijaId == aukcijaDTO.Id)
                    .OrderByDescending(x => x.Iznos)
                    .Take(1).SingleOrDefault();

                aukcijaDTO.NajvecaPonuda = najvecaPonuda;

                aukcijaDTO.NajveciPonudjac = korisnikRepo.GetById(aukcijaDTO.NajveciPonudjacId);

            }
            aukcijaDTO.Prodavac = korisnikRepo.GetById(aukcijaDTO.ProdavacId);

            PonudaRepository ponudaRepo = new PonudaRepository(_context);
            aukcijaDTO.Ponude = ponudaRepo.GetPonudeByAukcijaId(aukcijaDTO.Id);

            IDataRepository<OcjenaKorisnika> ocjenaKorisnikaRepo = new DataRepositoryBase<OcjenaKorisnika>(_context);
            aukcijaDTO.OcjeneKorisnika = ocjenaKorisnikaRepo.GetAll().Where(x => x.AukcijaId == aukcijaDTO.Id).ToList();

            IDataRepository<OcjenaArtikla> ocjenaArtiklaRepo = new DataRepositoryBase<OcjenaArtikla>(_context);
            aukcijaDTO.OcjeneArtikla = ocjenaArtiklaRepo.GetAll().Where(x => x.ArtikalId == aukcijaDTO.ArtikalId).ToList();

            IDataRepository<KomentarKorisnika> komentarKorisnikaRepo = new DataRepositoryBase<KomentarKorisnika>(_context);
            KomentarKorisnika komentar = komentarKorisnikaRepo.GetAll().Where(x => x.KomentatorId == korisnikId && x.AukcijaId == aukcijaDTO.Id).FirstOrDefault();
            if (komentar!=null && komentar.isDeleted)
            {
                komentar.TextKomentara = "Komentar obrisan.";
            }
            aukcijaDTO.KomentarKorisnika = komentar;

            IDataRepository<KomentarArtikla> komentarArtiklaRepo = new DataRepositoryBase<KomentarArtikla>(_context);
            KomentarArtikla komentarArtikla = komentarArtiklaRepo.GetAll().Where(x => x.KomentatorId == korisnikId && x.AukcijaId == aukcijaDTO.Id).FirstOrDefault();
            if (komentarArtikla != null && komentarArtikla.isDeleted)
            {
                komentarArtikla.TextKomentara = "Komentar obrisan.";
            }
            aukcijaDTO.KomentarArtikla = komentarArtikla;


            ArtikalRepository artikalRepo = new ArtikalRepository(_context);
            Artikal artikal = artikalRepo.GetByIdWithProsjecnaOcjena(aukcijaDTO.ArtikalId);
            aukcijaDTO.Artikal = artikal;

            return aukcijaDTO;
        }

        public IEnumerable<Aukcija> GetAllNezavrsene()
        {
            return _context.Aukcije.Where(x=>x.Zavrsena == false && x.Zavrsetak < DateTime.Now).ToList();
        }

        public IEnumerable<AukcijaDetailDTO> GetAllAktivne(int kategorijaId = 0, string stringZaPretragu = "")
        {
            var query = from a in _context.Aukcije
                        join ar in _context.Artikli on a.ArtikalId equals ar.Id
                        join pr in _context.Users on a.ProdavacId equals pr.Id
                        join np in _context.NaciniPlacanja on a.NacinPlacanjaId equals np.Id
                        join ka in _context.Kategorije on a.KategorijaId equals ka.Id  
                        where a.Zavrsena == false && (ar.Naziv.ToLower().Contains(stringZaPretragu) ||
                                                        ar.Proizvodjac.ToLower().Contains(stringZaPretragu) ||
                                                        ar.Model.ToLower().Contains(stringZaPretragu) ||
                                                string.IsNullOrEmpty(stringZaPretragu)) &&
                                                (a.KategorijaId == kategorijaId || kategorijaId == 0)
                        select new AukcijaDetailDTO()
                        {
                            Aukcija = a,
                            Artikal = ar,
                            NacinPlacanja = np,
                            Kategorija = ka,  
                            Slike = a.Slike
                        };
            
            return query.ToList().OrderBy(x=>x.Aukcija.Zavrsetak);
        }

        
        public IEnumerable<Ponuda> GetPonude( int aukcijaId )
        {
            return _context.Ponude.Where( x => x.AukcijaId == aukcijaId ).ToList();
        }

        public Ponuda GetNajvecaPonuda( int aukcijaId )
        {
            return _context.Ponude.OrderByDescending( x => x.Iznos ).FirstOrDefault( );
        }

        public IEnumerable<AukcijaDetailDTO> GetAllZavrsene( )
        {
            KorisnikRepository korisnikRepo = new KorisnikRepository(_context);

            var query = from a in _context.Aukcije
                        join ar in _context.Artikli on a.ArtikalId equals ar.Id
                        join pr in _context.Users on a.ProdavacId equals pr.Id
                        join np in _context.NaciniPlacanja on a.NacinPlacanjaId equals np.Id
                        join ka in _context.Kategorije on a.KategorijaId equals ka.Id
                        where a.Zavrsena == true && a.KupacId > 0
                        select new AukcijaDetailDTO()
                        {
                            Aukcija = a,
                            Artikal = ar,
                            NacinPlacanja = np,
                            Kategorija = ka,
                            Slike = a.Slike
                           
                        };

            IEnumerable<AukcijaDetailDTO> aukcije = query.ToList().OrderBy(x => x.Aukcija.Zavrsetak);

            foreach (var item in aukcije)
            {
                item.Prodavac = korisnikRepo.GetById(item.Aukcija.ProdavacId);
                item.Kupac = korisnikRepo.GetById((int)item.Aukcija.KupacId);
            }

            return aukcije;
        }

        public IEnumerable<AukcijaDetailDTO> GetAllZaKorisnika( int korisnikId, string kriterij )
        {

            IEnumerable<AukcijaDetailDTO> query;
            
            switch (kriterij)
            {
                case "kupac":
                    query = from a in _context.Aukcije.Include(a=>a.Ponude).Where(x => x.KupacId == korisnikId || 
                        x.Ponude.Where(p=>p.KorisnikId == korisnikId).ToList().Count()>0)
                            select new AukcijaDetailDTO()
                            {
                                Aukcija = a,
                                Slike = a.Slike,
                                Artikal = a.Artikal
                            };
                    break;
                case "prodavac":
                    query = from a in _context.Aukcije.Where(x => x.ProdavacId == korisnikId)
                            select new AukcijaDetailDTO()
                            {
                                Aukcija = a,
                                Slike = a.Slike,
                                Artikal = a.Artikal
                            };
                    break;
                default:
                    query = from a in _context.Aukcije.Include(a => a.Ponude).Where(x => x.ProdavacId == korisnikId || 
                        x.KupacId == korisnikId || 
                        x.Ponude.Where(p => p.KorisnikId == korisnikId).ToList().Count() > 0)
                                select new AukcijaDetailDTO()
                                {
                                    Aukcija = a,
                                    Slike = a.Slike,
                                    Artikal = a.Artikal
                                };
                    break;
            }
            

           List<AukcijaDetailDTO> aukcijeKorisnika = query.OrderByDescending(x=>x.Aukcija.Zavrsetak).ToList();
           KorisnikRepository korisnikRepo = new KorisnikRepository(_context);
           foreach (var item in aukcijeKorisnika)
           {
               item.Kupac = item.Aukcija.KupacId != null ? korisnikRepo.GetById((int)item.Aukcija.KupacId) : null;
               item.Prodavac = korisnikRepo.GetById(item.Aukcija.ProdavacId);
           }

           return aukcijeKorisnika;
                           
        }

        public IEnumerable<Aukcija> GetAktivneZaKorisnika( int korisnikId )
        {
            return _context.Aukcije.Where( x => ( x.KupacId == korisnikId || x.ProdavacId == korisnikId ) && x.Zavrsena == false ).ToList();
        }

        public IEnumerable<Aukcija> GetZavrseneZaKorisnika( int korisnikId )
        {
            return _context.Aukcije.Where( x => ( x.KupacId == korisnikId || x.ProdavacId == korisnikId ) && x.Zavrsena == true ).ToList();
        }


        public IEnumerable<AukcijaDTO> GetAukcijeAktivneByArtikal(int artikalId)
        {

            var query = from a in _context.Aukcije.Where(x => x.Zavrsena == false && x.ArtikalId == artikalId)
                        select new AukcijaDTO()
                        {
                            Id = a.Id,
                            NazivArtikla = a.Artikal.Naziv,
                            Proizvodjac = a.Artikal.Proizvodjac,
                            Model = a.Artikal.Model,
                            Slike = a.Slike
                        };

            return query.ToList();

        }
    }
}
