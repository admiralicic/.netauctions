
using BHao.Business.Service.Managers;
using BHao.Client.Entities;
using BHao.Client.Service.Contracts;
using BHao.Client.Service.Proxies;
using BHao.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {
        static void Main( string[] args )
        {
            //BHaoDataContext context = new BHaoDataContext( );


            //Aukcija aukcija = new Aukcija { KupacId = 2, KupiOdmahCijena = 250, MinimalnaCijena = 100, NacinPlacanjaId = 1, ProdavacId = 3, Naziv = "Neki naziv", Pocetak = DateTime.Now, Zavrsetak = DateTime.Now.AddDays( 2 ) };

            

            //context.Aukcije.Add( aukcija );
            //context.SaveChanges( );

            //aukcija = context.Aukcije.Find( 3 );
            //Console.WriteLine( "Aukcija Id: {0} ima nacin placanja {1}", aukcija.Id, aukcija.NacinPlacanja.Opis );
            //Console.ReadLine( );

           // AukcijeManager manager = new AukcijeManager( );

           //  Aukcija aukcija = new Aukcija
           //{
           //    Pocetak = DateTime.Now,
           //    Zavrsetak = DateTime.Now,
           //    KupiOdmahCijena = 300,
           //    MinimalnaCijena = 100,
           //    ProdavacId = 3,
           //    NacinPlacanjaId = 1,
           //    Naziv = "Naziv iz konzole",
           //    Napomena = "Napomena iz konzole",
           //    DetaljanOpis = "Detaljan opis iz konzole",
           //    ArtikalId = 2
           //};

           // Aukcija a = manager.KreirajAukciju(aukcija);

           // Console.WriteLine( a.Id );
           // Console.ReadLine( );

            //List<BHao.Business.Entities.Aukcija> aukcije1 = manager.GetAllAktivne( ).ToList( );

            //Console.WriteLine( aukcije1.Count );

            
        //    Console.ReadLine( );

        //    using ( AukcijeClient proxy = new AukcijeClient( ) )
        //    {
        //        proxy.Open( );
        //        Console.WriteLine( proxy.State );
        //        Console.ReadLine( );
        //        Aukcija aukcija = new Aukcija
        //   {
        //       Pocetak = DateTime.Now,
        //       Zavrsetak = DateTime.Now,
        //       KupiOdmahCijena = 300,
        //       MinimalnaCijena = 100,
        //       ProdavacId = 3,
        //       NacinPlacanjaId = 1,
        //       Naziv = "Naziv iz konzole",
        //       Napomena = "Napomena iz konzole",
        //       DetaljanOpis = "Detaljan opis iz konzole",
        //       ArtikalId = 2
        //   };
        //        List<BHao.Client.Entities.Aukcija> aukcije = proxy.GetAllAktivne( ).ToList( );
        //        foreach ( var a in aukcije )
        //        {
        //            Console.WriteLine( a.ProdavacId );
        //        }

        //        Console.ReadLine( );

        //        //Aukcija au = proxy.KreirajAukciju( aukcija );

        //        //Console.WriteLine( au.Id );


        //        //List<Aukcija> aukcije = proxy.GetAllAukcije( ).ToList( );


        //        //Grad g = proxy.Test( );
        //        //Console.WriteLine( g.Ime );

        //    //    //Console.WriteLine( aukcije.Count( ) );
        //        Console.ReadLine( );
        //        proxy.Close( );
        //    }





        }
    }
}
