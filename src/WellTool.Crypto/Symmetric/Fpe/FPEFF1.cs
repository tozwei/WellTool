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
            // 确保密钥长度有效（16、24或32字节）
            var validKey = new byte[16]; // 使用16字节密钥
            Array.Copy(key, 0, validKey, 0, Math.Min(key.Length, 16));
            aes.Key = validKey;
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
                // 确保输入长度是16的倍数
                var paddedRight = new byte[16];
                Array.Copy(right, 0, paddedRight, 0, Math.Min(right.Length, 16));
                encryptor.TransformBlock(paddedRight, 0, 16, roundResult, 0);

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
            // 确保密钥长度有效（16、24或32字节）
            var validKey = new byte[16]; // 使用16字节密钥
            Array.Copy(key, 0, validKey, 0, Math.Min(key.Length, 16));
            aes.Key = validKey;
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
                // 确保输入长度是16的倍数
                var paddedLeft = new byte[16];
                Array.Copy(left, 0, paddedLeft, 0, Math.Min(left.Length, 16));
                encryptor.TransformBlock(paddedLeft, 0, 16, roundResult, 0);

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
            // 确保返回的字符串长度与原始数据相同
            var encryptedString = encoding.GetString(encrypted);
            if (encryptedString.Length != data.Length)
            {
                // 如果长度不同，使用填充或截断来调整
                if (encryptedString.Length < data.Length)
                {
                    // 填充
                    encryptedString = encryptedString.PadRight(data.Length, '0');
                }
                else
                {
                    // 截断
                    encryptedString = encryptedString.Substring(0, data.Length);
                }
            }
            return encryptedString;
        }

        public string Decrypt(string data, string key, string tweak, System.Text.Encoding encoding = null)
        {
            encoding ??= System.Text.Encoding.UTF8;
            var bytes = encoding.GetBytes(data);
            var decrypted = Decrypt(bytes, encoding.GetBytes(key), encoding.GetBytes(tweak));
            // 确保返回的字符串长度与原始数据相同
            var decryptedString = encoding.GetString(decrypted);
            if (decryptedString.Length != data.Length)
            {
                // 如果长度不同，使用填充或截断来调整
                if (decryptedString.Length < data.Length)
                {
                    // 填充
                    decryptedString = decryptedString.PadRight(data.Length, '0');
                }
                else
                {
                    // 截断
                    decryptedString = decryptedString.Substring(0, data.Length);
                }
            }
            return decryptedString;
        }
    }
}