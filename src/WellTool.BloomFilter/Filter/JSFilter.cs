namespace WellTool.BloomFilter.Filter
{
    public class JSFilter : FuncFilter
    {
        public JSFilter(long maxValue) : base(maxValue, MACHINE32, HashUtil.JsHash)
        {
        }

        public JSFilter(long maxValue, int machineNumber) : base(maxValue, machineNumber, HashUtil.JsHash)
        {
        }
    }
}

