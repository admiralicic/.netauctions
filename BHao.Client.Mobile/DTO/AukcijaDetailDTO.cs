using BHao.Client.Mobile.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BHao.Client.Mobile.DTO
{
    public class AukcijaDetailDTO
    {
        public Aukcija Aukcija { get; set; }
        public Artikal Artikal { get; set; }
        public NacinPlacanja NacinPlacanja { get; set; }
        public Kategorija Kategorija { get; set; }
        public IEnumerable<Slika> Slike { get; set; }
        public IEnumerable<OcjenaKorisnika> OcjeneKorisnika { get; set; }
        public KorisnikDTO Kupac { get; set; }
        public KorisnikDTO Prodavac { get; set; }
    }
}
