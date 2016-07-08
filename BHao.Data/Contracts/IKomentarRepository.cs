using BHao.Business.Entities;
using BHao.Business.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHao.Data.Contracts
{
    public interface IKomentarRepository : IDataRepository<KomentarKorisnika>
    {
        KomentarDTO[] GetKomentari();
        KomentarDTO[] GetKomentariByKorisnikId(int korisnikId);
        KomentarDTO[] GetKomentariPregled(bool isPregledan);
    }
}
