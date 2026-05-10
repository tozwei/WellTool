using System;
using WellTool.Core.Convert;
using System.Collections.Generic;
using WellTool.Core.Convert;

namespace WellTool.Core.Convert.Impl
{
    /// <summary>
    /// 鏋氫妇杞崲鍣?    /// </summary>
    public class EnumConverter : IConverter
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
                throw new ConvertException("Cannot convert null to enum");
            }

            if (!targetType.IsEnum)
            {
                throw new ConvertException($"Target type {targetType.Name} is not an enum");
            }

            if (value is string stringValue)
            {
                return Enum.Parse(targetType, stringValue);
            }
            else if (value is int intValue)
            {
                return Enum.ToObject(targetType, intValue);
            }
            else if (value is long longValue)
            {
                return Enum.ToObject(targetType, longValue);
            }

            throw new ConvertException($"Cannot convert {value.GetType().Name} to enum");
        }

        /// <summary>
        /// 鑾峰彇鏀寔鐨勬簮绫诲瀷
        /// </summary>
        /// <returns>鏀寔鐨勬簮绫诲瀷鏁扮粍</returns>
        public Type[] GetSupportedSourceTypes()
        {
            return new[] { typeof(string), typeof(int), typeof(long) };
        }

        /// <summary>
        /// 鑾峰彇鏀寔鐨勭洰鏍囩被鍨?        /// </summary>
        /// <returns>鏀寔鐨勭洰鏍囩被鍨嬫暟缁?/returns>
        public Type[] GetSupportedTargetTypes()
        {
            return new[] { typeof(Enum) };
        }
    }
}
