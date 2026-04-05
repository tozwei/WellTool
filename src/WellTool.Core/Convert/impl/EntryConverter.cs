using System.Collections;
using System.Linq;
using WellTool.Core.Convert;
using WellTool.Core.Map;

namespace WellTool.Core.Convert.impl;

/// <summary>
/// 键值对转换器
/// </summary>
/// <typeparam name="TKey">键类型</typeparam>
/// <typeparam name="TValue">值类型</typeparam>
public class EntryConverter<TKey, TValue> : AbstractConverter<KeyValuePair<TKey, TValue>>
{
    protected override KeyValuePair<TKey, TValue> ConvertInternal(object value)
    {
        TKey key = default!;
        TValue val = default!;

        if (value is KeyValuePair<TKey, TValue> kvp)
        {
            return kvp;
        }
        if (value is IDictionary dictionary)
        {
            var enumerator = dictionary.GetEnumerator();
            if (enumerator.MoveNext())
            {
                var entry = enumerator.Current;
                if (entry is DictionaryEntry de)
                {
                    key = Convert.To<TKey>(de.Key);
                    val = Convert.To<TValue>(de.Value);
                }
            }
        }
        else if (value is IEnumerable<KeyValuePair<object, object>> kvps)
        {
            var first = kvps.FirstOrDefault();
            key = Convert.To<TKey>(first.Key);
            val = Convert.To<TValue>(first.Value);
        }
        else if (value is Tuple<TKey, TValue> tuple)
        {
            key = tuple.Item1;
            val = tuple.Item2;
        }
        else if (value is ValueTuple<TKey, TValue> vt)
        {
            key = vt.Item1;
            val = vt.Item2;
        }

        return new KeyValuePair<TKey, TValue>(key, val);
    }
}
