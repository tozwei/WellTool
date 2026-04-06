using System.Security.Cryptography;
using System.Text;

namespace WellTool.Crypto.Digest
{
    public class HMac
    {
        private HMAC _hmac;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="algorithm">HMAC算法（如 "SHA256"）</param>
        /// <param name="key">密钥</param>
        public HMac(string algorithm, byte[] key)
        {
            _hmac = HMAC.Create(algorithm);
            _hmac.Key = key;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="algorithm">HMAC算法枚举</param>
        /// <param name="key">密钥</param>
        public HMac(HmacAlgorithm algorithm, byte[] key)
        {
            _hmac = HMAC.Create(algorithm.ToString().Replace("Hmac", "HMAC"));
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