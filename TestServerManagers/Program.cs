using BHao.Business.Entities;
using BHao.Business.Service.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestServerManagers
{
    class Program
    {
        static void Main( string[] args )
        {
            AukcijeManager service = new AukcijeManager( );

            List<Aukcija> aukcije = service.GetAllAukcije( ).ToList( );

            Console.WriteLine( aukcije.Count );
            Console.ReadLine( );
        }
    }
}
