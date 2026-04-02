using System.Security.Cryptography;

namespace WellTool.Crypto.Symmetric
{
    public abstract class SymmetricDecryptor
    {
        protected SymmetricAlgorithm Algorithm { get; }

        protected SymmetricDecryptor(SymmetricAlgorithm algorithm)
        {
            Algorithm = algorithm;
        }

        public byte[] Decrypt(byte[] data, byte[] key, byte[] iv = null)
        {
            using (var decryptor = Algorithm.CreateDecryptor(key, iv ?? new byte[Algorithm.BlockSize / 8]))
            using (var ms = new System.IO.MemoryStream(data))
            using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            using (var output = new System.IO.MemoryStream())
            {
                cs.CopyTo(output);
                return output.ToArray();
            }
        }

        public string DecryptString(byte[] data, byte[] key, byte[] iv = null, System.Text.Encoding encoding = null)
        {
            encoding ??= System.Text.Encoding.UTF8;
            var decrypted = Decrypt(data, key, iv);
            return encoding.GetString(decrypted);
        }
    }
}