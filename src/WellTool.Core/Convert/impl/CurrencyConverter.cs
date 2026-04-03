using System;
using System.Globalization;

namespace WellTool.Core.Converter.impl
{
    /// <summary>
    /// 货币转换器
    /// </summary>
    public class CurrencyConverter : IConverter
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

            var str = value.ToString();
            return Currency.FromCode(str);
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
            return new Type[] { typeof(Currency) };
        }
    }
}
