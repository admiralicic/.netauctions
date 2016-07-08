using BHao.Business.Entities;
using BHao.Data;
using BHao.Web.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace BHao.Web.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {


        public SimpleAuthorizationServerProvider()
        {

        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            var _userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

            Korisnik korisnik = await _userManager.FindAsync(context.UserName, context.Password);

            if (korisnik == null)
            {
                context.SetError("invalid_grant", "Korisničko ime ili lozinka nisu ispravni.");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim("role", "Klijent"));
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));

            var props = new AuthenticationProperties(new Dictionary<string, string>
            {
                { "user_id", korisnik.Id.ToString()}
            });

            var ticket = new AuthenticationTicket(identity, props);
            //context.Validated(identity);
            context.Validated(ticket);
        }
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }



    }
}