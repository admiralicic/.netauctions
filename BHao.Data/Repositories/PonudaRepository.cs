using BHao.Business.Entities;
using BHao.Business.Entities.DTOs;
using BHao.Data.Common;
using BHao.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BHao.Data.Repositories
{
    public class PonudaRepository : DataRepositoryBase<Ponuda>, IPonudaRepository
    {
        public PonudaRepository(BHaoDataContext context)
            : base(context)
        {

        }

        public List<PonudaDTO> GetPonudeByAukcijaId(int aukcijaId)
        {
            var query = _context.Ponude.Include(x => x.Korisnik).Where(x => x.AukcijaId == aukcijaId).OrderByDescending(x => x.Iznos).ThenByDescending(x=>x.Id).ToList();

            List<PonudaDTO> ponude = new List<PonudaDTO>();

            foreach (var item in query)
            {
                PonudaDTO ponudaDTO = new PonudaDTO
                {
                    Id = item.Id,
                    Iznos = item.Iznos,
                    Vrijeme = item.Vrijeme,
                    MaksimalniIznos = item.MaksimalanIznos,
                    KorisnikIme = item.Korisnik.UserName,
                    AukcijaId = item.AukcijaId,
                    KorisnikId = item.KorisnikId
                };

                ponude.Add(ponudaDTO);
            }
            
            return ponude;
        }


    }
}
