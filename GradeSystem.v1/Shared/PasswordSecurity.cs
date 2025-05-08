using System;
using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography; //argon2

public class PasswordSecurity
{
    private const int SaltSize = 128 / 8; // 128 bitów
    private const int Iterations = 10000;//350000 to to na hostingu trzeba przetestować jak działa
    private const int HashSize = 256 / 8; // 256 bitów

    private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA512;

    public static (string Salt, string Hash) HashPassword(string password)
    {
        //var salt = GenerateSalt(SaltSize);
        //var hash = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(password),salt,Iterations,HashAlgorithmName.SHA256,HashSize);
        //return (Convert.ToBase64String(salt), Convert.ToBase64String(hash));
        byte[] salt=RandomNumberGenerator.GetBytes(SaltSize);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);
        return(Convert.ToHexString(salt), Convert.ToHexString(hash));
    }

    private static byte[] GenerateSalt(int size)
    {
        var salt = new byte[size];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(salt);
        return salt;
    }
    public static bool VerifyPassword(string password, string storedSalt, string storedHash)
    {
        //var saltBytes = Convert.FromBase64String(storedSalt);
        //var hashBytes = Rfc2898DeriveBytes.Pbkdf2(
        //    Encoding.UTF8.GetBytes(password),
        //    saltBytes,
        //    Iterations,
        //    HashAlgorithmName.SHA256,
        //    HashSize);
        //var newHash = Convert.ToBase64String(hashBytes);
        //return newHash == storedHash;
        byte[] saltBytes = Convert.FromHexString(storedSalt);
        byte[] hashBytes = Convert.FromHexString(storedHash);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, saltBytes, Iterations, Algorithm, HashSize);
        return CryptographicOperations.FixedTimeEquals(hash, hashBytes);
    }
}

