using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hyperbliss
{
    public partial class ApiClient
    {
        public virtual void Patch(string resource, object objectToSubmit)
        {
            Send(resource, PATCH, objectToSubmit);
        }
        public virtual async Task PatchAsync(string resource, object objectToSubmit)
        {
            await SendAsync(resource, PATCH, objectToSubmit);
        }

        public virtual TReturn PatchAndGetResult<TReturn>(string resource, object objectToSubmit, Func<JsonMapping, TReturn> mapping)
        {
            string json = Send(resource, PATCH, objectToSubmit);
            return ResolveMapping(json, mapping);
        }
        public virtual async Task<TReturn> PatchAndGetResultAsync<TReturn>(string resource, object objectToSubmit, Func<JsonMapping, TReturn> mapping)
        {
            string json = await SendAsync(resource, PATCH, objectToSubmit);
            return ResolveMapping<TReturn>(json, mapping);
        }

        public virtual TReturn[] PatchAndGetResultAsArray<TReturn>(string resource, object objectToSubmit, Func<JsonMapping, TReturn> mapping)
        {
            string json = Send(resource, PATCH, objectToSubmit);
            return ResolveMappingAsArray<TReturn>(json, mapping);
        }
        public virtual async Task<TReturn[]> PatchAndGetResultAsArrayAsync<TReturn>(string resource, object objectToSubmit, Func<JsonMapping, TReturn> mapping)
        {
            string json = await SendAsync(resource, PATCH, objectToSubmit);
            return ResolveMappingAsArray<TReturn>(json, mapping);
        }
    }
}
