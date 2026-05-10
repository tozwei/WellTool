namespace WellTool.BloomFilter.Filter
{
    public class RSFilter : FuncFilter
    {
        public RSFilter(long maxValue) : base(maxValue, MACHINE32, HashUtil.RsHash)
        {
        }

        public RSFilter(long maxValue, int machineNumber) : base(maxValue, machineNumber, HashUtil.RsHash)
        {
        }
    }
}

