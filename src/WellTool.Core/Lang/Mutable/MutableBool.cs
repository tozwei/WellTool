using System;

namespace WellTool.Core.Lang.Mutable
{
    /// <summary>
    /// 可变布尔类型
    /// </summary>
    public class MutableBool
    {
        private bool _value;

        /// <summary>
        /// 值
        /// </summary>
        public bool Value
        {
            get => _value;
            set => _value = value;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public MutableBool() : this(false)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public MutableBool(bool value)
        {
            _value = value;
        }

        /// <summary>
        /// 转换为布尔值
        /// </summary>
        public static implicit operator bool(MutableBool m) => m._value;

        /// <summary>
        /// 从布尔值转换
        /// </summary>
        public static implicit operator MutableBool(bool b) => new MutableBool(b);

        /// <summary>
        /// 取反
        /// </summary>
        public void Negate()
        {
            _value = !_value;
        }

        /// <summary>
        /// 设置为true
        /// </summary>
        public void SetTrue()
        {
            _value = true;
        }

        /// <summary>
        /// 设置为false
        /// </summary>
        public void SetFalse()
        {
            _value = false;
        }

        /// <summary>
        /// 切换值
        /// </summary>
        public bool Toggle()
        {
            _value = !_value;
            return _value;
        }

        public override string ToString()
        {
            return _value.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj is MutableBool other)
            {
                return _value == other._value;
            }
            if (obj is bool b)
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
