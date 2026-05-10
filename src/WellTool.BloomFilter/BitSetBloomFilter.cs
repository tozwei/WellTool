using System;

namespace WellTool.BloomFilter
{
    public class BitSetBloomFilter : BloomFilter
    {
        private readonly bool[] _bitSet;
        private readonly int _bitSetSize;
        private readonly int _addedElements;
        private readonly int _hashFunctionNumber;

        public BitSetBloomFilter(int c, int n, int k)
        {
            if (c <= 0) throw new ArgumentOutOfRangeException(nameof(c), "Parameter c must be positive");
            if (n <= 0) throw new ArgumentOutOfRangeException(nameof(n), "Parameter n must be positive");
            if (k < 1 || k > 8) throw new ArgumentOutOfRangeException(nameof(k), "hashFunctionNumber must be between 1 and 8");

            _hashFunctionNumber = k;
            _bitSetSize = (int)Math.Ceiling((double)c * k);
            _addedElements = n;
            _bitSet = new bool[_bitSetSize];
        }

        public BitSetBloomFilter(int expectedElements)
        {
            if (expectedElements <= 0) throw new ArgumentOutOfRangeException(nameof(expectedElements), "expectedElements must be positive");

            _hashFunctionNumber = 8;
            _bitSetSize = (int)Math.Ceiling((double)expectedElements * 8);
            _addedElements = expectedElements;
            _bitSet = new bool[_bitSetSize];
        }

        public bool Add(string str)
        {
            if (Contains(str))
            {
                return false;
            }

            long[] positions = CreateHashes(str, _hashFunctionNumber);
            foreach (long value in positions)
            {
                int position = (int)Math.Abs(value % _bitSetSize);
                _bitSet[position] = true;
            }
            return true;
        }

        public bool Contains(string str)
        {
            long[] positions = CreateHashes(str, _hashFunctionNumber);
            foreach (long i in positions)
            {
                int position = (int)Math.Abs(i % _bitSetSize);
                if (!_bitSet[position])
                {
                    return false;
                }
            }
            return true;
        }

        public double GetFalsePositiveProbability()
        {
            return Math.Pow((1 - Math.Exp(-_hashFunctionNumber * (double)_addedElements / _bitSetSize)), _hashFunctionNumber);
        }

        public static long[] CreateHashes(string str, int hashNumber)
        {
            long[] result = new long[hashNumber];
            for (int i = 0; i < hashNumber; i++)
            {
                result[i] = Hash(str, i);
            }
            return result;
        }

        public static long Hash(string str, int k)
        {
            switch (k)
            {
                case 0: return HashUtil.RsHash(str);
                case 1: return HashUtil.JsHash(str);
                case 2: return HashUtil.ElfHash(str);
                case 3: return HashUtil.BkdrHash(str);
                case 4: return HashUtil.ApHash(str);
                case 5: return HashUtil.DjbHash(str);
                case 6: return HashUtil.SdbmHash(str);
                case 7: return HashUtil.PjwHash(str);
                default: return 0;
            }
        }
    }
}
