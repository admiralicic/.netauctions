using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHao.Client.Entities
{
    public class Ponuda
    {
        public int Id { get; set; }

        public decimal Iznos { get; set; }

        public DateTime Vrijeme { get; set; }

        public decimal MaksimalanIznos { get; set; }

        public int KorisnikId { get; set; }

        public int AukcijaId { get; set; }

        public Korisnik Korisnik { get; set; }

        public Aukcija Aukcija { get; set; }
    }
}
