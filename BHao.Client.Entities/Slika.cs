using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHao.Client.Entities
{
    public class Slika
    {
        public int Id { get; set; }

        public string SlikaIme { get; set; }

        public string SlikaThumbIme { get; set; }

        public int AukcijalId { get; set; }

        public Aukcija Aukcija { get; set; }
    }
}
