using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymmetricalEncryptDecrypt
{
    /// <summary>
    /// Handles initiation of crypto services
    /// </summary>
    internal class CryptoManager
    {
        DesCryptoService des = new DesCryptoService();
        TripleDesCryptoService tripleDes = new TripleDesCryptoService();
        AesCryptoService aes = new AesCryptoService();
        ByteArrayToBase64Converter converter = new ByteArrayToBase64Converter();

        (byte[][], string) resultTuple = new();

        public string[] StartCryptoProcess(string input, string type)
        {
            if (type == "DES")
            {
                resultTuple = des.GetDesStrings(input);

                return ExtractFromTuple(resultTuple);
            }
            else if (type == "TripleDES")
            {
                resultTuple = tripleDes.GetTripleDesStrings(input);

                return ExtractFromTuple(resultTuple);
            }
            else
            {
                resultTuple = aes.GetAesStrings(input);

                return ExtractFromTuple(resultTuple);
            }
        }

        private string[] ExtractFromTuple((byte[][], string) tempTuple)
        {
            string[] cryptoStrings = new string[4];

            for (int i = 0; i < tempTuple.Item1.Length; i++)
            {
                cryptoStrings[i] = converter.GetByteArrayAsBase64(tempTuple.Item1[i]);
            }

            cryptoStrings[3] = tempTuple.Item2;

            return cryptoStrings;
        }
    }
}
