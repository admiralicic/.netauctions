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
    public class StatistikaDTO : DTOBase
    {
        [DataMember]
        public int BrojAukcija { get; set; }
        [DataMember]
        public decimal ProsjecnaVrijednostAukcija { get; set; }
        [DataMember]
        public IEnumerable<NajcesciArtikliDTO> NajcesceProdavaniArtikli { get; set; }
        [DataMember]
        public IEnumerable<AukcijaDetailDTO> ListaAukcija { get; set; }
    }
}
