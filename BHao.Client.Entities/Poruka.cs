using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHao.Client.Entities
{
    public class Poruka
    {
        public int Id { get; set; }

        public DateTime Datum { get; set; }

        public string TextPoruke { get; set; }

        public bool Procitana { get; set; }

        public int PosiljalacId { get; set; }

        public int PrimalacId { get; set; }

        public Korisnik Posiljalac { get; set; }

        public Korisnik Primalac { get; set; }

        public int AukcijaId { get; set; }

        public bool IsObrisanaPosiljalac { get; set; }

        public bool IsObrisanaPrimalac { get; set; }
    }
}
