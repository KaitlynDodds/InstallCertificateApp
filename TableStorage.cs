using System;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.RetryPolicies;
using System.Configuration;

namespace install_certificate_app
{
    public class TableStorage
    {
        private ConfigurationIndexer Config {get; set;}
        private CloudStorageAccount StorageAccount {get;}

        private CloudTableClient TableClient {get;}

        public TableStorage()
        {
            Config = new ConfigurationIndexer();
            // Parse the connection string and return a reference to the storage account.
            StorageAccount = CloudStorageAccount.Parse(Config.GetConnectionString());

            // Create the table client.
            TableClient = StorageAccount.CreateCloudTableClient();

        }

        public async Task SendEntityToTable(string certificateType, Computer computer)
        {

            // Retrieve a reference to the table.
            CloudTable table = TableClient.GetTableReference(Config.GetTableReference());
            TableClient.DefaultRequestOptions = new TableRequestOptions
            {
                RetryPolicy = new ExponentialRetry(TimeSpan.FromSeconds(10), 5),
                MaximumExecutionTime = TimeSpan.FromSeconds(10)
            };

            // Create the table if it doesn't exist.
            await table.CreateIfNotExistsAsync();

            // create new user entity
            UserEntity user = new UserEntity(certificateType, computer.HostName, computer.DomainName);
            user.DomainName = computer.DomainName;
            user.HostName = computer.HostName;
            user.UserName = computer.UserName;
            user.Email = computer.Email;

            // Create the TableOperation object that inserts the customer entity.
            TableOperation insertOperation = TableOperation.InsertOrReplace(user);

            // Execute the insert operation.
            await table.ExecuteAsync(insertOperation);
        }

    }
}
