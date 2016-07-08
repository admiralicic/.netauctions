using BHao.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHao.Data.Contracts
{
    public interface IArtikalRepository : IDataRepository<Artikal>
    {
        int KreirajIzAukcije(string naziv, string model, string proizvodjac);
        Artikal GetByIdWithProsjecnaOcjena(int id);
    }

    
    
}
