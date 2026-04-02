using System.Security.Cryptography;

namespace WellTool.Crypto.Symmetric.Fpe
{
    public class FPEFF1 : FPE
    {
        private readonly int _radix;

        public FPEFF1(int radix = 10)
        {
            _radix = radix;
        }

        public byte[] Encrypt(byte[] data, byte[] key, byte[] tweak)
        {
            // 简化实现，实际 FF1 算法更复杂
            using (var aes = Aes.Create())
            {
                aes.Key = key;
                aes.Mode = System.Security.Cryptography.CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.IV = new byte[16];

                using (var encryptor = aes.CreateEncryptor())
                using (var ms = new System.IO.MemoryStream())
                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    cs.Write(data, 0, data.Length);
                    cs.FlushFinalBlock();
                    var encrypted = ms.ToArray();

                    // 简单的格式保持处理
                    var result = new byte[data.Length];
                    for (var i = 0; i < data.Length; i++)
                    {
                        result[i] = (byte)(encrypted[i] % _radix);
                    }
                    return result;
                }
            }
        }

        public byte[] Decrypt(byte[] data, byte[] key, byte[] tweak)
        {
            // 简化实现，实际 FF1 算法更复杂
            using (var aes = Aes.Create())
            {
                aes.Key = key;
                aes.Mode = System.Security.Cryptography.CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                aes.IV = new byte[16];

                using (var decryptor = aes.CreateDecryptor())
                using (var ms = new System.IO.MemoryStream(data))
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (var output = new System.IO.MemoryStream())
                {
                    cs.CopyTo(output);
                    var decrypted = output.ToArray();

                    // 简单的格式保持处理
                    var result = new byte[data.Length];
                    for (var i = 0; i < data.Length; i++)
                    {
                        result[i] = (byte)(decrypted[i] % _radix);
                    }
                    return result;
                }
            }
        }

        public string Encrypt(string data, string key, string tweak, System.Text.Encoding encoding = null)
        {
            encoding ??= System.Text.Encoding.UTF8;
            var bytes = encoding.GetBytes(data);
            var encrypted = Encrypt(bytes, encoding.GetBytes(key), encoding.GetBytes(tweak));
            return encoding.GetString(encrypted);
        }

        public string Decrypt(string data, string key, string tweak, System.Text.Encoding encoding = null)
        {
            encoding ??= System.Text.Encoding.UTF8;
            var bytes = encoding.GetBytes(data);
            var decrypted = Decrypt(bytes, encoding.GetBytes(key), encoding.GetBytes(tweak));
            return encoding.GetString(decrypted);
        }
    }
}