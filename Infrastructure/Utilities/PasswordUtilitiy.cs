using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.Utilities;

public class PasswordUtilitiy
{
    public static string HashPassword(string password)
    {
        byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));

        StringBuilder sBuilder = new();
        foreach (var @byte in bytes)
        {
            sBuilder.Append(@byte.ToString("x2"));
        }

        return sBuilder.ToString();
    }
}