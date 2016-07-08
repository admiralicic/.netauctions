using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BHao.Client.Mobile.DTO
{
    public class KomentarDTO
    {
        public int Id { get; set; }
        public string TextKomentara { get; set; }
        public DateTime Datum { get; set; }
        public string Posiljalac { get; set; }
        public int PosiljalacId { get; set; }
        public string Primalac { get; set; }
        public int PrimalacId { get; set; }
        public string Aukcija { get; set; }
        public int AukcijaId { get; set; }
    }
}
