using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Web.Http;
using BHao.Client.Service.Proxies;
using BHao.Client.Entities.DTOs;
using BHao.Common.Core;
using BHao.Web.Models;

namespace BHao.Web.Controllers.API
{
    [Authorize(Roles = "Admin, Uposlenik")]
    public class KomentariApiController : BaseApiController
    {
        [HttpGet]
        [Route("api/komentariKorisnika")]
        public HttpResponseMessage GetKomentari(HttpRequestMessage request, bool isPregledan, int page, int pageSize)
        {
            HttpResponseMessage response = null;
            try
            {
                using (KomentariClient proxy = new KomentariClient())
                {
                    proxy.Open();
                    var komentari = proxy.GetKomentari(isPregledan);

                    RezultatKomentariKorisnikaModel rezultat = new RezultatKomentariKorisnikaModel
                    {
                        BrojKomentara = komentari.Count(),
                        KomentariKorisnika = komentari.Page(page, pageSize).ToArray()
                    };
                    proxy.Close();

                    response = request.CreateResponse<RezultatKomentariKorisnikaModel>(HttpStatusCode.OK, rezultat);
                }
            }
            catch (FaultException ex)
            {
                response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            return response;
        }

        [HttpGet]
        [Route("api/komentariArtikala")]
        public HttpResponseMessage GetKomentariArtikala(HttpRequestMessage request, bool isPregledan, int page, int pageSize)
        {
            HttpResponseMessage response = null;

            try
            {
                using (KomentariClient proxy = new KomentariClient())
                {
                    proxy.Open();
                    var komentari = proxy.GetKomentariArtikala(isPregledan);

                    RezultatKomentariArtiklaModel rezultat = new RezultatKomentariArtiklaModel
                    {
                        BrojKomentara = komentari.Count(),
                        KomentariArtikla = komentari.Page(page, pageSize).ToArray()
                    };

                    proxy.Close();

                    response = request.CreateResponse<RezultatKomentariArtiklaModel>(HttpStatusCode.OK, rezultat);
                }
            }
            catch (FaultException ex)
            {
                response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            return response;
        }

        [HttpGet]
        [Route("api/komentariKorisnika/ukloni")]
        public HttpResponseMessage Ukloni(HttpRequestMessage request, int komentarId)
        {
            HttpResponseMessage response = null;

            try
            {
                using (KomentariClient proxy = new KomentariClient())
                {
                    proxy.Open();
                    proxy.UkloniKomentar(komentarId);

                    proxy.Close();

                    response = request.CreateResponse(HttpStatusCode.OK);
                }

            }
            catch (FaultException ex)
            {
                response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            return response;
        }

        [HttpGet]
        [Route("api/komentariArtikala/ukloni")]
        public HttpResponseMessage UkloniKomentarArtikla(HttpRequestMessage request, int komentarId)
        {
            HttpResponseMessage response = null;

            try
            {
                using (KomentariClient proxy = new KomentariClient())
                {
                    proxy.Open();
                    proxy.UkloniKomentarArtikla(komentarId);

                    proxy.Close();

                    response = request.CreateResponse(HttpStatusCode.OK);
                }

            }
            catch (FaultException ex)
            {
                response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            return response;
        }


        [HttpGet]
        [Route("api/komentariKorisnika/pregledan")]
        public HttpResponseMessage OznaciPregledan(HttpRequestMessage request, int komentarId)
        {
            HttpResponseMessage response = null;

            try
            {
                using (KomentariClient proxy = new KomentariClient())
                {
                    proxy.Open();
                    proxy.OznaciPregledan(komentarId);
                    proxy.Close();

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (FaultException ex)
            {
                response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            return response;
        }

        [HttpGet]
        [Route("api/komentariArtikala/pregledan")]
        public HttpResponseMessage OznaciPregledanArtikla(HttpRequestMessage request, int komentarId)
        {
            HttpResponseMessage response = null;

            try
            {
                using (KomentariClient proxy = new KomentariClient())
                {
                    proxy.Open();
                    proxy.OznaciPregledanArtikla(komentarId);
                    proxy.Close();

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (FaultException ex)
            {
                response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            return response;

        }
    }
}
