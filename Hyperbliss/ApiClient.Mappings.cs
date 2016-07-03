﻿using Newtonsoft.Json.Linq;
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
                JContainer jsonContainer = Json as JContainer;
                if (jsonContainer == null)
                    throw new ApplicationException("Invalid mapping state.");

                return ApiClient.ResolveMappingAsArray(jsonContainer, mapping);
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