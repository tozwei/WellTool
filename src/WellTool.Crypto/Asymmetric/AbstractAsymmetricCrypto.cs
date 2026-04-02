using System;
using System.Security.Cryptography;

namespace WellTool.Crypto.Asymmetric
{
    public abstract class AbstractAsymmetricCrypto
    {
        protected abstract AsymmetricAlgorithm CreateAlgorithm();

        public byte[] Encrypt(byte[] data, KeyType keyType)
        {
            var algorithm = CreateAlgorithm();
            return keyType switch
            {
                KeyType.PublicKey => EncryptWithPublicKey(data, algorithm),
                KeyType.PrivateKey => EncryptWithPrivateKey(data, algorithm),
                _ => throw new ArgumentException("Invalid key type")
            };
        }

        public byte[] Decrypt(byte[] data, KeyType keyType)
        {
            var algorithm = CreateAlgorithm();
            return keyType switch
            {
                KeyType.PrivateKey => DecryptWithPrivateKey(data, algorithm),
                KeyType.PublicKey => DecryptWithPublicKey(data, algorithm),
                _ => throw new ArgumentException("Invalid key type")
            };
        }

        protected abstract byte[] EncryptWithPublicKey(byte[] data, AsymmetricAlgorithm algorithm);
        protected abstract byte[] EncryptWithPrivateKey(byte[] data, AsymmetricAlgorithm algorithm);
        protected abstract byte[] DecryptWithPrivateKey(byte[] data, AsymmetricAlgorithm algorithm);
        protected abstract byte[] DecryptWithPublicKey(byte[] data, AsymmetricAlgorithm algorithm);
    }
}