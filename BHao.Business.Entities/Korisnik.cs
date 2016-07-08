using BHao.Common.Core;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BHao.Business.Entities
{
    [DataContract]
    public class Korisnik : IdentityUser<int, Login, KorisnikUloga, KorisnikClaim> , IExtensibleDataObject
    {
        //[DataMember]
        //public int Id { get; set; }

        [DataMember]
        public string Ime { get; set; }

        [DataMember]
        public string Prezime { get; set; }

        [DataMember]
        public string Ulica { get; set; }

        [DataMember]
        public string Broj { get; set; }

        [DataMember]
        public string PostanskiBroj { get; set; }

        [DataMember]
        public string Telefon { get; set; }
               
        [DataMember]
        public int GradId { get; set; }

        [DataMember]
        public Grad Grad { get; set; }

        public bool IsDeleted { get; set; }
       
                       
        [DataMember]
        public ExtensionDataObject ExtensionData { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Korisnik, int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        
    }
}
