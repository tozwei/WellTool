using System;
using System.Reflection;

namespace WellTool.Core.Lang
{
    /// <summary>
    /// 对象工具类
    /// </summary>
    public static class ObjectUtil
    {
        /// <summary>
        /// 检查对象是否为null
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>是否为null</returns>
        public static bool IsNull(object obj)
        {
            return obj == null;
        }

        /// <summary>
        /// 检查对象是否不为null
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>是否不为null</returns>
        public static bool IsNotNull(object obj)
        {
            return !IsNull(obj);
        }

        /// <summary>
        /// 获取对象的类型
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>对象的类型</returns>
        public static Type GetType(object obj)
        {
            return obj?.GetType();
        }

        /// <summary>
        /// 检查对象是否为指定类型
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="type">类型</param>
        /// <returns>是否为指定类型</returns>
        public static bool IsType(object obj, Type type)
        {
            return obj != null && type != null && type.IsAssignableFrom(obj.GetType());
        }

        /// <summary>
        /// 检查对象是否为指定类型
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="obj">对象</param>
        /// <returns>是否为指定类型</returns>
        public static bool IsType<T>(object obj)
        {
            return IsType(obj, typeof(T));
        }

        /// <summary>
        /// 强制转换对象为指定类型
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="obj">对象</param>
        /// <returns>转换后的对象</returns>
        public static T Cast<T>(object obj)
        {
            return (T)obj;
        }

        /// <summary>
        /// 安全转换对象为指定类型
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="obj">对象</param>
        /// <returns>转换后的对象，转换失败返回默认值</returns>
        public static T SafeCast<T>(object obj)
        {
            return SafeCast(obj, default(T));
        }

        /// <summary>
        /// 安全转换对象为指定类型
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="obj">对象</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换后的对象，转换失败返回默认值</returns>
        public static T SafeCast<T>(object obj, T defaultValue)
        {
            try
            {
                return (T)obj;
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 克隆对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">对象</param>
        /// <returns>克隆后的对象</returns>
        public static T Clone<T>(T obj)
        {
            if (obj == null)
            {
                return default;
            }

            if (obj is ICloneable cloneable)
            {
                return (T)cloneable.Clone();
            }

            // 对于值类型，直接返回（值类型是按值传递的）
            if (typeof(T).IsValueType)
            {
                return obj;
            }

            // 对于字符串，直接返回（字符串是不可变的）
            if (obj is string)
            {
                return obj;
            }

            // 对于数组，创建新数组并复制元素
            if (obj is Array array)
            {
                return (T)array.Clone();
            }

            // 对于其他引用类型，使用反射创建新对象并复制属性
            var type = typeof(T);
            var newObj = Activator.CreateInstance<T>();
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                if (property.CanRead && property.CanWrite)
                {
                    var value = property.GetValue(obj);
                    property.SetValue(newObj, value);
                }
            }

            return newObj;
        }

        /// <summary>
        /// 比较两个对象是否相等
        /// </summary>
        /// <param name="obj1">第一个对象</param>
        /// <param name="obj2">第二个对象</param>
        /// <returns>是否相等</returns>
        public static bool Equals(object obj1, object obj2)
        {
            if (obj1 == null && obj2 == null)
            {
                return true;
            }

            if (obj1 == null || obj2 == null)
            {
                return false;
            }

            return obj1.Equals(obj2);
        }

        /// <summary>
        /// 获取对象的哈希码
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>哈希码</returns>
        public static int GetHashCode(object obj)
        {
            return obj?.GetHashCode() ?? 0;
        }

        /// <summary>
        /// 获取对象的字符串表示
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>字符串表示</returns>
        public static string ToString(object obj)
        {
            return obj?.ToString() ?? "null";
        }
    }
}