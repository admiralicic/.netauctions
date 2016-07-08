using BHao.Business.Entities;
using BHao.Business.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHao.Data.Repositories
{
    public class KorisnikRepository
    {
        private BHaoDataContext _context;

        public KorisnikRepository(BHaoDataContext context)
        {
            _context = context;
        }

        public KorisnikDTO GetById(int korisnikId)
        {
            Korisnik korisnik = _context.Users.Where(x => x.Id == korisnikId).FirstOrDefault();

            KorisnikDTO korisnikDTO = new KorisnikDTO()
            {
                Id = korisnik.Id,
                Ime = korisnik.Ime,
                Prezime = korisnik.Prezime,
                Ulica = korisnik.Ulica,
                Broj = korisnik.Broj,
                PostanskiBroj = korisnik.PostanskiBroj,
                Telefon = korisnik.Telefon,
                GradId = korisnik.GradId,
                Email = korisnik.Email,
                KorisnickoIme = korisnik.UserName
            };

            korisnikDTO.ProsjecnaOcjena = ProsjecnaOcjena(korisnikDTO.Id);

            KomentarRepository komentarRepo = new KomentarRepository(_context);
            korisnikDTO.KomentariKorisnika = komentarRepo.GetKomentariByKorisnikId(korisnik.Id);

            return korisnikDTO;
        }

        public KorisnikDTO GetByKorisnickoIme(string korisnickoIme)
        {
            Korisnik korisnik =  _context.Users.Where(x => x.UserName == korisnickoIme).FirstOrDefault();

            KorisnikDTO korisnikDTO = new KorisnikDTO()
            {
                Id = korisnik.Id,
                Ime = korisnik.Ime,
                Prezime = korisnik.Prezime,
                Ulica = korisnik.Ulica,
                Broj = korisnik.Broj,
                PostanskiBroj = korisnik.PostanskiBroj,
                Telefon = korisnik.Telefon,
                GradId = korisnik.GradId,
                Email = korisnik.Email,
                KorisnickoIme = korisnik.UserName
            };

            korisnikDTO.ProsjecnaOcjena = ProsjecnaOcjena(korisnikDTO.Id);

            
            return korisnikDTO;
        }

        private double ProsjecnaOcjena(int korisnikId)
        {
            var query = (from x in _context.OcjeneKorisnika
                         where x.OcijenjeniKorisnikId == korisnikId
                         select (double?)x.Ocjena).Average() ?? 0;
                        


            return Math.Round(query, 1);
        }
    }
}
