namespace WellTool.BloomFilter.Filter
{
    public class TianlFilter : FuncFilter
    {
        public TianlFilter(long maxValue) : base(maxValue, MACHINE32, HashUtil.TianlHash)
        {
        }

        public TianlFilter(long maxValue, int machineNumber) : base(maxValue, machineNumber, HashUtil.TianlHash)
        {
        }
    }
}

