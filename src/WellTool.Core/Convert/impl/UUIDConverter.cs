using System;
using WellTool.Core.Convert;

namespace WellTool.Core.Convert.impl
{
    /// <summary>
    /// UUID转换器
    /// </summary>
    public class UUIDConverter : IConverter
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
            if (value is Guid guid)
            {
                return guid;
            }
            var str = value.ToString().Trim();
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            return Guid.Parse(str);
        }

        /// <summary>
        /// 获取支持的源类型
        /// </summary>
        /// <returns>支持的源类型数组</returns>
        public Type[] GetSupportedSourceTypes()
        {
            return new[] { typeof(string), typeof(Guid) };
        }

        /// <summary>
        /// 获取支持的目标类型
        /// </summary>
        /// <returns>支持的目标类型数组</returns>
        public Type[] GetSupportedTargetTypes()
        {
            return new[] { typeof(Guid) };
        }
    }
}