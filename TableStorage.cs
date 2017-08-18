using System;
using System.Threading.Tasks;
using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Table; // Namespace for Table storage types
using System.Configuration;

namespace install_certificate_app 
{
    public class TableStorage
    {

        public CloudStorageAccount StorageAccount {get;}

        public CloudTableClient TableClient {get;}

        public TableStorage()
        {
            // Parse the connection string and return a reference to the storage account.
            StorageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=certdstributionstoracc;AccountKey=JcQKYLGyjlZmCAcY1aNKuxWCUhL2Q0qwZ+0qN8R/uKs6SsAhP9MiwJM5c/Nrh3dYI3aLNHOD2VPukbJBHzlt6g==;EndpointSuffix=core.windows.net");

            // Create the table client.
            TableClient = StorageAccount.CreateCloudTableClient();

        }

        public async Task SendEntityToTable(string certificateType, Computer computer)
        {

            // Retrieve a reference to the table.
            CloudTable table = TableClient.GetTableReference("ServiceFabricCertificates");

            // Create the table if it doesn't exist.
            await table.CreateIfNotExistsAsync();

            // create new user entity 
            UserEntity user = new UserEntity(certificateType, computer.HostName);
            user.DomainName = computer.DomainName;
            user.UserName = computer.UserName;
            user.Email = computer.Email;

            // Create the TableOperation object that inserts the customer entity.
            TableOperation insertOperation = TableOperation.InsertOrReplace(user);

            // Execute the insert operation.
            await table.ExecuteAsync(insertOperation);
        }

    }
}