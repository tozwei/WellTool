using System.Security.Cryptography;
using System.Text;

namespace WellTool.Crypto.Digest
{
    public class HMac
    {
        private HMacBase? _hmac;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="algorithm">HMAC算法（如 "SHA256"、"HMACSM3"）</param>
        /// <param name="key">密钥</param>
        public HMac(string algorithm, byte[] key)
        {
            _hmac = CreateHMac(algorithm, key);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="algorithm">HMAC算法枚举</param>
        /// <param name="key">密钥</param>
        public HMac(HmacAlgorithm algorithm, byte[] key)
        {
            string algoName = algorithm switch
            {
                HmacAlgorithm.HmacMD5 => "HMACMD5",
                HmacAlgorithm.HmacSHA1 => "HMACSHA1",
                HmacAlgorithm.HmacSHA256 => "HMACSHA256",
                HmacAlgorithm.HmacSHA384 => "HMACSHA384",
                HmacAlgorithm.HmacSHA512 => "HMACSHA512",
                _ => "HMAC" + algorithm.ToString().Replace("Hmac", "")
            };
            _hmac = CreateHMac(algoName, key);
        }

        private static HMacBase CreateHMac(string algorithm, byte[] key)
        {
            string upperAlgo = algorithm.ToUpperInvariant();
            
            // 使用 BouncyCastle 支持 SM3
            if (upperAlgo.Contains("SM3"))
            {
                var bouncyHmac = new Org.BouncyCastle.Crypto.Macs.HMac(new Org.BouncyCastle.Crypto.Digests.SM3Digest());
                bouncyHmac.Init(new Org.BouncyCastle.Crypto.Parameters.KeyParameter(key));
                return new BouncyHMacWrapper(bouncyHmac);
            }

            // 使用 .NET 内置 HMAC
            var netHmac = HMAC.Create(algorithm);
            if (netHmac == null)
            {
                throw new ArgumentException($"不支持的HMAC算法: {algorithm}");
            }
            netHmac.Key = key;
            return new NetHMacWrapper(netHmac);
        }

        public byte[] Digest(byte[] data)
        {
            if (_hmac == null) throw new InvalidOperationException("HMAC未初始化");
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

        private abstract class HMacBase
        {
            public abstract byte[] ComputeHash(byte[] data);
        }

        private class NetHMacWrapper : HMacBase
        {
            private readonly HMAC _hmac;
            public NetHMacWrapper(HMAC hmac) => _hmac = hmac;
            public override byte[] ComputeHash(byte[] data) => _hmac.ComputeHash(data);
        }

        private class BouncyHMacWrapper : HMacBase
        {
            private readonly Org.BouncyCastle.Crypto.Macs.HMac _hmac;
            public BouncyHMacWrapper(Org.BouncyCastle.Crypto.Macs.HMac hmac) => _hmac = hmac;
            public override byte[] ComputeHash(byte[] data)
            {
                var result = new byte[_hmac.GetMacSize()];
                _hmac.BlockUpdate(data, 0, data.Length);
                _hmac.DoFinal(result, 0);
                return result;
            }
        }
    }
}