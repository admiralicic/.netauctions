using BHao.Client.Mobile.Exceptions;
using BHao.Client.Mobile.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;
using Windows.Security.Credentials;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.Web.Http;
using Windows.Web.Http.Filters;
using Windows.Web.Http.Headers;

namespace BHao.Client.Mobile.Common
{
    public static class ViewModelHelper
    {
        public const string siteUrl = "http://169.254.80.80:1898";
        public static ApplicationDataContainer settings = ApplicationData.Current.LocalSettings;
        public static string accessTokenString;
        public static int prijavljeniKorisnikId;
        public static bool isPrijavljen;

        public static async Task<Token> GetToken(string korisnickoIme, string lozinka)
        {
            var pairs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", korisnickoIme),
                new KeyValuePair<string, string>("password", lozinka)
            };

            HttpFormUrlEncodedContent content = new HttpFormUrlEncodedContent(pairs);

            using (var client = ViewModelHelper.GetHttpClient(""))
            {
                Uri connectionUri = new Uri(ViewModelHelper.siteUrl + "/api/token");

                try
                {
                    HttpResponseMessage response = await client.PostAsync(connectionUri, content);

                    if (response.StatusCode == HttpStatusCode.Ok)
                    {
                        var result = response.Content.ToString();

                        Token token = (Token)JsonConvert.DeserializeObject(result, typeof(Token));
                        accessTokenString = token.AccessToken;
                        prijavljeniKorisnikId = token.UserId;
                        isPrijavljen = true;
                        SacuvajToken(token);

                        return token;
                    }
                    else
                    {
                        throw new AccessDeniedException();
                        //return null;
                    }
                }
                catch (AccessDeniedException)
                {
                    throw new AccessDeniedException();
                }
                catch (Exception ex)
                {

                    throw new Exception("Greška prilikom konekcije na server.");
                }

            }
        }

        public static HttpClient GetHttpClient(string accessToken)
        {



            HttpBaseProtocolFilter filter = new HttpBaseProtocolFilter();
            filter.AutomaticDecompression = true;

            HttpClient client = new HttpClient(filter);

            if (!String.IsNullOrWhiteSpace(accessToken))
            {
                client.DefaultRequestHeaders.Authorization = new HttpCredentialsHeaderValue("Bearer", accessToken);
                client.DefaultRequestHeaders.Accept.Add(new HttpMediaTypeWithQualityHeaderValue("application/json"));
            }

            return client;


        }

        public static bool InternetKonekcija()
        {
            var connectionProfile = NetworkInformation.GetInternetConnectionProfile();

            return (connectionProfile != null && connectionProfile.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess);

        }

        public static void SacuvajToken(Token token)
        {
            settings.Values["Token"] = token.AccessToken;
        }

        public static string UcitajToken()
        {
            return settings.Values["Token"].ToString();
        }

        public static PasswordCredential GetCredentialFromLocker()
        {
            PasswordCredential credential = null;

            var vault = new PasswordVault();
            try
            {
                var credentialList = vault.FindAllByResource("bauk");
                if (credentialList.Count > 0)
                {
                    credential = credentialList[0];
                    return credential;
                }
            }
            catch (Exception)
            {

                return null;
            }


            return null;
        }


    }
}
