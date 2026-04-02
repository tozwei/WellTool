using System.Security.Cryptography;

namespace WellTool.Crypto.Digest
{
    public static class DigesterFactory
    {
        public static HashAlgorithm Create(DigestAlgorithm algorithm)
        {
            return algorithm switch
            {
                DigestAlgorithm.MD5 => MD5.Create(),
                DigestAlgorithm.SHA1 => SHA1.Create(),
                DigestAlgorithm.SHA256 => SHA256.Create(),
                DigestAlgorithm.SHA384 => SHA384.Create(),
                DigestAlgorithm.SHA512 => SHA512.Create(),
                _ => throw new System.ArgumentException("Invalid digest algorithm")
            };
        }

        public static HMAC CreateHmac(HmacAlgorithm algorithm, byte[] key)
        {
            return algorithm switch
            {
                HmacAlgorithm.HmacMD5 => new HMACMD5(key),
                HmacAlgorithm.HmacSHA1 => new HMACSHA1(key),
                HmacAlgorithm.HmacSHA256 => new HMACSHA256(key),
                HmacAlgorithm.HmacSHA384 => new HMACSHA384(key),
                HmacAlgorithm.HmacSHA512 => new HMACSHA512(key),
                _ => throw new System.ArgumentException("Invalid HMAC algorithm")
            };
        }
    }
}