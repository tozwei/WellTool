using System;
using System.Text;
using BCrypt.Net;

namespace WellTool.Crypto.Digest
{
    public class BCrypt
    {
        public static string Hash(string password, int workFactor = 12)
        {
            return BCrypt.HashPassword(password, workFactor);
        }

        public static bool Verify(string password, string hash)
        {
            return BCrypt.Verify(password, hash);
        }

        public static bool Verify(byte[] password, string hash, Encoding encoding = null)
        {
            encoding ??= Encoding.UTF8;
            return BCrypt.Verify(encoding.GetString(password), hash);
        }
    }
}