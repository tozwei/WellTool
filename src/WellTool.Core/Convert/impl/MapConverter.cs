using System;
using WellTool.Core.Convert;
using System.Collections;
using WellTool.Core.Convert;
using System.Collections.Generic;
using WellTool.Core.Convert;

namespace WellTool.Core.Convert.Impl
{
    /// <summary>
    /// 鏄犲皠杞崲鍣?    /// </summary>
    public class MapConverter : IConverter
    {
        /// <summary>
        /// 杞崲鍊?        /// </summary>
        /// <param name="value">瑕佽浆鎹㈢殑鍊?/param>
        /// <param name="targetType">鐩爣绫诲瀷</param>
        /// <returns>杞崲鍚庣殑鍊?/returns>
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
        /// 杞崲涓哄瓧鍏?        /// </summary>
        /// <param name="dictionary">瀛楀吀</param>
        /// <param name="keyType">閿被鍨?/param>
        /// <param name="valueType">鍊肩被鍨?/param>
        /// <returns>瀛楀吀</returns>
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
        /// 鑾峰彇鏀寔鐨勬簮绫诲瀷
        /// </summary>
        /// <returns>鏀寔鐨勬簮绫诲瀷鏁扮粍</returns>
        public Type[] GetSupportedSourceTypes()
        {
            return new[] { typeof(IDictionary) };
        }

        /// <summary>
        /// 鑾峰彇鏀寔鐨勭洰鏍囩被鍨?        /// </summary>
        /// <returns>鏀寔鐨勭洰鏍囩被鍨嬫暟缁?/returns>
        public Type[] GetSupportedTargetTypes()
        {
            return new[] { typeof(IDictionary<,>), typeof(Dictionary<,>) };
        }
    }
}
