using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BHao.Web.Controllers.MVC
{
    public class PorukeController : Controller
    {
        // GET: Poruke
        [HttpGet]
        [Route("poruke")]
        public ActionResult Index()
        {
            return View();
        }
    }
}