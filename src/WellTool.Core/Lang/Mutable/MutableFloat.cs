using System;

namespace WellTool.Core.Lang.Mutable
{
    /// <summary>
    /// 可变单精度浮点数类型
    /// </summary>
    public class MutableFloat
    {
        private float _value;

        /// <summary>
        /// 值
        /// </summary>
        public float Value
        {
            get => _value;
            set => _value = value;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public MutableFloat() : this(0)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public MutableFloat(float value)
        {
            _value = value;
        }

        /// <summary>
        /// 隐式转换
        /// </summary>
        public static implicit operator float(MutableFloat m) => m._value;

        /// <summary>
        /// 从浮点数转换
        /// </summary>
        public static implicit operator MutableFloat(float f) => new MutableFloat(f);

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
        public void Add(float amount)
        {
            _value += amount;
        }

        /// <summary>
        /// 减少指定值
        /// </summary>
        public void Subtract(float amount)
        {
            _value -= amount;
        }

        /// <summary>
        /// 乘以指定值
        /// </summary>
        public void Multiply(float factor)
        {
            _value *= factor;
        }

        /// <summary>
        /// 除以指定值
        /// </summary>
        public void Divide(float divisor)
        {
            _value /= divisor;
        }

        public override string ToString()
        {
            return _value.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj is MutableFloat other)
            {
                return Math.Abs(_value - other._value) < 0.00001f;
            }
            if (obj is float f)
            {
                return Math.Abs(_value - f) < 0.00001f;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }
}
