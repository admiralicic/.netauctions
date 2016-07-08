using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BHao.Web.Controllers.MVC
{
    public class AdministracijaController : BaseController
    {
        // GET: Administracija/Postavke
        [Authorize(Roles="Admin")]
        public ActionResult Postavke()
        {
            return View();
        }

        // GET: Administracija/PregledUplata
        [Authorize(Roles = "Uposlenik, Admin")]
        public ActionResult PregledUplata()
        {
            return View();
        }

        // GET: Administracija/Uloge
        [Authorize(Roles = "Uposlenik, Admin")]
        public ActionResult Korisnici()
        {
            return View();
        }

        [Authorize(Roles = "Uposlenik, Admin")]
        public ActionResult PregledKomentara()
        {
            return View();
        }

        [Authorize(Roles = "Uposlenik, Admin")]
        public ActionResult PregledAukcija()
        {
            return View();
        }

        [Authorize(Roles = "Uposlenik, Admin")]
        public ActionResult Statistika()
        {
            return View();
        }
    }
}