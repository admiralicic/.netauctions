using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.ServiceModel;
using BHao.Client.Service.Proxies;
using BHao.Client.Entities;

namespace BHao.Web.Controllers.API
{
    public class PostavkeApiController : ApiController
    {
        [HttpGet]
        [Route( "api/lookup/naciniplacanja" )]
        public HttpResponseMessage GetNaciniPlacanja( HttpRequestMessage request )
        {

            HttpResponseMessage response = null;

            try
            {
                using ( SystemClient proxy = new SystemClient() )
                {
                    proxy.Open();
                    NacinPlacanja[] naciniPlacanja = proxy.GetNaciniPlacanja( );

                    response = request.CreateResponse<NacinPlacanja[]>( HttpStatusCode.OK, naciniPlacanja );
                    
                    proxy.Close();
                }
            }
            catch ( FaultException ex )
            {
                response = request.CreateResponse( HttpStatusCode.InternalServerError, ex.Message );
            }
            catch (Exception ex)
            {
                response = request.CreateResponse( HttpStatusCode.InternalServerError, ex.Message );
            }

            return response;
        }

        [HttpGet]
        [Route("api/lookup/kategorije")]
        public HttpResponseMessage GetKategorije( HttpRequestMessage request )
        {
            HttpResponseMessage response = null;

            try
            {
                using ( SystemClient proxy = new SystemClient() )
                {
                    proxy.Open();
                    Kategorija[] kategorije = proxy.GetKategorije( );

                    response = request.CreateResponse<Kategorija[]>( HttpStatusCode.OK, kategorije );

                    proxy.Close();
                }
            }
            catch ( FaultException ex )
            {
                response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            catch(Exception ex)
            {
                response = request.CreateResponse( HttpStatusCode.InternalServerError, ex.Message );
            }

            return response;
            
        }

        [HttpGet]
        [Route("api/lookup/artikli")]
        public HttpResponseMessage GetArtikli(HttpRequestMessage request)
        {
            HttpResponseMessage response = null;

            try
            {
                using (SystemClient proxy = new SystemClient())
                {
                    proxy.Open();

                    Artikal[] artikli = proxy.GetArtikli();

                    response = request.CreateResponse<Artikal[]>(HttpStatusCode.OK, artikli);

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

        [HttpGet]
        [Route("api/lookup/gradovi")]
        public HttpResponseMessage GetGradovi(HttpRequestMessage request)
        {
            HttpResponseMessage response = null;

            try
            {
                using (SystemClient proxy = new SystemClient())
                {
                    proxy.Open();

                    Grad[] gradovi = proxy.GetGradovi();

                    response = request.CreateResponse<Grad[]>(HttpStatusCode.OK, gradovi);

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

        


        [HttpGet]
        [Route("api/lookup/inkrementi")]
        public HttpResponseMessage GetInkrementi(HttpRequestMessage request)
        {
            HttpResponseMessage response = null;

            try
            {
                using (SystemClient proxy = new SystemClient())
                {
                    proxy.Open();
                    Inkrement[] inkrementi = proxy.GetInkrementi();
                    response = request.CreateResponse<Inkrement[]>(HttpStatusCode.OK, inkrementi);
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
        [Route("api/postavke/grad/sacuvaj")]
        public HttpResponseMessage SacuvajGrad(HttpRequestMessage request, [FromBody] Grad grad)
        {
            HttpResponseMessage response = null;

            try
            {
                using (SystemClient proxy = new SystemClient())
                {
                    proxy.Open();
                    proxy.SacuvajGrad(grad);
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
        [Route("api/postavke/kategorija/sacuvaj")]
        [Authorize]
        public HttpResponseMessage SacuvajKategorija(HttpRequestMessage request, [FromBody] Kategorija kategorija)
        {
            HttpResponseMessage response = null;

            try
            {
                using (SystemClient proxy = new SystemClient())
                {
                    proxy.Open();
                    proxy.SacuvajKategorija(kategorija);
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
        [Route("api/postavke/inkrement/sacuvaj")]
        [Authorize]
        public HttpResponseMessage SacuvajInkrement(HttpRequestMessage request, [FromBody] Inkrement inkrement)
        {
            HttpResponseMessage response = null;

            try
            {
                using (SystemClient proxy = new SystemClient())
                {
                    proxy.Open();
                    proxy.SacuvajInkrement(inkrement);
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
        [Route("api/postavke/nacinplacanja/sacuvaj")]
        [Authorize]
        public HttpResponseMessage SacuvajNacinPlacanja(HttpRequestMessage request, [FromBody] NacinPlacanja nacinPlacanja)
        {
            HttpResponseMessage response = null;

            try
            {
                using (SystemClient proxy = new SystemClient())
                {
                    proxy.Open();
                    proxy.SacuvajNacinPlacanja(nacinPlacanja);
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
        [Route("api/postavke/grad/obrisi")]
        [Authorize]
        public HttpResponseMessage ObrisiGrad(HttpRequestMessage request, [FromBody] int id)
        {
            HttpResponseMessage response = null;

            try
            {
                using (SystemClient proxy = new SystemClient())
                {
                    proxy.Open();
                    proxy.ObrisiGrad(id);
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
        [Route("api/postavke/kategorija/obrisi")]
        [Authorize]
        public HttpResponseMessage ObrisiKategorija(HttpRequestMessage request, [FromBody] int id)
        {
            HttpResponseMessage response = null;

            try
            {
                using (SystemClient proxy = new SystemClient())
                {
                    proxy.Open();
                    proxy.ObrisiKategorija(id);
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
        [Route("api/postavke/inkrement/obrisi")]
        [Authorize]
        public HttpResponseMessage ObrisiInkrement(HttpRequestMessage request, [FromBody] int id)
        {
            HttpResponseMessage response = null;

            try
            {
                using (SystemClient proxy = new SystemClient())
                {
                    proxy.Open();
                    proxy.ObrisiInkrement(id);
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
        [Route("api/postavke/nacinplacanja/obrisi")]
        [Authorize]
        public HttpResponseMessage ObrisiNacinPlacanja(HttpRequestMessage request, [FromBody] int id)
        {
            HttpResponseMessage response = null;

            try
            {
                using (SystemClient proxy = new SystemClient())
                {
                    proxy.Open();
                    proxy.ObrisiNacinPlacanja(id);
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
