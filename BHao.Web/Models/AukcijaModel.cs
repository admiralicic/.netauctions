using BHao.Client.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BHao.Web.Models
{
    public class AukcijaModel
    {
        public int Trajanje { get; set; }
        public decimal MinimalnaCijena { get; set; }
        public decimal KupiOdmahCijena { get; set; }
        public string Proizvodjac { get; set; }
        public string Model { get; set; }
        public string Naziv { get; set; }
        public string DetaljanOpis { get; set; }
        public string Napomena { get; set; }       
        public int NacinPlacanjaId { get; set; }
        public Slika[] Slike { get; set; }
        public int KategorijaId { get; set; }
    }

   
}