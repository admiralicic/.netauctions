using BHao.Business.Entities;
using BHao.Business.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHao.Data.Contracts
{
    public interface IPorukaRepository : IDataRepository<Poruka>
    {
        IEnumerable<PorukaDTO> GetPoslane(int korisnikId);
        IEnumerable<PorukaDTO> GetPrimljene(int korisnikId);
        IEnumerable<PorukaDTO> GetPoruke(int korisnikId);
        void ObrisiPoruku(PorukaDTO poruka, int korisnikId);
        

    }
}
