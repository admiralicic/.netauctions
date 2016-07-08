using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHao.Client.Mobile.Model
{
    public class Aukcija
    {
        
        public int Id { get; set; }
        public DateTime Pocetak { get; set; }
        public DateTime Zavrsetak { get; set; }
        public decimal? MinimalnaCijena { get; set; }
        public decimal? KupiOdmahCijena { get; set; }
        public string Napomena { get; set; }
        public string Naziv { get; set; }
        public string DetaljanOpis { get; set; }
        public decimal? NajvecaPonuda { get; set; }
        public bool Zavrsena { get; set; }
        public int ProdavacId { get; set; }
        public int? KupacId { get; set; }
        public int? ArtikalId { get; set; }
        public Artikal Artikal { get; set; }
        public int NacinPlacanjaId { get; set; }
        public NacinPlacanja NacinPlacanja { get; set; }
        public int KategorijaId { get; set; }
        public Kategorija Kategorija { get; set; }
        public ICollection<Slika> Slike { get; set; }
        public int NajveciPonudjacId { get; set; }
        public decimal? Provizija { get; set; }
        public DateTime? DatumPlacanja { get; set; }

       
        
    }
}
