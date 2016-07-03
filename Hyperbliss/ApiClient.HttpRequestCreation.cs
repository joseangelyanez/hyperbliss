using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Hyperbliss
{
    public partial class ApiClient
    {
        public event EventHandler<HttpRequestEventArgs> RequestCreated;

        protected virtual void OnRequestCreated(HttpRequestEventArgs e)
        {
            RequestCreated?.Invoke(this, e);
        }

        /// <summary>
        /// Creates a common HttpWebRequest for both async and sync operations.
        /// </summary>
        /// <param name="method"></param>
        /// <param name="location"></param>
        /// <param name="resource"></param>
        /// <param name="dataLength"></param>
        /// <returns></returns>
        private HttpWebRequest CreateHttpWebRequest(string method, string resource, int dataLength)
        {
            if (method == null)
                throw new ArgumentNullException("method");
            if (resource == null)
                throw new ArgumentNullException("resource");

            /* Trims the first "/" because UrlConfiguration.BaseUrl checks for a final "/". */
            if (resource.StartsWith("/"))
                resource = resource.Substring(1);
             
            WebRequest CreateWebRequest = WebRequest.Create(
                $"{UrlConfiguration.BaseUrl}{UrlConfiguration.ApplyValuesIntoResource(resource)}"
            );
            CreateWebRequest.Method = method;
            CreateWebRequest.ContentType = "application/json";

            HttpWebRequest httpCreateWebRequest = (HttpWebRequest)CreateWebRequest;

            if (dataLength > 0)
                httpCreateWebRequest.ContentLength = dataLength;

            return httpCreateWebRequest;
        }

        /// <summary>
        /// Creates an async raw Http request against the server using the Hyperbliss configuration system.
        /// </summary>
        /// <param name="method">The Http method.</param>
        /// <param name="location">The base Url for the resource.</param>
        /// <param name="resource">The resource to access.</param>
        /// <param name="data">The data to be sent as Json.</param>
        /// <returns>A string with the response.</returns>
        private async Task<string> CreateRequestAsync(string method, string resource, string data = null)
        {
            if (method == null)
                throw new ArgumentNullException("method");
            if (resource == null)
                throw new ArgumentNullException("resource");

            byte[] buffer = null;

            if (data != null)
                buffer = Encoding.UTF8.GetBytes(data);

            HttpWebRequest httpWebRequest = CreateHttpWebRequest(method, resource, buffer?.Length ?? 0);

            /* Signals the creation of the HttpWebRequest for additional configuration. */
            OnRequestCreated(new HttpRequestEventArgs() { Request = httpWebRequest });

            if (buffer != null)
            {
                Stream dataStream = await httpWebRequest.GetRequestStreamAsync();
                dataStream.Write(buffer, 0, buffer.Length);
                dataStream.Close();
            }

            try
            {
                using (HttpWebResponse response = (HttpWebResponse) await httpWebRequest.GetResponseAsync())
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    string jsonResponse = reader.ReadToEnd();
                    return jsonResponse;
                }
            }
            catch (Exception ex)
            {
                /* TODO: Handle better exceptions. */
                throw ex;
            }
        }

        /// <summary>
        /// Creates a raw Http request against the server using the Hyperbliss configuration system.
        /// </summary>
        /// <param name="method">The Http method.</param>
        /// <param name="location">The base Url for the resource.</param>
        /// <param name="resource">The resource to access.</param>
        /// <param name="data">The data to be sent as Json.</param>
        /// <returns>A string with the response.</returns>
        private string CreateRequest(string method, string resource, string data = null)
        {
            if (method == null)
                throw new ArgumentNullException("method");
            if (resource == null)
                throw new ArgumentNullException("resource");

            byte[] buffer = null;

            if (data != null)
                buffer = Encoding.UTF8.GetBytes(data);

            HttpWebRequest httpWebRequest = CreateHttpWebRequest(
                method, resource, buffer?.Length ?? 0);

            /* Signals the creation of the HttpWebRequest for additional configuration. */
            OnRequestCreated(new HttpRequestEventArgs() { Request = httpWebRequest });

            if (buffer != null)
            {
                Stream dataStream = httpWebRequest.GetRequestStream();
                dataStream.Write(buffer, 0, buffer.Length);
                dataStream.Close();
            }

            try
            {
                using (HttpWebResponse response = (HttpWebResponse) httpWebRequest.GetResponse())
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    string jsonResponse = reader.ReadToEnd();
                    return jsonResponse;
                }
            }
            catch (Exception ex)
            {
                /* TODO: Handle better exceptions. */
                throw ex;
            }
        }
    }
}
