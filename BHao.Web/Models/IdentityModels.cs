using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using BHao.Business.Entities;
using BHao.Data;

namespace BHao.Web.Models
{
    public class KorisnikStore : UserStore<Korisnik, Uloga, int, Login, KorisnikUloga, KorisnikClaim>
    {
        public KorisnikStore( BHaoDataContext context )
            : base( context )
        {

        }
    }

    public class KorisnikUlogaStore : RoleStore<Uloga, int, KorisnikUloga>
    {
        public KorisnikUlogaStore( BHaoDataContext context )
            : base( context )
        {

        }
    }






    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    //public class ApplicationUser : IdentityUser
    //{
    //    public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
    //    {
    //        // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
    //        var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
    //        // Add custom user claims here
    //        return userIdentity;
    //    }
    //}

    //public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    //{
    //    public ApplicationDbContext()
    //        : base("DefaultConnection", throwIfV1Schema: false)
    //    {
    //    }

    //    public static ApplicationDbContext Create()
    //    {
    //        return new ApplicationDbContext();
    //    }
    //}
}