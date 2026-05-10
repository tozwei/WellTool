namespace WellTool.BloomFilter.BitMap
{
    public interface IBitMap
    {
        void Add(long i);
        bool Contains(long i);
        void Remove(long i);
    }
}

