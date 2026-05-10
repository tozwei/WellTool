using System;

namespace WellTool.BloomFilter
{
    internal static class HashUtil
    {
        public static long RsHash(string str)
        {
            long hash = 0;
            for (int i = 0; i < str.Length; i++)
            {
                hash = hash * 31 + str[i];
            }
            return hash;
        }

        public static long JsHash(string str)
        {
            long hash = 1315423911;
            for (int i = 0; i < str.Length; i++)
            {
                hash ^= ((hash << 5) + str[i] + (hash >> 2));
            }
            return hash & 0x7FFFFFFF;
        }

        public static long ElfHash(string str)
        {
            long hash = 0;
            long x = 0;
            for (int i = 0; i < str.Length; i++)
            {
                hash = (hash << 4) + str[i];
                x = (hash & 0xF0000000);
                if (x != 0)
                {
                    hash ^= (x >> 24);
                    hash &= ~x;
                }
            }
            return hash & 0x7FFFFFFF;
        }

        public static long BkdrHash(string str)
        {
            long seed = 131;
            long hash = 0;
            for (int i = 0; i < str.Length; i++)
            {
                hash = (hash * seed) + str[i];
            }
            return hash & 0x7FFFFFFF;
        }

        public static long ApHash(string str)
        {
            long hash = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if ((i & 1) == 0)
                {
                    hash ^= ((hash << 7) ^ str[i] ^ (hash >> 3));
                }
                else
                {
                    hash ^= (~((hash << 11) ^ str[i] ^ (hash >> 5)));
                }
            }
            return hash & 0x7FFFFFFF;
        }

        public static long DjbHash(string str)
        {
            long hash = 5381;
            for (int i = 0; i < str.Length; i++)
            {
                hash += (hash << 5) + str[i];
            }
            return hash & 0x7FFFFFFF;
        }

        public static long SdbmHash(string str)
        {
            long hash = 0;
            for (int i = 0; i < str.Length; i++)
            {
                hash = str[i] + (hash << 6) + (hash << 16) - hash;
            }
            return hash & 0x7FFFFFFF;
        }

        public static long PjwHash(string str)
        {
            int bitsInUnsignedInt = 32;
            int threeQuarters = (bitsInUnsignedInt * 3) / 4;
            int oneEighth = bitsInUnsignedInt / 8;
            long highBits = (0xFFFFFFFF) << (bitsInUnsignedInt - oneEighth);
            long hash = 0;
            long test = 0;

            for (int i = 0; i < str.Length; i++)
            {
                hash = (hash << oneEighth) + str[i];
                test = (hash & highBits);
                if (test != 0)
                {
                    hash = ((hash ^ (test >> threeQuarters)) & (~highBits));
                }
            }
            return hash & 0x7FFFFFFF;
        }

        public static long HfHash(string str)
        {
            long hash = str.Length;
            for (int i = 0; i < str.Length; i++)
            {
                hash ^= ((i & 1) == 0) ? ((hash << 7) ^ str[i] ^ (hash >> 3)) : (~((hash << 11) ^ str[i] ^ (hash >> 5)));
            }
            return hash & 0x7FFFFFFF;
        }

        public static long HfIpHash(string str)
        {
            long hash = 0;
            foreach (char c in str)
            {
                hash ^= ((hash & 1) != 0) ? ((hash >> 7) ^ (c << 3)) : (~(hash << 3) ^ c);
                hash = (hash << 7) ^ c;
            }
            return hash & 0x7FFFFFFF;
        }

        public static long TianlHash(string str)
        {
            long hash = 0;
            for (int i = 0; i < str.Length; i++)
            {
                hash = (hash << 7) ^ (hash >> 3) ^ str[i];
            }
            return hash & 0x7FFFFFFFFFFFFFFF;
        }

        public static long JavaDefaultHash(string str)
        {
            long h = 0;
            for (int i = 0; i < str.Length; i++)
            {
                h = (31 * h) + str[i];
            }
            return h & 0x7FFFFFFFFFFFFFFF;
        }

        public static long FnvHash(string str)
        {
            const uint FNV1_32_INIT = 0x811C9DC5;
            const int FNV1_PRIME = 0x01000193;

            uint hash = FNV1_32_INIT;
            foreach (var c in str)
            {
                hash ^= (uint)c;
                hash *= (uint)FNV1_PRIME;
            }
            return hash & 0x7FFFFFFF;
        }
    }
}
