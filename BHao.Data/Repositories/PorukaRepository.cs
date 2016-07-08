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
    public class PorukaRepository : DataRepositoryBase<Poruka>, IPorukaRepository
    {
        public PorukaRepository(BHaoDataContext context)
            : base(context)
        {

        }

        public IEnumerable<PorukaDTO> GetPoslane(int korisnikId)
        {
            var query = from p in _context.Poruke.Where(x => x.PosiljalacId == korisnikId)
                        join a in _context.Aukcije on p.AukcijaId equals a.Id
                        select new PorukaDTO()
                        {
                            PorukaId = p.Id,
                            Datum = p.Datum,
                            TextPoruke = p.TextPoruke,
                            Procitana = p.Procitana,
                            PosiljalacUserName = p.Posiljalac.UserName,
                            PrimalacUserName = p.Primalac.UserName,
                            PosiljalacId = p.PosiljalacId,
                            PrimalacId = p.PrimalacId,
                            AukcijaId = p.AukcijaId,
                            AukcijaPredmet = a.Artikal.Naziv + " " + a.Artikal.Proizvodjac + " " + a.Artikal.Model
                        };

            return query.ToList().OrderByDescending(x => x.Datum); 
        }

        public IEnumerable<PorukaDTO> GetPrimljene(int korisnikId)
        {
            var query = from p in _context.Poruke.Where(x => x.PrimalacId == korisnikId)
                        join a in _context.Aukcije on p.AukcijaId equals a.Id
                        select new PorukaDTO()
                        {
                            PorukaId = p.Id,
                            Datum = p.Datum,
                            TextPoruke = p.TextPoruke,
                            Procitana = p.Procitana,
                            PosiljalacUserName = p.Posiljalac.UserName,
                            PrimalacUserName = p.Primalac.UserName,
                            PosiljalacId = p.PosiljalacId,
                            PrimalacId = p.PrimalacId,
                            AukcijaId = p.AukcijaId,
                            AukcijaPredmet = a.Artikal.Naziv + " " + a.Artikal.Proizvodjac + " " + a.Artikal.Model
                        };

            return query.ToList().OrderByDescending(x=>x.Datum);
        }

        public override int Delete(int id)
        {
            return 0;
        }

        public void ObrisiPoruku(PorukaDTO poruka, int korisnikId)
        {
            Poruka p = null;
            
            if (poruka.PrimalacId == korisnikId)
            {
                p = _context.Poruke.Where(x =>x.Id == poruka.PorukaId && x.PrimalacId == korisnikId).FirstOrDefault();
                p.IsObrisanaPrimalac = true;
                Update(p);
            }

            if (poruka.PosiljalacId == korisnikId)
            {
                p = _context.Poruke.Where(x => x.Id == poruka.PorukaId && x.PosiljalacId == korisnikId).FirstOrDefault();
                p.IsObrisanaPosiljalac = true;
                Update(p);
            }

            

            
        }

        public IEnumerable<PorukaDTO> GetPoruke(int korisnikId)
        {
            var query = from p in _context.Poruke.Where(x => (x.PosiljalacId == korisnikId && x.IsObrisanaPosiljalac == false) || 
                                                            (x.PrimalacId == korisnikId && x.IsObrisanaPrimalac == false))
                        join a in _context.Aukcije on p.AukcijaId equals a.Id
                        select new PorukaDTO()
                        {
                            PorukaId = p.Id,
                            Datum = p.Datum,
                            TextPoruke = p.TextPoruke,
                            Procitana = p.Procitana,
                            PosiljalacUserName = p.Posiljalac.UserName,
                            PrimalacUserName = p.Primalac.UserName,
                            PosiljalacId = p.PosiljalacId,
                            PrimalacId = p.PrimalacId,
                            AukcijaId = p.AukcijaId,
                            AukcijaPredmet = a.Artikal.Naziv + " " + a.Artikal.Proizvodjac + " " + a.Artikal.Model
                        };

            return query.ToList().OrderByDescending(x => x.Datum); 
        }
    }
}
