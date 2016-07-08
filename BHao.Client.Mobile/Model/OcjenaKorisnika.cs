using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHao.Client.Mobile.Model
{
    public class OcjenaKorisnika
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public int OcijenjeniKorisnikId { get; set; }
        public int OcjenjivacId { get; set; }
        public int Ocjena { get; set; }
        public Korisnik OcijenjeniKorisnik { get; set; }
        public Korisnik Ocjenjivac { get; set; }
        public int AukcijaId { get; set; }
    }
}
