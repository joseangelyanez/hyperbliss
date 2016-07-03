using System;
using System.Collections.Generic;
using System.Text;

namespace Hyperbliss
{
    public class ApiUrlConfiguration
    {
        private string m_baseUrl = string.Empty;

        public string BaseUrl
        {
            get {
                return m_baseUrl;
            }
            set {
                m_baseUrl = value;

                if (m_baseUrl != null)
                {
                    m_baseUrl = m_baseUrl.Trim();

                    if (!m_baseUrl.EndsWith("/"))
                    {
                        m_baseUrl = m_baseUrl + "/";
                    }
                }
            }
        }

        public List<ApiValue> Values { get; } = new List<ApiValue>();

        public string ApplyValuesIntoResource(string resource)
        {
            if (resource == null)
                throw new ArgumentNullException("Resource string cannot be null");

            if (Values.Count == 0)
                return resource;
            
            StringBuilder sbuilder = new StringBuilder();
            sbuilder.Append(resource);
            bool containsQuestionMark = BaseUrl.Contains("?");
            bool first = true;
            foreach (var apiValue in Values)
            {
                if (!containsQuestionMark)
                {
                    sbuilder.Append("?");
                    containsQuestionMark = true;
                }

                if (first)
                    first = false;
                else
                    sbuilder.Append("&");

                sbuilder.Append($"{apiValue.Key}={apiValue.Value}");
            }

            return sbuilder.ToString();
        }
    }
}
