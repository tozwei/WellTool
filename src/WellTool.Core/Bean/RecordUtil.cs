using System;
using System.Linq;
using System.Reflection;

namespace WellTool.Core.Bean
{
    /// <summary>
    /// Record类型工具类
    /// </summary>
    public static class RecordUtil
    {
        /// <summary>
        /// 将对象转换为Record
        /// </summary>
        /// <typeparam name="T">Record类型</typeparam>
        /// <param name="obj">源对象</param>
        /// <returns>Record实例</returns>
        public static T ToRecord<T>(object obj) where T : class
        {
            if (obj == null)
            {
                return null;
            }

            // 检查是否是Record类型
            if (!IsRecord(typeof(T)))
            {
                throw new ArgumentException($"Type {typeof(T).Name} is not a record type");
            }

            // 获取Record的构造函数
            var recordType = typeof(T);
            var properties = recordType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var constructor = recordType.GetConstructors().FirstOrDefault();

            if (constructor == null)
            {
                throw new ArgumentException($"No public constructor found for record type {typeof(T).Name}");
            }

            var parameters = constructor.GetParameters();
            var args = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                var param = parameters[i];
                var sourceProp = properties.FirstOrDefault(p => p.Name.Equals(param.Name, StringComparison.OrdinalIgnoreCase));
                if (sourceProp != null)
                {
                    var value = sourceProp.GetValue(obj);
                    if (value != null && !param.ParameterType.IsAssignableFrom(value.GetType()))
                    {
                        value = Converter.Converter.To(value, param.ParameterType);
                    }
                    args[i] = value;
                }
            }

            return (T)constructor.Invoke(args);
        }

        /// <summary>
        /// 判断是否是Record类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>是否是Record类型</returns>
        public static bool IsRecord(Type type)
        {
            // .NET中的Record类型有特定特征
            // 检查是否有Init属性（Record的特征）
            if (type.GetCustomAttribute<System.Runtime.CompilerServices.RecordAttribute>() != null)
            {
                return true;
            }

            // 检查类型名是否以Record结尾（编译后的Record类型特征）
            if (type.Name.EndsWith("Record"))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 获取Record的所有属性值
        /// </summary>
        /// <param name="record">Record实例</param>
        /// <returns>属性值字典</returns>
        public static System.Collections.Generic.Dictionary<string, object> GetRecordValues(object record)
        {
            if (record == null)
            {
                return null;
            }

            var result = new System.Collections.Generic.Dictionary<string, object>();
            var type = record.GetType();
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in properties)
            {
                result[prop.Name] = prop.GetValue(record);
            }

            return result;
        }
    }
}
