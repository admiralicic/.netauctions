using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BHao.Web.Models
{
    public class MojeAukcijeKriterij
    {
        public int KorisnikId { get; set; }
        public string Kriterij { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}