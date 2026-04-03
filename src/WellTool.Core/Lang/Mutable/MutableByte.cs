using System;

namespace WellTool.Core.Lang.Mutable
{
    /// <summary>
    /// 可变字节类型
    /// </summary>
    public class MutableByte
    {
        private byte _value;

        /// <summary>
        /// 值
        /// </summary>
        public byte Value
        {
            get => _value;
            set => _value = value;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public MutableByte() : this(0)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public MutableByte(byte value)
        {
            _value = value;
        }

        /// <summary>
        /// 隐式转换
        /// </summary>
        public static implicit operator byte(MutableByte m) => m._value;

        /// <summary>
        /// 从字节转换
        /// </summary>
        public static implicit operator MutableByte(byte b) => new MutableByte(b);

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
        public void Add(byte amount)
        {
            _value += amount;
        }

        /// <summary>
        /// 减少指定值
        /// </summary>
        public void Subtract(byte amount)
        {
            _value -= amount;
        }

        public override string ToString()
        {
            return _value.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj is MutableByte other)
            {
                return _value == other._value;
            }
            if (obj is byte b)
            {
                return _value == b;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }
}
