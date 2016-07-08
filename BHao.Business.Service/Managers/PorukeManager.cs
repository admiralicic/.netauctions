using BHao.Business.Entities;
using BHao.Business.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using BHao.Data;
using BHao.Data.Contracts;
using BHao.Data.Repositories;
using BHao.Business.Entities.DTOs;

namespace BHao.Business.Service.Managers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
        ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class PorukeManager : ManagerBase, IPorukeService
    {
        public PorukeManager()
        {

        }

        public int PosaljiPoruku(Poruka poruka)
        {
            try
            {
                using (BHaoDataContext context = new BHaoDataContext())
                {
                    if (String.IsNullOrEmpty(poruka.TextPoruke))
                    {
                        throw new FaultException("Ne možete poslati praznu poruku!");
                    }

                    IPorukaRepository repo = new PorukaRepository(context);
                    poruka.Datum = DateTime.Now;
                    poruka.IsObrisanaPosiljalac = false;
                    poruka.IsObrisanaPrimalac = false;
                    Poruka p = repo.Insert(poruka);

                    return p.Id;
                }
            }
            catch (FaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        
        public PorukaDTO[] GetPoslane(int korisnikId)
        {
            try
            {
                using (BHaoDataContext context = new BHaoDataContext())
                {
                    IPorukaRepository repo = new PorukaRepository(context);

                    return repo.GetPoslane(korisnikId).ToArray();
                }
            }
            catch (FaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public PorukaDTO[] GetPrimljene(int korisnikId)
        {
            try
            {
                using (BHaoDataContext context = new BHaoDataContext())
                {
                    IPorukaRepository repo = new PorukaRepository(context);

                    return repo.GetPrimljene(korisnikId).ToArray();
                }
            }
            catch (FaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public void ObrisiPoruku(PorukaDTO poruka, int korisnikId)
        {
            try
            {
                using (BHaoDataContext context = new BHaoDataContext())
                {
                    IPorukaRepository repo = new PorukaRepository(context);

                    repo.ObrisiPoruku(poruka, korisnikId);
                }
            }
            catch (FaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public Poruka GetPoruka(int porukaId)
        {
            try
            {
                using (BHaoDataContext context = new BHaoDataContext())
                {
                    IPorukaRepository repo = new PorukaRepository(context);

                    return repo.Get(porukaId);
                }
            }
            catch (FaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public Poruka OznaciProcitana(int porukaId)
        {
            try
            {
                using (BHaoDataContext context = new BHaoDataContext())
                {
                    IPorukaRepository repo = new PorukaRepository(context);

                    Poruka poruka = repo.Get(porukaId);
                    poruka.Procitana = true;
                    repo.Update(poruka);

                    return poruka;
                }
            }
            catch (FaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public PorukaDTO[] GetPoruke(int korisnikId)
        {
            try
            {
                using (BHaoDataContext context = new BHaoDataContext())
                {
                    IPorukaRepository repo = new PorukaRepository(context);

                    return repo.GetPoruke(korisnikId).ToArray();
                }
            }
            catch (FaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }


       
    }
}
