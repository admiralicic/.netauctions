using BHao.Client.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BHao.Web.Models
{
    public class RezultatModel
    {
        public AukcijaDetailDTO[] aukcije { get; set; }
        public int brojAukcija { get; set; }    
    }
}