using System;
using System.Collections;
using System.Collections.Generic;

namespace WellTool.Core.Map
{
    /// <summary>
    /// Map代理类，用于以对象属性的方式访问Map
    /// </summary>
    public class MapProxy : DynamicObject
    {
        private readonly IDictionary _map;

        /// <summary>
        /// 构造函数
        /// </summary>
        public MapProxy(IDictionary map)
        {
            _map = map ?? throw new ArgumentNullException(nameof(map));
        }

        /// <summary>
        /// 创建Map代理
        /// </summary>
        public static MapProxy Create(IDictionary map)
        {
            return new MapProxy(map);
        }

        /// <summary>
        /// 获取属性值
        /// </summary>
        public object this[string key] => _map.Contains(key) ? _map[key] : null;

        /// <summary>
        /// 尝试获取值
        /// </summary>
        public bool TryGet(string key, out object value)
        {
            if (_map.Contains(key))
            {
                value = _map[key];
                return true;
            }
            value = null;
            return false;
        }

        /// <summary>
        /// 设置值
        /// </summary>
        public void Set(string key, object value)
        {
            _map[key] = value;
        }

        /// <summary>
        /// 获取所有键
        /// </summary>
        public ICollection Keys => _map.Keys;

        /// <summary>
        /// 获取所有值
        /// </summary>
        public ICollection Values => _map.Values;

        /// <summary>
        /// 获取底层Map
        /// </summary>
        public IDictionary GetMap()
        {
            return _map;
        }
    }

    /// <summary>
    /// 动态对象基类
    /// </summary>
    public class DynamicObject
    {
    }
}
