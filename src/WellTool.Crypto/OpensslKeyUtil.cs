using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.OpenSsl;

namespace WellTool.Crypto
{
    public static class OpensslKeyUtil
    {
        public static AsymmetricCipherKeyPair ReadPrivateKey(string privateKeyPem)
        {
            using (var reader = new StringReader(privateKeyPem))
            {
                var pemReader = new PemReader(reader);
                return (AsymmetricCipherKeyPair)pemReader.ReadObject();
            }
        }

        public static AsymmetricKeyParameter ReadPublicKey(string publicKeyPem)
        {
            using (var reader = new StringReader(publicKeyPem))
            {
                var pemReader = new PemReader(reader);
                return (AsymmetricKeyParameter)pemReader.ReadObject();
            }
        }

        public static string WritePrivateKey(AsymmetricCipherKeyPair keyPair)
        {
            using (var writer = new StringWriter())
            {
                var pemWriter = new PemWriter(writer);
                pemWriter.WriteObject(keyPair.Private);
                pemWriter.Writer.Flush();
                return writer.ToString();
            }
        }

        public static string WritePublicKey(AsymmetricKeyParameter publicKey)
        {
            using (var writer = new StringWriter())
            {
                var pemWriter = new PemWriter(writer);
                pemWriter.WriteObject(publicKey);
                pemWriter.Writer.Flush();
                return writer.ToString();
            }
        }
    }
}