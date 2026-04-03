using System;
using System.Collections;

namespace WellTool.Core.Bean.Copier
{
    /// <summary>
    /// 字典值提供者
    /// </summary>
    public class MapValueProvider : IValueProvider<string>
    {
        private readonly IDictionary _map;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="map">字典对象</param>
        public MapValueProvider(IDictionary map)
        {
            _map = map;
        }

        /// <summary>
        /// 获取值
        /// </summary>
        public object Value(string key, Type valueType)
        {
            if (_map.Contains(key))
            {
                return _map[key];
            }
            return null;
        }

        /// <summary>
        /// 是否包含指定属性
        /// </summary>
        public bool ContainsKey(string key)
        {
            return _map.Contains(key);
        }
    }
}
