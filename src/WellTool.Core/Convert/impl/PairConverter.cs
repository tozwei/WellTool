using System;
using System.Collections.Generic;

namespace WellTool.Core.Converter.impl
{
    /// <summary>
    /// 键值对转换器
    /// </summary>
    public class PairConverter : IConverter
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

            // 从键值对中提取
            if (value is KeyValuePair<object, object> kvp)
            {
                return new WellTool.Core.Lang.Pair(kvp.Key, kvp.Value);
            }

            // 从字典中提取第一个键值对
            if (value is Dictionary<object, object> dict && dict.Count > 0)
            {
                var first = dict.GetEnumerator();
                first.MoveNext();
                return new WellTool.Core.Lang.Pair(first.Current.Key, first.Current.Value);
            }

            // 从IDictionary中提取
            if (value is System.Collections.IDictionary idict && idict.Count > 0)
            {
                var enumerator = idict.GetEnumerator();
                enumerator.MoveNext();
                var entry = enumerator.Entry;
                return new WellTool.Core.Lang.Pair(entry.Key, entry.Value);
            }

            throw new ConvertException($"Cannot convert {value.GetType().Name} to Pair");
        }

        /// <summary>
        /// 获取支持的源类型
        /// </summary>
        public Type[] GetSupportedSourceTypes()
        {
            return new Type[] { typeof(KeyValuePair<object, object>), typeof(Dictionary<object, object>),
                               typeof(System.Collections.IDictionary) };
        }

        /// <summary>
        /// 获取支持的目标类型
        /// </summary>
        public Type[] GetSupportedTargetTypes()
        {
            return new Type[] { typeof(WellTool.Core.Lang.Pair) };
        }
    }
}
