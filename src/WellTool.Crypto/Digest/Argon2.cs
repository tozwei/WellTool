using System;
using System.Text;
using Konscious.Security.Cryptography;

namespace WellTool.Crypto.Digest
{
    public class Argon2
    {
        public static byte[] Hash(byte[] password, byte[] salt, int iterations = 3, int memorySize = 65536, int parallelism = 4, int hashLength = 32)
        {
            using (var argon2 = new Argon2id(password))
            {
                argon2.Salt = salt;
                argon2.DegreeOfParallelism = parallelism;
                argon2.MemorySize = memorySize;
                argon2.Iterations = iterations;
                return argon2.GetBytes(hashLength);
            }
        }

        public static string HashHex(string password, string salt, int iterations = 3, int memorySize = 65536, int parallelism = 4, int hashLength = 32, Encoding encoding = null)
        {
            encoding ??= Encoding.UTF8;
            var hash = Hash(encoding.GetBytes(password), encoding.GetBytes(salt), iterations, memorySize, parallelism, hashLength);
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }

        public static bool Verify(byte[] password, byte[] hash, byte[] salt, int iterations = 3, int memorySize = 65536, int parallelism = 4, int hashLength = 32)
        {
            var computedHash = Hash(password, salt, iterations, memorySize, parallelism, hashLength);
            return AreEqual(computedHash, hash);
        }

        public static bool Verify(string password, string hash, string salt, int iterations = 3, int memorySize = 65536, int parallelism = 4, int hashLength = 32, Encoding encoding = null)
        {
            encoding ??= Encoding.UTF8;
            var computedHash = HashHex(password, salt, iterations, memorySize, parallelism, hashLength, encoding);
            return computedHash.Equals(hash, StringComparison.OrdinalIgnoreCase);
        }

        private static bool AreEqual(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;

            var result = 0;
            for (var i = 0; i < a.Length; i++)
            {
                result |= a[i] ^ b[i];
            }
            return result == 0;
        }
    }
}