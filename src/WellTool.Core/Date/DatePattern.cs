namespace WellTool.Core.Date
{
    /// <summary>
    /// 日期格式常量
    /// </summary>
    public static class DatePattern
    {
        /// <summary>
        /// 标准日期格式：yyyy-MM-dd
        /// </summary>
        public const string NORM_DATE_PATTERN = "yyyy-MM-dd";

        /// <summary>
        /// 标准时间格式：HH:mm:ss
        /// </summary>
        public const string NORM_TIME_PATTERN = "HH:mm:ss";

        /// <summary>
        /// 标准日期时间格式：yyyy-MM-dd HH:mm:ss
        /// </summary>
        public const string NORM_DATETIME_PATTERN = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// 标准日期时间格式（带毫秒）：yyyy-MM-dd HH:mm:ss.fff
        /// </summary>
        public const string NORM_DATETIME_MS_PATTERN = "yyyy-MM-dd HH:mm:ss.fff";

        /// <summary>
        /// 紧凑日期格式：yyyyMMdd
        /// </summary>
        public const string COMPACT_DATE_PATTERN = "yyyyMMdd";

        /// <summary>
        /// 紧凑时间格式：HHmmss
        /// </summary>
        public const string COMPACT_TIME_PATTERN = "HHmmss";

        /// <summary>
        /// 紧凑日期时间格式：yyyyMMddHHmmss
        /// </summary>
        public const string COMPACT_DATETIME_PATTERN = "yyyyMMddHHmmss";

        /// <summary>
        /// 紧凑日期时间格式（带毫秒）：yyyyMMddHHmmssfff
        /// </summary>
        public const string COMPACT_DATETIME_MS_PATTERN = "yyyyMMddHHmmssfff";

        /// <summary>
        /// 中文日期格式：yyyy年MM月dd日
        /// </summary>
        public const string CHINESE_DATE_PATTERN = "yyyy年MM月dd日";

        /// <summary>
        /// 中文时间格式：HH时mm分ss秒
        /// </summary>
        public const string CHINESE_TIME_PATTERN = "HH时mm分ss秒";

        /// <summary>
        /// 中文日期时间格式：yyyy年MM月dd日 HH时mm分ss秒
        /// </summary>
        public const string CHINESE_DATETIME_PATTERN = "yyyy年MM月dd日 HH时mm分ss秒";

        /// <summary>
        /// ISO8601日期格式：yyyy-MM-ddTHH:mm:ss
        /// </summary>
        public const string ISO8601_PATTERN = "yyyy-MM-ddTHH:mm:ss";

        /// <summary>
        /// ISO8601日期格式（带毫秒）：yyyy-MM-ddTHH:mm:ss.fff
        /// </summary>
        public const string ISO8601_MS_PATTERN = "yyyy-MM-ddTHH:mm:ss.fff";

        /// <summary>
        /// HTTP日期格式：EEE, dd MMM yyyy HH:mm:ss zzz
        /// </summary>
        public const string HTTP_DATETIME_PATTERN = "r";
    }
}