using System;
using WellTool.Core.Convert;
using System.Collections;
using WellTool.Core.Convert;
using System.Collections.Generic;
using WellTool.Core.Convert;
using System.Linq;
using WellTool.Core.Convert;

namespace WellTool.Core.Convert.Impl
{
    /// <summary>
    /// 闆嗗悎杞崲鍣?    /// </summary>
    public class CollectionConverter : IConverter
    {
        /// <summary>
        /// 杞崲鍊?        /// </summary>
        /// <param name="value">瑕佽浆鎹㈢殑鍊?/param>
        /// <param name="targetType">鐩爣绫诲瀷</param>
        /// <returns>杞崲鍚庣殑鍊?/returns>
        public object Convert(object value, Type targetType)
        {
            if (value == null)
            {
                return null;
            }

            IEnumerable enumerable;
            if (value is string strValue)
            {
                // 澶勭悊瀛楃涓诧紝鎸夐€楀彿鍒嗗壊
                enumerable = strValue.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => s.Trim());
            }
            else if (value is IEnumerable enumValue)
            {
                enumerable = enumValue;
            }
            else
            {
                throw new ConvertException($"Cannot convert {value.GetType().Name} to {targetType.Name}");
            }

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
            else if (targetType.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(HashSet<>))
            {
                return ConvertToHashSet(enumerable, targetType.GetGenericArguments()[0]);
            }

            throw new ConvertException($"Cannot convert {value.GetType().Name} to {targetType.Name}");
        }

        /// <summary>
        /// 杞崲涓烘暟缁?        /// </summary>
        /// <param name="enumerable">鍙灇涓惧璞?/param>
        /// <param name="elementType">鍏冪礌绫诲瀷</param>
        /// <returns>鏁扮粍</returns>
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
        /// 杞崲涓哄垪琛?        /// </summary>
        /// <param name="enumerable">鍙灇涓惧璞?/param>
        /// <param name="elementType">鍏冪礌绫诲瀷</param>
        /// <returns>鍒楄〃</returns>
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
        /// 杞崲涓篐ashSet
        /// </summary>
        /// <param name="enumerable">鍙灇涓惧璞?/param>
        /// <param name="elementType">鍏冪礌绫诲瀷</param>
        /// <returns>HashSet</returns>
        private object ConvertToHashSet(IEnumerable enumerable, Type elementType)
        {
            var hashSetType = typeof(HashSet<>).MakeGenericType(elementType);
            var hashSet = Activator.CreateInstance(hashSetType);

            // 浣跨敤鍙嶅皠璋冪敤Add鏂规硶
            var addMethod = hashSetType.GetMethod("Add");
            foreach (var item in enumerable)
            {
                addMethod.Invoke(hashSet, new[] { item });
            }

            return hashSet;
        }

        /// <summary>
        /// 鑾峰彇鏀寔鐨勬簮绫诲瀷
        /// </summary>
        /// <returns>鏀寔鐨勬簮绫诲瀷鏁扮粍</returns>
        public Type[] GetSupportedSourceTypes()
        {
            return new[] { typeof(IEnumerable), typeof(Array), typeof(string) };
        }

        /// <summary>
        /// 鑾峰彇鏀寔鐨勭洰鏍囩被鍨?        /// </summary>
        /// <returns>鏀寔鐨勭洰鏍囩被鍨嬫暟缁?/returns>
        public Type[] GetSupportedTargetTypes()
        {
            return new[] { typeof(IEnumerable<>), typeof(ICollection<>), typeof(IList<>), typeof(List<>), typeof(Array), typeof(HashSet<>) };
        }
    }
}
