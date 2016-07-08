using BHao.Client.Entities;
using BHao.Client.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace BHao.Client.Service.Contracts
{
    [ServiceContract]
    public interface ISystemService
    {
        [OperationContract]
        NacinPlacanja[] GetNaciniPlacanja( );

        [OperationContract]
        Artikal[] GetArtikli( );

        [OperationContract]
        Kategorija[] GetKategorije( );

        [OperationContract]
        Grad[] GetGradovi( );

        [OperationContract]
        KorisnikDTO GetKorisnikById(int korisnikId);

        [OperationContract]
        KorisnikDTO GetKorisnikByKorisnickoIme(string korisnickoIme);

        [OperationContract]
        Inkrement[] GetInkrementi();

        [OperationContract]
        void SacuvajGrad(Grad grad);

        [OperationContract]
        void SacuvajKategorija(Kategorija kategorija);

        [OperationContract]
        void SacuvajInkrement(Inkrement inkrement);

        [OperationContract]
        void SacuvajNacinPlacanja(NacinPlacanja nacinPlacanja);

        [OperationContract]
        void ObrisiGrad(int id);

        [OperationContract]
        void ObrisiKategorija(int id);

        [OperationContract]
        void ObrisiInkrement(int id);

        [OperationContract]
        void ObrisiNacinPlacanja(int id);
       
    }
}
