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
    public class KorisnikDTO : DTOBase
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Ime { get; set; }
        [DataMember]
        public string Prezime { get; set; }
        [DataMember]
        public string Ulica { get; set; }
        [DataMember]
        public string Broj { get; set; }
        [DataMember]
        public string PostanskiBroj { get; set; }
        [DataMember]
        public string Telefon { get; set; }
        [DataMember]
        public int GradId { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string KorisnickoIme { get; set; }
        [DataMember]
        public double ProsjecnaOcjena { get; set; }
        [DataMember]
        public IEnumerable<KomentarDTO> KomentariKorisnika { get; set; }
    }
}
