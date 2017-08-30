using System;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace install_certificate_app
{
    public class Certificate
    {
        private ConfigurationIndexer Config {get; set;}
        public string CertificateFileName { get; set; }

        public Certificate()
        {
            Config = new ConfigurationIndexer();

            // merge prefix with standard cert name
            CertificateFileName = Config.GetCertPrefix() + "_certificate.cer";
        }

        public void InstallCertificate()
        {
            Console.WriteLine(CertificateFileName);
            // create new X509 store for testing in local certificate store
            X509Store store = new X509Store(Config.GetStoreName(), StoreLocation.CurrentUser);
            try
            {
                store.Open(OpenFlags.ReadWrite);

                // create certificate from certificate file
                X509Certificate2 certificate = new X509Certificate2(CertificateFileName);

                // add certificate to the store
                store.Add(certificate);
            }
            finally
            {
                // close the store
                store.Close();
            }
        }

    }
}
