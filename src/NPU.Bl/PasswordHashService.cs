namespace LockNote.Bl;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

public static class PasswordHashService
{
    // not a secure way to handle passwords, but it's good enough for this project
    public static (byte[], string) HashPassword(string password)
    {
        // Generate a 128-bit salt using a sequence of
        // cryptographically strong random bytes.
        var salt = RandomNumberGenerator.GetBytes(128 / 8); // divide by 8 to convert bits to bytes
        // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
        var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
        
        return (salt, hashed);
    }
    
    public static bool VerifyPassword(string password, byte[] salt, string hashed)
    {
        // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
        var hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
        
        return hashedPassword == hashed;
    }
}