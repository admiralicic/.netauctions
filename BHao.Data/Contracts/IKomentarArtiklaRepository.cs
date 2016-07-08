using BHao.Business.Entities;
using BHao.Business.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHao.Data.Contracts
{
    public interface IKomentarArtiklaRepository : IDataRepository<KomentarArtikla>
    {
        KomentarArtiklaDTO[] GetKomentariPregled(bool isPregledan);
    }
}
