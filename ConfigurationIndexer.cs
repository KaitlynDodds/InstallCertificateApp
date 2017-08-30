using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace install_certificate_app
{
    public class ConfigurationIndexer
    {
        public static IConfigurationRoot Configuration {get; set;}

        public ConfigurationIndexer()
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json", optional: false);

            Configuration = builder.Build();

        }

        public string GetConnectionString()
        {
            return Configuration["cloud_storage_connection_string"];
        }

        public string GetStoreName()
        {
            return Configuration["store_name"];
        }

        public string GetTableReference()
        {
            return Configuration["cloud_storage_table"];
        }

        public string GetCertPrefix()
        {
            return Configuration["certificate_info:cert_prefix"];
        }

    }
}
