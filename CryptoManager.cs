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
        TripleDesCryptoService tDes = new TripleDesCryptoService();
        AesCryptoService aes = new AesCryptoService();

        public string[] StartCryptoProcess(string input, string type)
        {
            //if (type == "DES")
            //{

            //}
            //else if (type == "TripleDES") {
                
            //}
            //else
            //{
            //    return aes.GetAesStrings(input);
            //}
            return aes.GetAesStrings(input);

        }
    }
}
