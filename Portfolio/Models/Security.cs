using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using System.IO;

namespace Portfolio.Models
{
    public class SecurityConfig
    {
        public string AdminCookieName { get; set; }
        public string AdminPassword { get; set; }
        public string ToAddress { get; set; }
        public string FromAddress { get; set; }
        public string FromAddressPassword { get; set; }
    }

    public static class Security
    {
        public static SecurityConfig Config { get; private set; }

        static Security()
        {
            LoadConfig();
        }

        private static void LoadConfig()
        {
            string configFilePath = "Secrets/secrets.json";
            if (File.Exists(configFilePath))
            {
                string jsonContent = File.ReadAllText(configFilePath);
                Config = JsonConvert.DeserializeObject<SecurityConfig>(jsonContent);
            }
            else
            {
                throw new FileNotFoundException("Security config file not found.");
            }
        }

        public static string EncryptSHA256(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
