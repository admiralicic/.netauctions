using BHao.Client.Entities;
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
    public class PorukeClient : UserClientBase<IPorukeService>, IPorukeService
    {


        public int PosaljiPoruku(Poruka poruka)
        {
            return Channel.PosaljiPoruku(poruka);
        }

        public PorukaDTO[] GetPoruke(int korisnikId)
        {
            return Channel.GetPoruke(korisnikId);
        }

        public PorukaDTO[] GetPoslane(int korisnikId)
        {
            return Channel.GetPoslane(korisnikId);
        }

        public PorukaDTO[] GetPrimljene(int korisnikId)
        {
            return Channel.GetPrimljene(korisnikId);
        }

        public void ObrisiPoruku(PorukaDTO poruka, int korisnikId)
        {
            Channel.ObrisiPoruku(poruka, korisnikId);
        }

        public Poruka GetPoruka(int porukaId)
        {
            return Channel.GetPoruka(porukaId);
        }

        public Poruka OznaciProcitana(int porukaId)
        {
            return Channel.OznaciProcitana(porukaId);
        }
    }
}
