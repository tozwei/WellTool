using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WellTool.Core.Convert;

namespace WellTool.Core.Convert.impl
{
    /// <summary>
    /// 数组转换器
    /// </summary>
    public class ArrayConverter : IConverter
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

            if (!targetType.IsArray)
            {
                return value;
            }

            var elementType = targetType.GetElementType();
            var result = Array.CreateInstance(elementType, 0);

            if (value is IEnumerable enumerable)
            {
                var list = new List<object>();
                foreach (var item in enumerable)
                {
                    list.Add(item);
                }
                result = Array.CreateInstance(elementType, list.Count);
                for (int i = 0; i < list.Count; i++)
                {
                    var convertedValue = ConvertElement(list[i], elementType);
                    result.SetValue(convertedValue, i);
                }
            }
            else
            {
                result = Array.CreateInstance(elementType, 1);
                var convertedValue = ConvertElement(value, elementType);
                result.SetValue(convertedValue, 0);
            }

            return result;
        }

        /// <summary>
        /// 转换元素
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <param name="targetType">目标类型</param>
        /// <returns>转换后的值</returns>
        private object ConvertElement(object value, Type targetType)
        {
            if (value == null)
            {
                return null;
            }

            if (targetType.IsAssignableFrom(value.GetType()))
            {
                return value;
            }

            // 基本类型转换
            if (targetType.IsPrimitive || targetType == typeof(string) || targetType == typeof(decimal))
            {
                try
                {
                    return System.Convert.ChangeType(value, targetType);
                }
                catch
                {
                    return value;
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
            return new[] { typeof(Array) };
        }
    }
}

