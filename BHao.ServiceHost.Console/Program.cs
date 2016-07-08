using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using BHao.Business.Service.Contracts;
using BHao.Business.Service.Managers;
using System.ServiceModel.Description;
using BHao.Business.Service.Business_Engine;
using BHao.Business.Entities;

namespace BHao.ServiceHost.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Pokretanje servisa....");
            System.Console.WriteLine("");

            System.ServiceModel.ServiceHost hostAukcijeManager = new System.ServiceModel.ServiceHost(typeof(AukcijeManager));
            System.ServiceModel.ServiceHost hostSystemManager = new System.ServiceModel.ServiceHost(typeof(SystemManager));
            System.ServiceModel.ServiceHost hostPorukeManager = new System.ServiceModel.ServiceHost(typeof(PorukeManager));
            System.ServiceModel.ServiceHost hostKomentariManager = new System.ServiceModel.ServiceHost(typeof(KomentariManager));

            PokreniService(hostAukcijeManager, "AukcijeService");
            PokreniService(hostSystemManager, "SystemService");
            PokreniService(hostPorukeManager, "PorukeService");
            PokreniService(hostKomentariManager, "KomentariService");

            System.Timers.Timer timer = new System.Timers.Timer(5000);
            timer.Elapsed += OnTimerElapsed;
            timer.Start();

            System.Console.WriteLine("");
            System.Console.WriteLine("[Enter] za zaustavljanje servisa.");
            System.Console.ReadLine();
            timer.Stop();

            ZaustaviService(hostAukcijeManager, "AukcijeService");
            ZaustaviService(hostSystemManager, "SystemService");
            ZaustaviService(hostPorukeManager, "PorukeService");
            ZaustaviService(hostKomentariManager, "KomentariService");

            System.Console.ReadLine();

        }

        private static void OnTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            List<Aukcija> okoncaneAukcije = BHaoBusinessEngine.ProvjeriZavrsene();
            System.Console.WriteLine("Provjera isteklih aukcija {0}. Okoncano {1}!", DateTime.Now, okoncaneAukcije.Count());
            okoncaneAukcije.Clear();
        }

        static void PokreniService(System.ServiceModel.ServiceHost host, string nazivServisa)
        {
#if DEBUG
            ServiceDebugBehavior debug = host.Description.Behaviors.Find<ServiceDebugBehavior>();
            if (debug == null)
            {
                host.Description.Behaviors.Add(new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });
            }
            else
            {
                if (!debug.IncludeExceptionDetailInFaults)
                {
                    debug.IncludeExceptionDetailInFaults = true;
                }
            }
#endif
            host.Open();
            System.Console.WriteLine("Pokrenut {0}", nazivServisa);

            foreach (var endpoint in host.Description.Endpoints)
            {
                System.Console.WriteLine("");
                System.Console.WriteLine("Endpoint:");
                System.Console.WriteLine("Adresa: {0}", endpoint.Address.Uri);
                System.Console.WriteLine("Binding: {0}", endpoint.Binding.Name);
                System.Console.WriteLine("Contract: {0}", endpoint.Contract.Name);
                System.Console.WriteLine("");
            }
        }

        static void ZaustaviService(System.ServiceModel.ServiceHost host, string nazivServisa)
        {
            host.Close();
            System.Console.WriteLine("Zaustavljanje servisa {0} ", nazivServisa);
        }
    }
}
