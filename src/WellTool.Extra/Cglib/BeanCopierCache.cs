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
                @delegate = CreateCopyDelegateGeneric(sourceType, targetType);
                cache[key] = @delegate;
            }
            return @delegate;
        }

        /// <summary>
        /// 创建属性拷贝委托（用于反射调用）
        /// </summary>
        /// <param name="sourceType">源类型</param>
        /// <param name="targetType">目标类型</param>
        /// <returns>拷贝委托</returns>
        private Delegate CreateCopyDelegateGeneric(Type sourceType, Type targetType)
        {
            var sourceProperties = sourceType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var targetProperties = targetType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // 创建动态方法
            var sourceParam = System.Linq.Expressions.Expression.Parameter(typeof(object), "source");
            var targetParam = System.Linq.Expressions.Expression.Parameter(typeof(object), "target");

            var sourceCast = System.Linq.Expressions.Expression.Convert(sourceParam, sourceType);
            var targetCast = System.Linq.Expressions.Expression.Convert(targetParam, targetType);

            var expressions = new List<System.Linq.Expressions.Expression>();

            foreach (var sourceProperty in sourceProperties)
            {
                var targetProperty = targetProperties.FirstOrDefault(p => p.Name == sourceProperty.Name && p.PropertyType == sourceProperty.PropertyType);
                if (targetProperty != null && targetProperty.CanWrite && sourceProperty.CanRead)
                {
                    var sourceAccess = System.Linq.Expressions.Expression.Property(sourceCast, sourceProperty);
                    var targetAccess = System.Linq.Expressions.Expression.Property(targetCast, targetProperty);
                    var assignment = System.Linq.Expressions.Expression.Assign(targetAccess, sourceAccess);
                    expressions.Add(assignment);
                }
            }

            System.Linq.Expressions.Expression body;
            if (expressions.Count == 0)
            {
                body = System.Linq.Expressions.Expression.Empty();
            }
            else
            {
                body = System.Linq.Expressions.Expression.Block(expressions);
            }
            
            var lambdaType = typeof(Action<,>).MakeGenericType(sourceType, targetType);
            var lambda = System.Linq.Expressions.Expression.Lambda(lambdaType, body, sourceParam, targetParam);
            
            return lambda.Compile();
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