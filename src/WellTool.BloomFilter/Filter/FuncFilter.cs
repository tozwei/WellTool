using System;

namespace WellTool.BloomFilter.Filter
{
    public class FuncFilter : AbstractFilter
    {
        private readonly Func<string, long> _hashFunc;

        public FuncFilter(long maxValue, int machineNum, Func<string, long> hashFunc) : base(maxValue, machineNum)
        {
            _hashFunc = hashFunc;
        }

        public override long Hash(string str)
        {
            return _hashFunc(str);
        }
    }
}

