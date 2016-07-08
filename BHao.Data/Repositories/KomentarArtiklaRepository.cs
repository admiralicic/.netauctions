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
    public class KomentarArtiklaRepository : DataRepositoryBase<KomentarArtikla>, IKomentarArtiklaRepository
    {
        public KomentarArtiklaRepository(BHaoDataContext context)
            : base(context)
        {

        }

        public KomentarArtiklaDTO[] GetKomentariPregled(bool isPregledan)
        {
            var query = from k in _context.KomentariArtikala
                        join a in _context.Aukcije on k.AukcijaId equals a.Id
                        where k.isDeleted == false && k.KomentarPregledan == isPregledan
                        select new KomentarArtiklaDTO()
                        {
                            Id = k.Id,
                            Aukcija = a.Artikal.Naziv + " " + a.Artikal.Proizvodjac + " " + a.Artikal.Model,
                            TextKomentara = k.TextKomentara,
                            Komentator = k.Komentator.Email,
                            AukcijaId = a.Id,
                            KomentatorId = k.KomentatorId,
                            ArtikalId = k.ArtikalId,
                            Datum = k.Datum
                        };

            return query.OrderByDescending(x => x.Datum).ToArray();
        }
    }
}
