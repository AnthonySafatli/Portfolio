using Newtonsoft.Json;
using Portfolio.Configuration;
using Portfolio.Models;
using System.Security.Cryptography;
using System.Text;

namespace Portfolio.Services;

public static class SecurityService
{
    public static SecurityConfig Config { get; private set; }

    static SecurityService()
    {
        LoadConfig();
    }

    private static void LoadConfig()
    {
        string configFilePath = "Secrets/secrets.json";
        if (File.Exists(configFilePath))
        {
            string jsonContent = File.ReadAllText(configFilePath);
            SecurityConfig? config = JsonConvert.DeserializeObject<SecurityConfig>(jsonContent);

            if (config == null)
            {
                throw new Exception("Error parsing secrets file");
            }
            else
            {
                Config = config;
            }
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
