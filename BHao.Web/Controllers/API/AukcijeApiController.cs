using BHao.Client.Entities;
using BHao.Client.Service.Proxies;
using BHao.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using BHao.Client.Entities.DTOs;
using BHao.Common.Exceptions;
using System.Security.Claims;
using System.Web;

using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using BHao.Common.Core;

namespace BHao.Web.Controllers.API
{
    public class AukcijeApiController : ApiController
    {
        [HttpPost]
        [Route("api/aukcija/kreiraj")]
        [Authorize]
        public HttpResponseMessage Kreiraj(HttpRequestMessage request, [FromBody] AukcijaKreirajDTO aukcijaModel)
        {

            int prijavljeniKorisnik = User.Identity.GetUserId<int>();

            HttpResponseMessage response = null;
            try
            {
                using (AukcijeClient proxy = new AukcijeClient())
                {
                    proxy.Open();

                    Aukcija a = proxy.KreirajAukciju(aukcijaModel, prijavljeniKorisnik);
                    response = request.CreateResponse<int>(HttpStatusCode.OK, a.Id);
                    proxy.Close();
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

        [HttpPost]
        [Route("api/aukcija/all")]
        public HttpResponseMessage GetAll(HttpRequestMessage request, [FromBody] KriterijPretrageModel kriterij)
        {
            HttpResponseMessage response = null;

            try
            {
                using (AukcijeClient proxy = new AukcijeClient())
                {
                    proxy.Open();
                    AukcijaDetailDTO[] aukcije = proxy.GetAll(kriterij.Zavrsena);
                    if (kriterij.Page > 0)
                    {
                        RezultatModel rezultat = new RezultatModel
                        {
                            brojAukcija = aukcije.Count(),
                            aukcije = aukcije.Page(kriterij.Page, kriterij.PageSize).ToArray()
                        };

                        response = request.CreateResponse<RezultatModel>(HttpStatusCode.OK, rezultat);
                    }
                    else
                    {
                        response = request.CreateResponse<AukcijaDetailDTO[]>(HttpStatusCode.OK, aukcije);
                    }
                    proxy.Close();
                    
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


        [HttpPost]
        [Route("api/aukcija/allaktivne")]
        public HttpResponseMessage GetAukcijeAktivne(HttpRequestMessage request, [FromBody] KriterijPretrageModel kriterij)
        {
            HttpResponseMessage response = null;
            try
            {
                using (AukcijeClient proxy = new AukcijeClient())
                {

                    proxy.Open();
                    AukcijaDetailDTO[] aukcije = proxy.GetAllAktivne(kriterij.KategorijaId, kriterij.StringZaPretragu);
                    response = request.CreateResponse<AukcijaDetailDTO[]>(HttpStatusCode.OK, aukcije);
                    proxy.Close();
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

        [HttpPost]
        [Route("api/aukcija/zadnjeuspjesne")]
        public HttpResponseMessage GetZadnjeUspjesne(HttpRequestMessage request, [FromBody] KriterijPretrageModel kriterij)
        {
            HttpResponseMessage response = null;

            try
            {
                using (AukcijeClient proxy = new AukcijeClient())
                {
                    proxy.Open();
                    AukcijaDetailDTO[] aukcije = proxy.GetZadnjeUspjesne(kriterij.KategorijaId, kriterij.StringZaPretragu);
                    response = request.CreateResponse<AukcijaDetailDTO[]>(HttpStatusCode.OK, aukcije);
                    proxy.Close();
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

        [HttpPost]
        [Route("api/aukcija/detalji")]
        public HttpResponseMessage GetAukcijaDetail(HttpRequestMessage request, [FromBody] int aukcijaId)
        {
            HttpResponseMessage response = null;

            try
            {
                int korisnikId = User.Identity.GetUserId<int>();
                if (korisnikId < 1)
                {
                    ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;

                    var korisnikIme = ClaimsPrincipal.Current.Identity.Name;
                    if (!String.IsNullOrEmpty(korisnikIme))
                    {
                        var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
                        var korisnik = userManager.FindByName(korisnikIme);
                        korisnikId = korisnik.Id;
                    }
                    

                }

                using (AukcijeClient proxy = new AukcijeClient())
                {
                    proxy.Open();
                    AukcijaDTO aukcija = proxy.GetAukcijaDetail(aukcijaId, korisnikId);

                    aukcija.isAdmin = User.IsInRole("Admin") ? true : false;

                    response = request.CreateResponse<AukcijaDTO>(HttpStatusCode.OK, aukcija);
                    proxy.Close();
                }
            }
            catch (FaultException<PonudaCreateException> ex)
            {
                response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
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

        [HttpPost]
        [Route("api/aukcija/ponudi")]
        [Authorize]
        public HttpResponseMessage KreirajPonudu(HttpRequestMessage request, [FromBody] PonudaModel ponuda)
        {
            HttpResponseMessage response = null;

            int prijavljeniKorisnik = User.Identity.GetUserId<int>();
            if (prijavljeniKorisnik < 1)
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;

                var korisnikIme = ClaimsPrincipal.Current.Identity.Name;
                var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var korisnik = userManager.FindByName(korisnikIme);
                prijavljeniKorisnik = korisnik.Id;

            }

            try
            {
                using (AukcijeClient proxy = new AukcijeClient())
                {

                    proxy.Open();


                    AukcijaDTO detaljiAukcije = proxy.AukcijaKreirajPonudu(ponuda.AukcijaId, ponuda.IznosPonude, prijavljeniKorisnik);
                    response = request.CreateResponse<AukcijaDTO>(HttpStatusCode.OK, detaljiAukcije);
                    proxy.Close();


                }
            }
            catch (FaultException<PonudaCreateException> ex)
            {
                response = request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
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

        [HttpPost]
        [Route("api/aukcija/kupiodmah")]
        [Authorize]
        public HttpResponseMessage KupiOdmah(HttpRequestMessage request, [FromBody] int aukcijaId)
        {

            int prijavljeniKorisnik = User.Identity.GetUserId<int>();
            if (prijavljeniKorisnik < 1)
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;

                var korisnikIme = ClaimsPrincipal.Current.Identity.Name;
                var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var korisnik = userManager.FindByName(korisnikIme);
                prijavljeniKorisnik = korisnik.Id;

            }

            HttpResponseMessage response = null;
            try
            {
                using (AukcijeClient proxy = new AukcijeClient())
                {
                    proxy.Open();

                    proxy.KupiOdmah(aukcijaId, prijavljeniKorisnik);

                    response = request.CreateResponse(HttpStatusCode.OK);
                    proxy.Close();
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

        [HttpPost]
        [Route("api/aukcija/ocijeni")]
        [Authorize]
        public HttpResponseMessage OcijeniKorisnika(HttpRequestMessage request, [FromBody] OcjenaKorisnika ocjena)
        {
            HttpResponseMessage response = null;

            try
            {
                using (AukcijeClient proxy = new AukcijeClient())
                {
                    proxy.Open();

                    proxy.OcijeniKorisnika(ocjena);

                    response = request.CreateResponse(HttpStatusCode.OK);
                    proxy.Close();
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

        [HttpPost]
        [Route("api/aukcija/artikal/ocijeni")]
        [Authorize]
        public HttpResponseMessage OcijeniArtikal(HttpRequestMessage request, [FromBody] OcjenaArtikla ocjena)
        {
            HttpResponseMessage response = null;

            try
            {
                using (AukcijeClient proxy = new AukcijeClient())
                {
                    proxy.Open();

                    proxy.OcijeniArtikal(ocjena);
                    response = request.CreateResponse(HttpStatusCode.OK);
                    proxy.Close();
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

        [HttpPost]
        [Route("api/aukcija/komentar/dodaj")]
        [Authorize]
        public HttpResponseMessage DodajKomentar(HttpRequestMessage request, [FromBody] KomentarKorisnika komentar)
        {
            HttpResponseMessage response = null;

            try
            {
                using (AukcijeClient proxy = new AukcijeClient())
                {
                    proxy.Open();

                    proxy.DodajKomentar(komentar);

                    response = request.CreateResponse(HttpStatusCode.OK);
                    proxy.Close();
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

        [HttpPost]
        [Route("api/aukcija/komentarArtikla/dodaj")]
        [Authorize]
        public HttpResponseMessage DodajKomentarArtikla(HttpRequestMessage request, [FromBody] KomentarArtikla komentar)
        {
            HttpResponseMessage response = null;

            try
            {
                using (AukcijeClient proxy = new AukcijeClient())
                {
                    proxy.Open();

                    proxy.DodajKomentarArtikla(komentar);

                    response = request.CreateResponse(HttpStatusCode.OK);

                    proxy.Close();
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

        [HttpPost]
        [Route("api/aukcija/getzakorisnika")]
        [Authorize]
        public HttpResponseMessage GetAukcijeZaKorisnika(HttpRequestMessage request, [FromBody] MojeAukcijeKriterij kriterij)
        {
            HttpResponseMessage response = null;
            try
            {
                using (AukcijeClient proxy = new AukcijeClient())
                {
                    proxy.Open();
                    AukcijaDetailDTO[] aukcije = proxy.GetAukcijeByKorisnik(kriterij.KorisnikId, kriterij.Kriterij).ToArray();

                    if (kriterij.Page > 0)
                    {
                        RezultatModel rezultat = new RezultatModel
                        {
                            brojAukcija = aukcije.Count(),
                            aukcije = aukcije.Page(kriterij.Page, kriterij.PageSize).ToArray()
                        };

                        response = request.CreateResponse<RezultatModel>(HttpStatusCode.OK, rezultat);
                    }
                    else
                    {
                        response = request.CreateResponse<AukcijaDetailDTO[]>(HttpStatusCode.OK, aukcije);
                    }

                    proxy.Close();
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

        [HttpPost]
        [Route("api/aukcija/getpreporuke")]
        [Authorize]
        public HttpResponseMessage GetPreporuke(HttpRequestMessage request, [FromBody] int artikalId)
        {
            HttpResponseMessage response = null;

            int prijavljeniKorisnikId = User.Identity.GetUserId<int>();

            try
            {
                using (AukcijeClient proxy = new AukcijeClient())
                {
                    proxy.Open();
                    AukcijaDTO[] prepouke = proxy.GetPreporuke(artikalId, prijavljeniKorisnikId);
                    response = request.CreateResponse<AukcijaDTO[]>(HttpStatusCode.OK, prepouke);
                    proxy.Close();

                }
            }
            catch (FaultException ex)
            {
                response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            catch(Exception ex)
            {
                response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }


            return response;
        }

        [HttpGet]
        [Route("api/aukcija/zavrsene")]
        public HttpResponseMessage GetZavrseneAukcije(HttpRequestMessage request, string filterPlacanja, string stringZaPretragu, int page, int pageSize)
        {
            HttpResponseMessage response = null;

            try
            {
                using (AukcijeClient proxy = new AukcijeClient())
                {
                    proxy.Open();

                    AukcijaDetailDTO[] aukcije = proxy.GetAllZavrsene(filterPlacanja, stringZaPretragu);

                    if (page > 0)
                    {
                        RezultatModel rezultat = new RezultatModel
                        {
                            brojAukcija = aukcije.Count(),
                            aukcije = aukcije.Page(page, pageSize).ToArray()
                        };
                        
                        response = request.CreateResponse<RezultatModel>(HttpStatusCode.OK, rezultat);
                    }
                    else
                    {
                        response = request.CreateResponse<AukcijaDetailDTO[]>(HttpStatusCode.OK, aukcije);
                    }
                    proxy.Close();
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

        [HttpPost]
        [Route("api/aukcija/evidentirajplacanje")]
        public HttpResponseMessage UpdatePlacanje(HttpRequestMessage request, [FromBody] PlacanjeModel placanje)
        {
            HttpResponseMessage response = null;

            try
            {
                using (AukcijeClient proxy = new AukcijeClient())
                {
                    proxy.Open();
                    proxy.EvidentirajPlacanje(placanje.AukcijaId, placanje.DatumPlacanja);
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

        [HttpPost]
        [Route("api/aukcija/ukloni")]
        public HttpResponseMessage Ukloni(HttpRequestMessage request, [FromBody]int aukcijaId)
        {
            HttpResponseMessage response = null;

            try
            {
                using (AukcijeClient proxy = new AukcijeClient())
                {
                    proxy.Open();
                    proxy.Ukloni(aukcijaId);
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
        [HttpPost]
        [Route("api/statistika")]
        public HttpResponseMessage GetStatistika(HttpRequestMessage request, [FromBody] ReportKriterijModel kriterij)
        {
            HttpResponseMessage response = null;

            try
            {
                using (AukcijeClient proxy = new AukcijeClient())
                {
                    proxy.Open();
                    StatistikaDTO statistika = proxy.GetStatistika(kriterij.KategorijaId, kriterij.PrikazOd, kriterij.PrikazDo);
                    proxy.Close();

                    response = request.CreateResponse<StatistikaDTO>(HttpStatusCode.OK, statistika);
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
