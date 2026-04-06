using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;

namespace WellTool.Extra.Cglib
{
    /// <summary>
    /// BeanCopier属性缓存
    /// 缓存用于防止多次反射造成的性能问题
    /// </summary>
    public static class BeanCopierCache
    {
        /// <summary>
        /// 获取单例实例
        /// </summary>
        public static BeanCopierCacheAccessor Instance { get; } = new BeanCopierCacheAccessor();

        private static readonly ConcurrentDictionary<string, Delegate> Cache = new ConcurrentDictionary<string, Delegate>();

        /// <summary>
        /// 访问器类，用于提供 Instance 属性
        /// </summary>
        public class BeanCopierCacheAccessor
        {
            /// <summary>
            /// 获得类与转换器生成的key对应的BeanCopier（非泛型版本）
            /// </summary>
            public Action<object, object> Get(Type sourceType, Type targetType)
            {
                var key = $"{sourceType.FullName}#{targetType.FullName}#0";
                var result = BeanCopierCache.Get<object, object>(false);
                return (src, tgt) =>
                {
                    var source = Activator.CreateInstance(sourceType);
                    var target = Activator.CreateInstance(targetType);
                    result(source, target);
                };
            }

            /// <summary>
            /// 获得类与转换器生成的key对应的BeanCopier
            /// </summary>
            public Action<TSource, TTarget> Get<TSource, TTarget>() where TSource : new() where TTarget : new()
            {
                return BeanCopierCache.Get<TSource, TTarget>(false);
            }

            /// <summary>
            /// 获得类与转换器生成的key对应的BeanCopier
            /// </summary>
            public Action<TSource, TTarget> Get<TSource, TTarget>(bool useConverter) where TSource : new() where TTarget : new()
            {
                return BeanCopierCache.Get<TSource, TTarget>(useConverter);
            }

            /// <summary>
            /// 获得类与转换器生成的key对应的BeanCopier
            /// </summary>
            public Action<TSource, TTarget> Get<TSource, TTarget>(Func<object, object, object> converter) where TSource : new() where TTarget : new()
            {
                return BeanCopierCache.Get<TSource, TTarget>(converter);
            }
        }

        /// <summary>
        /// 获得类与转换器生成的key对应的BeanCopier
        /// </summary>
        /// <typeparam name="TSource">源Bean类型</typeparam>
        /// <typeparam name="TTarget">目标Bean类型</typeparam>
        /// <param name="useConverter">是否使用转换器</param>
        /// <returns>缓存的拷贝函数</returns>
        public static Action<TSource, TTarget> Get<TSource, TTarget>(bool useConverter) where TSource : new() where TTarget : new()
        {
            var key = GenKey(typeof(TSource), typeof(TTarget), useConverter);
            return (Action<TSource, TTarget>)Cache.GetOrAdd(key, k => CreateCopier<TSource, TTarget>(useConverter));
        }

        /// <summary>
        /// 获得类与转换器生成的key对应的BeanCopier
        /// </summary>
        /// <typeparam name="TSource">源Bean类型</typeparam>
        /// <typeparam name="TTarget">目标Bean类型</typeparam>
        /// <param name="converter">转换器</param>
        /// <returns>缓存的拷贝函数</returns>
        public static Action<TSource, TTarget> Get<TSource, TTarget>(Func<object, object, object> converter) where TSource : new() where TTarget : new()
        {
            return Get<TSource, TTarget>(converter != null);
        }

        /// <summary>
        /// 获得类与转换器生成的key
        /// </summary>
        /// <param name="srcClass">源Bean的类</param>
        /// <param name="targetClass">目标Bean的类</param>
        /// <param name="useConverter">是否使用转换器</param>
        /// <returns>属性名和Map映射的key</returns>
        private static string GenKey(Type srcClass, Type targetClass, bool useConverter)
        {
            return $"{srcClass.FullName}#{targetClass.FullName}#{(useConverter ? 1 : 0)}";
        }

        /// <summary>
        /// 创建BeanCopier
        /// </summary>
        private static Action<TSource, TTarget> CreateCopier<TSource, TTarget>(bool useConverter) where TSource : new() where TTarget : new()
        {
            var sourceType = typeof(TSource);
            var targetType = typeof(TTarget);

            var sourceProps = sourceType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var targetProps = targetType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var targetPropDict = new Dictionary<string, PropertyInfo>(StringComparer.OrdinalIgnoreCase);
            foreach (var prop in targetProps)
            {
                targetPropDict[prop.Name] = prop;
            }

            return (source, target) =>
            {
                foreach (var sourceProp in sourceProps)
                {
                    if (!targetPropDict.TryGetValue(sourceProp.Name, out var targetProp)) continue;
                    if (!targetProp.CanWrite || !sourceProp.CanRead) continue;

                    try
                    {
                        var value = sourceProp.GetValue(source);
                        if (value != null && targetProp.PropertyType != sourceProp.PropertyType)
                        {
                            value = Convert.ChangeType(value, targetProp.PropertyType);
                        }
                        targetProp.SetValue(target, value);
                    }
                    catch
                    {
                        // 忽略属性复制错误
                    }
                }
            };
        }
    }
}