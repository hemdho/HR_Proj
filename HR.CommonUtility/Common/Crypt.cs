using System;

namespace HR.CommonUtility
{
    using BCrypt.Net;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    public static class Crypt
    {
        private static readonly Int16 intFact = 4;
        private static readonly string strKey = "m93pu1354b2e7068xcqldrv898ka8319";

        #region BCrypt
        public static string GetPassword(string strPassword)
        {
            return BCrypt.HashPassword(strPassword, intFact);
        }

        public static bool Verify(string strPassword, string strVerify)
        {
            bool blnVerify = false;
            try
            {
                blnVerify = BCrypt.Verify(strPassword, strVerify);
            }
            catch (Exception ex)
            {
                //log here
            }
            return blnVerify;
        }
        #endregion BCrypt

        #region Encrypt-Decrypt
        public static string EncryptString(string strInput)
        {
            byte[] bIv = new byte[16];
            byte[] bArray;
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(strKey);
                aes.IV = bIv;
                ICryptoTransform ctEncrypt = aes.CreateEncryptor(aes.Key, aes.IV);
                using (MemoryStream msStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)msStream, ctEncrypt, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            swWriter.Write(strInput);
                        }
                        bArray = msStream.ToArray();
                    }
                }
            }
            return Convert.ToBase64String(bArray);
        }

        public static string DecryptString(string strInput)
        {
            byte[] bIv = new byte[16];
            byte[] bArray = Convert.FromBase64String(strInput);
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(strKey);
                aes.IV = bIv;
                ICryptoTransform ctDecryt = aes.CreateDecryptor(aes.Key, aes.IV);
                using (MemoryStream msStream = new MemoryStream(bArray))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)msStream, ctDecryt, CryptoStreamMode.Read))
                    {
                        using (StreamReader srReader = new StreamReader((Stream)cryptoStream))
                        {
                            return srReader.ReadToEnd();
                        }
                    }
                }
            }
        }
        #endregion Encrypt-Decrypt
    }

}