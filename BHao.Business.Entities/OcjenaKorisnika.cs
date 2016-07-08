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
    public class OcjenaKorisnika : EntityBase
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public DateTime Datum { get; set; }

        [DataMember]
        public int OcijenjeniKorisnikId { get; set; }

        [DataMember]
        public int OcjenjivacId { get; set; }

        [DataMember]
        public int Ocjena { get; set; }

        
        public Korisnik OcijenjeniKorisnik { get; set; }

        
        public Korisnik Ocjenjivac { get; set; }

        [DataMember]
        public int AukcijaId { get; set; }
    }
}
