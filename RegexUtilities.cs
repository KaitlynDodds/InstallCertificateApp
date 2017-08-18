using System;
using System.Globalization;
using System.Text.RegularExpressions;

/*
* Code obtained from Microsoft .NET Guide
* https://docs.microsoft.com/en-us/dotnet/standard/base-types/how-to-verify-that-strings-are-in-valid-email-format
*
* Does not perform authentication to validate the email address (only determines
* if its format is valid for an email address). Does not verify that the top 
* level domain name is a valid domain name, only verifies that the top-level
* domain name consists of between two and twenty-four ASCII characters with 
* alphanumeric first and last characters, and the remaining characters being 
* either alphanumeric or a hyphen 
*/
namespace install_certificate_app {
    public static class RegexUtilities
    {
        static bool invalid = false;

            /*
            * Returns true is the string contains a valid email address, false if the string
            * does not 
            */
        public static bool IsValidEmail(string strIn)
        {
            invalid = false;
            if (String.IsNullOrEmpty(strIn))
                return false;

            // Use IdnMapping class to convert Unicode domain names.
            try {
                // @"(@)(.+)$ regex is used to separate the domain name from the email address
                strIn = Regex.Replace(strIn, @"(@)(.+)$", DomainMapper,
                                        RegexOptions.None, TimeSpan.FromMilliseconds(200));
            }
            catch (RegexMatchTimeoutException) {
                return false;
            }

                if (invalid)
                return false;

            // Return true if strIn is in valid e-mail format.
            try {
                // confirms that the address conforms to a regular expression pattern 
                return Regex.IsMatch(strIn,
                        @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                        @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                        RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException) {
                return false;
            }
        }

            /*
            Detects any invalid characters in the domain name, translates Unicode characters that 
            are outside the US-ASCII character range to Punycode, returns the Punycode domain name
            preceded by the @ symbol to the IsValidEmail method  
            */
        private static string DomainMapper(Match match)
        {
            // IdnMapping class with default property values.
            IdnMapping idn = new IdnMapping();

            string domainName = match.Groups[2].Value;
            try {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException) {
                invalid = true;
            }
            return match.Groups[1].Value + domainName;
        }
    }
}