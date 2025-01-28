using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace MAIeDosar.API.Services.MConnect
{
    public class ResponseSettings
    {
        public bool MessageSigned { get; set; }
        public X509Certificate2 ServiceCertificate { get; set; }
    }
}
