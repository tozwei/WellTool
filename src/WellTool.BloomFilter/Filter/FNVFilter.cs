namespace WellTool.BloomFilter.Filter
{
    public class FNVFilter : FuncFilter
    {
        public FNVFilter(long maxValue) : base(maxValue, MACHINE32, HashUtil.FnvHash)
        {
        }

        public FNVFilter(long maxValue, int machineNumber) : base(maxValue, machineNumber, HashUtil.FnvHash)
        {
        }
    }
}

