using System;

namespace WellTool.Core.Lang.Mutable
{
    /// <summary>
    /// 可变对象类型
    /// </summary>
    public class MutableObj<T>
    {
        private T? _value;

        /// <summary>
        /// 值
        /// </summary>
        public T? Value
        {
            get => _value;
            set => _value = value;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public MutableObj()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public MutableObj(T value)
        {
            _value = value;
        }

        /// <summary>
        /// 获取值
        /// </summary>
        public T? Get()
        {
            return _value;
        }

        /// <summary>
        /// 设置值
        /// </summary>
        public void Set(T? value)
        {
            _value = value;
        }

        /// <summary>
        /// 获取值，为null时返回默认值
        /// </summary>
        public T GetOrDefault(T defaultValue)
        {
            return _value ?? defaultValue;
        }

        /// <summary>
        /// 获取值，为null时使用工厂方法
        /// </summary>
        public T GetOrDefault(System.Func<T> factory)
        {
            return _value ?? factory();
        }

        /// <summary>
        /// 如果为null则设置值
        /// </summary>
        public void SetIfNull(T? value)
        {
            if (_value == null)
            {
                _value = value;
            }
        }

        /// <summary>
        /// 如果为null则使用工厂方法设置值
        /// </summary>
        public void SetIfNull(System.Func<T?> factory)
        {
            if (_value == null)
            {
                _value = factory();
            }
        }

        public override string ToString()
        {
            return _value?.ToString() ?? "";
        }

        public override bool Equals(object obj)
        {
            if (obj is MutableObj<T> other)
            {
                return Equals(_value, other._value);
            }
            return Equals(_value, obj);
        }

        public override int GetHashCode()
        {
            return _value?.GetHashCode() ?? 0;
        }
    }
}
