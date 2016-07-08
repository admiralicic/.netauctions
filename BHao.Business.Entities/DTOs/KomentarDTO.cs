using BHao.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BHao.Business.Entities.DTOs
{
    [DataContract]
    public class KomentarDTO : DTOBase
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string TextKomentara { get; set; }
        [DataMember]
        public DateTime Datum { get; set; }
        [DataMember]
        public string Posiljalac { get; set; }
        [DataMember]
        public int PosiljalacId { get; set; }
        [DataMember]
        public string Primalac { get; set; }
        [DataMember]
        public int PrimalacId { get; set; }
        [DataMember]
        public string Aukcija { get; set; }
        [DataMember]
        public int AukcijaId { get; set; }
    }
}
