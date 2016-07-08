using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHao.Business.Entities
{
    public class Uloga : IdentityRole<int, KorisnikUloga>
    {
        public Uloga( ) { }
        public Uloga( string name ) { Name = name; }
    }
}
