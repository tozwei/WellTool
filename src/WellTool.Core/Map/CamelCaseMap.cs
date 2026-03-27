using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace WellTool.Core.Map
{
    public class CamelCaseMap<V> : IDictionary<string, V>
    {
        private readonly Dictionary<string, V> innerMap = new Dictionary<string, V>();

        public V this[string key]
        {
            get => innerMap[ToCamelCase(key)];
            set => innerMap[ToCamelCase(key)] = value;
        }

        public ICollection<string> Keys => innerMap.Keys;

        public ICollection<V> Values => innerMap.Values;

        public int Count => innerMap.Count;

        public bool IsReadOnly => false;

        public void Add(string key, V value)
        {
            innerMap.Add(ToCamelCase(key), value);
        }

        public void Add(KeyValuePair<string, V> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            innerMap.Clear();
        }

        public bool Contains(KeyValuePair<string, V> item)
        {
            return innerMap.Contains(new KeyValuePair<string, V>(ToCamelCase(item.Key), item.Value));
        }

        public bool ContainsKey(string key)
        {
            return innerMap.ContainsKey(ToCamelCase(key));
        }

        public void CopyTo(KeyValuePair<string, V>[] array, int arrayIndex)
        {
            int i = 0;
            foreach (var item in innerMap)
            {
                if (i >= array.Length - arrayIndex)
                    break;
                array[arrayIndex + i] = item;
                i++;
            }
        }

        public IEnumerator<KeyValuePair<string, V>> GetEnumerator()
        {
            return innerMap.GetEnumerator();
        }

        public bool Remove(string key)
        {
            return innerMap.Remove(ToCamelCase(key));
        }

        public bool Remove(KeyValuePair<string, V> item)
        {
            if (TryGetValue(item.Key, out V value) && EqualityComparer<V>.Default.Equals(value, item.Value))
            {
                return innerMap.Remove(ToCamelCase(item.Key));
            }
            return false;
        }

        public bool TryGetValue(string key, out V value)
        {
            return innerMap.TryGetValue(ToCamelCase(key), out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private string ToCamelCase(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return key;
            }

            var sb = new StringBuilder();
            bool nextUpper = false;

            for (int i = 0; i < key.Length; i++)
            {
                char c = key[i];
                if (c == '_' || c == '-' || c == ' ')
                {
                    nextUpper = true;
                }
                else if (nextUpper)
                {
                    sb.Append(char.ToUpper(c, CultureInfo.InvariantCulture));
                    nextUpper = false;
                }
                else if (i == 0)
                {
                    sb.Append(char.ToLower(c, CultureInfo.InvariantCulture));
                }
                else
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }
    }
}