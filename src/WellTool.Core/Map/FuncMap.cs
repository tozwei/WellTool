using System;
using System.Collections.Generic;

namespace WellTool.Core.Map
{
    /// <summary>
    /// 函数值Map，当键不存在时使用函数生成值
    /// </summary>
    public class FuncMap<K, V> : Dictionary<K, V>
    {
        private readonly Func<K, V> _valueFactory;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="valueFactory">值工厂函数</param>
        public FuncMap(Func<K, V> valueFactory)
        {
            _valueFactory = valueFactory ?? throw new ArgumentNullException(nameof(valueFactory));
        }

        /// <summary>
        /// 获取值，如果不存在则使用工厂函数创建
        /// </summary>
        public new V this[K key]
        {
            get
            {
                if (TryGetValue(key, out var value))
                {
                    return value;
                }
                value = _valueFactory(key);
                base[key] = value;
                return value;
            }
            set
            {
                base[key] = value;
            }
        }

        /// <summary>
        /// 获取或添加值
        /// </summary>
        public V GetOrAdd(K key)
        {
            return this[key];
        }

        /// <summary>
        /// 获取或添加值
        /// </summary>
        public V GetOrAdd(K key, Func<V> factory)
        {
            if (TryGetValue(key, out var value))
            {
                return value;
            }
            value = factory();
            base[key] = value;
            return value;
        }
    }
}
