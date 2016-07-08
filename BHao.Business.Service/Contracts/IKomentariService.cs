using BHao.Business.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace BHao.Business.Service.Contracts
{
    [ServiceContract]
    public interface IKomentariService
    {
        [OperationContract]
        KomentarDTO[] GetKomentari(bool isPregledan);

        [OperationContract]
        void UkloniKomentar(int komentarId);

        [OperationContract]
        void OznaciPregledan(int komentarId);

        [OperationContract]
        KomentarArtiklaDTO[] GetKomentariArtikala(bool isPregledan);

        [OperationContract]
        void UkloniKomentarArtikla(int komentarId);

        [OperationContract]
        void OznaciPregledanArtikla(int komentarId);
        

    }
}
