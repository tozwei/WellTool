namespace WellTool.BloomFilter.Filter
{
    public class PJWFilter : FuncFilter
    {
        public PJWFilter(long maxValue) : base(maxValue, MACHINE32, HashUtil.PjwHash)
        {
        }

        public PJWFilter(long maxValue, int machineNumber) : base(maxValue, machineNumber, HashUtil.PjwHash)
        {
        }
    }
}

