using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Security.Cryptography;
namespace Yammer
{


        public class Security
        {
            
            
            public static string Decrypt(string stringToDecrypt, string sEncryptionKey)
            {
                byte[] key = {  };
                byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };
                byte[] inputByteArray = new byte[stringToDecrypt.Length + 1];
                try {
                    key = System.Text.Encoding.UTF8.GetBytes(Left(sEncryptionKey, 8));
                    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                    inputByteArray = Convert.FromBase64String(stringToDecrypt);
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                    return encoding.GetString(ms.ToArray());
                }
                catch (Exception e) {
                    return e.Message;
                }
            }
            
            public static string Encrypt(string stringToEncrypt, string SEncryptionKey)
            {
                byte[] key = {  };
                byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };
                try {
                    key = System.Text.Encoding.UTF8.GetBytes(Left(SEncryptionKey, 8));
                    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                    byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    return Convert.ToBase64String(ms.ToArray());
                }
                catch (Exception e) {
                    return e.Message;
                }
            }

            private static string Left(string param, int length)
            {
                //we start at 0 since we want to get the characters starting from the
                //left and with the specified lenght and assign it to a variable
                string result = param.Substring(0, length);
                //return the result of the operation
                return result;
            }

            // Create an md5 sum string of this string
            public static string GetMd5Sum(string str)
            {
                // First we need to convert the string into bytes, which
                // means using a text encoder.
                Encoder enc = System.Text.Encoding.Unicode.GetEncoder();

                // Create a buffer large enough to hold the string
                byte[] unicodeText = new byte[str.Length * 2];
                enc.GetBytes(str.ToCharArray(), 0, str.Length, unicodeText, 0, true);

                // Now that we have a byte array we can ask the CSP to hash it
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] result = md5.ComputeHash(unicodeText);

                // Build the final string by converting each byte
                // into hex and appending it to a StringBuilder
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < result.Length; i++)
                {
                    sb.Append(result[i].ToString("X2"));
                }

                // And return it
                return sb.ToString();
            }

        }



}
