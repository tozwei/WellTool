namespace WellTool.BloomFilter
{
    public static class BloomFilterUtil
    {
        public static BitSetBloomFilter CreateBitSet(int c, int n, int k)
        {
            return new BitSetBloomFilter(c, n, k);
        }

        public static BitSetBloomFilter CreateBitSetBloomFilter(int expectedElements)
        {
            return new BitSetBloomFilter(expectedElements, expectedElements, 8);
        }

        public static BitMapBloomFilter CreateBitMap(long m)
        {
            return new BitMapBloomFilter(m);
        }

        public static BitMapBloomFilter CreateBitMapBloomFilter(long m)
        {
            return new BitMapBloomFilter(m);
        }
    }
}
