using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BHao.Web.Models
{
    public class ReportKriterijModel
    {
        public int KategorijaId { get; set; }
        public DateTime? PrikazOd { get; set; }
        public DateTime? PrikazDo { get; set; }
    }
}