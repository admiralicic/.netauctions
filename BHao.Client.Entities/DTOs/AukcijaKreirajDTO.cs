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
    public class AukcijaKreirajDTO : DTOBase
    {
        [DataMember]
        public int Trajanje { get; set; }
        [DataMember]
        public decimal MinimalnaCijena { get; set; }
        [DataMember]
        public decimal KupiOdmahCijena { get; set; }
        [DataMember]
        public string Proizvodjac { get; set; }
        [DataMember]
        public string Model { get; set; }
        [DataMember]
        public string Naziv { get; set; }
        [DataMember]
        public string DetaljanOpis { get; set; }
        [DataMember]
        public string Napomena { get; set; }
        [DataMember]
        public int NacinPlacanjaId { get; set; }
        [DataMember]
        public Slika[] Slike { get; set; }
        [DataMember]
        public int KategorijaId { get; set; }

    }
}
