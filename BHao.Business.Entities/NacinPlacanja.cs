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
    public class NacinPlacanja : EntityBase
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Opis { get; set; }
    }
}
