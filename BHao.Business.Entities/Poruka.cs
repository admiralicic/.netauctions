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
    public class Poruka : EntityBase
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public DateTime Datum { get; set; }

        [DataMember]
        public string TextPoruke { get; set; }

        [DataMember]
        public bool Procitana { get; set; }

        [DataMember]
        public int PosiljalacId { get; set; }

        [DataMember]
        public int PrimalacId { get; set; }

        public Korisnik Posiljalac { get; set; }

        public Korisnik Primalac { get; set; }

        [DataMember]
        public int AukcijaId { get; set; }

        [DataMember]
        public bool IsObrisanaPosiljalac { get; set; }

        [DataMember]
        public bool IsObrisanaPrimalac { get; set; }
    }
}
