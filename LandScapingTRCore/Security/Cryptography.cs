using System.Security.Cryptography;
using System.Text;

public class Cryptography
{
    private static IConfiguration Configuration { get; set; }

    static Cryptography()
    {
        // Set up configuration to read from appsettings.json
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

        Configuration = builder.Build();
    }

    private static string Key => Configuration.GetSection("EncryptionSettings")["Key"];
    private static string IV => Configuration.GetSection("EncryptionSettings")["IV"];


    public static string Encrypt(string plainText)
    {
        using (TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider())
        {
            tdes.Key = Encoding.ASCII.GetBytes(Key);
            tdes.IV = Encoding.ASCII.GetBytes(IV);

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, tdes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
                    cs.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cs.FlushFinalBlock();
                }
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }

    public static string Decrypt(string encryptedText)
    {
        using (TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider())
        {
            tdes.Key = Encoding.ASCII.GetBytes(Key);
            tdes.IV = Encoding.ASCII.GetBytes(IV);

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, tdes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
                    cs.Write(encryptedBytes, 0, encryptedBytes.Length);
                    cs.FlushFinalBlock();
                }
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }
    }
}