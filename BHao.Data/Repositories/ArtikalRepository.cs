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
    public class ArtikalRepository : DataRepositoryBase<Artikal>, IArtikalRepository
    {
        public ArtikalRepository(BHaoDataContext context)
            : base(context)
        {

        }

        public int KreirajIzAukcije(string naziv, string model, string proizvodjac)
        {

            Artikal a = _context.Artikli.Where(x => x.Naziv == naziv && x.Model == model && x.Proizvodjac == proizvodjac).FirstOrDefault();

            if (a == null)
            {
                a = Insert(new Artikal { Naziv = naziv, Proizvodjac = proizvodjac, Model = model });
            }

            return a.Id;
        }

        public Artikal GetByIdWithProsjecnaOcjena(int id)
        {
            Artikal artikal = _context.Artikli.Where(x => x.Id == id).FirstOrDefault();

            if (artikal != null)
            {
                artikal.ProsjecnaOcjena = ProsjecnaOcjena(id);
                artikal.Komentari = KomentariArtikla(id);
            }

            return artikal;
        }

        private double ProsjecnaOcjena(int artikalId)
        {
            var query = (from oa in _context.OcjeneArtikala
                         where oa.ArtikalId == artikalId
                         select (double?)oa.Ocjena).Average() ?? 0;

            return Math.Round(query, 1);
        }

        private IEnumerable<KomentarArtiklaDTO> KomentariArtikla(int artikalId)
        {
            var query = (from k in _context.KomentariArtikala
                         where k.ArtikalId == artikalId && k.isDeleted == false
                         join ko in _context.Users on k.KomentatorId equals ko.Id
                         select new KomentarArtiklaDTO 
                         { 
                             ArtikalId = k.Id,
                             AukcijaId = k.AukcijaId,
                             Datum = k.Datum,
                             Komentator = ko.UserName,
                             KomentatorId = ko.Id,
                             TextKomentara = k.TextKomentara
                         }).ToArray();

            return query;
        }
    }
}
