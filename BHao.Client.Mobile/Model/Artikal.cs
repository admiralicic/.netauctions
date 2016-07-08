using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHao.Client.Mobile.Model
{

    public class Artikal
    {
        
        public int Id { get; set; }
        public string Proizvodjac { get; set; }
        public string Model { get; set; }
        public string Naziv { get; set; }
        public double ProsjecnaOcjena { get; set; }

        public string PuniNaziv
        {
            get { return Proizvodjac + " " + Model; }
        }
        
       
    }
}
