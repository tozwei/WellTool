using System;
using WellTool.Core.Convert;

namespace WellTool.Core.Convert.Impl
{
    /// <summary>
    /// 鏃ユ湡杞崲锟?
    /// </summary>
    public class DateConverter : IConverter
    {
        private readonly string _format;

        /// <summary>
        /// 鏋勯€犲嚱锟?
        /// </summary>
        /// <param name="format">鏃ユ湡鏍煎紡</param>
        public DateConverter(string format = null)
        {
            _format = format;
        }

        /// <summary>
        /// 杞崲锟?
        /// </summary>
        public object Convert(object value, Type targetType)
        {
            if (value == null)
            {
                return null;
            }

            // 澶勭悊 DateTime
            if (value is DateTime dt)
            {
                return dt;
            }



            // 澶勭悊鏁板瓧锛堟绉掓椂闂存埑锟?
            if (value is long longValue)
            {
                // 姣鏃堕棿锟?
                if (longValue > 1e12)
                {
                    return DateTimeOffset.FromUnixTimeMilliseconds(longValue).DateTime;
                }
                // 绉掓椂闂存埑
                return DateTimeOffset.FromUnixTimeSeconds(longValue).DateTime;
            }

            // 澶勭悊瀛楃锟?
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

            // 灏濊瘯澶氱鏍煎紡瑙ｆ瀽
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

            // 鏈€鍚庡皾璇曢€氱敤瑙ｆ瀽
            if (DateTime.TryParse(str, out DateTime dtResult))
            {
                return dtResult;
            }

            throw new ConvertException($"Cannot convert '{str}' to DateTime");
        }

        /// <summary>
        /// 鑾峰彇鏀寔鐨勬簮绫诲瀷
        /// </summary>
        public Type[] GetSupportedSourceTypes()
        {
            return new Type[] { typeof(string), typeof(long), typeof(DateTime) };
        }

        /// <summary>
        /// 鑾峰彇鏀寔鐨勭洰鏍囩被锟?
        /// </summary>
        public Type[] GetSupportedTargetTypes()
        {
            return new Type[] { typeof(DateTime) };
        }
    }
}

