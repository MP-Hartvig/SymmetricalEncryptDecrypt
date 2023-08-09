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
        /// <summary>
        /// Gets the AES encryption/decryption information
        /// </summary>
        /// <param name="input">The user's input to be encrypterd/decrypted</param>
        /// <returns></returns>
        public (byte[][], string) GetAesStrings(string input)
        {
            byte[][] aesBytes = new byte[4][];
            
            Aes aes = Aes.Create();

            // Adds a padding mode to the encryption/decryption process
            aes.Padding = PaddingMode.PKCS7;

            byte[] key = aes.Key;
            byte[] iv = aes.IV;

            byte[] cipher = AesEncryptStringToByteArray(aes, input, key, iv);

            aesBytes[0] = key;
            aesBytes[1] = iv;
            aesBytes[2] = AesEncryptStringToByteArray(aes, input, key, iv);

            return (aesBytes, AesDecryptToString(aes, cipher, key, iv));
        }

        private byte[] AesEncryptStringToByteArray(Aes aes, string input, byte[] key, byte[] iv)
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

            return streamResult;
        }

        private string AesDecryptToString(Aes aes, byte[] cipher, byte[] key, byte[] iv)
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
