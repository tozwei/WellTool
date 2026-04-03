using System;
using System.Collections.Generic;

namespace WellTool.Core.Map
{
    /// <summary>
    /// 大小写不敏感的有序Map
    /// </summary>
    public class CaseInsensitiveLinkedMap<V> : Dictionary<string, V>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CaseInsensitiveLinkedMap() : base(StringComparer.OrdinalIgnoreCase)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public CaseInsensitiveLinkedMap(int initialCapacity) : base(initialCapacity, StringComparer.OrdinalIgnoreCase)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public CaseInsensitiveLinkedMap(IDictionary<string, V> map) : base(map, StringComparer.OrdinalIgnoreCase)
        {
        }
    }
}
