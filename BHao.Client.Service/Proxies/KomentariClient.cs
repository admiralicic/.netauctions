using BHao.Client.Entities.DTOs;
using BHao.Client.Service.Contracts;
using BHao.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHao.Client.Service.Proxies
{
    public class KomentariClient:UserClientBase<IKomentariService>, IKomentariService
    {
        public KomentarDTO[] GetKomentari(bool isPregledan)
        {
            return Channel.GetKomentari(isPregledan);
        }


        public void UkloniKomentar(int komentarId)
        {
            Channel.UkloniKomentar(komentarId);
        }


        public void OznaciPregledan(int komentarId)
        {
            Channel.OznaciPregledan(komentarId);
        }


        public KomentarArtiklaDTO[] GetKomentariArtikala(bool isPregledan)
        {
            return Channel.GetKomentariArtikala(isPregledan);
        }

        public void UkloniKomentarArtikla(int komentarId)
        {
            Channel.UkloniKomentarArtikla(komentarId);
        }

        public void OznaciPregledanArtikla(int komentarId)
        {
            Channel.OznaciPregledanArtikla(komentarId);
        }
    }
}
