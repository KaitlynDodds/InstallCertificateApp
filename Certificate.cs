using System;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace install_certificate_app
{
    public class Certificate 
    {
        public string CertificatePrefix { get; }

        public string CertificateFileName { get; }

        public Certificate()
        {
            CertificatePrefix = "";  // TODO: get cert prefix from config file 
            CertificateFileName = CertificatePrefix + "certificate.";  // merge prefix with standard cert name 
        }

        public void InstallCertificate() 
        {
            // create new X509 store for testing in local certificate store 
            X509Store store = new X509Store("ServiceFabricClientStore", StoreLocation.CurrentUser);
            try 
            {
                store.Open(OpenFlags.ReadWrite);

                // create certificate from certificate file
                X509Certificate2 certificate = new X509Certificate2(CertificateFileName);
            
                // add certificate to the store 
                store.Add(certificate);
            } 
            catch (CryptographicException ex)
            {
                throw new CryptographicException($"{ex.Message}: {ex.StackTrace}");
            } 
            catch (SecurityException ex)
            {
                throw new CryptographicException($"{ex.Message}: {ex.StackTrace}");                
            }
            catch (ArgumentNullException ex)
            {
                throw new CryptographicException($"{ex.Message}: {ex.StackTrace}");                
            }
            catch (ArgumentException ex)
            {
                throw new CryptographicException($"{ex.Message}: {ex.StackTrace}");                
            }
            finally 
            {
                // close the store 
                store.Close();
            }
        }

    }
}