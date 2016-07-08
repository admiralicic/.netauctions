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
    public class OcjenaArtikla : EntityBase
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public DateTime Datum { get; set; }

        [DataMember]
        public int Ocjena { get; set; }

        [DataMember]
        public int ArtikalId { get; set; }

        [DataMember]
        public int OcijenioId { get; set; }

        [DataMember]
        public int AukcijaId { get; set; }

        
        public Artikal Artikal { get; set; }

        
        public Korisnik Ocijenio { get; set; }
    }
}
