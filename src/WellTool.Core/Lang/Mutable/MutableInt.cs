using System;

namespace WellTool.Core.Lang.Mutable
{
    /// <summary>
    /// 可变整数类型
    /// </summary>
    public class MutableInt
    {
        private int _value;

        /// <summary>
        /// 值
        /// </summary>
        public int Value
        {
            get => _value;
            set => _value = value;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public MutableInt() : this(0)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public MutableInt(int value)
        {
            _value = value;
        }

        /// <summary>
        /// 隐式转换
        /// </summary>
        public static implicit operator int(MutableInt m) => m._value;

        /// <summary>
        /// 从整数转换
        /// </summary>
        public static implicit operator MutableInt(int i) => new MutableInt(i);

        /// <summary>
        /// 增加
        /// </summary>
        public void Increment()
        {
            _value++;
        }

        /// <summary>
        /// 减少
        /// </summary>
        public void Decrement()
        {
            _value--;
        }

        /// <summary>
        /// 增加指定值
        /// </summary>
        public void Add(int amount)
        {
            _value += amount;
        }

        /// <summary>
        /// 减少指定值
        /// </summary>
        public void Subtract(int amount)
        {
            _value -= amount;
        }

        /// <summary>
        /// 乘以指定值
        /// </summary>
        public void Multiply(int factor)
        {
            _value *= factor;
        }

        /// <summary>
        /// 除以指定值
        /// </summary>
        public void Divide(int divisor)
        {
            _value /= divisor;
        }

        public override string ToString()
        {
            return _value.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj is MutableInt other)
            {
                return _value == other._value;
            }
            if (obj is int i)
            {
                return _value == i;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }
}
