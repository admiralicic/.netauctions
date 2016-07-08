using BHao.Client.Entities;
using BHao.Client.Entities.DTOs;
using BHao.Client.Service.Contracts;
using BHao.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BHao.Client.Service.Proxies
{
    public class AukcijeClient : UserClientBase<IAukcijeService>, IAukcijeService
    {

        public Entities.Aukcija GetAukcija( int id )
        {
            return Channel.GetAukcija( id );
        }

        public Entities.Aukcija[] GetAllAukcije( )
        {
            return Channel.GetAllAukcije( );
        }


        public void OkoncajAukciju( int aukcijaId )
        {
            Channel.OkoncajAukciju( aukcijaId );
        }

        public AukcijaDetailDTO[] GetAllAktivne(int kategorijaId, string stringZaPretragu )
        {
            return Channel.GetAllAktivne(kategorijaId, stringZaPretragu);
        }

        public AukcijaDetailDTO[] GetZadnjeUspjesne(int kategorijaId, string stringZaPretragu)
        {
            return Channel.GetZadnjeUspjesne(kategorijaId, stringZaPretragu);
        }

        public Aukcija KreirajAukciju(AukcijaKreirajDTO aukcijaModel, int prijavljeniKorisnik)
        {
            return Channel.KreirajAukciju(aukcijaModel, prijavljeniKorisnik);
        }


        public AukcijaDTO GetAukcijaDetail(int aukcijaId, int korisnikId)
        {
            return Channel.GetAukcijaDetail(aukcijaId, korisnikId);
        }



        public AukcijaDTO AukcijaKreirajPonudu(int aukcijaId, decimal iznosPonude, int prijavljeniKorisnik)
        {
            
            return Channel.AukcijaKreirajPonudu(aukcijaId,iznosPonude, prijavljeniKorisnik);
        }

        public void KupiOdmah(int aukcijaId, int korisnikId)
        {
            Channel.KupiOdmah(aukcijaId, korisnikId);
        }

        public void OcijeniKorisnika(OcjenaKorisnika ocjena)
        {
            Channel.OcijeniKorisnika(ocjena);
        }

        public void OcijeniArtikal(OcjenaArtikla ocjena)
        {
            Channel.OcijeniArtikal(ocjena);
        }

        public void DodajKomentar(KomentarKorisnika komentar)
        {
            Channel.DodajKomentar(komentar);
        }

        public void DodajKomentarArtikla(KomentarArtikla komentar)
        {
            Channel.DodajKomentarArtikla(komentar);
        }
        
        public AukcijaDetailDTO[] GetAukcijeByKorisnik(int korisnikId, string kriterij)
        {
            return Channel.GetAukcijeByKorisnik(korisnikId, kriterij);
        }


        public AukcijaDTO[] GetPreporuke(int artikalId, int prijavljeniKorisnikId)
        {
            return Channel.GetPreporuke(artikalId, prijavljeniKorisnikId);
        }


        public AukcijaDetailDTO[] GetAllZavrsene(string filterPlacanja, string stringZaPretragu)
        {
            return Channel.GetAllZavrsene(filterPlacanja, stringZaPretragu);
        }


        public void EvidentirajPlacanje(int aukcijaId, DateTime datumPlacanja)
        {
            Channel.EvidentirajPlacanje(aukcijaId, datumPlacanja);
        }


        public AukcijaDetailDTO[] GetAll(bool zavrsena)
        {
            return Channel.GetAll(zavrsena);
        }


        public void Ukloni(int aukcijaId)
        {
            Channel.Ukloni(aukcijaId);
        }


        public StatistikaDTO GetStatistika(int kategorijaId, DateTime? prikazOd, DateTime? prikazDo)
        {
            return Channel.GetStatistika(kategorijaId, prikazOd, prikazDo);
        }


      
    }
}
