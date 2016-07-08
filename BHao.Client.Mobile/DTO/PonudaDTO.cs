using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BHao.Client.Mobile.DTO
{
    public class PonudaDTO
    {
        public int Id { get; set; }
        public DateTime Vrijeme { get; set; }
        public decimal Iznos { get; set; }
        public decimal MaksimalniIznos { get; set; }
        public string KorisnikIme { get; set; }
        public int AukcijaId { get; set; }
        public int KorisnikId { get; set; }
    }
}
