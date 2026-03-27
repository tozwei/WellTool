using System;
using System.Collections;
using System.Collections.Generic;

namespace WellTool.Core.Map
{
    public class BiMap<K, V> : IDictionary<K, V>
    {
        private readonly Dictionary<K, V> keyToValue = new Dictionary<K, V>();
        private readonly Dictionary<V, K> valueToKey = new Dictionary<V, K>();

        public V this[K key]
        {
            get => keyToValue[key];
            set
            {
                if (keyToValue.TryGetValue(key, out var oldValue))
                {
                    valueToKey.Remove(oldValue);
                }
                keyToValue[key] = value;
                valueToKey[value] = key;
            }
        }

        public ICollection<K> Keys => keyToValue.Keys;

        public ICollection<V> Values => valueToKey.Keys;

        public int Count => keyToValue.Count;

        public bool IsReadOnly => false;

        public void Add(K key, V value)
        {
            if (keyToValue.ContainsKey(key))
            {
                throw new ArgumentException($"Key already exists: {key}");
            }
            if (valueToKey.ContainsKey(value))
            {
                throw new ArgumentException($"Value already exists: {value}");
            }
            keyToValue.Add(key, value);
            valueToKey.Add(value, key);
        }

        public void Add(KeyValuePair<K, V> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            keyToValue.Clear();
            valueToKey.Clear();
        }

        public bool Contains(KeyValuePair<K, V> item)
        {
            return keyToValue.TryGetValue(item.Key, out var value) && EqualityComparer<V>.Default.Equals(value, item.Value);
        }

        public bool ContainsKey(K key)
        {
            return keyToValue.ContainsKey(key);
        }

        public bool ContainsValue(V value)
        {
            return valueToKey.ContainsKey(value);
        }

        public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex)
        {
            ((IDictionary<K, V>)keyToValue).CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
        {
            return keyToValue.GetEnumerator();
        }

        public bool Remove(K key)
        {
            if (keyToValue.TryGetValue(key, out var value))
            {
                keyToValue.Remove(key);
                valueToKey.Remove(value);
                return true;
            }
            return false;
        }

        public bool Remove(KeyValuePair<K, V> item)
        {
            if (Contains(item))
            {
                return Remove(item.Key);
            }
            return false;
        }

        public bool TryGetValue(K key, out V value)
        {
            return keyToValue.TryGetValue(key, out value);
        }

        public K GetKey(V value)
        {
            return valueToKey[value];
        }

        public bool TryGetKey(V value, out K key)
        {
            return valueToKey.TryGetValue(value, out key);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}