using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;

namespace Tratament.Web.Services.MConectService
{
    public class MConnectHelper
    {

        public static string URLService { get; set; }

        public static string CertificateSerialNumber { get; set; }

        public static void InitializeSettings(IConfiguration config)
        {

            URLService = config.GetValue<string>("MConnectService:URLService");
            CertificateSerialNumber = config.GetValue<string>("MConnectService:CertificateSerialNumber");
        }

        public static IClientChannel CreateClient()
        {
            IClientChannel mconnectClient;

            var msBinding = new BasicHttpBinding();

            // So we can download reports bigger than 64 KBytes
            // See https://stackoverflow.com/questions/884235/wcf-how-to-increase-message-size-quota
            msBinding.MaxBufferPoolSize = 20000000;
            msBinding.MaxBufferSize = 20000000;
            msBinding.MaxReceivedMessageSize = 20000000;
            msBinding.SendTimeout = new TimeSpan(0, 0, 120);
            msBinding.ReceiveTimeout = new TimeSpan(0, 0, 120);
            msBinding.OpenTimeout = new TimeSpan(0, 0, 120);
            msBinding.CloseTimeout = new TimeSpan(0, 0, 120);
            msBinding.Security.Mode = BasicHttpSecurityMode.Transport;
            msBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;

            var rsEndpointAddress = new EndpointAddress(new Uri(URLService));
            var channelFactory = new ChannelFactory<IClientChannel>(msBinding, rsEndpointAddress);

            X509Store Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            Store.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection CertColl = Store.Certificates.Find(X509FindType.FindBySerialNumber, CertificateSerialNumber, false);

            channelFactory.Credentials.ClientCertificate.Certificate = CertColl[0];

            mconnectClient = channelFactory.CreateChannel();

            return mconnectClient;
        }
    }
}
