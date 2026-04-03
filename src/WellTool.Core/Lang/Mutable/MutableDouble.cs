//using System;

//namespace WellTool.Core.Lang.Mutable
//{
//    /// <summary>
//    /// 可变双精度浮点数类型
//    /// </summary>
//    public class MutableDouble
//    {
//        private double _value;

//        /// <summary>
//        /// 值
//        /// </summary>
//        public double Value
//        {
//            get => _value;
//            set => _value = value;
//        }

//        /// <summary>
//        /// 构造函数
//        /// </summary>
//        public MutableDouble() : this(0)
//        {
//        }

//        /// <summary>
//        /// 构造函数
//        /// </summary>
//        public MutableDouble(double value)
//        {
//            _value = value;
//        }

//        /// <summary>
//        /// 隐式转换
//        /// </summary>
//        public static implicit operator double(MutableDouble m) => m._value;

//        /// <summary>
//        /// 从浮点数转换
//        /// </summary>
//        public static implicit operator MutableDouble(double d) => new MutableDouble(d);

//        /// <summary>
//        /// 增加
//        /// </summary>
//        public void Increment()
//        {
//            _value++;
//        }

//        /// <summary>
//        /// 减少
//        /// </summary>
//        public void Decrement()
//        {
//            _value--;
//        }

//        /// <summary>
//        /// 增加指定值
//        /// </summary>
//        public void Add(double amount)
//        {
//            _value += amount;
//        }

//        /// <summary>
//        /// 减少指定值
//        /// </summary>
//        public void Subtract(double amount)
//        {
//            _value -= amount;
//        }

//        /// <summary>
//        /// 乘以指定值
//        /// </summary>
//        public void Multiply(double factor)
//        {
//            _value *= factor;
//        }

//        /// <summary>
//        /// 除以指定值
//        /// </summary>
//        public void Divide(double divisor)
//        {
//            _value /= divisor;
//        }

//        public override string ToString()
//        {
//            return _value.ToString();
//        }

//        public override bool Equals(object obj)
//        {
//            if (obj is MutableDouble other)
//            {
//                return Math.Abs(_value - other._value) < 0.0000001;
//            }
//            if (obj is double d)
//            {
//                return Math.Abs(_value - d) < 0.0000001;
//            }
//            return false;
//        }

//        public override int GetHashCode()
//        {
//            return _value.GetHashCode();
//        }
//    }
//}
