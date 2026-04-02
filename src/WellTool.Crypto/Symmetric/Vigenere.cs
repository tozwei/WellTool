namespace WellTool.Crypto.Symmetric
{
    public class Vigenere
    {
        private readonly string _key;
        private readonly System.Text.Encoding _encoding;

        public Vigenere(string key, System.Text.Encoding encoding = null)
        {
            _key = key;
            _encoding = encoding ?? System.Text.Encoding.UTF8;
        }

        public string Encrypt(string plaintext)
        {
            var key = _key.ToUpper();
            var result = new System.Text.StringBuilder();

            for (var i = 0; i < plaintext.Length; i++)
            {
                var c = plaintext[i];
                if (char.IsLetter(c))
                {
                    var offset = char.IsUpper(c) ? 'A' : 'a';
                    var keyIndex = i % key.Length;
                    var keyChar = key[keyIndex];
                    var shift = keyChar - 'A';
                    var encrypted = (char)((c - offset + shift) % 26 + offset);
                    result.Append(encrypted);
                }
                else
                {
                    result.Append(c);
                }
            }

            return result.ToString();
        }

        public string Decrypt(string ciphertext)
        {
            var key = _key.ToUpper();
            var result = new System.Text.StringBuilder();

            for (var i = 0; i < ciphertext.Length; i++)
            {
                var c = ciphertext[i];
                if (char.IsLetter(c))
                {
                    var offset = char.IsUpper(c) ? 'A' : 'a';
                    var keyIndex = i % key.Length;
                    var keyChar = key[keyIndex];
                    var shift = keyChar - 'A';
                    var decrypted = (char)((c - offset - shift + 26) % 26 + offset);
                    result.Append(decrypted);
                }
                else
                {
                    result.Append(c);
                }
            }

            return result.ToString();
        }

        public byte[] Encrypt(byte[] data)
        {
            var plaintext = _encoding.GetString(data);
            var encrypted = Encrypt(plaintext);
            return _encoding.GetBytes(encrypted);
        }

        public byte[] Decrypt(byte[] data)
        {
            var ciphertext = _encoding.GetString(data);
            var decrypted = Decrypt(ciphertext);
            return _encoding.GetBytes(decrypted);
        }
    }
}