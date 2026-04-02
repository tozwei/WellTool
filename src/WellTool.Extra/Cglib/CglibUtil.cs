using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WellTool.Extra.Cglib
{
    /// <summary>
    /// Cglib工具类
    /// </summary>
    public static class CglibUtil
    {
        /// <summary>
        /// 拷贝Bean对象属性到目标类型<br>
        /// 此方法通过指定目标类型自动创建之，然后拷贝属性
        /// </summary>
        /// <typeparam name="T">目标对象类型</typeparam>
        /// <param name="source">源bean对象</param>
        /// <param name="targetClass">目标bean类，自动实例化此对象</param>
        /// <returns>目标对象</returns>
        public static T Copy<T>(object source, Type targetClass) where T : class
        {
            var target = Activator.CreateInstance(targetClass) as T;
            CopyToObject(source, target);
            return target;
        }

        /// <summary>
        /// 拷贝Bean对象属性到目标类型<br>
        /// 此方法通过指定目标类型自动创建之，然后拷贝属性
        /// </summary>
        /// <typeparam name="T">目标对象类型</typeparam>
        /// <param name="source">源bean对象</param>
        /// <returns>目标对象</returns>
        public static T Copy<T>(object source) where T : new()
        {
            var target = new T();
            CopyToObject(source, target);
            return target;
        }
        
        /// <summary>
        /// 拷贝Bean对象属性（内部使用，直接处理 object 类型）
        /// </summary>
        /// <param name="source">源bean对象</param>
        /// <param name="target">目标bean对象</param>
        private static void CopyToObject(object source, object target)
        {
            if (source == null || target == null)
            {
                throw new ArgumentNullException(source == null ? nameof(source) : nameof(target), "Source and target beans must be not null.");
            }

            var sourceType = source.GetType();
            var targetType = target.GetType();

            // 直接使用反射进行属性复制
            var sourceProperties = sourceType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var targetProperties = targetType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var sourceProperty in sourceProperties)
            {
                if (!sourceProperty.CanRead)
                    continue;
                    
                var targetProperty = Array.Find(targetProperties, p => 
                    p.Name == sourceProperty.Name && 
                    p.PropertyType == sourceProperty.PropertyType && 
                    p.CanWrite);
                    
                if (targetProperty != null)
                {
                    try
                    {
                        var value = sourceProperty.GetValue(source);
                        targetProperty.SetValue(target, value);
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        /// 拷贝Bean对象属性（不同类型）
        /// </summary>
        /// <typeparam name="TSource">源类型</typeparam>
        /// <typeparam name="TTarget">目标类型</typeparam>
        /// <param name="source">源bean对象</param>
        /// <returns>目标对象</returns>
        public static TTarget Copy<TSource, TTarget>(TSource source) where TTarget : new()
        {
            var target = new TTarget();
            CopyToGeneric(source, target);
            return target;
        }
        
        /// <summary>
        /// 拷贝Bean对象属性（泛型版本，内部使用）
        /// </summary>
        /// <typeparam name="TSource">源类型</typeparam>
        /// <typeparam name="TTarget">目标类型</typeparam>
        /// <param name="source">源bean对象</param>
        /// <param name="target">目标bean对象</param>
        private static void CopyToGeneric<TSource, TTarget>(TSource source, TTarget target)
        {
            if (source == null || target == null)
            {
                throw new ArgumentNullException(source == null ? nameof(source) : nameof(target), "Source and target beans must be not null.");
            }

            var sourceType = typeof(TSource);
            var targetType = typeof(TTarget);
            
            var sourceProperties = sourceType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var targetProperties = targetType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var sourceProperty in sourceProperties)
            {
                if (!sourceProperty.CanRead)
                    continue;
                    
                var targetProperty = Array.Find(targetProperties, p => 
                    p.Name == sourceProperty.Name && 
                    p.PropertyType == sourceProperty.PropertyType && 
                    p.CanWrite);
                    
                if (targetProperty != null)
                {
                    try
                    {
                        var value = sourceProperty.GetValue(source);
                        targetProperty.SetValue(target, value);
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        /// 拷贝Bean对象属性
        /// </summary>
        /// <param name="source">源bean对象</param>
        /// <param name="target">目标bean对象</param>
        public static void Copy(object source, object target)
        {
            if (source == null || target == null)
            {
                throw new ArgumentNullException(source == null ? nameof(source) : nameof(target), "Source and target beans must be not null.");
            }

            var sourceType = source.GetType();
            var targetType = target.GetType();

            // 直接使用反射进行属性复制
            var sourceProperties = sourceType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var targetProperties = targetType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var sourceProperty in sourceProperties)
            {
                if (!sourceProperty.CanRead)
                    continue;
                    
                var targetProperty = Array.Find(targetProperties, p => 
                    p.Name == sourceProperty.Name && 
                    p.PropertyType == sourceProperty.PropertyType && 
                    p.CanWrite);
                    
                if (targetProperty != null)
                {
                    try
                    {
                        var value = sourceProperty.GetValue(source);
                        targetProperty.SetValue(target, value);
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        /// 拷贝Bean对象属性（泛型版本）
        /// </summary>
        /// <typeparam name="TSource">源类型</typeparam>
        /// <typeparam name="TTarget">目标类型</typeparam>
        /// <param name="source">源bean对象</param>
        /// <param name="target">目标bean对象</param>
        public static void Copy<TSource, TTarget>(TSource source, TTarget target)
        {
            if (source == null || target == null)
            {
                throw new ArgumentNullException(source == null ? nameof(source) : nameof(target), "Source and target beans must be not null.");
            }

            // 直接使用反射进行属性复制
            var sourceType = typeof(TSource);
            var targetType = typeof(TTarget);
            
            var sourceProperties = sourceType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var targetProperties = targetType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var sourceProperty in sourceProperties)
            {
                if (!sourceProperty.CanRead)
                    continue;
                    
                var targetProperty = Array.Find(targetProperties, p => 
                    p.Name == sourceProperty.Name && 
                    p.PropertyType == sourceProperty.PropertyType && 
                    p.CanWrite);
                    
                if (targetProperty != null)
                {
                    try
                    {
                        var value = sourceProperty.GetValue(source);
                        targetProperty.SetValue(target, value);
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        /// 拷贝List Bean对象属性
        /// </summary>
        /// <typeparam name="S">源bean类型</typeparam>
        /// <typeparam name="T">目标bean类型</typeparam>
        /// <param name="source">源bean对象list</param>
        /// <returns>目标bean对象list</returns>
        public static List<T> CopyList<S, T>(IEnumerable<S> source) where T : new()
        {
            return CopyList(source, () => new T());
        }

        /// <summary>
        /// 拷贝List Bean对象属性
        /// </summary>
        /// <typeparam name="S">源bean类型</typeparam>
        /// <typeparam name="T">目标bean类型</typeparam>
        /// <param name="source">源bean对象list</param>
        /// <param name="targetSupplier">目标bean对象供应器</param>
        /// <returns>目标bean对象list</returns>
        public static List<T> CopyList<S, T>(IEnumerable<S> source, Func<T> targetSupplier)
        {
            return CopyList(source, targetSupplier, null);
        }

        /// <summary>
        /// 拷贝List Bean对象属性
        /// </summary>
        /// <typeparam name="S">源bean类型</typeparam>
        /// <typeparam name="T">目标bean类型</typeparam>
        /// <param name="source">源bean对象list</param>
        /// <param name="targetSupplier">目标bean对象供应器</param>
        /// <param name="callback">回调对象</param>
        /// <returns>目标bean对象list</returns>
        public static List<T> CopyList<S, T>(IEnumerable<S> source, Func<T> targetSupplier, Action<S, T> callback)
        {
            if (source == null)
            {
                return new List<T>();
            }

            return source.Select(s => {
                var t = targetSupplier();
                Copy(s, t);
                callback?.Invoke(s, t);
                return t;
            }).ToList();
        }

        /// <summary>
        /// 将Bean转换为Map
        /// </summary>
        /// <param name="bean">Bean对象</param>
        /// <returns>Map</returns>
        public static Dictionary<string, object> ToMap(object bean)
        {
            if (bean == null)
            {
                return new Dictionary<string, object>();
            }

            var map = new Dictionary<string, object>();
            var properties = bean.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in properties)
            {
                if (property.CanRead)
                {
                    try
                    {
                        var value = property.GetValue(bean);
                        map[property.Name] = value;
                    }
                    catch { }
                }
            }
            return map;
        }

        /// <summary>
        /// 将Map中的内容填充至Bean中
        /// </summary>
        /// <typeparam name="T">Bean类型</typeparam>
        /// <param name="map">Map</param>
        /// <param name="bean">Bean</param>
        /// <returns>bean</returns>
        public static T FillBean<T>(Dictionary<string, object> map, T bean)
        {
            if (map == null || bean == null)
            {
                return bean;
            }

            var properties = bean.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in properties)
            {
                if (property.CanWrite && map.TryGetValue(property.Name, out var value))
                {
                    try
                    {
                        if (value != null && property.PropertyType.IsAssignableFrom(value.GetType()))
                        {
                            property.SetValue(bean, value);
                        }
                        else if (value != null)
                        {
                            // 尝试类型转换
                            var convertedValue = Convert.ChangeType(value, property.PropertyType);
                            property.SetValue(bean, convertedValue);
                        }
                    }
                    catch { }
                }
            }
            return bean;
        }

        /// <summary>
        /// 将Map转换为Bean
        /// </summary>
        /// <typeparam name="T">Bean类型</typeparam>
        /// <param name="map">Map</param>
        /// <returns>bean</returns>
        public static T ToBean<T>(Dictionary<string, object> map) where T : new()
        {
            var bean = new T();
            return FillBean(map, bean);
        }
    }
}