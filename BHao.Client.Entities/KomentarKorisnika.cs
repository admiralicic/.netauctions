using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHao.Client.Entities
{
    public class KomentarKorisnika
    {
        public int Id { get; set; }

        public DateTime Datum { get; set; }

        public string TextKomentara { get; set; }

        public int KomentiraniKorisnikId { get; set; }

        public int KomentatorId { get; set; }

        public int AukcijaId { get; set; }

        public bool KomentarPregledan { get; set; }

        public Korisnik KomentiraniKorisnik { get; set; }

        public Korisnik Komentator { get; set; }
    }
}
