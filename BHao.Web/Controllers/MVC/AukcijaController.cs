using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BHao.Web.Controllers.MVC
{
    public class AukcijaController : Controller
    {
        // GET: Aukcija
        [Route("~/")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("aukcija/kreiraj")]
        [Authorize]
        public ActionResult Kreiraj( )
        {
            
            return View( );
        }

        [HttpGet]
        [Route("aukcija/{id}/detalji")]
        public ActionResult Detalji()
        {
            return View();
        }

        [HttpGet]
        [Route("aukcija/mojeAukcije")]
        [Authorize]
        public ActionResult MojeAukcije()
        {
            return View();
        }
    }
}