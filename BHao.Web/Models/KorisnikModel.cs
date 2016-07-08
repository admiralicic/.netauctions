using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BHao.Web.Models
{
    public class KorisnikModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Ulica { get; set; }
        public string Broj { get; set; }
        public int GradId { get; set; }
        public string PostanskiBroj { get; set; }
        public string Telefon { get; set; }
        public string[] Uloge { get; set; }
        public bool IsOnemogucen { get; set; }
        public string Lozinka { get; set; }
    }
}