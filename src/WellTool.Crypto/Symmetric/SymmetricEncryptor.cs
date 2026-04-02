using System.Security.Cryptography;

namespace WellTool.Crypto.Symmetric
{
    public abstract class SymmetricEncryptor
    {
        protected SymmetricAlgorithm Algorithm { get; }

        protected SymmetricEncryptor(SymmetricAlgorithm algorithm)
        {
            Algorithm = algorithm;
        }

        public byte[] Encrypt(byte[] data, byte[] key, byte[] iv = null)
        {
            using (var encryptor = Algorithm.CreateEncryptor(key, iv ?? new byte[Algorithm.BlockSize / 8]))
            using (var ms = new System.IO.MemoryStream())
            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            {
                cs.Write(data, 0, data.Length);
                cs.FlushFinalBlock();
                return ms.ToArray();
            }
        }

        public byte[] EncryptString(string data, byte[] key, byte[] iv = null, System.Text.Encoding encoding = null)
        {
            encoding ??= System.Text.Encoding.UTF8;
            var bytes = encoding.GetBytes(data);
            return Encrypt(bytes, key, iv);
        }
    }
}