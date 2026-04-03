using System;
using WellTool.Core.Convert;

namespace WellTool.Core.Convert.impl
{
    /// <summary>
    /// 布尔转换器
    /// </summary>
    public class BooleanConverter : IConverter
    {
        /// <summary>
        /// 转换值
        /// </summary>
        public object Convert(object value, Type targetType)
        {
            if (value == null)
            {
                return false;
            }

            // 处理数字类型：0为false，其他为true
            if (value is Number number)
            {
                return number.ToDouble() != 0;
            }

            // 处理布尔类型
            if (value is bool b)
            {
                return b;
            }

            // 处理字符串
            if (value is string str)
            {
                return ToBoolean(str);
            }

            // 处理数字类型
            if (value is IConvertible convertible)
            {
                return convertible.ToBoolean(null);
            }

            return ToBoolean(value.ToString());
        }

        /// <summary>
        /// 将字符串转换为布尔值
        /// </summary>
        private static bool ToBoolean(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return false;
            }

            str = str.Trim().ToLower();

            // true/false
            if (str == "true" || str == "false")
            {
                return bool.Parse(str);
            }

            // yes/no, y/n, on/off, 1/0
            if (str == "yes" || str == "y" || str == "t" || str == "ok" || str == "1" || str == "on" ||
                str == "是" || str == "对" || str == "真" || str == "對" || str == "√")
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 获取支持的源类型
        /// </summary>
        public Type[] GetSupportedSourceTypes()
        {
            return new Type[] { typeof(string), typeof(bool), typeof(int), typeof(long), typeof(double), typeof(Number) };
        }

        /// <summary>
        /// 获取支持的目标类型
        /// </summary>
        public Type[] GetSupportedTargetTypes()
        {
            return new Type[] { typeof(bool) };
        }
    }
}
