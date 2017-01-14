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
		/// <summary>
        /// Represents an object that can read json and map to an anonymous object.
        /// </summary>
        public sealed class JsonMapping
        {
            public object Json { get; set; }

            internal JsonMapping(object json)
            {
                Json = json;
            }

            public JsonMapping MapAsObject(string property)
            {
                if (property == null)
                    throw new ArgumentNullException(nameof(property));

                /* Gets the object container. */
                JContainer container = (JContainer)Json;

                /* Gets the property value. */
                var propertyValue = container[property];
                if (propertyValue == null)
                    throw new ApplicationException($"Child property '{property}' could not be mapped because it could not be found in the response.");
                
                return new JsonMapping(propertyValue);
            }

            public string Map(string property)
            {
                JContainer container = (JContainer)Json;
                if (container[property] == null)
                    throw new ApplicationException($"Property '{property}' could not be mapped because it could not be found in the response.");

                return container[property].ToString();
            }

            public T Map<T>(string property)
            {
                JContainer container = (JContainer)Json;
                if (container[property] == null)
                    throw new ApplicationException($"Property '{property}' could not be mapped because it could not be found in the response.");

                return container[property].Value<T>();
            }

            public IEnumerable<T> MapAsArray<T>(string property, Func<JsonMapping, T> mapping)
            {
                if (property == null)
                    throw new ArgumentNullException(nameof(property));
                if (mapping == null)
                    throw new ArgumentNullException(nameof(mapping));

                JContainer jsonContainer = Json as JContainer;
                if (jsonContainer == null)
                    throw new ApplicationException("Invalid mapping state.");

                /* Gets the container with the specified property. */
                var childContainer = jsonContainer[property] as JContainer;

                /* Throws if container is not found.*/
                if (childContainer == null)
                    throw new ApplicationException($"Could not find property '{property}' or the property could not be read as an array. If the property is not an array consider using MapAsObject.");

                /* Resolves the array with the child container. */
                return ApiClient.ResolveMappingAsArray(childContainer, mapping);
            }
        }

        internal static TReturn ResolveMapping<TReturn>(string json, Func<JsonMapping, TReturn> mapping)
        {
            JContainer container = Json.FromString<JContainer>(json) as JContainer;
            if (container == null)
            {
                JsonMapping n = new JsonMapping(json);
                return mapping(n);
            }

            return ResolveMapping(container, mapping);
        }

        internal static TReturn ResolveMapping<TReturn>(JContainer container, Func<JsonMapping, TReturn> mapping)
        {
            if (container.Type.ToString() == "Array")
                throw new InvalidOperationException(
                    "The returned json is an array use the GetArray method instead");

            JsonMapping n = new JsonMapping(container);
            return mapping(n);
        }

        internal static TReturn[] ResolveMappingAsArray<TReturn>(string json, Func<JsonMapping, TReturn> mapping)
        {
            JContainer container = Json.FromString<JContainer>(json);
            return ResolveMappingAsArray<TReturn>(container, mapping);
        }

        internal static TReturn[] ResolveMappingAsArray<TReturn>(JContainer container, Func<JsonMapping, TReturn> mapping)
        {
            if (container.Type.ToString() != "Array")
                throw new InvalidOperationException(
                    "The returned json is not an array.");

            List<TReturn> arrayList = new List<TReturn>();

            for (int i = 0; i < container.Count; i++)
            {
                JsonMapping n = new JsonMapping(container[i]);
                var item = mapping(n);
                arrayList.Add(item);
            }

            return arrayList.ToArray();
        }
    }
}
