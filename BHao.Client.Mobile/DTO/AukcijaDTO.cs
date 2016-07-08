using BHao.Client.Mobile.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BHao.Client.Mobile.DTO
{
    public class AukcijaDTO
    {
        public int Id { get; set; }
        public DateTime Pocetak { get; set; }
        public DateTime Zavrsetak { get; set; }
        public string NazivArtikla { get; set; }
        public string Proizvodjac { get; set; }
        public string Model { get; set; }
        public decimal? PocetnaCijena { get; set; }
        public decimal? KupiOdmahCijena { get; set; }
        public string DetaljanOpis { get; set; }
        public int ProdavacId { get; set; }
        public string NacinPlacanja { get; set; }
        public IEnumerable<Slika> Slike { get; set; }
        public string Napomena { get; set; }
        public Ponuda NajvecaPonuda { get; set; }
        public int NajveciPonudjacId { get; set; }
        public KorisnikDTO NajveciPonudjac { get; set; }
        public KorisnikDTO Prodavac { get; set; }
        public IEnumerable<PonudaDTO> Ponude { get; set; }
        public bool Zavrsena { get; set; }
        public IEnumerable<OcjenaKorisnika> OcjeneKorisnika { get; set; }
        public IEnumerable<OcjenaArtikla> OcjeneArtikla { get; set; }
        public int ArtikalId { get; set; }
        public KomentarKorisnika KomentarKorisnika { get; set; }
        public int KupacId { get; set; }
        public Artikal Artikal { get; set; }

        public string NacinPlacanjaString
        {
            get { return String.Format("Način plaćanja: {0}", this.NacinPlacanja); }
        }

        public string TrenutnaPonudaString
        {
            get
            {
                if (this.Zavrsena == false)
                {
                    return String.Format("Trenutna cijena: {0} KM", this.NajvecaPonuda != null ? this.NajvecaPonuda.Iznos : this.PocetnaCijena);
                }
                else
                {
                    return String.Format("Konačna cijena: {0} KM", this.NajvecaPonuda != null ? this.NajvecaPonuda.Iznos : this.PocetnaCijena);
                }
                
            }
        }

        public string ProdavacString
        {
            get
            {
                return string.Format("Prodavac: {0}", this.Prodavac.KorisnickoIme);
            }
        }

        public string TrajanjeDoString
        {
            get
            {
                if (this.Zavrsena == false)
                    return string.Format("Aukcija traje do: {0:dd/MM/yyyy H:mm:ss}", this.Zavrsetak);
                else
                    return string.Format("Aukcija završena : {0:dd/MM/yyyy H:mm:ss}", this.Zavrsetak);
            }
        }

        public string KupiOdmahString
        {
            get
            {
                return string.Format("Kupite odmah za: {0} KM", this.KupiOdmahCijena);
            }
        }

        public string NajveciPonudjacString
        {
            get
            {
                if (this.Zavrsena == false)
                {
                    return this.NajveciPonudjac != null ?
                        string.Format("Najveća ponuda: {0}", this.NajveciPonudjac.KorisnickoIme) :
                        "Trenutno nema ponuda!";
                }
                else
                {
                    return this.NajveciPonudjac != null ?
                        string.Format("Kupac: {0}", this.NajveciPonudjac.KorisnickoIme) :
                        string.Format("Aukcija nije uspjela");
                }


            }
        }

        public string DetaljanOpisString
        {
            get
            {
                return string.Format("Više o artiklu: {0}", this.DetaljanOpis);
            }
        }

        public string NapomenaString
        {
            get
            {
                return string.Format("Napomena: {0}", this.Napomena);
            }
        }
    }
}
