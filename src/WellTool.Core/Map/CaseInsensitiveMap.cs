using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace WellTool.Core.Map
{
    public class CaseInsensitiveMap<V> : IDictionary<string, V>
    {
        private readonly Dictionary<string, V> innerMap = new Dictionary<string, V>(StringComparer.InvariantCultureIgnoreCase);

        public V this[string key]
        {
            get => innerMap[key];
            set => innerMap[key] = value;
        }

        public ICollection<string> Keys => innerMap.Keys;

        public ICollection<V> Values => innerMap.Values;

        public int Count => innerMap.Count;

        public bool IsReadOnly => false;

        public void Add(string key, V value)
        {
            innerMap.Add(key, value);
        }

        public void Add(KeyValuePair<string, V> item)
        {
            innerMap.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            innerMap.Clear();
        }

        public bool Contains(KeyValuePair<string, V> item)
        {
            return innerMap.Contains(item);
        }

        public bool ContainsKey(string key)
        {
            return innerMap.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, V>[] array, int arrayIndex)
        {
            innerMap.CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<string, V>> GetEnumerator()
        {
            return innerMap.GetEnumerator();
        }

        public bool Remove(string key)
        {
            return innerMap.Remove(key);
        }

        public bool Remove(KeyValuePair<string, V> item)
        {
            return innerMap.Remove(item);
        }

        public bool TryGetValue(string key, out V value)
        {
            return innerMap.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}