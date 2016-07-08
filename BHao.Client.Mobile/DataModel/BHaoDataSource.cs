using BHao.Client.Mobile.Common;
using BHao.Client.Mobile.DTO;
using BHao.Client.Mobile.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Web.Http;
using Windows.Web.Http.Headers;
using System.Linq;
using BHao.Client.Mobile.Exceptions;

namespace BHao.Client.Mobile.DataModel
{
    public class BHaoDataSource
    {
        public BHaoDataSource()
        {

        }

        public async Task<List<AukcijaDetailDTO>> GetAktivneAukcije(string pretraga = "")
        {
            KriterijPretrageModel kp = new KriterijPretrageModel { StringZaPretragu = pretraga };
            Uri uri = new Uri(ViewModelHelper.siteUrl + "/api/aukcija/allaktivne");

            var content = JsonConvert.SerializeObject(kp);

            try
            {
                using (HttpClient httpClient = ViewModelHelper.GetHttpClient(""))
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, uri);
                    request.Content = new HttpStringContent(content);
                    request.Content.Headers.ContentType = new HttpMediaTypeHeaderValue("application/json");
                    HttpResponseMessage response = await httpClient.SendRequestAsync(request).AsTask();

                    if (response.StatusCode == HttpStatusCode.Ok)
                    {
                        List<AukcijaDetailDTO> aukcije = JsonConvert.DeserializeObject<List<AukcijaDetailDTO>>(response.Content.ToString());

                        return aukcije;
                    }

                    return null;
                }
            }
            catch (Exception)
            {

                throw new Exception("Greška prilikom konekcije na server.");
            }

        }

        public async Task<AukcijaDTO> GetAukcija(int aukcijaId)
        {

            var json = JsonConvert.SerializeObject(aukcijaId);

            HttpStringContent content = new HttpStringContent(json);
            content.Headers.ContentType = new HttpMediaTypeHeaderValue("application/json");
            Uri uri = new Uri(ViewModelHelper.siteUrl + "/api/aukcija/detalji");

            try
            {
                using (HttpClient httpClient = ViewModelHelper.GetHttpClient(ViewModelHelper.accessTokenString))
                {
                    HttpResponseMessage response = await httpClient.PostAsync(uri, content);

                    if (response.StatusCode == HttpStatusCode.Ok)
                    {
                        AukcijaDTO aukcija = JsonConvert.DeserializeObject<AukcijaDTO>(response.Content.ToString());
                        return aukcija;
                    }

                    return null;
                }
            }
            catch (Exception)
            {

                throw new Exception("Greška prilikom konekcije na server.");
            }

        }

        public async Task<List<AukcijaDetailDTO>> GetAukcijeKorisnika(string p)
        {
            MojeAukcijeKriterij kriterij = new MojeAukcijeKriterij
            {
                KorisnikId = ViewModelHelper.prijavljeniKorisnikId,
                Kriterij = p
            };

            var json = JsonConvert.SerializeObject(kriterij);
            HttpStringContent content = new HttpStringContent(json);
            content.Headers.ContentType = new HttpMediaTypeHeaderValue("application/json");
            Uri uri = new Uri(ViewModelHelper.siteUrl + "/api/aukcija/getzakorisnika");

            try
            {
                using (HttpClient httpClient = ViewModelHelper.GetHttpClient(ViewModelHelper.accessTokenString))
                {
                    HttpResponseMessage response = await httpClient.PostAsync(uri, content);

                    if (response.StatusCode == HttpStatusCode.Ok)
                    {
                        try
                        {
                            List<AukcijaDetailDTO> aukcije = JsonConvert.DeserializeObject<List<AukcijaDetailDTO>>(response.Content.ToString());
                            aukcije = aukcije.OrderByDescending(x => x.Aukcija.Zavrsetak).ToList();
                            if (aukcije.Count > 0)
                            {
                                return aukcije;
                            }
                            else
                            {
                                return null;
                            }
                        }
                        catch (Exception)
                        {

                            throw new AccessDeniedException();
                        }

                    }

                    return null;
                }
            }
            catch(AccessDeniedException)
            {
                throw new AccessDeniedException("Korisnik nije prijavljen");
            }
            catch (Exception)
            {

                throw new Exception("Greška prilikom konekcije na server.");
            }

        }

        public async Task<bool> KreirajPonudu(decimal iznosPonude, int aukcijaId)
        {
            try
            {
                var ponuda = new PonudaModel 
                {
                    AukcijaId = aukcijaId,
                    IznosPonude = iznosPonude
                };

                var json = JsonConvert.SerializeObject(ponuda);
                HttpStringContent content = new HttpStringContent(json);
                content.Headers.ContentType = new HttpMediaTypeHeaderValue("application/json");
                Uri uri = new Uri(ViewModelHelper.siteUrl + "/api/aukcija/ponudi");

                using (HttpClient httpClient = ViewModelHelper.GetHttpClient(ViewModelHelper.accessTokenString))
                {
                    HttpResponseMessage response = await httpClient.PostAsync(uri, content);

                    if (response.StatusCode == HttpStatusCode.Ok)
                    {
                        return true;
                    }
                    else
                    {
                        throw new PonudaException(response.Content.ToString());
                    }
                }
            }
            catch (PonudaException ex)
            {
                throw new PonudaException(ex.Message);
            }
            catch (AccessDeniedException)
            {
                throw new AccessDeniedException("Korisnik nije prijavljen");
            }
            catch (Exception)
            {

                throw new Exception("Greška prilikom konekcije na server.");
            }
        }

        public async Task<bool> KupiOdmah(int aukcijaId)
        {
            try
            {
                var json = JsonConvert.SerializeObject(aukcijaId);
                HttpStringContent content = new HttpStringContent(json);
                content.Headers.ContentType = new HttpMediaTypeHeaderValue("application/json");
                Uri uri = new Uri(ViewModelHelper.siteUrl + "/api/aukcija/kupiodmah");

                using (HttpClient httpClient = ViewModelHelper.GetHttpClient(ViewModelHelper.accessTokenString))
                {
                    HttpResponseMessage response = await httpClient.PostAsync(uri, content);

                    if (response.StatusCode == HttpStatusCode.Ok)
                    {
                        return true;
                    }
                    else
                    {
                        throw new PonudaException(response.Content.ToString());
                    }
                }
            }
            catch (PonudaException ex)
            {
                throw new PonudaException(ex.Message);
            }
            catch (AccessDeniedException)
            {
                throw new AccessDeniedException("Korisnik nije prijavljen");
            }
            catch (Exception)
            {

                throw new Exception("Greška prilikom konekcije na server.");
            }
        }
    }
}
