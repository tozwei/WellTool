namespace WellTool.BloomFilter.Filter
{
    public class DefaultFilter : FuncFilter
    {
        public DefaultFilter(long maxValue) : base(maxValue, MACHINE32, HashUtil.JavaDefaultHash)
        {
        }

        public DefaultFilter(long maxValue, int machineNumber) : base(maxValue, machineNumber, HashUtil.JavaDefaultHash)
        {
        }
    }
}

