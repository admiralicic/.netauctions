using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHao.Client.Entities
{
    public class Korisnik
    {
        //public int Id { get; set; }

        public string Ime { get; set; }

        public string Prezime { get; set; }

        public string Ulica { get; set; }

        public string Broj { get; set; }

        public string PostanskiBroj { get; set; }

        public string Telefon { get; set; }

        public int GradId { get; set; }

        public Grad Grad { get; set; }

        bool IsOnemogucen { get; set; }
    }
}
