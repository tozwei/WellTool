using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WellTool.Core.Convert.impl
{
    /// <summary>
    /// 集合转换器
    /// </summary>
    public class CollectionConverter : IConverter
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

            if (value is IEnumerable enumerable)
            {
                if (targetType.IsArray)
                {
                    return ConvertToArray(enumerable, targetType.GetElementType());
                }
                else if (targetType.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(List<>))
                {
                    return ConvertToList(enumerable, targetType.GetGenericArguments()[0]);
                }
                else if (targetType.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(IList<>))
                {
                    return ConvertToList(enumerable, targetType.GetGenericArguments()[0]);
                }
                else if (targetType.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(ICollection<>))
                {
                    return ConvertToList(enumerable, targetType.GetGenericArguments()[0]);
                }
                else if (targetType.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {
                    return ConvertToList(enumerable, targetType.GetGenericArguments()[0]);
                }
            }

            throw new ConvertException($"Cannot convert {value.GetType().Name} to {targetType.Name}");
        }

        /// <summary>
        /// 转换为数组
        /// </summary>
        /// <param name="enumerable">可枚举对象</param>
        /// <param name="elementType">元素类型</param>
        /// <returns>数组</returns>
        private object ConvertToArray(IEnumerable enumerable, Type elementType)
        {
            var list = new List<object>();
            foreach (var item in enumerable)
            {
                list.Add(item);
            }

            var array = Array.CreateInstance(elementType, list.Count);
            for (int i = 0; i < list.Count; i++)
            {
                array.SetValue(list[i], i);
            }

            return array;
        }

        /// <summary>
        /// 转换为列表
        /// </summary>
        /// <param name="enumerable">可枚举对象</param>
        /// <param name="elementType">元素类型</param>
        /// <returns>列表</returns>
        private object ConvertToList(IEnumerable enumerable, Type elementType)
        {
            var listType = typeof(List<>).MakeGenericType(elementType);
            var list = Activator.CreateInstance(listType) as IList;

            foreach (var item in enumerable)
            {
                list.Add(item);
            }

            return list;
        }

        /// <summary>
        /// 获取支持的源类型
        /// </summary>
        /// <returns>支持的源类型数组</returns>
        public Type[] GetSupportedSourceTypes()
        {
            return new[] { typeof(IEnumerable), typeof(Array) };
        }

        /// <summary>
        /// 获取支持的目标类型
        /// </summary>
        /// <returns>支持的目标类型数组</returns>
        public Type[] GetSupportedTargetTypes()
        {
            return new[] { typeof(IEnumerable<>), typeof(ICollection<>), typeof(IList<>), typeof(List<>), typeof(Array) };
        }
    }
}