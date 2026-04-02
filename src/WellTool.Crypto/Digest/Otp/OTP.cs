using System.Security.Cryptography;
using System.Text;

namespace WellTool.Crypto.Digest.Otp
{
    public abstract class OTP
    {
        protected byte[] Key { get; }
        protected int Digits { get; }
        protected HMAC Algorithm { get; }

        public OTP(byte[] key, int digits = 6, string algorithm = "HmacSHA1")
        {
            Key = key;
            Digits = digits;
            Algorithm = CreateHMAC(algorithm, key);
        }

        public OTP(string key, int digits = 6, string algorithm = "HmacSHA1", Encoding encoding = null)
        {
            encoding ??= Encoding.UTF8;
            Key = encoding.GetBytes(key);
            Digits = digits;
            Algorithm = CreateHMAC(algorithm, Key);
        }

        protected abstract byte[] GetDigest(long counter);

        public string Generate(long counter)
        {
            var digest = GetDigest(counter);
            var offset = digest[digest.Length - 1] & 0x0F;
            var code = (digest[offset] & 0x7F) << 24 |
                       (digest[offset + 1] & 0xFF) << 16 |
                       (digest[offset + 2] & 0xFF) << 8 |
                       (digest[offset + 3] & 0xFF);
            var mod = (int)Math.Pow(10, Digits);
            return (code % mod).ToString().PadLeft(Digits, '0');
        }

        public bool Verify(string code, long counter)
        {
            return Generate(counter).Equals(code);
        }

        private HMAC CreateHMAC(string algorithm, byte[] key)
        {
            return algorithm switch
            {
                "HmacSHA1" => new HMACSHA1(key),
                "HmacSHA256" => new HMACSHA256(key),
                "HmacSHA512" => new HMACSHA512(key),
                _ => throw new ArgumentException("Invalid algorithm")
            };
        }
    }
}