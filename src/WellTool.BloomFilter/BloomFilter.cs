namespace WellTool.BloomFilter
{
    public interface BloomFilter
    {
        bool Contains(string str);
        bool Add(string str);
    }
}
