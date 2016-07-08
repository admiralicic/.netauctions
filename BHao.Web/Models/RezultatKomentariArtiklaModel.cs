using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BHao.Client.Entities.DTOs;

namespace BHao.Web.Models
{
    public class RezultatKomentariArtiklaModel
    {
        public KomentarArtiklaDTO[] KomentariArtikla { get; set; }
        public int BrojKomentara { get; set; }
    }
}