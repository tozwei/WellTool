using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace WellTool.Extra.Cglib
{
    /// <summary>
    /// Cglib工具类
    /// </summary>
    public static class CglibUtil
    {
        /// <summary>
        /// 将对象转换为字典
        /// </summary>
        /// <param name="obj">源对象</param>
        /// <returns>字典</returns>
        public static Dictionary<string, object> ToMap(object obj)
        {
            if (obj == null) return new Dictionary<string, object>();

            var result = new Dictionary<string, object>();
            var type = obj.GetType();
            var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                if (prop.CanRead)
                {
                    result[prop.Name] = prop.GetValue(obj);
                }
            }

            return result;
        }

        /// <summary>
        /// 将字典转换为对象
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="map">源字典</param>
        /// <returns>目标对象</returns>
        public static T ToBean<T>(Dictionary<string, object> map) where T : new()
        {
            if (map == null) return new T();

            var result = new T();
            var type = typeof(T);
            var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                if (prop.CanWrite && map.TryGetValue(prop.Name, out var value))
                {
                    try
                    {
                        if (value != null && prop.PropertyType != value.GetType())
                        {
                            value = Convert.ChangeType(value, prop.PropertyType);
                        }
                        prop.SetValue(result, value);
                    }
                    catch
                    {
                        // 忽略属性赋值错误
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 拷贝Bean对象属性到目标类型
        /// </summary>
        /// <typeparam name="T">目标对象类型</typeparam>
        /// <param name="source">源bean对象</param>
        /// <param name="targetClass">目标bean类</param>
        /// <returns>目标对象</returns>
        public static T Copy<T>(object source, Type targetClass) where T : new()
        {
            return Copy<T>(source, targetClass, null);
        }

        /// <summary>
        /// 拷贝Bean对象属性
        /// </summary>
        /// <typeparam name="T">目标对象类型</typeparam>
        /// <param name="source">源bean对象</param>
        /// <param name="targetClass">目标bean类</param>
        /// <param name="converter">转换器</param>
        /// <returns>目标对象</returns>
        public static T Copy<T>(object source, Type targetClass, Func<object, object, object> converter) where T : new()
        {
            var target = Activator.CreateInstance<T>();
            Copy(source, target, converter);
            return target;
        }

        /// <summary>
        /// 拷贝Bean对象属性
        /// </summary>
        /// <param name="source">源bean对象</param>
        /// <param name="target">目标bean对象</param>
        public static void Copy(object source, object target)
        {
            Copy(source, target, null);
        }

        /// <summary>
        /// 拷贝Bean对象属性
        /// </summary>
        /// <param name="source">源bean对象</param>
        /// <param name="target">目标bean对象</param>
        /// <param name="converter">转换器</param>
        public static void Copy(object source, object target, Func<object, object, object> converter)
        {
            if (source == null) throw new ArgumentNullException(nameof(source), "Source bean must be not null.");
            if (target == null) throw new ArgumentNullException(nameof(target), "Target bean must be not null.");

            var sourceType = source.GetType();
            var targetType = target.GetType();

            // 获取源和目标的所有属性
            var sourceProps = sourceType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var targetProps = targetType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .ToDictionary(p => p.Name.ToLower(), p => p);

            foreach (var sourceProp in sourceProps)
            {
                if (!targetProps.TryGetValue(sourceProp.Name.ToLower(), out var targetProp)) continue;
                if (!targetProp.CanWrite) continue;
                if (!sourceProp.CanRead) continue;

                try
                {
                    var value = sourceProp.GetValue(source);
                    if (converter != null)
                    {
                        value = converter(value, null);
                    }
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
        }

        /// <summary>
        /// 拷贝List Bean对象属性
        /// </summary>
        /// <typeparam name="S">源bean类型</typeparam>
        /// <typeparam name="T">目标bean类型</typeparam>
        /// <param name="source">源bean对象list</param>
        /// <param name="targetCreator">目标对象创建函数</param>
        /// <returns>目标bean对象list</returns>
        public static List<T> CopyList<S, T>(IEnumerable<S> source, Func<T> targetCreator) where T : new()
        {
            return CopyList(source, targetCreator, null, null);
        }

        /// <summary>
        /// 拷贝List Bean对象属性
        /// </summary>
        /// <typeparam name="S">源bean类型</typeparam>
        /// <typeparam name="T">目标bean类型</typeparam>
        /// <param name="source">源bean对象list</param>
        /// <param name="targetCreator">目标对象创建函数</param>
        /// <param name="converter">转换器</param>
        /// <returns>目标bean对象list</returns>
        public static List<T> CopyList<S, T>(IEnumerable<S> source, Func<T> targetCreator, Func<object, object, object> converter) where T : new()
        {
            return CopyList(source, targetCreator, converter, null);
        }

        /// <summary>
        /// 拷贝List Bean对象属性
        /// </summary>
        /// <typeparam name="S">源bean类型</typeparam>
        /// <typeparam name="T">目标bean类型</typeparam>
        /// <param name="source">源bean对象list</param>
        /// <param name="targetCreator">目标对象创建函数</param>
        /// <param name="callback">回调函数</param>
        /// <returns>目标bean对象list</returns>
        public static List<T> CopyList<S, T>(IEnumerable<S> source, Func<T> targetCreator, Action<S, T> callback) where T : new()
        {
            return CopyList(source, targetCreator, null, callback);
        }

        /// <summary>
        /// 拷贝List Bean对象属性
        /// </summary>
        /// <typeparam name="S">源bean类型</typeparam>
        /// <typeparam name="T">目标bean类型</typeparam>
        /// <param name="source">源bean对象list</param>
        /// <param name="targetCreator">目标对象创建函数</param>
        /// <param name="converter">转换器</param>
        /// <param name="callback">回调函数</param>
        /// <returns>目标bean对象list</returns>
        public static List<T> CopyList<S, T>(IEnumerable<S> source, Func<T> targetCreator, Func<object, object, object> converter, Action<S, T> callback) where T : new()
        {
            return source.Select(s =>
            {
                var t = targetCreator();
                Copy(s, t, converter);
                callback?.Invoke(s, t);
                return t;
            }).ToList();
        }
    }
}
