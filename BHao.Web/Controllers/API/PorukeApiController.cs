using BHao.Client.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Web;
using System.Web.Http;
using BHao.Client.Service;
using BHao.Client.Service.Proxies;
using Microsoft.AspNet.Identity;
using BHao.Client.Entities.DTOs;

namespace BHao.Web.Controllers.API
{
    public class PorukeApiController : ApiController
    {
        [HttpPost]
        [Route("api/poruka/posalji")]
        [Authorize]
        public HttpResponseMessage PosaljiPoruku(HttpRequestMessage request, [FromBody] Poruka poruka)
        {
            HttpResponseMessage response = null;

            try
            {
                if (poruka.PosiljalacId != User.Identity.GetUserId<int>())
                {
                    response = request.CreateResponse(HttpStatusCode.Unauthorized);
                    return response;
                }

                using (PorukeClient proxy = new PorukeClient())
                {
                    proxy.Open();
                    proxy.PosaljiPoruku(poruka);
                    proxy.Close();

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (FaultException ex)
            {
                response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            return response;
        }

        //[HttpPost]
        //[Route("api/poruke/poslane")]
        //[Authorize]
        //public HttpResponseMessage GetPoslane(HttpRequestMessage request, [FromBody] int korisnikId)
        //{
        //    HttpResponseMessage response = null;

        //    try
        //    {
        //        if (korisnikId != User.Identity.GetUserId<int>())
        //        {
        //            response = request.CreateResponse(HttpStatusCode.Unauthorized);
        //            return response;
        //        }

        //        using (PorukeClient proxy = new PorukeClient())
        //        {
        //            proxy.Open();
        //            PorukaDTO[] poruke = proxy.GetPoslane(korisnikId);
        //            proxy.Close();

        //            response = request.CreateResponse<PorukaDTO[]>(HttpStatusCode.OK, poruke);
        //        }
        //    }
        //    catch (FaultException ex)
        //    {

        //        response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
        //    }

        //    return response;
        //}

        [HttpPost]
        [Route("api/poruke")]
        [Authorize]
        public HttpResponseMessage GetPoruke(HttpRequestMessage request, [FromBody] int korisnikId)
        {
            HttpResponseMessage response = null;

            try
            {
                if (korisnikId != User.Identity.GetUserId<int>())
                {
                    response = request.CreateResponse(HttpStatusCode.Unauthorized);
                    return response;
                }

                using (PorukeClient proxy = new PorukeClient())
                {
                    proxy.Open();
                    PorukaDTO[] poruke = proxy.GetPoruke(korisnikId);
                    proxy.Close();

                    response = request.CreateResponse<PorukaDTO[]>(HttpStatusCode.OK, poruke);
                }
            }
            catch (FaultException ex)
            {

                response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            return response;
        }

        [HttpPost]
        [Route("api/poruke/oznaciProcitana")]
        [Authorize]
        public HttpResponseMessage OznaciProcitana (HttpRequestMessage request, [FromBody] int porukaId)
        {
            HttpResponseMessage response = null;

            try
            {
                using (PorukeClient proxy = new PorukeClient())
                {
                    proxy.Open();
                    proxy.OznaciProcitana(porukaId);
                    proxy.Close();

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (FaultException ex)
            {
                response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            return response;
        }

        [HttpPost]
        [Route("api/poruke/obrisi")]
        [Authorize]
        public HttpResponseMessage ObrisiPoruku (HttpRequestMessage request, [FromBody] PorukaDTO poruka)
        {
            HttpResponseMessage response = null;

            try
            {
                if (poruka.PrimalacId != User.Identity.GetUserId<int>() && poruka.PosiljalacId != User.Identity.GetUserId<int>())
                {
                    response = request.CreateResponse(HttpStatusCode.Unauthorized);

                    return response;
                }

                using (PorukeClient proxy = new PorukeClient())
                {
                    proxy.Open();
                    proxy.ObrisiPoruku(poruka, User.Identity.GetUserId<int>());
                    proxy.Close();

                    response = request.CreateResponse(HttpStatusCode.OK);
                }

            }
            catch (FaultException ex)
            {
                response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);               
            }


            return response;
        }
    }
}