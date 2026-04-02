using System.Security.Cryptography;

namespace WellTool.Crypto.Digest.Mac
{
    public class HMacEngine : MacEngine
    {
        private HMAC _hmac;
        private MacAlgorithm _algorithm;

        public HMacEngine(MacAlgorithm algorithm)
        {
            _algorithm = algorithm;
        }

        public void Init(byte[] key)
        {
            _hmac = _algorithm switch
            {
                MacAlgorithm.HmacMD5 => new HMACMD5(key),
                MacAlgorithm.HmacSHA1 => new HMACSHA1(key),
                MacAlgorithm.HmacSHA256 => new HMACSHA256(key),
                MacAlgorithm.HmacSHA384 => new HMACSHA384(key),
                MacAlgorithm.HmacSHA512 => new HMACSHA512(key),
                _ => throw new System.ArgumentException("Invalid MAC algorithm")
            };
        }

        public void Update(byte[] data)
        {
            _hmac.TransformBlock(data, 0, data.Length, null, 0);
        }

        public byte[] DoFinal()
        {
            _hmac.TransformFinalBlock(new byte[0], 0, 0);
            var result = _hmac.Hash;
            Reset();
            return result;
        }

        public void Reset()
        {
            _hmac.Initialize();
        }
    }
}