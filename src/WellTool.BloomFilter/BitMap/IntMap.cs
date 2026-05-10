namespace WellTool.BloomFilter.BitMap
{
    public class IntMap : IBitMap
    {
        private readonly int[] _data;
        private readonly int _capacity;

        public IntMap(int capacity)
        {
            _capacity = capacity;
            _data = new int[capacity];
        }

        public void Add(long i)
        {
            int pos = (int)(i >> 5);
            if (pos >= _capacity) return;
            _data[pos] |= (int)(1 << (int)(i & 31));
        }

        public bool Contains(long i)
        {
            int pos = (int)(i >> 5);
            if (pos >= _capacity) return false;
            return (_data[pos] & (int)(1 << (int)(i & 31))) != 0;
        }

        public void Remove(long i)
        {
            int pos = (int)(i >> 5);
            if (pos >= _capacity) return;
            _data[pos] &= ~(int)(1 << (int)(i & 31));
        }
    }
}

