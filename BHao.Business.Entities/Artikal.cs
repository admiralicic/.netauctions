using BHao.Business.Entities.DTOs;
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
    public class Artikal : EntityBase
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Proizvodjac { get; set; }

        [DataMember]
        public string Model { get; set; }

        [DataMember]
        public string Naziv { get; set; }

        [DataMember]
        public double ProsjecnaOcjena { get; set; }

        [DataMember]
        public IEnumerable<KomentarArtiklaDTO> Komentari { get; set; }           
    }
}
