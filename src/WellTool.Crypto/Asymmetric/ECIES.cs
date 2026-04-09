using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Security;

namespace WellTool.Crypto.Asymmetric
{
    public class ECIES
    {
        private readonly IAsymmetricCipherKeyPairGenerator _keyGenerator;
        private readonly string _curveName;

        public ECIES(string curveName = "P-256")
        {
            _curveName = curveName;
            _keyGenerator = new ECKeyPairGenerator();
            
            // 获取曲线参数 - BouncyCastle 使用不同的命名
            X9ECParameters ecParams = X962NamedCurves.GetByName(_curveName);
            
            if (ecParams == null)
            {
                // 尝试NIST曲线名
                ecParams = ECNamedCurveTable.GetByName(_curveName);
            }
            
            if (ecParams == null)
            {
                throw new ArgumentException($"未找到指定的曲线: {curveName}");
            }
            
            var domainParams = new ECDomainParameters(
                ecParams.Curve,
                ecParams.G,
                ecParams.N,
                ecParams.H,
                ecParams.GetSeed());
            
            var parameters = new ECKeyGenerationParameters(domainParams, new SecureRandom());
            _keyGenerator.Init(parameters);
        }

        public AsymmetricCipherKeyPair GenerateKeyPair()
        {
            return _keyGenerator.GenerateKeyPair();
        }

        public byte[] Encrypt(byte[] data, AsymmetricKeyParameter publicKey)
        {
            var ecPublicKey = (ECPublicKeyParameters)publicKey;
            var ecParams = ecPublicKey.Parameters;
            var ephKeyPair = _keyGenerator.GenerateKeyPair();
            var ephPublicKey = (ECPublicKeyParameters)ephKeyPair.Public;
            var ephPrivateKey = (ECPrivateKeyParameters)ephKeyPair.Private;

            var sharedSecret = ecPublicKey.Q.Multiply(ephPrivateKey.D).Normalize();
            var derivedKey = DeriveKey(sharedSecret);

            var aesEngine = new AesEngine();
            var cbcMode = new CbcBlockCipher(aesEngine);
            var cipher = new PaddedBufferedBlockCipher(cbcMode);
            var iv = new byte[16];
            new SecureRandom().NextBytes(iv);
            var keyParam = new KeyParameter(derivedKey);
            var cipherParam = new ParametersWithIV(keyParam, iv);
            cipher.Init(true, cipherParam);

            var ciphertext = new byte[cipher.GetOutputSize(data.Length)];
            var len = cipher.ProcessBytes(data, 0, data.Length, ciphertext, 0);
            cipher.DoFinal(ciphertext, len);

            var ephPublicKeyEncoded = ephPublicKey.Q.GetEncoded();
            var result = new byte[ephPublicKeyEncoded.Length + iv.Length + ciphertext.Length];
            ephPublicKeyEncoded.CopyTo(result, 0);
            iv.CopyTo(result, ephPublicKeyEncoded.Length);
            ciphertext.CopyTo(result, ephPublicKeyEncoded.Length + iv.Length);

            return result;
        }

        public byte[] Decrypt(byte[] data, AsymmetricKeyParameter privateKey)
        {
            var ecPrivateKey = (ECPrivateKeyParameters)privateKey;
            var ecParams = ecPrivateKey.Parameters;

            // 确定点的编码长度（未压缩编码）
            var pointLength = 1 + (ecParams.Curve.FieldSize / 8) * 2;
            if (data.Length < pointLength + 16)
            {
                throw new ArgumentException("Data too short");
            }

            var ephPublicKeyPoint = ecParams.Curve.DecodePoint(data[..pointLength]);
            var iv = data[pointLength..(pointLength + 16)];
            var ciphertext = data[(pointLength + 16)..];

            var sharedSecret = ephPublicKeyPoint.Multiply(ecPrivateKey.D).Normalize();
            var derivedKey = DeriveKey(sharedSecret);

            var aesEngine = new AesEngine();
            var cbcMode = new CbcBlockCipher(aesEngine);
            var cipher = new PaddedBufferedBlockCipher(cbcMode);
            var keyParam = new KeyParameter(derivedKey);
            var cipherParam = new ParametersWithIV(keyParam, iv);
            cipher.Init(false, cipherParam);

            var plaintext = new byte[cipher.GetOutputSize(ciphertext.Length)];
            var len = cipher.ProcessBytes(ciphertext, 0, ciphertext.Length, plaintext, 0);
            var finalLen = cipher.DoFinal(plaintext, len);

            var result = new byte[len + finalLen];
            plaintext[..(len + finalLen)].CopyTo(result, 0);

            return result;
        }

        private byte[] DeriveKey(ECPoint point)
        {
            // 使用 SHA-256 哈希来生成固定长度的密钥
            var digest = new Sha256Digest();
            var encoded = point.AffineXCoord.GetEncoded();
            digest.BlockUpdate(encoded, 0, encoded.Length);
            var key = new byte[digest.GetDigestSize()];
            digest.DoFinal(key, 0);
            return key;
        }
    }
}