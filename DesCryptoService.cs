using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SymmetricalEncryptDecrypt
{
    internal class DesCryptoService
    {
        /// <summary>
        /// Gets the DES encryption/decryption information
        /// </summary>
        /// <param name="input">The user's input to be encrypterd/decrypted</param>
        /// <returns></returns>
        public (byte[][], string) GetDesStrings(string input)
        {
            byte[][] desBytes = new byte[4][];

            DES des = DES.Create();

            // Adds a padding mode to the encryption/decryption process
            des.Padding = PaddingMode.PKCS7;

            byte[] key = des.Key;
            byte[] iv = des.IV;

            byte[] cipher = DesEncryptStringToByteArray(des, input, key, iv);

            desBytes[0] = key;
            desBytes[1] = iv;
            desBytes[2] = DesEncryptStringToByteArray(des, input, key, iv);

            return (desBytes, DesDecryptToString(des, cipher, key, iv));
        }

        private byte[] DesEncryptStringToByteArray(DES des, string input, byte[] key, byte[] iv)
        {
            if (input == string.Empty)
            {
                throw new Exception("Input was empty");
            }

            byte[] streamResult;

            using (des)
            {
                ICryptoTransform encryptor = des.CreateEncryptor(key, iv);

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

        private string DesDecryptToString(DES des, byte[] cipher, byte[] key, byte[] iv)
        {
            if (cipher == null)
            {
                throw new Exception("Cipher was null");
            }

            string streamResult;

            using (des)
            {
                ICryptoTransform decryptor = des.CreateDecryptor(key, iv);

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
