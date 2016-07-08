using BHao.Client.Entities;
using BHao.Client.Entities.DTOs;
using BHao.Client.Service.Contracts;
using BHao.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;


namespace BHao.Client.Service.Proxies
{
    public class SystemClient : UserClientBase<ISystemService>, ISystemService
    {
        public NacinPlacanja[] GetNaciniPlacanja( )
        {
            return Channel.GetNaciniPlacanja();
        }

        public Artikal[] GetArtikli( )
        {
            return Channel.GetArtikli();
        }

        public Kategorija[] GetKategorije( )
        {
            return Channel.GetKategorije();
        }

        public Grad[] GetGradovi( )
        {
            return Channel.GetGradovi();
        }


        public KorisnikDTO GetKorisnikById(int korisnikId)
        {
            return Channel.GetKorisnikById(korisnikId);
        }

        public KorisnikDTO GetKorisnikByKorisnickoIme(string korisnickoIme)
        {
            return Channel.GetKorisnikByKorisnickoIme(korisnickoIme);
        }

        public Inkrement[] GetInkrementi()
        {
            return Channel.GetInkrementi();
        }


        public void SacuvajGrad(Grad grad)
        {
            Channel.SacuvajGrad(grad);
        }

        public void SacuvajKategorija(Kategorija kategorija)
        {
            Channel.SacuvajKategorija(kategorija);
        }

        public void SacuvajInkrement(Inkrement inkrement)
        {
            Channel.SacuvajInkrement(inkrement);
        }

        public void SacuvajNacinPlacanja(NacinPlacanja nacinPlacanja)
        {
            Channel.SacuvajNacinPlacanja(nacinPlacanja);
        }


        public void ObrisiGrad(int id)
        {
            Channel.ObrisiGrad(id);
        }

        public void ObrisiKategorija(int id)
        {
            Channel.ObrisiKategorija(id);
        }

        public void ObrisiInkrement(int id)
        {
            Channel.ObrisiInkrement(id);
        }

        public void ObrisiNacinPlacanja(int id)
        {
            Channel.ObrisiNacinPlacanja(id);
        }
    }
}
