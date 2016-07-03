using Newtonsoft.Json;
using System.IO;

namespace Hyperbliss
{
    public static class ObjectExtensions
    {        
        public static object FromJSON(this string json)
        {
            if (!string.IsNullOrEmpty(json))
            {
                using (var sr = new StringReader(json))
                using (var jr = new JsonTextReader(sr))
                {
                    var js = new JsonSerializer();
                    return js.Deserialize(jr);
                }
            }
            else
            {
                return null;
            }
        }

        public static string ToJson(this object obj)
        {
            string json = string.Empty;

            if (obj != null)
            {
                json = JsonConvert.SerializeObject(obj);
            }

            return json;
        }
    }
}
