namespace WellTool.BloomFilter.BitMap
{
    public class LongMap : IBitMap
    {
        private readonly long[] _data;
        private readonly int _capacity;

        public LongMap(int capacity)
        {
            _capacity = capacity;
            _data = new long[capacity];
        }

        public void Add(long i)
        {
            int pos = (int)(i >> 6);
            if (pos >= _capacity) return;
            _data[pos] |= 1L << (int)(i & 63);
        }

        public bool Contains(long i)
        {
            int pos = (int)(i >> 6);
            if (pos >= _capacity) return false;
            return (_data[pos] & (1L << (int)(i & 63))) != 0;
        }

        public void Remove(long i)
        {
            int pos = (int)(i >> 6);
            if (pos >= _capacity) return;
            _data[pos] &= ~(1L << (int)(i & 63));
        }
    }
}

