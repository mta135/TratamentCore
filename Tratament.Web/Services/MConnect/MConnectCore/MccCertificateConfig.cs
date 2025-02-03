using Microsoft.Extensions.Configuration;
using System.Security.Cryptography.X509Certificates;

namespace Tratament.Web.Services.MConnect.MConnectCore
{
    public class MccCertificateConfig
    {

        private static string ServiceCertifcate;
        private static string ClientCertifcate;

        public static void InitializeSettings(IConfiguration config)
        {
            ServiceCertifcate = config.GetValue<string>("MConnect:ServiceCertifcate");
            ClientCertifcate = config.GetValue<string>("MConnect:ClientCertifcate");
        }


        #region Service Certificate
        public static X509Certificate2 GetServiceCertificate()
        {
            X509Store Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            Store.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection CertColl = Store.Certificates.Find(X509FindType.FindBySerialNumber, ServiceCertifcate, false);

            X509Certificate2 certificate = new X509Certificate2(CertColl[0]);

            return certificate;
        }


        #endregion


        #region Client Certifcate
        public static X509Certificate2 GetClientCerificate()
        {
            X509Store Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            Store.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection CertColl = Store.Certificates.Find(X509FindType.FindBySerialNumber, ClientCertifcate, false);

            X509Certificate2 certificate = new X509Certificate2(CertColl[0]);

            certificate.GetRSAPrivateKey();
            return certificate;
        }

        #endregion
    }
}
