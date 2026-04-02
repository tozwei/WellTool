namespace WellTool.Crypto.Digest.Otp
{
    public class HOTP : OTP
    {
        private long _counter;

        public HOTP(byte[] key, int digits = 6, string algorithm = "HmacSHA1", long counter = 0)
            : base(key, digits, algorithm)
        {
            _counter = counter;
        }

        public HOTP(string key, int digits = 6, string algorithm = "HmacSHA1", long counter = 0, System.Text.Encoding encoding = null)
            : base(key, digits, algorithm, encoding)
        {
            _counter = counter;
        }

        protected override byte[] GetDigest(long counter)
        {
            var counterBytes = new byte[8];
            for (var i = 7; i >= 0; i--)
            {
                counterBytes[i] = (byte)(counter & 0xFF);
                counter >>= 8;
            }
            return Algorithm.ComputeHash(counterBytes);
        }

        public string Generate()
        {
            return Generate(_counter++);
        }

        public string Generate(long counter)
        {
            _counter = counter + 1;
            return base.Generate(counter);
        }

        public bool Verify(string code)
        {
            return Verify(code, _counter);
        }

        public bool Verify(string code, long counter)
        {
            var result = base.Verify(code, counter);
            if (result)
            {
                _counter = counter + 1;
            }
            return result;
        }

        public long Counter => _counter;
    }
}