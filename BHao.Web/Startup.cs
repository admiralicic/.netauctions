using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System.Security.Claims;
using System.Web.Http;

[assembly: OwinStartupAttribute(typeof(BHao.Web.Startup))]
namespace BHao.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            //HttpConfiguration config = new HttpConfiguration();

            //WebApiConfig.Register(config);
            //app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            //app.UseWebApi(config);

            
        }
    }
}
