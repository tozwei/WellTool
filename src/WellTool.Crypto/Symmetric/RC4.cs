using System.Text;
using System.Text.RegularExpressions;

namespace WellTool.Crypto.Symmetric
{
    public class RC4
    {
        private byte[] _sbox;
        private byte[] _initialBox;
        private int _i;
        private int _j;

        public RC4(byte[] key)
        {
            Initialize(key);
        }

        public RC4(string key, System.Text.Encoding encoding = null)
        {
            encoding ??= System.Text.Encoding.UTF8;
            Initialize(encoding.GetBytes(key));
        }

        private void Initialize(byte[] key)
        {
            _sbox = new byte[256];
            for (var i = 0; i < 256; i++)
            {
                _sbox[i] = (byte)i;
            }

            var j = 0;
            for (var i = 0; i < 256; i++)
            {
                j = (j + _sbox[i] + key[i % key.Length]) % 256;
                Swap(_sbox, i, j);
            }

            // 保存初始状态用于重置
            _initialBox = (byte[])_sbox.Clone();
            _i = 0;
            _j = 0;
        }

        /// <summary>
        /// 重置RC4状态到初始状态
        /// </summary>
        public void Reset()
        {
            _sbox = (byte[])_initialBox.Clone();
            _i = 0;
            _j = 0;
        }

        public byte[] Encrypt(byte[] data)
        {
            Reset(); // 重置状态确保每次加密从相同状态开始
            var result = new byte[data.Length];
            for (var k = 0; k < data.Length; k++)
            {
                _i = (_i + 1) % 256;
                _j = (_j + _sbox[_i]) % 256;
                Swap(_sbox, _i, _j);
                var t = (_sbox[_i] + _sbox[_j]) % 256;
                result[k] = (byte)(data[k] ^ _sbox[t]);
            }
            return result;
        }

        public byte[] Decrypt(byte[] data)
        {
            Reset(); // 重置状态确保解密与加密使用相同状态
            var result = new byte[data.Length];
            for (var k = 0; k < data.Length; k++)
            {
                _i = (_i + 1) % 256;
                _j = (_j + _sbox[_i]) % 256;
                Swap(_sbox, _i, _j);
                var t = (_sbox[_i] + _sbox[_j]) % 256;
                result[k] = (byte)(data[k] ^ _sbox[t]);
            }
            return result;
        }

        public string EncryptString(string data, System.Text.Encoding encoding = null)
        {
            encoding ??= System.Text.Encoding.UTF8;
            var bytes = encoding.GetBytes(data);
            var encrypted = Encrypt(bytes);
            return System.Convert.ToBase64String(encrypted);
        }

        public string DecryptString(string data, System.Text.Encoding encoding = null)
        {
            encoding ??= System.Text.Encoding.UTF8;
            var bytes = System.Convert.FromBase64String(data);
            var decrypted = Decrypt(bytes);
            return encoding.GetString(decrypted);
        }

        /// <summary>
        /// 加密字符串并返回十六进制密文
        /// </summary>
        /// <param name="data">明文</param>
        /// <returns>密文（十六进制）</returns>
        public string EncryptHex(string data)
        {
            var bytes = Encoding.UTF8.GetBytes(data);
            var encrypted = Encrypt(bytes);

            #if NET6_0_OR_GREATER
                        // .NET 6.0+：使用高效的 Convert.ToHexString
                        return Convert.ToHexString(encrypted).ToLower();
            #else
                // .NET 6.0 以下（含 .NET Standard 2.1）：使用 BitConverter
                // BitConverter.ToString 会生成 "A1-B2-C3" 格式，需要移除 "-"
                return BitConverter.ToString(encrypted).Replace("-", string.Empty).ToLower();
            #endif
        }

        /// <summary>
        /// 解密十六进制字符串
        /// </summary>
        /// <param name="hexData">密文（十六进制）</param>
        /// <returns>明文</returns>
        public string DecryptStr(string hexData)
        {
            byte[] bytes;

            #if NET6_0_OR_GREATER
                        // .NET 6.0+：直接解析
                        bytes = Convert.FromHexString(hexData);
            #else
                // .NET 6.0 以下（含 .NET Standard 2.1）：手动解析
                // 1. 移除可能存在的 "-" 连字符（兼容 BitConverter 生成的格式）
                string cleanHex = Regex.Replace(hexData, "-", "");
    
                // 2. 每两个字符解析为一个字节
                bytes = new byte[cleanHex.Length / 2];
                for (int i = 0; i < cleanHex.Length; i += 2)
                {
                    // Convert.ToByte(string, 16) 将十六进制字符串转换为字节
                    bytes[i / 2] = Convert.ToByte(cleanHex.Substring(i, 2), 16);
                }
            #endif

            var decrypted = Decrypt(bytes);
            return Encoding.UTF8.GetString(decrypted);
        }

        private static void Swap(byte[] array, int i, int j)
        {
            var temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}