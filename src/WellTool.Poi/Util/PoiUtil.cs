using System.Collections.Generic;

namespace WellTool.Poi.Util
{
    public static class PoiUtil
    {
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(IList<TKey> keys, IList<TValue> values, bool ignoreNullValue = false)
        {
            var dictionary = new Dictionary<TKey, TValue>();
            if (keys == null || values == null)
            {
                return dictionary;
            }

            int minCount = System.Math.Min(keys.Count, values.Count);
            for (int i = 0; i < minCount; i++)
            {
                var key = keys[i];
                var value = values[i];
                if (!ignoreNullValue || value != null)
                {
                    dictionary[key] = value;
                }
            }
            return dictionary;
        }
    }
}