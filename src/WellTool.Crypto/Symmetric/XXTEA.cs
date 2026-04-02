namespace WellTool.Crypto.Symmetric
{
    public class XXTEA
    {
        private const uint DELTA = 0x9E3779B9;

        public static byte[] Encrypt(byte[] data, byte[] key)
        {
            if (data.Length == 0)
                return data;

            var v = ToUInt32Array(Pad(data));
            var k = ToUInt32Array(Pad(key, 16));
            var n = v.Length - 1;

            uint z = v[n], y = v[0], sum = 0, e, p, q;
            q = (uint)(6 + 52 / (n + 1));

            while (q-- > 0)
            {
                sum += DELTA;
                e = (sum >> 2) & 3;
                for (p = 0; p < n; p++)
                {
                    y = v[p + 1];
                    z = v[p] += ((z >> 5 ^ y << 2) + (y >> 3 ^ z << 4)) ^ ((sum ^ y) + (k[(p & 3) ^ e] ^ z));
                }
                y = v[0];
                z = v[n] += ((z >> 5 ^ y << 2) + (y >> 3 ^ z << 4)) ^ ((sum ^ y) + (k[(n & 3) ^ e] ^ z));
            }

            return ToByteArray(v);
        }

        public static byte[] Decrypt(byte[] data, byte[] key)
        {
            if (data.Length == 0)
                return data;

            var v = ToUInt32Array(data);
            var k = ToUInt32Array(Pad(key, 16));
            var n = v.Length - 1;

            uint z = v[n], y = v[0], sum, e, p, q;
            q = (uint)(6 + 52 / (n + 1));
            sum = q * DELTA;

            while (sum != 0)
            {
                e = (sum >> 2) & 3;
                for (p = (uint)n; p > 0; p--)
                {
                    z = v[p - 1];
                    y = v[p] -= ((z >> 5 ^ y << 2) + (y >> 3 ^ z << 4)) ^ ((sum ^ y) + (k[(p & 3) ^ e] ^ z));
                }
                z = v[n];
                y = v[0] -= ((z >> 5 ^ y << 2) + (y >> 3 ^ z << 4)) ^ ((sum ^ y) + (k[(0 & 3) ^ e] ^ z));
                sum -= DELTA;
            }

            return Unpad(ToByteArray(v));
        }

        public static string EncryptString(string data, string key, System.Text.Encoding encoding = null)
        {
            encoding ??= System.Text.Encoding.UTF8;
            var bytes = encoding.GetBytes(data);
            var encrypted = Encrypt(bytes, encoding.GetBytes(key));
            return System.Convert.ToBase64String(encrypted);
        }

        public static string DecryptString(string data, string key, System.Text.Encoding encoding = null)
        {
            encoding ??= System.Text.Encoding.UTF8;
            var bytes = System.Convert.FromBase64String(data);
            var decrypted = Decrypt(bytes, encoding.GetBytes(key));
            return encoding.GetString(decrypted);
        }

        private static byte[] Pad(byte[] data, int length = -1)
        {
            var blockSize = 4;
            var padLength = length > 0 ? length : blockSize - (data.Length % blockSize);
            var result = new byte[data.Length + padLength];
            data.CopyTo(result, 0);
            for (var i = data.Length; i < result.Length; i++)
            {
                result[i] = (byte)padLength;
            }
            return result;
        }

        private static byte[] Unpad(byte[] data)
        {
            if (data.Length == 0)
                return data;

            var padLength = data[data.Length - 1];
            if (padLength > data.Length)
                return data;

            var result = new byte[data.Length - padLength];
            System.Array.Copy(data, 0, result, 0, result.Length);
            return result;
        }

        private static uint[] ToUInt32Array(byte[] data)
        {
            var length = data.Length / 4;
            var result = new uint[length];
            for (var i = 0; i < length; i++)
            {
                result[i] = (uint)(data[i * 4] | (data[i * 4 + 1] << 8) | (data[i * 4 + 2] << 16) | (data[i * 4 + 3] << 24));
            }
            return result;
        }

        private static byte[] ToByteArray(uint[] data)
        {
            var length = data.Length * 4;
            var result = new byte[length];
            for (var i = 0; i < data.Length; i++)
            {
                result[i * 4] = (byte)(data[i] & 0xFF);
                result[i * 4 + 1] = (byte)((data[i] >> 8) & 0xFF);
                result[i * 4 + 2] = (byte)((data[i] >> 16) & 0xFF);
                result[i * 4 + 3] = (byte)((data[i] >> 24) & 0xFF);
            }
            return result;
        }
    }
}