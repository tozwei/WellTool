using System;

namespace WellTool.Json
{
    /// <summary>
    /// JSON Null 值表示
    /// </summary>
    public class JSONNull
    {
        /// <summary>
        /// JSON Null 的单例实例
        /// </summary>
        public static readonly JSONNull NULL = new JSONNull();

        /// <summary>
        /// 私有构造函数
        /// </summary>
        private JSONNull()
        {
        }

        /// <summary>
        /// 检查是否为空
        /// </summary>
        public static bool IsNull(object obj)
        {
            return obj == null || obj is JSONNull || obj == JSONNull.NULL;
        }

        /// <summary>
        /// 返回 "null" 字符串
        /// </summary>
        public override string ToString()
        {
            return "null";
        }

        /// <summary>
        /// 返回空字符串
        /// </summary>
        public string ToString(string defaultValue)
        {
            return defaultValue;
        }

        /// <summary>
        /// 返回 0
        /// </summary>
        public int ToInt(int defaultValue)
        {
            return defaultValue;
        }

        /// <summary>
        /// 返回 0L
        /// </summary>
        public long ToLong(long defaultValue)
        {
            return defaultValue;
        }

        /// <summary>
        /// 返回 0.0
        /// </summary>
        public double ToDouble(double defaultValue)
        {
            return defaultValue;
        }

        /// <summary>
        /// 返回 false
        /// </summary>
        public bool ToBool(bool defaultValue)
        {
            return defaultValue;
        }

        /// <summary>
        /// 返回默认值
        /// </summary>
        public T ToValue<T>(T defaultValue)
        {
            return defaultValue;
        }
    }
}
