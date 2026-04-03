using System;

namespace WellTool.Core.Lang.Id
{
    /// <summary>
    /// ID常量
    /// </summary>
    public static class IdConstants
    {
        /// <summary>
        /// 空ID
        /// </summary>
        public const string NULL = "null";

        /// <summary>
        /// 未知ID
        /// </summary>
        public const string UNKNOWN = "unknown";

        /// <summary>
        /// 默认雪花ID机器ID
        /// </summary>
        public const int DEFAULT_MACHINE_ID = 1;

        /// <summary>
        /// 最大机器ID
        /// </summary>
        public const int MAX_MACHINE_ID = 1023;

        /// <summary>
        /// 最大数据中心ID
        /// </summary>
        public const int MAX_DATACENTER_ID = 31;

        /// <summary>
        /// Twitter雪花算法时间戳基准
        /// </summary>
        public const long TWEPOCH = 1288834974657L;

        /// <summary>
        /// 机器ID位数
        /// </summary>
        public const int MACHINE_ID_BITS = 10;

        /// <summary>
        /// 数据中心ID位数
        /// </summary>
        public const int DATACENTER_ID_BITS = 5;

        /// <summary>
        /// 序列号位数
        /// </summary>
        public const int SEQUENCE_BITS = 12;

        /// <summary>
        /// 机器ID位移
        /// </summary>
        public const int MACHINE_ID_SHIFT = SEQUENCE_BITS;

        /// <summary>
        /// 数据中心ID位移
        /// </summary>
        public const int DATACENTER_ID_SHIFT = SEQUENCE_BITS + MACHINE_ID_BITS;

        /// <summary>
        /// 时间戳位移
        /// </summary>
        public const int TIMESTAMP_SHIFT = SEQUENCE_BITS + MACHINE_ID_BITS + DATACENTER_ID_BITS;

        /// <summary>
        /// 序列号掩码
        /// </summary>
        public const int SEQUENCE_MASK = -1 ^ (-1 << SEQUENCE_BITS);

        /// <summary>
        /// 机器ID掩码
        /// </summary>
        public const int MACHINE_ID_MASK = -1 ^ (-1 << MACHINE_ID_BITS);

        /// <summary>
        /// 数据中心ID掩码
        /// </summary>
        public const int DATACENTER_ID_MASK = -1 ^ (-1 << DATACENTER_ID_BITS);
    }
}
