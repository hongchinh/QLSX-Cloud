using System;
using System.Security.Cryptography;
using System.Text;

public class EncryptionHelper
{
    private static readonly string key = "CamiCamiCamiCamiCamiCamiCamiCami"; // Key phải có đủ 16, 24 hoặc 32 byte cho các khóa AES-128, AES-192 hoặc AES-256
    private static readonly string iv = "CamiCamiCamiCami"; // IV phải có đủ 16 byte

    public static string Encrypt(string password)
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);
        byte[] ivBytes = Encoding.UTF8.GetBytes(iv);
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = keyBytes;
            aesAlg.IV = ivBytes;

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (System.IO.MemoryStream msEncrypt = new System.IO.MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    csEncrypt.Write(passwordBytes, 0, passwordBytes.Length);
                    csEncrypt.FlushFinalBlock();
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }
    }

    public static string Decrypt(string encryptedPassword)
    {
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);
        byte[] ivBytes = Encoding.UTF8.GetBytes(iv);
        byte[] encryptedPasswordBytes = Convert.FromBase64String(encryptedPassword);

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = keyBytes;
            aesAlg.IV = ivBytes;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (System.IO.MemoryStream msDecrypt = new System.IO.MemoryStream(encryptedPasswordBytes))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    byte[] passwordBytes = new byte[msDecrypt.Length];
                    int decryptedByteCount = csDecrypt.Read(passwordBytes, 0, passwordBytes.Length);
                    return Encoding.UTF8.GetString(passwordBytes, 0, decryptedByteCount);
                }
            }
        }
    }
}

