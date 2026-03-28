using System;
using System.Collections.Generic;

namespace WellTool.Core.Converter.impl
{
    /// <summary>
    /// 字符串转换器
    /// </summary>
    public class StringConverter : IConverter
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

            // 处理数组类型
            if (value.GetType().IsArray)
            {
                var array = (Array)value;
                var elements = new System.Text.StringBuilder("[");
                for (int i = 0; i < array.Length; i++)
                {
                    if (i > 0)
                    {
                        elements.Append(", ");
                    }
                    elements.Append(ConvertToString(array.GetValue(i)));
                }
                elements.Append("]");
                return elements.ToString();
            }

            // 处理集合类型
            if (value is System.Collections.IEnumerable enumerable && !(value is string))
            {
                var elements = new System.Text.StringBuilder("[");
                bool first = true;
                foreach (var item in enumerable)
                {
                    if (!first)
                    {
                        elements.Append(", ");
                    }
                    elements.Append(ConvertToString(item));
                    first = false;
                }
                elements.Append("]");
                return elements.ToString();
            }

            return value.ToString();
        }

        /// <summary>
        /// 将对象转换为字符串
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <returns>转换后的字符串</returns>
        private string ConvertToString(object value)
        {
            if (value == null)
            {
                return "null";
            }
            if (value is string str)
            {
                return str;
            }
            if (value.GetType().IsArray || value is System.Collections.IEnumerable && !(value is string))
            {
                return Convert(value, typeof(string)).ToString();
            }
            return value.ToString();
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
            return new[] { typeof(string) };
        }
    }
}