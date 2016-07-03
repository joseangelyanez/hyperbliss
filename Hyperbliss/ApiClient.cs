using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hyperbliss
{
    public partial class ApiClient
    {
        public ApiClient()
        {
            UrlConfiguration = GetUrlConfiguration();
        }
        
        public ApiClient(string baseUrl)
        {
            UrlConfiguration.BaseUrl = baseUrl;
        }
        
        private string Send(string resource, string method, object objectToSubmit = null)
        {
            if (resource == null)
                throw new ArgumentNullException("resource");
            if (method == null)
                throw new ArgumentNullException("method");

            string result = null;
            string postedJson = null;

            if (method != GET && objectToSubmit != null)
                postedJson = objectToSubmit.ToJson();
            
            result = CreateRequest(method, resource, postedJson);

            return result;
        }
        private async Task<string> SendAsync(string resource, string method, object objectToSubmit = null)
        {
            if (resource == null)
                throw new ArgumentNullException("resource");
            if (method == null)
                throw new ArgumentNullException("method");

            string result = null;
            string postedJson = null;

            if (method != GET && objectToSubmit != null)
                postedJson = objectToSubmit.ToJson();

            try
            {
                result = await CreateRequestAsync(method, resource, postedJson);
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }
    }
}
