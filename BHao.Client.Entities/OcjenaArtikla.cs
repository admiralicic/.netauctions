using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHao.Client.Entities
{
    public class OcjenaArtikla
    {
        public int Id { get; set; }

        public DateTime Datum { get; set; }

        public int Ocjena { get; set; }

        public int ArtikalId { get; set; }

        public int OcijenioId { get; set; }

        public int AukcijaId { get; set; }

        public Artikal Artikal { get; set; }

        public Korisnik Ocijenio { get; set; }
    }
}
