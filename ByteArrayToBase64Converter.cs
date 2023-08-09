using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymmetricalEncryptDecrypt
{
    internal class ByteArrayToBase64Converter
    {
        string convertErrorMsg = "Convert to base64 from byte array failed with the following message: ";

        public string GetByteArrayAsBase64(byte[] bytes)
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
    }
}
