namespace WellTool.BloomFilter.Filter
{
    public class HfIpFilter : FuncFilter
    {
        public HfIpFilter(long maxValue) : base(maxValue, MACHINE32, HashUtil.HfIpHash)
        {
        }

        public HfIpFilter(long maxValue, int machineNumber) : base(maxValue, machineNumber, HashUtil.HfIpHash)
        {
        }
    }
}

