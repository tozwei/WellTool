using System;
using System.Security.Cryptography;

namespace WellTool.Core.Lang.Id
{
    /// <summary>
    /// NanoId生成器，生成URL友好的唯一ID
    /// </summary>
    public class NanoId
    {
        private static readonly Random _random = new Random();
        private readonly char[] _alphabet;
        private readonly int _mask;
        private readonly int _size;

        /// <summary>
        /// 默认ID大小
        /// </summary>
        public const int DEFAULT_SIZE = 21;

        /// <summary>
        /// 默认字母表
        /// </summary>
        public const string DEFAULT_ALPHABET = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ-_";

        /// <summary>
        /// 构造函数
        /// </summary>
        public NanoId() : this(DEFAULT_ALPHABET, DEFAULT_SIZE)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public NanoId(string alphabet) : this(alphabet, DEFAULT_SIZE)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public NanoId(string alphabet, int size)
        {
            if (string.IsNullOrEmpty(alphabet))
            {
                throw new ArgumentException("Alphabet cannot be null or empty");
            }

            if (size < 1)
            {
                throw new ArgumentException("Size must be greater than 0");
            }

            _alphabet = alphabet.ToCharArray();
            _size = size;
            _mask = NextPowerOfTwo(alphabet.Length) - 1;
        }

        /// <summary>
        /// 生成ID
        /// </summary>
        public string Generate()
        {
            var id = new char[_size];
            var bytes = new byte[_size];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(bytes);
            }

            for (int i = 0; i < _size; i++)
            {
                id[i] = _alphabet[bytes[i] & _mask];
            }

            return new string(id);
        }

        /// <summary>
        /// 生成ID
        /// </summary>
        public static string GenerateId()
        {
            return new NanoId().Generate();
        }

        /// <summary>
        /// 生成指定大小和字母表的ID
        /// </summary>
        public static string GenerateId(int size, string alphabet = DEFAULT_ALPHABET)
        {
            return new NanoId(alphabet, size).Generate();
        }

        /// <summary>
        /// 计算下一个2的幂
        /// </summary>
        private static int NextPowerOfTwo(int value)
        {
            int power = 1;
            while (power < value)
            {
                power <<= 1;
            }
            return power;
        }
    }
}
