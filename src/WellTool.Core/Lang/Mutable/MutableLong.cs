//using System;

//namespace WellTool.Core.Lang.Mutable
//{
//    /// <summary>
//    /// 可变长整数类型
//    /// </summary>
//    public class MutableLong
//    {
//        private long _value;

//        /// <summary>
//        /// 值
//        /// </summary>
//        public long Value
//        {
//            get => _value;
//            set => _value = value;
//        }

//        /// <summary>
//        /// 构造函数
//        /// </summary>
//        public MutableLong() : this(0)
//        {
//        }

//        /// <summary>
//        /// 构造函数
//        /// </summary>
//        public MutableLong(long value)
//        {
//            _value = value;
//        }

//        /// <summary>
//        /// 隐式转换
//        /// </summary>
//        public static implicit operator long(MutableLong m) => m._value;

//        /// <summary>
//        /// 从长整数转换
//        /// </summary>
//        public static implicit operator MutableLong(long l) => new MutableLong(l);

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
//        public void Add(long amount)
//        {
//            _value += amount;
//        }

//        /// <summary>
//        /// 减少指定值
//        /// </summary>
//        public void Subtract(long amount)
//        {
//            _value -= amount;
//        }

//        public override string ToString()
//        {
//            return _value.ToString();
//        }

//        public override bool Equals(object obj)
//        {
//            if (obj is MutableLong other)
//            {
//                return _value == other._value;
//            }
//            if (obj is long l)
//            {
//                return _value == l;
//            }
//            return false;
//        }

//        public override int GetHashCode()
//        {
//            return _value.GetHashCode();
//        }
//    }
//}
