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
    public class Ponuda : EntityBase
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public decimal Iznos { get; set; }

        [DataMember]
        public DateTime Vrijeme { get; set; }

        [DataMember]
        public decimal MaksimalanIznos { get; set; }

        [DataMember]
        public int KorisnikId { get; set; }

        [DataMember]
        public int AukcijaId { get; set; }

        
        public Korisnik Korisnik { get; set; }

        
        public Aukcija Aukcija { get; set; }

    }
}
