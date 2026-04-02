using System.Security.Cryptography;
using RSACryptoServiceProvider = System.Security.Cryptography.RSA;

namespace WellTool.Crypto.Asymmetric
{
    public abstract class BaseAsymmetric : AbstractAsymmetricCrypto
    {
        protected AsymmetricAlgorithm Algorithm { get; set; }

        protected override AsymmetricAlgorithm CreateAlgorithm()
        {
            if (Algorithm == null)
            {
                Algorithm = CreateSpecificAlgorithm();
            }
            return Algorithm;
        }

        protected abstract AsymmetricAlgorithm CreateSpecificAlgorithm();

        protected override byte[] EncryptWithPublicKey(byte[] data, AsymmetricAlgorithm algorithm)
        {
            var rsa = algorithm as System.Security.Cryptography.RSA;
            if (rsa != null)
            {
                return rsa.Encrypt(data, System.Security.Cryptography.RSAEncryptionPadding.OaepSHA256);
            }
            throw new System.NotSupportedException("Algorithm not supported");
        }

        protected override byte[] EncryptWithPrivateKey(byte[] data, AsymmetricAlgorithm algorithm)
        {
            var rsa = algorithm as System.Security.Cryptography.RSA;
            if (rsa != null)
            {
                return rsa.SignData(data, System.Security.Cryptography.HashAlgorithmName.SHA256, System.Security.Cryptography.RSASignaturePadding.Pkcs1);
            }
            throw new System.NotSupportedException("Algorithm not supported");
        }

        protected override byte[] DecryptWithPrivateKey(byte[] data, AsymmetricAlgorithm algorithm)
        {
            var rsa = algorithm as System.Security.Cryptography.RSA;
            if (rsa != null)
            {
                return rsa.Decrypt(data, System.Security.Cryptography.RSAEncryptionPadding.OaepSHA256);
            }
            throw new System.NotSupportedException("Algorithm not supported");
        }

        protected override byte[] DecryptWithPublicKey(byte[] data, AsymmetricAlgorithm algorithm)
        {
            throw new System.NotSupportedException("Public key decryption not supported");
        }
    }
}