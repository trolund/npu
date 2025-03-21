using System.Security.Cryptography;
using System.Text;

namespace LockNote.Bl;

public static class Encryption
{
    private static readonly HashAlgorithmName HashAlgo = HashAlgorithmName.SHA256;
    public static string Encrypt(string plaintext, string password)
    {
        var plaintextBytes = Encoding.UTF8.GetBytes(plaintext);

        // Generate a random salt
        var salt = new byte[16];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        using (var passwordBytes = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgo))
        {
            using (var encryptor = Aes.Create())
            {
                encryptor.Key = passwordBytes.GetBytes(32);
                encryptor.IV = passwordBytes.GetBytes(16);

                using (var ms = new MemoryStream())
                {
                    // Store salt at the beginning of the encrypted data
                    ms.Write(salt, 0, salt.Length);

                    using (var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(plaintextBytes, 0, plaintextBytes.Length);
                        cs.FlushFinalBlock(); // Ensure all bytes are written
                    }

                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }
    }

    public static string Decrypt(string encrypted, string password)
    {
        var encryptedBytes = Convert.FromBase64String(encrypted);

        // Extract salt from the encrypted data
        var salt = new byte[16];
        Array.Copy(encryptedBytes, 0, salt, 0, 16);

        using var passwordBytes = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgo);
        using var encryptor = Aes.Create();
        encryptor.Key = passwordBytes.GetBytes(32);
        encryptor.IV = passwordBytes.GetBytes(16);

        using var ms = new MemoryStream();
        using (var cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
        {
            cs.Write(encryptedBytes, 16, encryptedBytes.Length - 16); // Skip salt
            cs.FlushFinalBlock(); // Ensure proper decryption
        }

        return Encoding.UTF8.GetString(ms.ToArray());
    }
}