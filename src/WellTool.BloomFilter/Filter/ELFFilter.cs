namespace WellTool.BloomFilter.Filter
{
    public class ELFFilter : FuncFilter
    {
        public ELFFilter(long maxValue) : base(maxValue, MACHINE32, HashUtil.ElfHash)
        {
        }

        public ELFFilter(long maxValue, int machineNumber) : base(maxValue, machineNumber, HashUtil.ElfHash)
        {
        }
    }
}

