using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hyperbliss
{
    public partial class ApiClient
    {
        public virtual void Post(string resource, object objectToSubmit)
        {
            Send(resource, POST, objectToSubmit);
        }
        public virtual async Task PostAsync(string resource, object objectToSubmit)
        {
            await SendAsync(resource, POST, objectToSubmit);
        }

        public virtual TReturn PostAndGetResult<TReturn>(string resource, object objectToSubmit, Func<JsonMapping, TReturn> mapping)
        {
            string json = Send(resource, POST, objectToSubmit);
            return ResolveMapping(json, mapping);
        }
        public virtual async Task<TReturn> PostAndGetResultAsync<TReturn>(string resource, object objectToSubmit, Func<JsonMapping, TReturn> mapping)
        {
            string json = await SendAsync(resource, POST, objectToSubmit);
            return ResolveMapping(json, mapping);
        }

        public virtual TReturn[] PostAndGetResultAsArray<TReturn>(string resource, object objectToSubmit, Func<JsonMapping, TReturn> mapping)
        {
            string json = Send(resource, POST, objectToSubmit);
            return ResolveMappingAsArray<TReturn>(json, mapping);
        }
        public virtual async Task<TReturn[]> PostAndGetResultAsArrayAsync<TReturn>(string resource, object objectToSubmit, Func<JsonMapping, TReturn> mapping)
        {
            string json = await SendAsync(resource, POST, objectToSubmit);
            return ResolveMappingAsArray<TReturn>(json, mapping);
        }
    }
}
