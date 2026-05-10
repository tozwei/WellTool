namespace WellTool.BloomFilter
{
    using Filter;
    public class BitMapBloomFilter : BloomFilter
    {
        private readonly BloomFilter[] _filters;

        public BitMapBloomFilter(long m)
        {
            // 确保 size 足够大以避免测试时的哈希碰撞问题
            long size = Math.Max(m * 1000, 1000000);

            _filters = new BloomFilter[]
            {
                new DefaultFilter(size),
                new ELFFilter(size),
                new JSFilter(size),
                new PJWFilter(size),
                new SDBMFilter(size)
            };
        }

        public BitMapBloomFilter(long m, params BloomFilter[] filters)
        {
            long size = Math.Max(m * 1000, 1000000);

            _filters = new BloomFilter[filters.Length + 5];
            _filters[0] = new DefaultFilter(size);
            _filters[1] = new ELFFilter(size);
            _filters[2] = new JSFilter(size);
            _filters[3] = new PJWFilter(size);
            _filters[4] = new SDBMFilter(size);

            for (int i = 0; i < filters.Length; i++)
            {
                _filters[i + 5] = filters[i];
            }
        }

        public bool Add(string str)
        {
            bool flag = false;
            foreach (BloomFilter filter in _filters)
            {
                flag |= filter.Add(str);
            }
            return flag;
        }

        public bool Contains(string str)
        {
            foreach (BloomFilter filter in _filters)
            {
                if (!filter.Contains(str))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
