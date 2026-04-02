using System;

namespace WellTool.Crypto.Digest.Otp
{
    public class TOTP : OTP
    {
        private readonly int _timeStep; // 时间步长，默认为 30 秒

        public TOTP(byte[] key, int digits = 6, string algorithm = "HmacSHA1", int timeStep = 30)
            : base(key, digits, algorithm)
        {
            _timeStep = timeStep;
        }

        public TOTP(string key, int digits = 6, string algorithm = "HmacSHA1", int timeStep = 30, System.Text.Encoding encoding = null)
            : base(key, digits, algorithm, encoding)
        {
            _timeStep = timeStep;
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
            return Generate(GetCurrentCounter());
        }

        public string Generate(DateTime timestamp)
        {
            var counter = (long)Math.Floor((timestamp - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds / _timeStep);
            return Generate(counter);
        }

        public bool Verify(string code)
        {
            return Verify(code, GetCurrentCounter());
        }

        public bool Verify(string code, DateTime timestamp)
        {
            var counter = (long)Math.Floor((timestamp - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds / _timeStep);
            return Verify(code, counter);
        }

        public bool Verify(string code, int window = 1)
        {
            var currentCounter = GetCurrentCounter();
            for (var i = -window; i <= window; i++)
            {
                if (Verify(code, currentCounter + i))
                {
                    return true;
                }
            }
            return false;
        }

        private long GetCurrentCounter()
        {
            return (long)Math.Floor((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds / _timeStep);
        }
    }
}