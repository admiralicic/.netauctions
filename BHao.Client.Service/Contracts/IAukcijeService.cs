using BHao.Client.Entities;
using BHao.Client.Entities.DTOs;
using BHao.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace BHao.Client.Service.Contracts
{
    [ServiceContract]
    public interface IAukcijeService
    {
        [OperationContract]
        Aukcija GetAukcija( int id );
        [OperationContract]
        Aukcija[] GetAllAukcije( );

        [OperationContract]
        void OkoncajAukciju( int aukcijaId );
        [OperationContract]
        AukcijaDetailDTO[] GetAllAktivne(int kategorijaId, string stringZaPretragu);
        [OperationContract]
        AukcijaDetailDTO[] GetZadnjeUspjesne(int kategorijaId, string stringZaPretragu);
        [OperationContract]
        Aukcija KreirajAukciju(AukcijaKreirajDTO aukcijaModel, int prijavljeniKorisnik);
        [OperationContract]
        AukcijaDTO GetAukcijaDetail(int aukcijaId, int korisnikId);
        [OperationContract]
        [FaultContract(typeof(PonudaCreateException))]
        AukcijaDTO AukcijaKreirajPonudu(int aukcijaId, decimal iznosPonude, int prijavljeniKorisnik);
        [OperationContract]
        void KupiOdmah(int aukcijaId, int korisnikId);
        [OperationContract]
        void OcijeniKorisnika(OcjenaKorisnika ocjena);
        [OperationContract]
        void OcijeniArtikal(OcjenaArtikla ocjena);
        [OperationContract]
        void DodajKomentar(KomentarKorisnika komentar);
        [OperationContract]
        AukcijaDetailDTO[] GetAukcijeByKorisnik(int korisnikId, string kriterij);
        [OperationContract]
        AukcijaDTO[] GetPreporuke(int artikalId, int prijavljeniKorisnikId);
        [OperationContract]
        AukcijaDetailDTO[] GetAllZavrsene(string filterPlacanja, string stringZaPretragu);
        [OperationContract]
        void EvidentirajPlacanje(int aukcijaId, DateTime datumPlacanja);
        [OperationContract]
        AukcijaDetailDTO[] GetAll(bool zavrsena);
        [OperationContract]
        void Ukloni(int aukcijaId);
        [OperationContract]
        void DodajKomentarArtikla(KomentarArtikla komentar);
        [OperationContract]
        StatistikaDTO GetStatistika(int kategorijaId, DateTime? prikazOd, DateTime? prikazDo);


    }
}
