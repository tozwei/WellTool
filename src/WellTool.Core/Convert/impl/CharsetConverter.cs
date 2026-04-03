using System;
using System.Globalization;

namespace WellTool.Core.Converter.impl
{
    /// <summary>
    /// 字符集转换器
    /// </summary>
    public class CharsetConverter : IConverter
    {
        /// <summary>
        /// 转换值
        /// </summary>
        public object Convert(object value, Type targetType)
        {
            if (value == null)
            {
                return null;
            }

            var charsetName = value.ToString();
            return System.Text.Encoding.GetEncoding(charsetName);
        }

        /// <summary>
        /// 获取支持的源类型
        /// </summary>
        public Type[] GetSupportedSourceTypes()
        {
            return new Type[] { typeof(string) };
        }

        /// <summary>
        /// 获取支持的目标类型
        /// </summary>
        public Type[] GetSupportedTargetTypes()
        {
            return new Type[] { typeof(System.Text.Encoding) };
        }
    }
}
