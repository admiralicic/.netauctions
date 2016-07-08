using BHao.Business.Entities;
using BHao.Business.Entities.DTOs;
using BHao.Data.Common;
using BHao.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHao.Data.Repositories
{
    public class KomentarRepository : DataRepositoryBase<KomentarKorisnika>, IKomentarRepository
    {
        public KomentarRepository(BHaoDataContext context)
            : base(context)
        {

        }


        public KomentarDTO[] GetKomentari()
        {
            var query = from k in _context.KomentariKorisnika
                        join a in _context.Aukcije on k.AukcijaId equals a.Id
                        where k.isDeleted == false
                        select new KomentarDTO()
                        {
                            Id = k.Id,
                            Aukcija = a.Artikal.Naziv + " " + a.Artikal.Proizvodjac + " " + a.Artikal.Model,
                            TextKomentara = k.TextKomentara,
                            Posiljalac = k.Komentator.Email,
                            Primalac = k.KomentiraniKorisnik.Email,
                            AukcijaId = a.Id,
                            PosiljalacId = k.KomentatorId,
                            PrimalacId = k.KomentiraniKorisnikId,
                            Datum = k.Datum
                        };

            return query.OrderByDescending(x=>x.Datum).ToArray();
        }



        public KomentarDTO[] GetKomentariPregled(bool isPregledan)
        {
            var query = from k in _context.KomentariKorisnika
                        join a in _context.Aukcije on k.AukcijaId equals a.Id
                        where k.isDeleted == false && k.KomentarPregledan == isPregledan
                        select new KomentarDTO()
                        {
                            Id = k.Id,
                            Aukcija = a.Artikal.Naziv + " " + a.Artikal.Proizvodjac + " " + a.Artikal.Model,
                            TextKomentara = k.TextKomentara,
                            Posiljalac = k.Komentator.Email,
                            Primalac = k.KomentiraniKorisnik.Email,
                            AukcijaId = a.Id,
                            PosiljalacId = k.KomentatorId,
                            PrimalacId = k.KomentiraniKorisnikId,
                            Datum = k.Datum
                        };

            return query.OrderByDescending(x => x.Datum).ToArray();
        }

        public KomentarDTO[] GetKomentariByKorisnikId(int korisnikId)
        {
            var query = from k in _context.KomentariKorisnika
                        join a in _context.Aukcije on k.AukcijaId equals a.Id
                        where k.KomentiraniKorisnikId == korisnikId && k.isDeleted == false
                        select new KomentarDTO()
                        {
                            Id = k.Id,
                            Aukcija = a.Artikal.Naziv + " " + a.Artikal.Proizvodjac + " " + a.Artikal.Model,
                            TextKomentara = k.TextKomentara,
                            Posiljalac = k.Komentator.Email,
                            Primalac = k.KomentiraniKorisnik.Email,
                            AukcijaId = a.Id,
                            PosiljalacId = k.KomentatorId,
                            PrimalacId = k.KomentiraniKorisnikId,
                            Datum = k.Datum
                        };

            return query.OrderByDescending(x => x.Datum).ToArray();
        }


    }
}
