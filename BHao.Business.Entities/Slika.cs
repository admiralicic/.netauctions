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
    public class Slika : EntityBase
    { 
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string SlikaIme { get; set; }

        [DataMember]
        public string SlikaThumbIme { get; set; }

        [DataMember]
        public int AukcijaId { get; set; }

        
        public Aukcija Aukcija { get; set; }

    }
}
