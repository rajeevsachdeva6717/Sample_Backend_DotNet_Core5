using System;
using System.IO;
using System.Security.Cryptography;

namespace Sample.Common
{
    public class EncryptionDecryption
    {
        /// <summary>
        /// To encrypt password string.
        /// </summary>
        /// <param name="enteredPassword"></param>
        /// <param name="passwordSalt"></param>
        /// <returns></returns>
        public static string Encrypt(string enteredPassword, string passwordSalt)
        {
            byte[] clearBytes = System.Text.Encoding.Unicode.GetBytes(enteredPassword);
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(passwordSalt, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });

            MemoryStream ms = new MemoryStream();
            Rijndael alg = Rijndael.Create();
            alg.Key = pdb.GetBytes(32);
            alg.IV = pdb.GetBytes(16);

            CryptoStream cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(clearBytes, 0, clearBytes.Length);
            cs.Close();
            byte[] encryptedData = ms.ToArray();

            return Convert.ToBase64String(encryptedData);
        }

        /// <summary>
        /// Generate salt for password.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetSalt(int length)
        {
            byte[] randomArray = new byte[length];

            string randomString;
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomArray);
            randomString = Convert.ToBase64String(randomArray);

            return randomString;
        }

        /// <Summary>
        ///<Descripton>This function will return  decrypted password</Descripton>        
        /// </Summary>       
        public static string Decrypt(string cipherText, string password)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);

            PasswordDeriveBytes pdb = new PasswordDeriveBytes(password, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });

            byte[] decryptedData = Decrypt(cipherBytes, pdb.GetBytes(32), pdb.GetBytes(16));

            return System.Text.Encoding.Unicode.GetString(decryptedData);
        }

        /// <Summary>
        ///<Descripton>This function will return  decrypted byte stream</Descripton>        
        /// </Summary>
        public static byte[] Decrypt(byte[] cipherData, byte[] key, byte[] iv)
        {
            MemoryStream memoryStream = new MemoryStream();

            Rijndael alg = Rijndael.Create();
            alg.Key = key;
            alg.IV = iv;

            CryptoStream cs = new CryptoStream(memoryStream, alg.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(cipherData, 0, cipherData.Length);

            cs.Close();

            byte[] decryptedData = memoryStream.ToArray();

            return decryptedData;
        }
    }
}
