using System;
using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Table; // Namespace for Table storage types

namespace install_certificate_app
{
    public class UserEntity : TableEntity
    {
        public string UserName {get; set;}
        public string DomainName {get; set;}
        public string HostName {get; set;}
        public string Email {get; set;}
        public UserEntity() {}

        public UserEntity(string certificateType, string hostName, string domainName)
        {
            // TODO: kzd -> validate inputs
            this.PartitionKey = ValidateCertificate(certificateType);
            this.RowKey = ValidateFQDN(hostName, domainName);
        }

        private string ValidateCertificate(string certificateType)
        {
            if (String.IsNullOrEmpty(certificateType))
            {
                throw new ArgumentException($"Invalid Certificate Type: {certificateType}");
            }
            else
            {
                certificateType = certificateType.ToLower().Trim();
            }

            // certificateType must be either 'admin' or 'readonly'
            if (!certificateType.Equals("admin") && !certificateType.Equals("readonly"))
            {
                throw new ArgumentException($"Invalid Certificate Type: {certificateType}");
            }
            else
            {
                return certificateType;
            }
        }

        private string ValidateFQDN(string hostName, string domainName)
        {
            if (String.IsNullOrEmpty(hostName))
            {
                throw new ArgumentException($"Invalid Host Name: {hostName}");
            }
            else
            {
                hostName = hostName.ToLower().Trim();
            }

            if (String.IsNullOrEmpty(domainName))
            {
                throw new ArgumentException($"Invalid Domain Name: {domainName}");
            }
            else
            {
                domainName = domainName.ToLower().Trim();
            }

            return $"{hostName}:{domainName}";
        }

    }

}
