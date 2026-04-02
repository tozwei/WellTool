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

        private static void Swap(byte[] array, int i, int j)
        {
            var temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}