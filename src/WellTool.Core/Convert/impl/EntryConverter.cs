using System.Collections;
using System.Linq;
using WellTool.Core.Convert;
using WellTool.Core.Map;

namespace WellTool.Core.Convert.Impl;

/// <summary>
/// 閿€煎杞崲鍣?
/// </summary>
/// <typeparam name="TKey">閿被鍨?/typeparam>
/// <typeparam name="TValue">鍊肩被鍨?/typeparam>
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
                    key = (TKey)de.Key;
                    val = (TValue)de.Value;
                }
            }
        }
        else if (value is IEnumerable<KeyValuePair<object, object>> kvps)
        {
            var first = kvps.FirstOrDefault();
            key = (TKey)first.Key;
            val = (TValue)first.Value;
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

