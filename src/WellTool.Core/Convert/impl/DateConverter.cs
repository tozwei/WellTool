using System;

namespace WellTool.Core.Converter.impl
{
    /// <summary>
    /// 日期转换器
    /// </summary>
    public class DateConverter : IConverter
    {
        private readonly string _format;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="format">日期格式</param>
        public DateConverter(string format = null)
        {
            _format = format;
        }

        /// <summary>
        /// 转换值
        /// </summary>
        public object Convert(object value, Type targetType)
        {
            if (value == null)
            {
                return null;
            }

            // 处理 DateTime
            if (value is DateTime dt)
            {
                return dt;
            }

            // 处理 DateOnly
            if (value is DateOnly dateOnly)
            {
                return dateOnly.ToDateTime(TimeOnly.MinValue);
            }

            // 处理数字（毫秒时间戳）
            if (value is long longValue)
            {
                // 毫秒时间戳
                if (longValue > 1e12)
                {
                    return DateTimeOffset.FromUnixTimeMilliseconds(longValue).DateTime;
                }
                // 秒时间戳
                return DateTimeOffset.FromUnixTimeSeconds(longValue).DateTime;
            }

            // 处理字符串
            var str = value.ToString();
            if (string.IsNullOrWhiteSpace(str))
            {
                return null;
            }

            if (!string.IsNullOrEmpty(_format))
            {
                if (DateTime.TryParseExact(str, _format, null, System.Globalization.DateTimeStyles.None, out DateTime result))
                {
                    return result;
                }
            }

            // 尝试多种格式解析
            string[] formats = {
                "yyyy-MM-dd HH:mm:ss",
                "yyyy-MM-dd",
                "yyyy/MM/dd",
                "yyyy/MM/dd HH:mm:ss",
                "yyyyMMdd",
                "yyyyMMddHHmmss"
            };

            foreach (var format in formats)
            {
                if (DateTime.TryParseExact(str, format, null, System.Globalization.DateTimeStyles.None, out DateTime result))
                {
                    return result;
                }
            }

            // 最后尝试通用解析
            if (DateTime.TryParse(str, out DateTime dtResult))
            {
                return dtResult;
            }

            throw new ConvertException($"Cannot convert '{str}' to DateTime");
        }

        /// <summary>
        /// 获取支持的源类型
        /// </summary>
        public Type[] GetSupportedSourceTypes()
        {
            return new Type[] { typeof(string), typeof(long), typeof(DateTime), typeof(DateOnly) };
        }

        /// <summary>
        /// 获取支持的目标类型
        /// </summary>
        public Type[] GetSupportedTargetTypes()
        {
            return new Type[] { typeof(DateTime) };
        }
    }
}
