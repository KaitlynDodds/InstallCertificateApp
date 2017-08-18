using System;
using Newtonsoft.Json;
using System.Net.NetworkInformation;

namespace install_certificate_app
{
    public class Computer
    {
        public string HostName { get; }

        public string DomainName { get; }

        public string UserName { get; }

        public string Email { get; }

        public Computer() 
        {
            IPGlobalProperties computerProperties = IPGlobalProperties.GetIPGlobalProperties();
            DomainName = computerProperties.DomainName;
            HostName = computerProperties.HostName;
            UserName = Environment.UserName;
            Email = GetUserEmail();
        }

        public string GetJson()
        {
            return JsonConvert.SerializeObject(this);             
        }

        private string GetUserEmail() 
        {
            string userEmail = null;
            Boolean isValid = false;
                  
            do {
                Console.WriteLine("\n\nPlease enter your email address: ");
                string email = Console.ReadLine().Trim().ToLower();
                // Todo: check that is valid email 
                if (RegexUtilities.IsValidEmail(email)) // check that is valid email
                {
                    // confirm email 
                    Console.WriteLine($"Confirm Email Address: ");
                    string emailConfirmation = Console.ReadLine().Trim().ToLower();
                    if (email.Equals(emailConfirmation)) 
                    {
                        isValid = true;
                        userEmail = email;
                        Console.WriteLine("========== Thank You.\n\n");
                    } else 
                    {
                        Console.WriteLine("========== Email did not match");
                    } 
                } else 
                {
                    Console.WriteLine("========== Invalid Email Address");
                }
            } while (!isValid);

            return userEmail;
        }

    }
}