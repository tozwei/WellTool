using System;
using System.Collections.Generic;

namespace WellTool.Core.Map
{
    /// <summary>
    /// 驼峰命名的LinkedMap
    /// </summary>
    public class CamelCaseLinkedMap<K, V> : LinkedMap<K, V>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CamelCaseLinkedMap() : base(true)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public CamelCaseLinkedMap(int initialCapacity) : base(initialCapacity, true)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public CamelCaseLinkedMap(IDictionary<K, V> map) : base(map, true)
        {
        }
    }
}
