using System;

namespace WellTool.Core.Converter.impl
{
    /// <summary>
    /// URI转换器
    /// </summary>
    public class URIConverter : IConverter
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

            if (value is Uri uri)
            {
                return uri;
            }

            var str = value.ToString();
            return new Uri(str);
        }

        /// <summary>
        /// 获取支持的源类型
        /// </summary>
        public Type[] GetSupportedSourceTypes()
        {
            return new Type[] { typeof(string), typeof(Uri) };
        }

        /// <summary>
        /// 获取支持的目标类型
        /// </summary>
        public Type[] GetSupportedTargetTypes()
        {
            return new Type[] { typeof(Uri) };
        }
    }
}
