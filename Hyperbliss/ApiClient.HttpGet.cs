using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hyperbliss
{
    public partial class ApiClient
    {
        public virtual T Get<T>(string resource)
        {
            return Json.FromString<T>(
                    Send(resource, GET)
                );
        }
        public virtual async Task<T> GetAsync<T>(string resource)
        {
            return Json.FromString<T>(
                    await SendAsync(resource, GET)
                );
        }

        public virtual object Get(string resource)
        {
            return Json.FromString<object>(
                    Send(resource, GET)
                );
        }
        public virtual async Task<object> GetAsync(string resource)
        {
            return Json.FromString<object>(
                    await SendAsync(resource, GET)
                );
        }

        public virtual TReturn Get<TReturn>(
            string resource,
            Func<JsonMapping, TReturn> mapping)
        {
            if (mapping == null)
                throw new ArgumentNullException("mapping");

            object json = Get(resource);
            return ResolveMapping<TReturn>((JContainer)json, mapping);
        }
        public virtual async Task<TReturn> GetAsync<TReturn>(
            string resource,
            Func<JsonMapping, TReturn> mapping)
        {
            if (mapping == null)
                throw new ArgumentNullException("mapping");

            object json = await GetAsync(resource);

            return ResolveMapping<TReturn>((JContainer)json, mapping);
        }

        public virtual TReturn[] GetArray<TReturn>(
            string resource,
            Func<JsonMapping, TReturn> mapping)
        {
            if (resource == null)
                throw new ArgumentNullException("resource");
            if (mapping == null)
                throw new ArgumentNullException("mapping");

            object json = Get(resource);
            JContainer container = (JContainer)json;

            return ResolveMappingAsArray<TReturn>(container, mapping);
        }
        public virtual async Task<TReturn[]> GetArrayAsync<TReturn>(
            string resource,
            Func<JsonMapping, TReturn> mapping)
        {
            if (resource == null)
                throw new ArgumentNullException("resource");
            if (mapping == null)
                throw new ArgumentNullException("mapping");

            object json = await GetAsync(resource);
            JContainer container = (JContainer)json;

            return ResolveMappingAsArray<TReturn>(container, mapping);
        }
    }
}
