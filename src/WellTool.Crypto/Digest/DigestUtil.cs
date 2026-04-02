using System.Security.Cryptography;
using System.Text;

namespace WellTool.Crypto.Digest
{
    public static class DigestUtil
    {
        public static byte[] Digest(byte[] data, DigestAlgorithm algorithm)
        {
            using (var hash = DigesterFactory.Create(algorithm))
            {
                return hash.ComputeHash(data);
            }
        }

        public static string DigestHex(byte[] data, DigestAlgorithm algorithm)
        {
            var digest = Digest(data, algorithm);
            return BitConverter.ToString(digest).Replace("-", "").ToLower();
        }

        public static string DigestHex(string data, DigestAlgorithm algorithm, Encoding encoding = null)
        {
            encoding ??= Encoding.UTF8;
            return DigestHex(encoding.GetBytes(data), algorithm);
        }

        public static byte[] Hmac(byte[] data, byte[] key, HmacAlgorithm algorithm)
        {
            using (var hmac = DigesterFactory.CreateHmac(algorithm, key))
            {
                return hmac.ComputeHash(data);
            }
        }

        public static string HmacHex(byte[] data, byte[] key, HmacAlgorithm algorithm)
        {
            var hmac = Hmac(data, key, algorithm);
            return BitConverter.ToString(hmac).Replace("-", "").ToLower();
        }

        public static string HmacHex(string data, string key, HmacAlgorithm algorithm, Encoding encoding = null)
        {
            encoding ??= Encoding.UTF8;
            return HmacHex(encoding.GetBytes(data), encoding.GetBytes(key), algorithm);
        }
    }
}