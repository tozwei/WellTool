using System;

namespace WellTool.Core.Util
{
    /// <summary>
    /// 布尔工具类
    /// </summary>
    public static class BooleanUtil
    {
        /// <summary>
        /// 将字符串转换为布尔值
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>布尔值</returns>
        public static bool ToBoolean(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return false;
            }

            str = str.Trim().ToLower();

            return str == "true" || str == "yes" || str == "y" || str == "t" ||
                   str == "ok" || str == "1" || str == "on" ||
                   str == "是" || str == "对" || str == "真" || str == "√";
        }

        /// <summary>
        /// 将布尔值转换为字符串
        /// </summary>
        /// <param name="value">布尔值</param>
        /// <returns>字符串</returns>
        public static string ToString(bool value)
        {
            return value.ToString().ToLower();
        }

        /// <summary>
        /// 将布尔值转换为整数
        /// </summary>
        /// <param name="value">布尔值</param>
        /// <returns>整数</returns>
        public static int ToInt(bool value)
        {
            return value ? 1 : 0;
        }

        /// <summary>
        /// 将整数转换为布尔值
        /// </summary>
        /// <param name="value">整数</param>
        /// <returns>布尔值</returns>
        public static bool ToBoolean(int value)
        {
            return value != 0;
        }

        /// <summary>
        /// 将布尔值转换为byte
        /// </summary>
        public static byte ToByte(bool value)
        {
            return (byte)(value ? 1 : 0);
        }

        /// <summary>
        /// 将布尔值转换为char
        /// </summary>
        public static char ToChar(bool value)
        {
            return value ? 'Y' : 'N';
        }

        /// <summary>
        /// 将布尔值取反
        /// </summary>
        public static bool Negate(bool value)
        {
            return !value;
        }

        /// <summary>
        /// 安全获取布尔值，为null时返回默认值
        /// </summary>
        public static bool GetSafeBoolean(bool? value)
        {
            return value ?? false;
        }

        /// <summary>
        /// 检查两个布尔值是否相等
        /// </summary>
        public static bool AreEqual(bool? a, bool? b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// 检查是否为"true"值
        /// </summary>
        public static bool IsTrue(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return false;
            }

            str = str.Trim().ToLower();
            return str == "true" || str == "yes" || str == "y" || str == "1" ||
                   str == "on" || str == "ok" || str == "是" || str == "真" || str == "√";
        }

        /// <summary>
        /// 检查是否为"false"值
        /// </summary>
        public static bool IsFalse(string str)
        {
            return !IsTrue(str);
        }
    }
}
