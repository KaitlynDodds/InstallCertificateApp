using System;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;

namespace install_certificate_app
{
    class Program
    {

        public static ConfigurationIndexer Config {get; set;}

        static void Main(string[] args)
        {
            Config = new ConfigurationIndexer();

            MainAsync().Wait();
        }

        static async Task MainAsync() 
        {
            Console.WriteLine("========== Install Service Fabric Client Certificate ===========\n\n");

            // get computer info from system, user email from user                          
            Console.WriteLine("========== Gathering Information...");
            Computer computer = new Computer();
            Console.WriteLine("========== Finished Gathering information...\n\n");            

            // Sending computer information
            Console.WriteLine("========== Recording User Information...");
            TableStorage tableStorage = new TableStorage();
            await tableStorage.SendEntityToTable(Config.GetCertPrefix(), computer);
            Console.WriteLine("========== Finished Recording Information...\n\n");            

            // install certificate
            Console.WriteLine("========== Installing Client Certificate...");
            Certificate certificate = new Certificate();
            certificate.InstallCertificate();
            Console.WriteLine("========== Certificate Installation Complete...\n\n");

            Console.WriteLine("========== Finished ==========\n\n");
        }

    }
}
