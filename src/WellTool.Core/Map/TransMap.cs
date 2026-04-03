using System;
using System.Collections.Generic;

namespace WellTool.Core.Map
{
    /// <summary>
    /// 转换Map，值会被转换后返回
    /// </summary>
    public class TransMap<TKey, TValue, TResult> : Dictionary<TKey, TValue>
    {
        private readonly Func<TValue, TResult> _transformer;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="transformer">转换函数</param>
        public TransMap(Func<TValue, TResult> transformer)
        {
            _transformer = transformer ?? throw new ArgumentNullException(nameof(transformer));
        }

        /// <summary>
        /// 获取转换后的值
        /// </summary>
        public TResult Get(TKey key)
        {
            if (TryGetValue(key, out var value))
            {
                return _transformer(value);
            }
            return default;
        }

        /// <summary>
        /// 获取转换后的值，如果不存在返回默认值
        /// </summary>
        public TResult GetOrDefault(TKey key, TResult defaultValue = default)
        {
            if (TryGetValue(key, out var value))
            {
                return _transformer(value);
            }
            return defaultValue;
        }
    }

    /// <summary>
    /// 字符串转换Map
    /// </summary>
    public class StrTransMap<TValue> : TransMap<string, TValue, string>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="transformer">转换函数</param>
        public StrTransMap(Func<TValue, string> transformer) : base(transformer)
        {
        }
    }
}
