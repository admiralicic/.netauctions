using BHao.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BHao.Business.Entities
{
    [DataContract]
    public class Aukcija : EntityBase
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public DateTime Pocetak { get; set; }
        [DataMember]
        public DateTime Zavrsetak { get; set; }
        [DataMember]
        public decimal? MinimalnaCijena { get; set; }
        [DataMember]
        public decimal? KupiOdmahCijena { get; set; }
        [DataMember]
        public string Napomena { get; set; }
        [DataMember]
        public string Naziv { get; set; }
        [DataMember]
        public string DetaljanOpis { get; set; }
        [DataMember]
        public decimal? NajvecaPonuda { get; set; }
        [DataMember]
        public bool Zavrsena { get; set; }
        [DataMember]
        public int ProdavacId { get; set; }
        public Korisnik Prodavac { get; set; }
        [DataMember]
        public int? KupacId { get; set; }
        public Korisnik Kupac { get; set; }
        [DataMember]
        public int? ArtikalId { get; set; }
        [DataMember]
        public Artikal Artikal { get; set; }
        [DataMember]
        public int NacinPlacanjaId { get; set; }
        [DataMember]
        public NacinPlacanja NacinPlacanja { get; set; }
        [DataMember]
        public int KategorijaId { get; set; }
        [DataMember]
        public Kategorija Kategorija { get; set; }
        [DataMember]
        public ICollection<Slika> Slike { get; set; }
        [DataMember]
        public int NajveciPonudjacId { get; set; }
        public ICollection<Ponuda> Ponude { get; set; }
        [DataMember]
        public decimal? Provizija { get; set; }
        [DataMember]
        public DateTime? DatumPlacanja { get; set; }
    }
}
