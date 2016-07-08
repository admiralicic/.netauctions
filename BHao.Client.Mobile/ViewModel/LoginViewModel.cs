using BHao.Client.Mobile.Common;
using BHao.Client.Mobile.DataModel;
using BHao.Client.Mobile.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Web.Http;
using Windows.Web.Http.Filters;
using Windows.Web.Http.Headers;


namespace BHao.Client.Mobile.ViewModel
{
    public class LoginViewModel
    {
        public LoginViewModel()
        {

        }

        public async Task<bool> Prijava(string korisnickoIme, string lozinka)
        {
            
            Token token = await ViewModelHelper.GetToken(korisnickoIme, lozinka);

            if (token != null)
            {
                ViewModelHelper.accessTokenString = token.AccessToken;
                ViewModelHelper.isPrijavljen = true;
                return true;
            }
            else
                ViewModelHelper.isPrijavljen = false;
                return false;

        }

       

   

    }
}
