using System;

namespace WellTool.BloomFilter.Filter
{
    public abstract class AbstractFilter : BloomFilter
    {
        protected const int MACHINE32 = 32;
        protected const int MACHINE64 = 64;

        protected BitMap.IBitMap? _bm;
        protected long _size;

        protected AbstractFilter(long maxValue)
        {
            Init(maxValue, MACHINE32);
        }

        protected AbstractFilter(long maxValue, int machineNum)
        {
            Init(maxValue, machineNum);
        }

        protected void Init(long maxValue, int machineNum)
        {
            _size = maxValue;
            int capacity = (int)((_size + machineNum - 1) / machineNum);

            switch (machineNum)
            {
                case MACHINE32:
                    _bm = new BitMap.IntMap(capacity);
                    break;
                case MACHINE64:
                    _bm = new BitMap.LongMap(capacity);
                    break;
                default:
                    throw new InvalidOperationException("Error Machine number!");
            }
        }

        public bool Contains(string str)
        {
            long hash = Math.Abs(Hash(str)) % _size;
            return _bm?.Contains(hash) ?? false;
        }

        public bool Add(string str)
        {
            long hash = Math.Abs(Hash(str)) % _size;
            if (_bm?.Contains(hash) == true)
            {
                return false;
            }

            _bm?.Add(hash);
            return true;
        }

        public abstract long Hash(string str);
    }
}

