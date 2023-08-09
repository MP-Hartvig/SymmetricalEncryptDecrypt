using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SymmetricalEncryptDecrypt
{
    internal class AesCryptoService
    {
        string convertErrorMsg = "Convert to base64 from byte array failed with the following message: ";

        public string[] GetAesStrings(string input)
        {
            string[] aesStrings = new string[3];
            
            Aes aes = Aes.Create();
            aes.Padding = PaddingMode.PKCS7;

            byte[] tempCipher = AesEncryptString(aes, input, aes.Key, aes.IV);

            aesStrings[0] = GetByteArrayAsBase64(aes.Key);
            aesStrings[1] = GetByteArrayAsBase64(aes.IV);
            aesStrings[2] = GetByteArrayAsBase64(tempCipher);
            aesStrings[3] = AesDecryptToString(aes, tempCipher, aes.Key, aes.IV);

            return aesStrings;
        }

        private string GetByteArrayAsBase64(byte[] bytes)
        {
            try
            {
                return Convert.ToBase64String(bytes);
            }
            catch (Exception e)
            {
                return convertErrorMsg + e.Message;
            }
        }

        public byte[] AesEncryptString(Aes aes, string input, byte[] key, byte[] iv)
        {
            if (input == string.Empty)
            {
                throw new Exception("Input was empty");
            }

            byte[] streamResult;

            using (aes)
            {
                ICryptoTransform encryptor = aes.CreateEncryptor(key, iv);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(input);
                        }
                        streamResult = ms.ToArray();
                    }
                }
            }

            try
            {
                return streamResult;
            }
            catch (Exception e)
            {
                throw new Exception(convertErrorMsg + e.Message);
            }
        }

        public string AesDecryptToString(Aes aes, byte[] cipher, byte[] key, byte[] iv)
        {
            if (cipher == null)
            {
                throw new Exception("Cipher was null");
            }

            string streamResult;

            using (aes)
            {
                ICryptoTransform decryptor = aes.CreateDecryptor(key, iv);

                using (MemoryStream ms = new MemoryStream(cipher))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            streamResult = sr.ReadToEnd();
                        }
                    }
                }
            }

            return streamResult;
        }
    }
}
