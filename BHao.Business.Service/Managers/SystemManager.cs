using BHao.Business.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BHao.Data;
using System.ServiceModel;
using BHao.Data.Contracts;
using BHao.Data.Repositories;
using BHao.Business.Entities;
using BHao.Data.Common;
using BHao.Business.Entities.DTOs;

namespace BHao.Business.Service.Managers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
        ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class SystemManager : ManagerBase, ISystemService
    {
        public SystemManager()
        {

        }

        public NacinPlacanja[] GetNaciniPlacanja()
        {
            try
            {
                using (BHaoDataContext context = new BHaoDataContext())
                {
                    IDataRepository<NacinPlacanja> repo = new DataRepositoryBase<NacinPlacanja>(context);

                    return repo.GetAll().Where(x => x.isDeleted == false).ToArray();
                }
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        public Artikal[] GetArtikli()
        {
            try
            {
                using (BHaoDataContext context = new BHaoDataContext())
                {
                    IDataRepository<Artikal> repo = new DataRepositoryBase<Artikal>(context);

                    return repo.GetAll().Where(x => x.isDeleted == false).ToArray();
                }
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        public Kategorija[] GetKategorije()
        {
            try
            {
                using (BHaoDataContext context = new BHaoDataContext())
                {
                    IDataRepository<Kategorija> repo = new DataRepositoryBase<Kategorija>(context);

                    return repo.GetAll().Where(x => x.isDeleted == false).OrderBy(x => x.Naziv).ToArray();
                }
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        public Grad[] GetGradovi()
        {
            try
            {
                using (BHaoDataContext context = new BHaoDataContext())
                {
                    IDataRepository<Grad> repo = new DataRepositoryBase<Grad>(context);

                    return repo.GetAll().Where(x => x.isDeleted == false).OrderBy(x => x.Naziv).ToArray();
                }
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }


        public KorisnikDTO GetKorisnikById(int korisnikId)
        {
            try
            {
                using (BHaoDataContext context = new BHaoDataContext())
                {
                    KorisnikRepository repo = new KorisnikRepository(context);

                    return repo.GetById(korisnikId);
                }
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        public KorisnikDTO GetKorisnikByKorisnickoIme(string korisnickoIme)
        {
            try
            {
                using (BHaoDataContext context = new BHaoDataContext())
                {
                    KorisnikRepository repo = new KorisnikRepository(context);

                    return repo.GetByKorisnickoIme(korisnickoIme);
                }
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }

        public Inkrement[] GetInkrementi()
        {
            try
            {
                using (BHaoDataContext context = new BHaoDataContext())
                {
                    InkrementRepository repo = new InkrementRepository(context);
                    return repo.GetAll().Where(x => x.isDeleted == false).OrderBy(x=>x.Cijena).ToArray();
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public void SacuvajGrad(Grad grad)
        {
            try
            {
                using (BHaoDataContext context = new BHaoDataContext())
                {
                    IDataRepository<Grad> repo = new DataRepositoryBase<Grad>(context);

                    if (repo.GetAll().Where(x=>x.Naziv == grad.Naziv && x.isDeleted == false).FirstOrDefault() != null)
                    {
                        throw new Exception(String.Format("Grad '{0}' već postoji u bazi.", grad.Naziv));
                    }

                    if (grad.Id != 0)
                    {
                        Grad g = repo.Get(grad.Id);
                        g.Naziv = grad.Naziv;
                        g.isDeleted = grad.isDeleted;
                        repo.Update(g);
                    }
                    else
                    {
                        repo.Insert(grad);
                    }

                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public void SacuvajKategorija(Kategorija kategorija)
        {
            try
            {
                using (BHaoDataContext context = new BHaoDataContext())
                {
                    IDataRepository<Kategorija> repo = new DataRepositoryBase<Kategorija>(context);

                    if (repo.GetAll().Where(x => x.Naziv == kategorija.Naziv && x.isDeleted == false).FirstOrDefault() != null)
                    {
                        throw new Exception(String.Format("Kategorija '{0}' već postoji u bazi.", kategorija.Naziv.ToLower()));
                    }

                    if (kategorija.Id != 0)
                    {
                        Kategorija k = repo.Get(kategorija.Id);
                        k.Naziv = kategorija.Naziv;
                        k.isDeleted = kategorija.isDeleted;
                        repo.Update(k);
                    }
                    else
                    {
                        repo.Insert(kategorija);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public void SacuvajInkrement(Inkrement inkrement)
        {
            try
            {
                using (BHaoDataContext context = new BHaoDataContext())
                {
                    IDataRepository<Inkrement> repo = new DataRepositoryBase<Inkrement>(context);

                    if (repo.GetAll().Where(x => (x.Cijena == inkrement.Cijena || x.IznosInkrementa == inkrement.IznosInkrementa) && x.isDeleted == false).FirstOrDefault() != null)
                    {
                        throw new Exception("Cijena ili iznos inkrementa već postoji u bazi.");
                    }

                    if (inkrement.Id != 0)
                    {
                        Inkrement i = repo.Get(inkrement.Id);
                        i.Cijena = inkrement.Cijena;
                        i.IznosInkrementa = inkrement.IznosInkrementa;
                        i.isDeleted = inkrement.isDeleted;

                        repo.Update(i);
                    }
                    else
                    {
                        repo.Insert(inkrement);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public void SacuvajNacinPlacanja(NacinPlacanja nacinPlacanja)
        {
            try
            {
                using (BHaoDataContext context = new BHaoDataContext())
                {
                    IDataRepository<NacinPlacanja> repo = new DataRepositoryBase<NacinPlacanja>(context);

                    if (repo.GetAll().Where(x => x.Opis == nacinPlacanja.Opis && x.isDeleted == false).FirstOrDefault() != null)
                    {
                        throw new Exception(String.Format("Način plaćanja '{0}' već postoji u bazi.", nacinPlacanja.Opis.ToLower()));
                    }

                    if (nacinPlacanja.Id != 0)
                    {
                        NacinPlacanja np = repo.Get(nacinPlacanja.Id);
                        np.Opis = nacinPlacanja.Opis;
                        np.isDeleted = nacinPlacanja.isDeleted;

                        repo.Update(np);
                    }
                    else
                    {
                        repo.Insert(nacinPlacanja);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }


        public void ObrisiGrad(int id)
        {
            try
            {
                using (BHaoDataContext context = new BHaoDataContext())
                {
                    IDataRepository<Grad> repo = new DataRepositoryBase<Grad>(context);

                    if (id > 0)
                    {
                        Grad grad = repo.Get(id);
                        grad.isDeleted = true;
                        repo.Update(grad);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public void ObrisiKategorija(int id)
        {
            try
            {
                using (BHaoDataContext context = new BHaoDataContext())
                {
                    IDataRepository<Kategorija> repo = new DataRepositoryBase<Kategorija>(context);

                    if (id > 0)
                    {
                        Kategorija kategorija = repo.Get(id);
                        kategorija.isDeleted = true;
                        repo.Update(kategorija);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public void ObrisiInkrement(int id)
        {
            try
            {
                using (BHaoDataContext context = new BHaoDataContext())
                {
                    IDataRepository<Inkrement> repo = new DataRepositoryBase<Inkrement>(context);

                    if (id > 0)
                    {
                        Inkrement inkrement = repo.Get(id);
                        inkrement.isDeleted = true;
                        repo.Update(inkrement);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public void ObrisiNacinPlacanja(int id)
        {
            try
            {
                using (BHaoDataContext context = new BHaoDataContext())
                {
                    IDataRepository<NacinPlacanja> repo = new DataRepositoryBase<NacinPlacanja>(context);

                    if (id > 0)
                    {
                        NacinPlacanja np = repo.Get(id);
                        np.isDeleted = true;
                        repo.Update(np);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
    }
}
