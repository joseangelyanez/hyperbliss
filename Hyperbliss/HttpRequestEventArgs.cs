using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Hyperbliss
{
    public class HttpRequestEventArgs : EventArgs
    {
        public HttpWebRequest Request { get; set; }
    }
}
