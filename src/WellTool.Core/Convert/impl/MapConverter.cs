using System;
using WellTool.Core.Convert;
using System.Collections;
using WellTool.Core.Convert;
using System.Collections.Generic;
using WellTool.Core.Convert;

namespace WellTool.Core.Convert.impl
{
    /// <summary>
    /// 映射转换�?    /// </summary>
    public class MapConverter : IConverter
    {
        /// <summary>
        /// 转换�?        /// </summary>
        /// <param name="value">要转换的�?/param>
        /// <param name="targetType">目标类型</param>
        /// <returns>转换后的�?/returns>
        public object Convert(object value, Type targetType)
        {
            if (value == null)
            {
                return null;
            }

            if (value is IDictionary dictionary)
            {
                if (targetType.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(Dictionary<,>))
                {
                    return ConvertToDictionary(dictionary, targetType.GetGenericArguments()[0], targetType.GetGenericArguments()[1]);
                }
                else if (targetType.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(IDictionary<,>))
                {
                    return ConvertToDictionary(dictionary, targetType.GetGenericArguments()[0], targetType.GetGenericArguments()[1]);
                }
            }

            throw new ConvertException($"Cannot convert {value.GetType().Name} to {targetType.Name}");
        }

        /// <summary>
        /// 转换为字�?        /// </summary>
        /// <param name="dictionary">字典</param>
        /// <param name="keyType">键类�?/param>
        /// <param name="valueType">值类�?/param>
        /// <returns>字典</returns>
        private object ConvertToDictionary(IDictionary dictionary, Type keyType, Type valueType)
        {
            var dictType = typeof(Dictionary<,>).MakeGenericType(keyType, valueType);
            var dict = Activator.CreateInstance(dictType) as IDictionary;

            foreach (DictionaryEntry entry in dictionary)
            {
                dict.Add(entry.Key, entry.Value);
            }

            return dict;
        }

        /// <summary>
        /// 获取支持的源类型
        /// </summary>
        /// <returns>支持的源类型数组</returns>
        public Type[] GetSupportedSourceTypes()
        {
            return new[] { typeof(IDictionary) };
        }

        /// <summary>
        /// 获取支持的目标类�?        /// </summary>
        /// <returns>支持的目标类型数�?/returns>
        public Type[] GetSupportedTargetTypes()
        {
            return new[] { typeof(IDictionary<,>), typeof(Dictionary<,>) };
        }
    }
}