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
        byte[] salt=RandomNumberGenerator.GetBytes(SaltSize);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);
        return(Convert.ToHexString(salt), Convert.ToHexString(hash));
    }

    public static bool VerifyPassword(string password, string storedSalt, string storedHash)
    {
        byte[] saltBytes = Convert.FromHexString(storedSalt);
        byte[] hashBytes = Convert.FromHexString(storedHash);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, saltBytes, Iterations, Algorithm, HashSize);
        return CryptographicOperations.FixedTimeEquals(hash, hashBytes);
    }
}

