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
        //public string[] DesEncrypt(string input)
        //{
        //    var test = DES.Create();
        //    string[] result = new string[2];
        //    byte[] txt = Encoding.UTF8.GetBytes(input);
        //    byte[] key = new byte[32];
        //    byte[] nonce = new byte[Des];
        //    byte[] cipher = new byte[txt.Length];
        //    byte[] tag = new byte[AesGcm.TagByteSizes.MaxSize];

        //    RandomNumberGenerator.Fill(key);
        //    RandomNumberGenerator.Fill(nonce);

        //    AesGcm aes = new AesGcm(key);

        //    aes.Encrypt(nonce, txt, cipher, tag);

        //    result[0] = Encoding.UTF8.GetString(cipher) ?? string.Empty;
        //    result[1] = DesDecrypt(aes, cipher, nonce, tag) ?? string.Empty;
        //    return result;
        //}

        //public string DesDecrypt(DES des, byte[] cipher, byte[] nonce, byte[] tag)
        //{
        //    byte[] decryptedTxt = new byte[cipher.Length];

        //    des.(nonce, cipher, tag, decryptedTxt);

        //    return Encoding.UTF8.GetString(decryptedTxt);
        //}
    }
}
