using System.Security.Cryptography;

namespace WellTool.Crypto
{
    public class CipherWrapper
    {
        private readonly SymmetricAlgorithm _algorithm;

        public CipherWrapper(SymmetricAlgorithm algorithm)
        {
            _algorithm = algorithm;
        }

        public byte[] Encrypt(byte[] data, byte[] key, byte[] iv = null)
        {
            using (var encryptor = _algorithm.CreateEncryptor(key, iv ?? new byte[_algorithm.BlockSize / 8]))
            using (var ms = new System.IO.MemoryStream())
            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            {
                cs.Write(data, 0, data.Length);
                cs.FlushFinalBlock();
                return ms.ToArray();
            }
        }

        public byte[] Decrypt(byte[] data, byte[] key, byte[] iv = null)
        {
            using (var decryptor = _algorithm.CreateDecryptor(key, iv ?? new byte[_algorithm.BlockSize / 8]))
            using (var ms = new System.IO.MemoryStream(data))
            using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            using (var output = new System.IO.MemoryStream())
            {
                cs.CopyTo(output);
                return output.ToArray();
            }
        }
    }
}