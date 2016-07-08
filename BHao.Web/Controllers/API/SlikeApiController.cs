using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.IO;
using System.Web;
using BHao.Web.Models;
using System.Drawing;
using ImageResizer;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;

namespace BHao.Web.Controllers.API
{
    public class SlikeApiController : ApiController
    {

        /* Korišten kod učitavanje file streama u memoriju sa: 
        // http://stackoverflow.com/questions/17072767/web-api-how-to-access-multipart-form-values-when-using-multipartmemorystreampro/17073113#17073113 
        */


        [HttpPost]
        [Route("api/slike/upload")]
        public async Task<HttpResponseMessage> UploadSlike3()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable));
            }

            string uploadPath = HttpContext.Current.Server.MapPath("~/uploads");

            Dictionary<string, string> versions = new Dictionary<string, string>();
            versions.Add("_thumb", "width=100&height=100&crop=auto&format=jpg"); //Crop to square thumbnail
            versions.Add("_medium", "maxwidth=400&maxheight=400&format=jpg"); //Fit inside 400x400 area, jpeg
            //versions.Add( "_large", "maxwidth=1900&maxheight=1900&format=jpg" ); //Fit inside 1900x1200 area

            SlikaUploadResponseModel slika = new SlikaUploadResponseModel();

            try
            {

                var provider = await Request.Content.ReadAsMultipartAsync<InMemoryMultipartFormDataStreamProvider>(new InMemoryMultipartFormDataStreamProvider());

                NameValueCollection formData = provider.FormData;
                IList<HttpContent> files = provider.Files;

                HttpContent file = files[0];
                Stream fileStream = await file.ReadAsStreamAsync();

                Image image = Image.FromStream(fileStream);

                string slikaGuid = Guid.NewGuid().ToString();

                String slikaIme = Path.Combine(uploadPath, slikaGuid + "_medium");
                String slikaThumbIme = Path.Combine(uploadPath, slikaGuid + "_thumb");

                ImageBuilder.Current.Build(new ImageJob(image, slikaIme, new Instructions(versions["_medium"]), false, true));
                ImageBuilder.Current.Build(new ImageJob(image, slikaThumbIme, new Instructions(versions["_thumb"]), false, true));

                slika.SlikaIme = slikaGuid + "_medium" + ".jpg";
                slika.SlikaThumbIme = slikaGuid + "_thumb" + ".jpg";


                return Request.CreateResponse(HttpStatusCode.Created, slika);

            }
            catch (Exception)
            {

                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

    }

    /* Korišten kod učitavanje file streama u memoriju sa: 
    // http://stackoverflow.com/questions/17072767/web-api-how-to-access-multipart-form-values-when-using-multipartmemorystreampro/17073113#17073113 
    */

    public class InMemoryMultipartFormDataStreamProvider : MultipartStreamProvider
    {
        private NameValueCollection _formData = new NameValueCollection();
        private List<HttpContent> _fileContents = new List<HttpContent>();

        // Set of indexes of which HttpContents we designate as form data
        private Collection<bool> _isFormData = new Collection<bool>();

        /// <summary>
        /// Gets a <see cref="NameValueCollection"/> of form data passed as part of the multipart form data.
        /// </summary>
        public NameValueCollection FormData
        {
            get { return _formData; }
        }

        /// <summary>
        /// Gets list of <see cref="HttpContent"/>s which contain uploaded files as in-memory representation.
        /// </summary>
        public List<HttpContent> Files
        {
            get { return _fileContents; }
        }

        public override Stream GetStream(HttpContent parent, HttpContentHeaders headers)
        {
            // For form data, Content-Disposition header is a requirement

            var fileName = headers.ContentDisposition.FileName;

            ContentDispositionHeaderValue contentDisposition = headers.ContentDisposition;
            if (contentDisposition != null)
            {
                // We will post process this as form data
                _isFormData.Add(String.IsNullOrEmpty(contentDisposition.FileName));

                var ext = Path.GetExtension(contentDisposition.FileName.Replace("\"", string.Empty));

                if (ext.Equals(".jpeg", StringComparison.InvariantCultureIgnoreCase) || ext.Equals(".jpg", StringComparison.InvariantCultureIgnoreCase))
                {
                    return new MemoryStream();
                }
                else
                {
                    return null;
                }

            }

            // If no Content-Disposition header was present.
            throw new InvalidOperationException(string.Format("Did not find required '{0}' header field in MIME multipart body part..", "Content-Disposition"));
        }

        /// <summary>
        /// Read the non-file contents as form data.
        /// </summary>
        /// <returns></returns>
        public override async Task ExecutePostProcessingAsync()
        {
            // Find instances of non-file HttpContents and read them asynchronously
            // to get the string content and then add that as form data
            for (int index = 0; index < Contents.Count; index++)
            {
                if (_isFormData[index])
                {
                    HttpContent formContent = Contents[index];
                    // Extract name from Content-Disposition header. We know from earlier that the header is present.
                    ContentDispositionHeaderValue contentDisposition = formContent.Headers.ContentDisposition;
                    string formFieldName = UnquoteToken(contentDisposition.Name) ?? String.Empty;

                    // Read the contents as string data and add to form data
                    string formFieldValue = await formContent.ReadAsStringAsync();
                    FormData.Add(formFieldName, formFieldValue);
                }
                else
                {
                    _fileContents.Add(Contents[index]);
                }
            }
        }

        /// <summary>
        /// Remove bounding quotes on a token if present
        /// </summary>
        /// <param name="token">Token to unquote.</param>
        /// <returns>Unquoted token.</returns>
        private static string UnquoteToken(string token)
        {
            if (String.IsNullOrWhiteSpace(token))
            {
                return token;
            }

            if (token.StartsWith("\"", StringComparison.Ordinal) && token.EndsWith("\"", StringComparison.Ordinal) && token.Length > 1)
            {
                return token.Substring(1, token.Length - 2);
            }

            return token;
        }
    }
       
}
