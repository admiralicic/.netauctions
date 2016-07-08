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
    public class AukcijaDetailDTO : DTOBase
    {
        [DataMember]
        public Aukcija Aukcija { get; set; }
        [DataMember]
        public Artikal Artikal { get; set; }
        [DataMember]
        public NacinPlacanja NacinPlacanja { get; set; }
        [DataMember]
        public Kategorija Kategorija { get; set; }
        [DataMember]
        public IEnumerable<Slika> Slike { get; set; }
        [DataMember]
        public IEnumerable<OcjenaKorisnika> OcjeneKorisnika { get; set; }
        [DataMember]
        public KorisnikDTO Kupac { get; set; }
        [DataMember]
        public KorisnikDTO Prodavac { get; set; }
    }
}
