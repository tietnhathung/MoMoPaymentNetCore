using System.Security.Cryptography;

namespace MoMoSdk.Utils;

public static class HmacHelper
{
    public static string Compute(string key = "", string message = "")
    {
        byte[] keyByte = System.Text.Encoding.UTF8.GetBytes(key);
        byte[] messageBytes = System.Text.Encoding.UTF8.GetBytes(message);
        byte[] hashMessage = new HMACSHA256(keyByte).ComputeHash(messageBytes);
        return BitConverter.ToString(hashMessage).Replace("-", "").ToLower();
    }
}