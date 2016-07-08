using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BHao.Business.Entities.DTOs
{
    [DataContract]
    public class PorukaDTO
    {
        [DataMember]
        public int PorukaId { get; set; }
        [DataMember]
        public DateTime Datum { get; set; }
        [DataMember]
        public string TextPoruke { get; set; }
        [DataMember]
        public bool Procitana { get; set; }
        [DataMember]
        public string PosiljalacUserName { get; set; }
        [DataMember]
        public string PrimalacUserName { get; set; }
        [DataMember]
        public int PosiljalacId { get; set; }
        [DataMember]
        public int PrimalacId { get; set; }
        [DataMember]
        public int AukcijaId { get; set; }
        [DataMember]
        public string AukcijaPredmet { get; set; }
        [DataMember]
        public bool IsObrisanaPosiljalac { get; set; }
        [DataMember]
        public bool IsObrisanaPrimalac { get; set; }

    }
}
