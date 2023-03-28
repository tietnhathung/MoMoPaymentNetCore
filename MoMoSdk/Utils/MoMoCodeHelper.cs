using System.Security.Cryptography;

namespace MoMoSdk.Utils;

public static class MoMoCodeHelper
{
    public static string Hmac(string key = "", string message = "")
    {
        byte[] keyByte = System.Text.Encoding.UTF8.GetBytes(key);
        byte[] messageBytes = System.Text.Encoding.UTF8.GetBytes(message);
        byte[] hashMessage = new HMACSHA256(keyByte).ComputeHash(messageBytes);
        return BitConverter.ToString(hashMessage).Replace("-", "").ToLower();
    }
    
    public static string Base64Encode(string plainText) 
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }
    
    public static string Base64Decode(string base64EncodedData) 
    {
        var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }
}