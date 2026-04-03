using System;
using WellTool.Core.Convert;

namespace WellTool.Core.Convert.impl
{
    /// <summary>
    /// 字符转换器
    /// </summary>
    public class CharacterConverter : IConverter
    {
        /// <summary>
        /// 转换值
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <param name="targetType">目标类型</param>
        /// <returns>转换后的值</returns>
        public object Convert(object value, Type targetType)
        {
            if (value == null)
            {
                return null;
            }

            if (targetType != typeof(char) && targetType != typeof(char?))
            {
                return value;
            }

            if (value is char c)
            {
                return c;
            }

            if (value is string str)
            {
                if (str.Length > 0)
                {
                    return str[0];
                }
            }
            else if (value is IConvertible convertible)
            {
                try
                {
                    return convertible.ToChar(null);
                }
                catch
                {
                    // 转换失败，返回原值
                }
            }

            return value;
        }

        /// <summary>
        /// 获取支持的源类型
        /// </summary>
        /// <returns>支持的源类型数组</returns>
        public Type[] GetSupportedSourceTypes()
        {
            return new[] { typeof(object) };
        }

        /// <summary>
        /// 获取支持的目标类型
        /// </summary>
        /// <returns>支持的目标类型数组</returns>
        public Type[] GetSupportedTargetTypes()
        {
            return new[] { typeof(char), typeof(char?) };
        }
    }
}
