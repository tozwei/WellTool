namespace WellTool.BloomFilter.Filter
{
    public class HfFilter : FuncFilter
    {
        public HfFilter(long maxValue) : base(maxValue, MACHINE32, HashUtil.HfHash)
        {
        }

        public HfFilter(long maxValue, int machineNumber) : base(maxValue, machineNumber, HashUtil.HfHash)
        {
        }
    }
}

