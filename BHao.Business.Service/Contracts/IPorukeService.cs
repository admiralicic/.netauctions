using BHao.Business.Entities;
using BHao.Business.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace BHao.Business.Service.Contracts
{
    [ServiceContract]
    public interface IPorukeService
    {
        [OperationContract]
        int PosaljiPoruku(Poruka poruka);

        [OperationContract]
        PorukaDTO[] GetPoruke(int korisnikId);

        [OperationContract]
        PorukaDTO[] GetPoslane(int korisnikId);

        [OperationContract]
        PorukaDTO[] GetPrimljene(int korisnikId);

        [OperationContract]
        void ObrisiPoruku(PorukaDTO poruka, int korisnikId);

        [OperationContract]
        Poruka GetPoruka(int porukaId);

        [OperationContract]
        Poruka OznaciProcitana(int porukaId);

        

    }
}
