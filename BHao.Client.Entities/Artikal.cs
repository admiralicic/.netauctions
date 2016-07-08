using BHao.Client.Entities.DTOs;
using BHao.Common.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHao.Client.Entities
{

    public class Artikal : ObjectBase
    {
        
        public int Id { get; set; }
        
        public string Proizvodjac { get; set; }
        
        public string Model { get; set; }
        
        public string Naziv { get; set; }

        public double ProsjecnaOcjena { get; set; }

        public IEnumerable<KomentarArtiklaDTO> Komentari { get; set; } 
    }
}
