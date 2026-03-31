using System.Security.Cryptography;
using System.Text;

namespace WellTool.Crypto.Digest
{
    public class HMac
    {
        private HMAC _hmac;
        
        public HMac(string algorithm, byte[] key)
        {
            _hmac = HMAC.Create(algorithm);
            _hmac.Key = key;
        }
        
        public byte[] Digest(byte[] data)
        {
            return _hmac.ComputeHash(data);
        }
        
        public string DigestHex(byte[] data)
        {
            var digest = Digest(data);
            return BitConverter.ToString(digest).Replace("-", "").ToLower();
        }
        
        public string DigestHex(string data)
        {
            return DigestHex(Encoding.UTF8.GetBytes(data));
        }
    }
}