using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hyperbliss
{
    public partial class ApiClient
    {
        public virtual void Put(string resource, object objectToSubmit)
        {
            Send(resource, PUT, objectToSubmit);
        }
        public virtual async Task PutAsync(string resource, object objectToSubmit)
        {
            await SendAsync(resource, PUT, objectToSubmit);
        }

        public virtual TReturn PutAndGetResult<TReturn>(string resource, object objectToSubmit, Func<JsonMapping, TReturn> mapping)
        {
            string json = Send(resource, PUT, objectToSubmit);
            return ResolveMapping(json, mapping);
        }
        public virtual async Task<TReturn> PutAndGetResultAsync<TReturn>(string resource, object objectToSubmit, Func<JsonMapping, TReturn> mapping)
        {
            string json = await SendAsync(resource, PUT, objectToSubmit);
            return ResolveMapping<TReturn>(json, mapping);
        }

        public virtual TReturn[] PutAndGetResultAsArray<TReturn>(string resource, object objectToSubmit, Func<JsonMapping, TReturn> mapping)
        {
            string json = Send(resource, PUT, objectToSubmit);
            return ResolveMappingAsArray<TReturn>(json, mapping);
        }
        public virtual async Task<TReturn[]> PutAndGetResultAsArrayAsync<TReturn>(string resource, object objectToSubmit, Func<JsonMapping, TReturn> mapping)
        {
            string json = await SendAsync(resource, PUT, objectToSubmit);
            return ResolveMappingAsArray<TReturn>(json, mapping);
        }
    }
}
