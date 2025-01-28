using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MAIeDosar.API.Services.MConnect
{
    public class RequestSettings
    {
        public HttpMethod Method { get; set; }
        public Uri Uri { get; set; }
        public string RequestHeaders { get; set; }
        public string SoapAction { get; set; }
        public string ContentHeaders { get; set; }
        public string Content { get; set; }
        public bool SignMessage { get; set; }
    }
}
