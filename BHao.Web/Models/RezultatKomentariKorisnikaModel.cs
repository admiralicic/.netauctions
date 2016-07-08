using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BHao.Client.Entities.DTOs;

namespace BHao.Web.Models
{
    public class RezultatKomentariKorisnikaModel
    {
        public KomentarDTO[] KomentariKorisnika { get; set; }
        public int BrojKomentara { get; set; }
    }
}