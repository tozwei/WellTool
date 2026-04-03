using System;

namespace WellTool.Core.Map
{
    /// <summary>
    /// 使用Func作为Key的Map
    /// </summary>
    public class FuncKeyMap<V> : Dictionary<Func<object>, V>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public FuncKeyMap()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public FuncKeyMap(int capacity) : base(capacity)
        {
        }

        /// <summary>
        /// 根据对象获取值
        /// </summary>
        public V GetValue(object key)
        {
            foreach (var kvp in this)
            {
                if (kvp.Key(key))
                {
                    return kvp.Value;
                }
            }
            return default;
        }

        /// <summary>
        /// 是否包含匹配的对象
        /// </summary>
        public bool ContainsKey(object key)
        {
            foreach (var kvp in this)
            {
                if (kvp.Key(key))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
