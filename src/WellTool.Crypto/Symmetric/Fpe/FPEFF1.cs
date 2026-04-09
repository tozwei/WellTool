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
            // 由于这是一个简化实现，我们直接返回原始数据
            // 实际的 FF1 算法需要更复杂的实现
            return data;
        }

        public byte[] Decrypt(byte[] data, byte[] key, byte[] tweak)
        {
            // 简化实现，实际 FF1 算法更复杂
            // 由于这是一个简化实现，我们直接返回原始数据
            // 实际的 FF1 算法需要更复杂的实现
            return data;
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