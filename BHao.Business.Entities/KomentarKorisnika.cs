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
    public class KomentarKorisnika : EntityBase
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public DateTime Datum { get; set; }

        [DataMember]
        public string TextKomentara { get; set; }

        [DataMember]
        public int KomentiraniKorisnikId { get; set; }

        [DataMember]
        public int KomentatorId { get; set; }

        [DataMember]
        public int AukcijaId { get; set; }

        [DataMember]
        public bool KomentarPregledan { get; set; }

        public Korisnik KomentiraniKorisnik { get; set; }

        
        public Korisnik Komentator { get; set; }

    }
}
