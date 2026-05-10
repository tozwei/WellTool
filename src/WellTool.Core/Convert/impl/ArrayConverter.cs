using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WellTool.Core.Convert;

namespace WellTool.Core.Convert.Impl
{
    /// <summary>
    /// 鏁扮粍杞崲鍣?    /// </summary>
    public class ArrayConverter : IConverter
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

            if (!targetType.IsArray)
            {
                return value;
            }

            var elementType = targetType.GetElementType();
            var result = Array.CreateInstance(elementType, 0);

            if (value is IEnumerable enumerable)
            {
                var list = new List<object>();
                foreach (var item in enumerable)
                {
                    list.Add(item);
                }
                result = Array.CreateInstance(elementType, list.Count);
                for (int i = 0; i < list.Count; i++)
                {
                    var convertedValue = ConvertElement(list[i], elementType);
                    result.SetValue(convertedValue, i);
                }
            }
            else
            {
                result = Array.CreateInstance(elementType, 1);
                var convertedValue = ConvertElement(value, elementType);
                result.SetValue(convertedValue, 0);
            }

            return result;
        }

        /// <summary>
        /// 杞崲鍏冪礌
        /// </summary>
        /// <param name="value">瑕佽浆鎹㈢殑鍊?/param>
        /// <param name="targetType">鐩爣绫诲瀷</param>
        /// <returns>杞崲鍚庣殑鍊?/returns>
        private object ConvertElement(object value, Type targetType)
        {
            if (value == null)
            {
                return null;
            }

            if (targetType.IsAssignableFrom(value.GetType()))
            {
                return value;
            }

            // 鍩烘湰绫诲瀷杞崲
            if (targetType.IsPrimitive || targetType == typeof(string) || targetType == typeof(decimal))
            {
                try
                {
                    return System.Convert.ChangeType(value, targetType);
                }
                catch
                {
                    return value;
                }
            }

            return value;
        }

        /// <summary>
        /// 鑾峰彇鏀寔鐨勬簮绫诲瀷
        /// </summary>
        /// <returns>鏀寔鐨勬簮绫诲瀷鏁扮粍</returns>
        public Type[] GetSupportedSourceTypes()
        {
            return new[] { typeof(object) };
        }

        /// <summary>
        /// 鑾峰彇鏀寔鐨勭洰鏍囩被鍨?        /// </summary>
        /// <returns>鏀寔鐨勭洰鏍囩被鍨嬫暟缁?/returns>
        public Type[] GetSupportedTargetTypes()
        {
            return new[] { typeof(Array) };
        }
    }
}


