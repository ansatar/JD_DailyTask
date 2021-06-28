using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace JD.Utils.Models
{
    public class HttpWebClientResponse
    {
        public string Result { get; set; }
        public HttpResponseHeaders httpResponseHeaders { get; set; }
    }
}
