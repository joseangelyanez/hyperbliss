using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hyperbliss
{
    /// <summary>
    /// Static class that contains methods to handle Json data.
    /// </summary>
    public static class Json
    {
        /// <summary>
        /// Returns an object that matches a Json data representation.
        /// </summary>
        /// <typeparam name="T">The type of the object it should be mapped to.</typeparam>
        /// <param name="jsonString">The json data.</param>
        /// <returns>An object with the data specified in the jsonString.</returns>
        public static T FromString<T>(string jsonString)
        {
            if (!string.IsNullOrEmpty(jsonString))
            {
                using (var sr = new StringReader(jsonString))
                using (var jr = new JsonTextReader(sr))
                {
                    var js = new JsonSerializer();
                    return js.Deserialize<T>(jr);
                }
            }
            else
            {
                return default(T);
            }
        }
    }
}
