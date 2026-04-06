namespace WellTool.Crypto.Symmetric
{
    public class RC4
    {
        private byte[] _sbox;
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

            _i = 0;
            _j = 0;
        }

        public byte[] Encrypt(byte[] data)
        {
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
            return Encrypt(data); // RC4 加密和解密使用相同的算法
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
        /// 加密字符串并返回十六进制
        /// </summary>
        /// <param name="data">明文</param>
        /// <returns>密文（十六进制）</returns>
        public string EncryptHex(string data)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(data);
            var encrypted = Encrypt(bytes);
            return BitConverter.ToString(encrypted).Replace("-", "").ToLower();
        }

        /// <summary>
        /// 解密十六进制字符串
        /// </summary>
        /// <param name="data">密文（十六进制）</param>
        /// <returns>明文</returns>
        public string DecryptStr(string hexData)
        {
            var bytes = new byte[hexData.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hexData.Substring(i * 2, 2), 16);
            }
            var decrypted = Decrypt(bytes);
            return System.Text.Encoding.UTF8.GetString(decrypted);
        }

        private static void Swap(byte[] array, int i, int j)
        {
            var temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}