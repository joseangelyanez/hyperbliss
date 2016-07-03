using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hyperbliss
{
    public partial class ApiClient
    {
        public virtual void Delete(string resource)
        {
            Send(resource, DELETE, new { });
        }
        public virtual async Task DeleteAsync(string resource)
        {
            await SendAsync(resource, DELETE, new { });
        }

        public virtual void Delete(string resource, object objectToSubmit)
        {
            Send(resource, DELETE, objectToSubmit);
        }
        public virtual async Task DeleteAsync(string resource, object objectToSubmit)
        {
            await SendAsync(resource, DELETE, objectToSubmit);
        }

        public virtual TReturn DeleteAndGetResult<TReturn>(string resource, object objectToSubmit, Func<JsonMapping, TReturn> mapping)
        {
            string json = Send(resource, DELETE, objectToSubmit);
            return ResolveMapping(json, mapping);
        }
        public virtual async Task<TReturn> DeleteAndGetResultAsync<TReturn>(string resource, object objectToSubmit, Func<JsonMapping, TReturn> mapping)
        {
            string json = await SendAsync(resource, DELETE, objectToSubmit);
            return ResolveMapping<TReturn>(json, mapping);
        }

        public virtual TReturn[] DeleteAndGetResultAsArray<TReturn>(string resource, object objectToSubmit, Func<JsonMapping, TReturn> mapping)
        {
            string json = Send(resource, DELETE, objectToSubmit);
            return ResolveMappingAsArray(json, mapping);
        }
        public virtual async Task<TReturn[]> DeleteAndGetResultAsArrayAsync<TReturn>(string resource, object objectToSubmit, Func<JsonMapping, TReturn> mapping)
        {
            string json = await SendAsync(resource, DELETE, objectToSubmit);
            return ResolveMappingAsArray<TReturn>(json, mapping);
        }
    }
}
