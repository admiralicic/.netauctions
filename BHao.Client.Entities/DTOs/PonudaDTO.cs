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
    public class PonudaDTO : DTOBase
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public DateTime Vrijeme { get; set; }
        [DataMember]
        public decimal Iznos { get; set; }
        [DataMember]
        public decimal MaksimalniIznos { get; set; }
        [DataMember]
        public string KorisnikIme { get; set; }
        [DataMember]
        public int AukcijaId { get; set; }
        [DataMember]
        public int KorisnikId { get; set; }
    }
}
