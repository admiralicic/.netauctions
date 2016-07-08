using BHao.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BHao.Client.Entities
{

    [DataContract]
    public class KomentarArtikla
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public DateTime Datum { get; set; }

        [DataMember]
        public string TextKomentara { get; set; }

        [DataMember]
        public int KomentatorId { get; set; }

        [DataMember]
        public int ArtikalId { get; set; }

        [DataMember]
        public int AukcijaId { get; set; }

        [DataMember]
        public bool KomentarPregledan { get; set; }

        public Korisnik Komentator { get; set; }

        public Artikal Artikal { get; set; }

    }
}
