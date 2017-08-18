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
        public string Email {get; set;}        
        public UserEntity() {}

        public UserEntity(string certificateType, string hostName)
        {
            this.PartitionKey = certificateType;
            this.RowKey = hostName;
        }

    }

}