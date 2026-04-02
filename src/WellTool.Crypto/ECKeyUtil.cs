using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math.EC;
using Org.BouncyCastle.Security;

namespace WellTool.Crypto
{
    public static class ECKeyUtil
    {
        public static AsymmetricCipherKeyPair GenerateKeyPair(string curveName = "secp256r1")
        {
            var keyGenerator = new ECKeyPairGenerator();
            var ecParams = SecNamedCurves.GetByName(curveName);
            var parameters = new ECKeyGenerationParameters(new ECDomainParameters(ecParams), new SecureRandom());
            keyGenerator.Init(parameters);
            return keyGenerator.GenerateKeyPair();
        }

        public static byte[] GetPublicKey(AsymmetricCipherKeyPair keyPair)
        {
            var publicKey = (ECPublicKeyParameters)keyPair.Public;
            return publicKey.Q.GetEncoded();
        }

        public static byte[] GetPrivateKey(AsymmetricCipherKeyPair keyPair)
        {
            var privateKey = (ECPrivateKeyParameters)keyPair.Private;
            return privateKey.D.ToByteArray();
        }
    }
}