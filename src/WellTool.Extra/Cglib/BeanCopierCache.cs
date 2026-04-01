using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace WellTool.Extra.Cglib
{
    /// <summary>
    /// BeanCopier属性缓存<br>
    /// 缓存用于防止多次反射造成的性能问题
    /// </summary>
    public class BeanCopierCache
    {
        /// <summary>
        /// BeanCopier属性缓存单例
        /// </summary>
        public static readonly BeanCopierCache Instance = new BeanCopierCache();

        private readonly Dictionary<string, Delegate> cache = new Dictionary<string, Delegate>();

        /// <summary>
        /// 获得类与转换器生成的key在缓存中对应的元素
        /// </summary>
        /// <typeparam name="TSource">源类型</typeparam>
        /// <typeparam name="TTarget">目标类型</typeparam>
        /// <returns>缓存中对应的拷贝委托</returns>
        public Action<TSource, TTarget> Get<TSource, TTarget>()
        {
            var key = GenKey(typeof(TSource), typeof(TTarget), false);
            if (!cache.TryGetValue(key, out var @delegate))
            {
                @delegate = CreateCopyDelegate<TSource, TTarget>();
                cache[key] = @delegate;
            }
            return (Action<TSource, TTarget>)@delegate;
        }

        /// <summary>
        /// 获得类与转换器生成的key在缓存中对应的元素
        /// </summary>
        /// <param name="sourceType">源类型</param>
        /// <param name="targetType">目标类型</param>
        /// <returns>缓存中对应的拷贝委托</returns>
        public Delegate Get(Type sourceType, Type targetType)
        {
            var key = GenKey(sourceType, targetType, false);
            if (!cache.TryGetValue(key, out var @delegate))
            {
                // 动态创建拷贝委托
                var method = typeof(BeanCopierCache).GetMethod("CreateCopyDelegate", BindingFlags.NonPublic | BindingFlags.Instance);
                var genericMethod = method.MakeGenericMethod(sourceType, targetType);
                @delegate = genericMethod.Invoke(this, null) as Delegate;
                cache[key] = @delegate;
            }
            return @delegate;
        }

        /// <summary>
        /// 获得类与转换器生成的key<br>
        /// 结构类似于：srcClassName#targetClassName#0
        /// </summary>
        /// <param name="srcClass">源Bean的类</param>
        /// <param name="targetClass">目标Bean的类</param>
        /// <param name="useConverter">是否使用转换器</param>
        /// <returns>属性名和Map映射的key</returns>
        private string GenKey(Type srcClass, Type targetClass, bool useConverter)
        {
            var key = new StringBuilder()
                .Append(srcClass.FullName)
                .Append('#').Append(targetClass.FullName)
                .Append('#').Append(useConverter ? 1 : 0);
            return key.ToString();
        }

        /// <summary>
        /// 创建属性拷贝委托
        /// </summary>
        /// <typeparam name="TSource">源类型</typeparam>
        /// <typeparam name="TTarget">目标类型</typeparam>
        /// <returns>拷贝委托</returns>
        private Action<TSource, TTarget> CreateCopyDelegate<TSource, TTarget>()
        {
            var sourceType = typeof(TSource);
            var targetType = typeof(TTarget);

            var sourceProperties = sourceType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var targetProperties = targetType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            return (source, target) =>
            {
                if (source == null || target == null)
                {
                    return;
                }

                foreach (var sourceProperty in sourceProperties)
                {
                    var targetProperty = targetProperties.FirstOrDefault(p => p.Name == sourceProperty.Name && p.PropertyType == sourceProperty.PropertyType);
                    if (targetProperty != null && targetProperty.CanWrite && sourceProperty.CanRead)
                    {
                        try
                        {
                            var value = sourceProperty.GetValue(source);
                            targetProperty.SetValue(target, value);
                        }
                        catch { }
                    }
                }
            };
        }
    }
}