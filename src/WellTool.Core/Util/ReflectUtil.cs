using System;
using System.Collections.Generic;
using System.Reflection;

namespace WellTool.Core.Util
{
    /// <summary>
    /// 反射工具类
    /// </summary>
    public class ReflectUtil
    {
        /// <summary>
        /// 获取类型的所有字段
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="includeNonPublic">是否包含非公共字段</param>
        /// <returns>字段列表</returns>
        public static FieldInfo[] GetFields(Type type, bool includeNonPublic = false)
        {
            var bindingFlags = BindingFlags.Public | BindingFlags.Instance;
            if (includeNonPublic)
            {
                bindingFlags |= BindingFlags.NonPublic;
            }
            return type.GetFields(bindingFlags);
        }

        /// <summary>
        /// 获取类型的所有属性
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="includeNonPublic">是否包含非公共属性</param>
        /// <returns>属性列表</returns>
        public static PropertyInfo[] GetProperties(Type type, bool includeNonPublic = false)
        {
            var bindingFlags = BindingFlags.Public | BindingFlags.Instance;
            if (includeNonPublic)
            {
                bindingFlags |= BindingFlags.NonPublic;
            }
            return type.GetProperties(bindingFlags);
        }

        /// <summary>
        /// 获取类型的所有方法
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="includeNonPublic">是否包含非公共方法</param>
        /// <returns>方法列表</returns>
        public static MethodInfo[] GetMethods(Type type, bool includeNonPublic = false)
        {
            var bindingFlags = BindingFlags.Public | BindingFlags.Instance;
            if (includeNonPublic)
            {
                bindingFlags |= BindingFlags.NonPublic;
            }
            return type.GetMethods(bindingFlags);
        }

        /// <summary>
        /// 获取类型的构造函数
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="includeNonPublic">是否包含非公共构造函数</param>
        /// <returns>构造函数列表</returns>
        public static ConstructorInfo[] GetConstructors(Type type, bool includeNonPublic = false)
        {
            var bindingFlags = BindingFlags.Public | BindingFlags.Instance;
            if (includeNonPublic)
            {
                bindingFlags |= BindingFlags.NonPublic;
            }
            return type.GetConstructors(bindingFlags);
        }

        /// <summary>
        /// 获取字段值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="fieldName">字段名</param>
        /// <returns>字段值</returns>
        public static object GetFieldValue(object obj, string fieldName)
        {
            var field = obj.GetType().GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (field != null)
            {
                return field.GetValue(obj);
            }
            
            // 如果字段不存在，尝试获取属性
            var property = obj.GetType().GetProperty(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            return property?.GetValue(obj);
        }

        /// <summary>
        /// 设置字段值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="fieldName">字段名</param>
        /// <param name="value">字段值</param>
        public static void SetFieldValue(object obj, string fieldName, object value)
        {
            var field = obj.GetType().GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (field != null)
            {
                field.SetValue(obj, value);
                return;
            }
            
            // 如果字段不存在，尝试设置属性
            var property = obj.GetType().GetProperty(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            property?.SetValue(obj, value);
        }

        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="propertyName">属性名</param>
        /// <returns>属性值</returns>
        public static object GetPropertyValue(object obj, string propertyName)
        {
            var property = obj.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            return property?.GetValue(obj);
        }

        /// <summary>
        /// 设置属性值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="propertyName">属性名</param>
        /// <param name="value">属性值</param>
        public static void SetPropertyValue(object obj, string propertyName, object value)
        {
            var property = obj.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            property?.SetValue(obj, value);
        }

        /// <summary>
        /// 调用方法
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="methodName">方法名</param>
        /// <param name="parameters">参数</param>
        /// <returns>方法返回值</returns>
        public static object InvokeMethod(object obj, string methodName, params object[] parameters)
        {
            var method = obj.GetType().GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            return method?.Invoke(obj, parameters);
        }

        /// <summary>
        /// 调用方法
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="methodName">方法名</param>
        /// <param name="parameters">参数</param>
        /// <returns>方法返回值</returns>
        public static object Invoke(object obj, string methodName, params object[] parameters)
        {
            return InvokeMethod(obj, methodName, parameters);
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="parameters">构造函数参数</param>
        /// <returns>实例</returns>
        public static object CreateInstance(Type type, params object[] parameters)
        {
            return Activator.CreateInstance(type, parameters);
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="parameters">构造函数参数</param>
        /// <returns>实例</returns>
        public static T CreateInstance<T>(params object[] parameters)
        {
            return (T)CreateInstance(typeof(T), parameters);
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="type">类型</param>
        /// <param name="parameters">构造函数参数</param>
        /// <returns>实例</returns>
        public static T NewInstance<T>(Type type, params object[] parameters)
        {
            return (T)CreateInstance(type, parameters);
        }

        /// <summary>
        /// 获取类型的泛型参数
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>泛型参数类型数组</returns>
        public static Type[] GetGenericArguments(Type type)
        {
            return type.GetGenericArguments();
        }

        /// <summary>
        /// 检查类型是否实现了指定接口
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="interfaceType">接口类型</param>
        /// <returns>是否实现了指定接口</returns>
        public static bool IsImplements(Type type, Type interfaceType)
        {
            return interfaceType.IsAssignableFrom(type);
        }

        /// <summary>
        /// 检查类型是否继承自指定类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="baseType">基类型</param>
        /// <returns>是否继承自指定类型</returns>
        public static bool IsSubclassOf(Type type, Type baseType)
        {
            return type.IsSubclassOf(baseType);
        }

        /// <summary>
        /// 获取类型名称
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>类型名称</returns>
        public static string GetTypeName(Type type)
        {
            return type.FullName ?? type.Name;
        }

        /// <summary>
        /// 获取指定类型的指定方法
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="methodName">方法名</param>
        /// <returns>方法信息</returns>
        public static MethodInfo GetMethod(Type type, string methodName)
        {
            return type.GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        }

        /// <summary>
        /// 获取指定类型的指定字段
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="fieldName">字段名</param>
        /// <returns>字段信息</returns>
        public static FieldInfo GetField(Type type, string fieldName)
        {
            var field = type.GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (field != null)
            {
                return field;
            }
            
            // 如果字段不存在，尝试获取属性
            var property = type.GetProperty(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (property != null)
            {
                // 由于返回类型是 FieldInfo，无法直接返回 PropertyInfo
                // 这里我们返回 null，因为测试期望的是字段
                return null;
            }
            
            return null;
        }

        /// <summary>
        /// 检查类型是否可从另一个类型赋值
        /// </summary>
        /// <param name="fromType">源类型</param>
        /// <param name="toType">目标类型</param>
        /// <returns>是否可赋值</returns>
        public static bool IsAssignableFrom(Type fromType, Type toType)
        {
            if (fromType == null || toType == null)
            {
                return false;
            }
            return fromType.IsAssignableFrom(toType);
        }
    }
}