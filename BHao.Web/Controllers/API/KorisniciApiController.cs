using BHao.Business.Entities;
using BHao.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace BHao.Web.Controllers.API
{
    public class KorisniciApiController : BaseApiController
    {
        [HttpGet]
        [Route("api/administracija/korisnici")]
        [Authorize]
        public async Task<HttpResponseMessage> GetKorisnici(HttpRequestMessage request, string filter, string ulogeFilter)
        {
            HttpResponseMessage response = null;

            try
            {
                List<Korisnik> korisnici = this.AppUserManager.Users.ToList();
                List<KorisnikModel> kmList = new List<KorisnikModel>();

                foreach (var korisnik in korisnici)
                {
                    KorisnikModel km = new KorisnikModel()
                    {
                        Id = korisnik.Id,
                        Email = korisnik.Email,
                        GradId = korisnik.GradId,
                        Ime = korisnik.Ime,
                        IsOnemogucen = korisnik.IsDeleted,
                        PostanskiBroj = korisnik.PostanskiBroj,
                        Prezime = korisnik.Prezime,
                        Telefon = korisnik.Telefon,
                        Ulica = korisnik.Ulica,
                        Broj = korisnik.Broj                        
                    };

                    var roles = await this.AppUserManager.GetRolesAsync(korisnik.Id);
                    km.Uloge = roles.ToArray();
                    kmList.Add(km);
                }

                if (!string.IsNullOrEmpty(filter))
                {
                    kmList = kmList.Where(x => x.Ime.ToLower().Contains(filter) ||
                        x.Prezime.ToLower().Contains(filter) ||
                        x.Email.ToLower().Contains(filter)).ToList();
                }

                if (ulogeFilter == "uposlenici")
                {
                    kmList = kmList.Where(x => x.Uloge.Contains("Admin") || x.Uloge.Contains("Uposlenik")).ToList();
                }

                if (ulogeFilter == "klijenti")
                {
                    kmList = kmList.Where(x => x.Uloge.Contains("Klijent")).ToList();
                }

                response = request.CreateResponse<List<KorisnikModel>>(HttpStatusCode.OK, kmList);
            }
            catch (Exception ex)
            {
                response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return response;
        }

        [HttpGet]
        [Route("api/administracija/uloge")]
        public HttpResponseMessage GetUloge(HttpRequestMessage request)
        {
            HttpResponseMessage response = null;

            try
            {
                var uloge = this.AppRoleManager.Roles.ToList();
                List<string> ulogeLista = new List<string>();
                foreach (var uloga in uloge)
                {
                    ulogeLista.Add(uloga.Name);
                }
                response = request.CreateResponse(HttpStatusCode.OK, ulogeLista);
            }
            catch (Exception ex)
            {

                response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            return response;
        }

        [HttpPost]
        [Route("api/administracija/korisnici/kreiraj")]
        [Authorize]
        public async Task<HttpResponseMessage> KorisnikKreiraj(HttpRequestMessage request, [FromBody] KorisnikModel model)
        {
            HttpResponseMessage response = null;

            try
            {
                var user = new Korisnik
                {
                    UserName = model.Email,
                    Email = model.Email,
                    GradId = model.GradId,
                    Ime = model.Ime,
                    Prezime = model.Prezime,
                    Ulica = model.Ulica,
                    Broj = model.Broj,
                    Telefon = model.Telefon,
                    PostanskiBroj = model.PostanskiBroj,
                    IsDeleted = model.IsOnemogucen
                };
                var result = await AppUserManager.CreateAsync(user, model.Lozinka);
                if (result.Succeeded)
                {
                    if ((model.Uloge.Contains("Admin") ||model.Uloge.Contains("Uposlenik")) && model.Uloge.Contains("Klijent"))
                    {
                        model.Uloge = model.Uloge.Where(val => val != "Klijent").ToArray();
                    }
                    foreach (var uloga in model.Uloge)
                    {
                        await AppUserManager.AddToRoleAsync(user.Id, uloga);
                    }
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, result.Errors);
                }
            }
            catch (Exception ex)
            {

                response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            return response;
        }


        [HttpPost]
        [Route("api/administracija/korisnici/azuriraj")]
        public async Task<HttpResponseMessage> KorisnikUpdate(HttpRequestMessage request, [FromBody] KorisnikModel model)
        {
            HttpResponseMessage response = null;
            try
            {
                var user = await AppUserManager.FindByIdAsync(model.Id);

                user.Id = model.Id;
                user.Email = model.Email;
                user.GradId = model.GradId;
                user.Ime = model.Ime;
                user.Prezime = model.Prezime;
                user.Ulica = model.Ulica;
                user.Broj = model.Broj;
                user.Telefon = model.Telefon;
                user.PostanskiBroj = model.PostanskiBroj;
                user.IsDeleted = model.IsOnemogucen;


                var result = await AppUserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    var prethodneUloge = await AppUserManager.GetRolesAsync(user.Id);

                    foreach (var uloga in prethodneUloge)
                    {
                        await AppUserManager.RemoveFromRoleAsync(user.Id, uloga);
                    }

                    if ((model.Uloge.Contains("Admin") || model.Uloge.Contains("Uposlenik")) && model.Uloge.Contains("Klijent"))
                    {
                        model.Uloge = model.Uloge.Where(val => val != "Klijent").ToArray();
                    }

                    foreach (var uloga in model.Uloge)
                    {
                        await AppUserManager.AddToRoleAsync(user.Id, uloga);
                    }
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, result.Errors);
                }
            }
            catch (Exception ex)
            {

                response = request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            return response;
        }
    }
}
