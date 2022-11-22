using System.Security.Cryptography;
using System.Text;

namespace ClinicService.Domain.Utils;

public static class PasswordUtils
{
    private const string _secretKey = "Fz8wMguqN2DGWiD1ICvRxQ==";


    public static (string passwordHash, string passwordSalt) CreatePasswordHash(string password)
    {
        byte[] buffer = new byte[16];
        using var secureRandom = new RNGCryptoServiceProvider();
        secureRandom.GetBytes(buffer);

        string passwordSalt = Convert.ToBase64String(buffer);
        string passwordHash = GetPasswordHash(password, passwordSalt);

        return (passwordHash, passwordSalt);
    }
    public static bool Verify(string password, string passwordHash, string passwordSalt)
        => GetPasswordHash(password, passwordSalt) == passwordHash;
    private static string GetPasswordHash(string password, string passwordSalt)
    {
        password = $"{password}~{passwordSalt}~{_secretKey}";
        byte[] buffer = Encoding.UTF8.GetBytes(password);

        using var sha512 = new SHA512Managed();
        byte[] passwordHash = sha512.ComputeHash(buffer);
  
        return Convert.ToBase64String(passwordHash);
    }
}