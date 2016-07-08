using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BHao.Client.Mobile.DTO
{
    public class KorisnikDTO
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Ulica { get; set; }
        public string Broj { get; set; }
        public string PostanskiBroj { get; set; }
        public string Telefon { get; set; }
        public int GradId { get; set; }
        public string Email { get; set; }
        public string KorisnickoIme { get; set; }
        public double ProsjecnaOcjena { get; set; }
        public IEnumerable<KomentarDTO> KomentariKorisnika { get; set; }
    }
}
