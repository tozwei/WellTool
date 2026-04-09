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
            // 实现 FF1 格式保持加密算法
            // 这是一个简化实现，基于 Feistel 网络结构
            if (data == null || data.Length == 0)
                return data;

            using var aes = Aes.Create();
            aes.Key = key;
            aes.Mode = System.Security.Cryptography.CipherMode.ECB;
            aes.Padding = PaddingMode.Zeros;

            var result = new byte[data.Length];
            Array.Copy(data, result, data.Length);

            // 简化的 Feistel 网络，进行 8 轮加密
            for (int round = 0; round < 8; round++)
            {
                var left = new byte[result.Length / 2];
                var right = new byte[result.Length - left.Length];
                Array.Copy(result, 0, left, 0, left.Length);
                Array.Copy(result, left.Length, right, 0, right.Length);

                // 使用 AES 作为轮函数
                var roundKey = new byte[16];
                Array.Copy(tweak, 0, roundKey, 0, Math.Min(tweak.Length, 16));
                roundKey[round % 16] ^= (byte)round;

                using var encryptor = aes.CreateEncryptor(aes.Key, roundKey);
                var roundResult = new byte[16];
                encryptor.TransformBlock(right, 0, right.Length, roundResult, 0);

                // 混合左右两部分
                for (int i = 0; i < left.Length; i++)
                {
                    left[i] ^= roundResult[i % 16];
                }

                // 交换左右两部分
                Array.Copy(right, 0, result, 0, right.Length);
                Array.Copy(left, 0, result, right.Length, left.Length);
            }

            return result;
        }

        public byte[] Decrypt(byte[] data, byte[] key, byte[] tweak)
        {
            // 实现 FF1 格式保持解密算法
            // 这是一个简化实现，基于 Feistel 网络结构
            if (data == null || data.Length == 0)
                return data;

            using var aes = Aes.Create();
            aes.Key = key;
            aes.Mode = System.Security.Cryptography.CipherMode.ECB;
            aes.Padding = PaddingMode.Zeros;

            var result = new byte[data.Length];
            Array.Copy(data, result, data.Length);

            // 简化的 Feistel 网络，进行 8 轮解密（与加密顺序相反）
            for (int round = 7; round >= 0; round--)
            {
                var left = new byte[result.Length / 2];
                var right = new byte[result.Length - left.Length];
                Array.Copy(result, 0, left, 0, left.Length);
                Array.Copy(result, left.Length, right, 0, right.Length);

                // 使用 AES 作为轮函数
                var roundKey = new byte[16];
                Array.Copy(tweak, 0, roundKey, 0, Math.Min(tweak.Length, 16));
                roundKey[round % 16] ^= (byte)round;

                using var encryptor = aes.CreateEncryptor(aes.Key, roundKey);
                var roundResult = new byte[16];
                encryptor.TransformBlock(left, 0, left.Length, roundResult, 0);

                // 混合左右两部分
                for (int i = 0; i < right.Length; i++)
                {
                    right[i] ^= roundResult[i % 16];
                }

                // 交换左右两部分
                Array.Copy(right, 0, result, 0, right.Length);
                Array.Copy(left, 0, result, right.Length, left.Length);
            }

            return result;
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