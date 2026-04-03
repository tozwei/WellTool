using System;

namespace WellTool.Core.Converter.impl
{
    /// <summary>
    /// UUID转换器
    /// </summary>
    public class UUIDConverter : IConverter
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

            if (value is Guid guid)
            {
                return guid;
            }

            var str = value.ToString();
            return Guid.Parse(str);
        }

        /// <summary>
        /// 获取支持的源类型
        /// </summary>
        public Type[] GetSupportedSourceTypes()
        {
            return new Type[] { typeof(string), typeof(Guid) };
        }

        /// <summary>
        /// 获取支持的目标类型
        /// </summary>
        public Type[] GetSupportedTargetTypes()
        {
            return new Type[] { typeof(Guid) };
        }
    }
}
