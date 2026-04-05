using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace WellTool.Core.Bean
{
    /// <summary>
    /// Bean属性缓存，使用弱引用字典防止内存泄漏
    /// </summary>
    public class BeanInfoCache
    {
        private static readonly Lazy<BeanInfoCache> _instance = new Lazy<BeanInfoCache>(() => new BeanInfoCache());
        public static BeanInfoCache Instance => _instance.Value;

        private readonly ConcurrentDictionary<Type, Dictionary<string, PropDesc>> _pdCache = new ConcurrentDictionary<Type, Dictionary<string, PropDesc>>();
        private readonly ConcurrentDictionary<Type, Dictionary<string, PropDesc>> _ignoreCasePdCache = new ConcurrentDictionary<Type, Dictionary<string, PropDesc>>();

        /// <summary>
        /// 获取属性描述符映射
        /// </summary>
        public Dictionary<string, PropDesc> GetPropertyDescriptorMap(Type beanClass, bool ignoreCase)
        {
            var cache = ignoreCase ? _ignoreCasePdCache : _pdCache;
            return cache.GetOrAdd(beanClass, _ => CreatePropertyDescriptorMap(beanClass, ignoreCase));
        }

        /// <summary>
        /// 获取属性描述符映射，如果不存在则使用提供的工厂方法创建
        /// </summary>
        public Dictionary<string, PropDesc> GetPropertyDescriptorMap(Type beanClass, bool ignoreCase, Func<Dictionary<string, PropDesc>> supplier)
        {
            var cache = ignoreCase ? _ignoreCasePdCache : _pdCache;
            return cache.GetOrAdd(beanClass, _ => supplier());
        }

        /// <summary>
        /// 添加到缓存
        /// </summary>
        public void PutPropertyDescriptorMap(Type beanClass, Dictionary<string, PropDesc> map, bool ignoreCase)
        {
            var cache = ignoreCase ? _ignoreCasePdCache : _pdCache;
            cache[beanClass] = map;
        }

        /// <summary>
        /// 清空缓存
        /// </summary>
        public void Clear()
        {
            _pdCache.Clear();
            _ignoreCasePdCache.Clear();
        }

        /// <summary>
        /// 创建属性描述符映射
        /// </summary>
        private static Dictionary<string, PropDesc> CreatePropertyDescriptorMap(Type beanClass, bool ignoreCase)
        {
            var result = new Dictionary<string, PropDesc>(ignoreCase ? StringComparer.OrdinalIgnoreCase : StringComparer.Ordinal);
            var beanDesc = BeanDesc.GetBeanDesc(beanClass);

            foreach (var prop in beanDesc.GetProps())
            {
                result[prop.Key] = prop.Value;
            }

            return result;
        }
    }
}
