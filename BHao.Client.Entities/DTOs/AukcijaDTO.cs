using BHao.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BHao.Client.Entities.DTOs
{   
    [DataContract]
    public class AukcijaDTO : DTOBase
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public DateTime Pocetak { get; set; }
        [DataMember]
        public DateTime Zavrsetak { get; set; }
        [DataMember]
        public string NazivArtikla { get; set; }
        [DataMember]
        public string Proizvodjac { get; set; }
        [DataMember]
        public string Model { get; set; }
        [DataMember]
        public decimal? PocetnaCijena { get; set; }
        [DataMember]
        public decimal? KupiOdmahCijena { get; set; }
        [DataMember]
        public string DetaljanOpis { get; set; }
        [DataMember]
        public int ProdavacId { get; set; }
        [DataMember]
        public string NacinPlacanja { get; set; }
        [DataMember]
        public IEnumerable<Slika> Slike { get; set; }
        [DataMember]
        public string Napomena { get; set; }
        [DataMember]
        public Ponuda NajvecaPonuda { get; set; }
        [DataMember]
        public int NajveciPonudjacId { get; set; }
        [DataMember]
        public KorisnikDTO NajveciPonudjac { get; set; }
        [DataMember]
        public KorisnikDTO Prodavac { get; set; }
        [DataMember]
        public IEnumerable<PonudaDTO> Ponude { get; set; }
        [DataMember]
        public bool Zavrsena { get; set; }
        [DataMember]
        public IEnumerable<OcjenaKorisnika> OcjeneKorisnika { get; set; }
        [DataMember]
        public IEnumerable<OcjenaArtikla> OcjeneArtikla { get; set; }
        [DataMember]
        public int ArtikalId { get; set; }
        [DataMember]
        public KomentarKorisnika KomentarKorisnika { get; set; }
        [DataMember]
        public KomentarArtikla KomentarArtikla { get; set; }
        [DataMember]
        public int KupacId { get; set; }
        [DataMember]
        public Artikal Artikal { get; set; }
        [DataMember]
        public bool isAdmin { get; set; }
    }
}
