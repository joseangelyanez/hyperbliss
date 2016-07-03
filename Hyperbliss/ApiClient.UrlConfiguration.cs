using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hyperbliss
{
    public partial class ApiClient
    {
        public static Action<ApiUrlConfiguration> ConfigureUrl { get; set; }
        public ApiUrlConfiguration UrlConfiguration { get; } = new ApiUrlConfiguration();

        protected virtual ApiUrlConfiguration GetUrlConfiguration()
        {
            /* Overrides configuration with application global configuration system. */
            if (string.IsNullOrEmpty(UrlConfiguration.BaseUrl))
            {
                if (ConfigureUrl != null)
                {
                    var config = new ApiUrlConfiguration();
                    ConfigureUrl(config);
                    return config;
                }
            }
            
            return UrlConfiguration;    
        }
    }
}
