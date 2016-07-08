using BHao.Business.Entities.DTOs;
using BHao.Business.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BHao.Data;
using BHao.Data.Contracts;
using BHao.Data.Repositories;
using System.ServiceModel;
using BHao.Business.Entities;

namespace BHao.Business.Service.Managers
{
    public class KomentariManager : ManagerBase, IKomentariService
    {
        public KomentarDTO[] GetKomentari(bool isPregledan)
        {
            try
            {
                using (BHaoDataContext context = new BHaoDataContext())
                {
                    IKomentarRepository repo = new KomentarRepository(context);

                    var komentari = repo.GetKomentariPregled(isPregledan);

                    return komentari;
                }
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
            
        }

        public KomentarArtiklaDTO[] GetKomentariArtikala(bool isPregledan)
        {
            try
            {
                using (BHaoDataContext context = new BHaoDataContext())
                {
                    IKomentarArtiklaRepository repo = new KomentarArtiklaRepository(context);

                    var komentari = repo.GetKomentariPregled(isPregledan);

                    return komentari;
                }
            }
            catch (Exception ex)
            {

                throw new FaultException(ex.Message);
            }
        }


        public void UkloniKomentar(int komentarId)
        {
            using (BHaoDataContext context = new BHaoDataContext())
            {
                IKomentarRepository repo = new KomentarRepository(context);
                var komentar = repo.Get(komentarId);
                komentar.isDeleted = true;
                repo.Update(komentar);

                PosaljiPoruku(context, komentar.KomentatorId, komentar.AukcijaId, komentar.TextKomentara);
            }
        }

        public void UkloniKomentarArtikla(int komentarId)
        {
            using (BHaoDataContext context = new BHaoDataContext())
            {
                IKomentarArtiklaRepository repo = new KomentarArtiklaRepository(context);
                var komentar = repo.Get(komentarId);
                komentar.isDeleted = true;
                repo.Update(komentar);

                PosaljiPoruku(context, komentar.KomentatorId, komentar.AukcijaId, komentar.TextKomentara);
            }
        }

        public void OznaciPregledan(int komentarId)
        {
            using (BHaoDataContext context = new BHaoDataContext())
            {
                IKomentarRepository repo = new KomentarRepository(context);
                var komentar = repo.Get(komentarId);
                komentar.KomentarPregledan = true;
                repo.Update(komentar);
               
            }
        }

        public void OznaciPregledanArtikla(int komentarId)
        {
            using (BHaoDataContext context = new BHaoDataContext())
            {
                IKomentarArtiklaRepository repo = new KomentarArtiklaRepository(context);
                var komentar = repo.Get(komentarId);
                komentar.KomentarPregledan = true;
                repo.Update(komentar);
            }
        }

        private static void PosaljiPoruku(BHaoDataContext context, int komentatorId, int aukcijaId, string textKomentara)
        {
            Poruka poruka = new Poruka()
            {
                Datum = DateTime.Now,
                PosiljalacId = 14,
                PrimalacId = komentatorId,
                TextPoruke = String.Format("Vaš komentar '{0}' je uklonjen!", textKomentara),
                AukcijaId = aukcijaId
            };

            PorukaRepository porukaRepo = new PorukaRepository(context);
            porukaRepo.Insert(poruka);
        }
    }
}
