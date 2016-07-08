using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BHao.Web.Models
{
    public class KriterijPretrageModel
    {
        public int KategorijaId { get; set; }
        public string StringZaPretragu { get; set; }
        public bool Zavrsena { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}