namespace WellTool.BloomFilter.Filter
{
    public class SDBMFilter : FuncFilter
    {
        public SDBMFilter(long maxValue) : base(maxValue, MACHINE32, HashUtil.SdbmHash)
        {
        }

        public SDBMFilter(long maxValue, int machineNumber) : base(maxValue, machineNumber, HashUtil.SdbmHash)
        {
        }
    }
}

