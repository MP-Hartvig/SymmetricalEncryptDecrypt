using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SymmetricalEncryptDecrypt
{
    internal class TripleDesCryptoService
    {
        /// <summary>
        /// Gets the TripleDES encryption/decryption information
        /// </summary>
        /// <param name="input">The user's input to be encrypterd/decrypted</param>
        /// <returns></returns>
        public (byte[][], string) GetTripleDesStrings(string input)
        {
            byte[][] desBytes = new byte[4][];

            TripleDES tripleDES = TripleDES.Create();

            // Adds a padding mode to the encryption/decryption process
            tripleDES.Padding = PaddingMode.PKCS7;

            byte[] key = tripleDES.Key;
            byte[] iv = tripleDES.IV;

            byte[] cipher = DesEncryptStringToByteArray(tripleDES, input, key, iv);

            desBytes[0] = key;
            desBytes[1] = iv;
            desBytes[2] = DesEncryptStringToByteArray(tripleDES, input, key, iv);

            return (desBytes, DesDecryptToString(tripleDES, cipher, key, iv));
        }

        private byte[] DesEncryptStringToByteArray(TripleDES tripleDES, string input, byte[] key, byte[] iv)
        {
            if (input == string.Empty)
            {
                throw new Exception("Input was empty");
            }

            byte[] streamResult;

            using (tripleDES)
            {
                ICryptoTransform encryptor = tripleDES.CreateEncryptor(key, iv);

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

        private string DesDecryptToString(TripleDES tripleDES, byte[] cipher, byte[] key, byte[] iv)
        {
            if (cipher == null)
            {
                throw new Exception("Cipher was null");
            }

            string streamResult;

            using (tripleDES)
            {
                ICryptoTransform decryptor = tripleDES.CreateDecryptor(key, iv);

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
