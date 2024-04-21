using System.Security.Cryptography;
using System.Text;

namespace Portfolio.Models;

public static class Security
{
    public const string AdminCookieName = "AdminCookieAuth";
    public const string AdminPassword = "***REMOVED***";

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
