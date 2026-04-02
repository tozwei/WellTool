using System.Security.Cryptography;

namespace WellTool.Crypto
{
    public static class SignUtil
    {
        public static byte[] Sign(byte[] data, RSA privateKey, HashAlgorithmName hashAlgorithm = default)
        {
            hashAlgorithm = hashAlgorithm == default ? HashAlgorithmName.SHA256 : hashAlgorithm;
            return privateKey.SignData(data, hashAlgorithm, RSASignaturePadding.Pkcs1);
        }

        public static bool Verify(byte[] data, byte[] signature, RSA publicKey, HashAlgorithmName hashAlgorithm = default)
        {
            hashAlgorithm = hashAlgorithm == default ? HashAlgorithmName.SHA256 : hashAlgorithm;
            return publicKey.VerifyData(data, signature, hashAlgorithm, RSASignaturePadding.Pkcs1);
        }

        public static string SignHex(byte[] data, RSA privateKey, HashAlgorithmName hashAlgorithm = default)
        {
            var signature = Sign(data, privateKey, hashAlgorithm);
            return BitConverter.ToString(signature).Replace("-", "").ToLower();
        }

        public static bool VerifyHex(byte[] data, string signatureHex, RSA publicKey, HashAlgorithmName hashAlgorithm = default)
        {
            var signature = ConvertHexToBytes(signatureHex);
            return Verify(data, signature, publicKey, hashAlgorithm);
        }

        private static byte[] ConvertHexToBytes(string hex)
        {
            var bytes = new byte[hex.Length / 2];
            for (var i = 0; i < hex.Length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }
    }
}