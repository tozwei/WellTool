using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WellTool.Core.Map
{
	/// <summary>
	/// Map包装类，通过包装一个已有Map实现特定功能。例如自定义Key的规则或Value规则
	/// </summary>
	/// <typeparam name="K">键类型</typeparam>
	/// <typeparam name="V">值类型</typeparam>
	public class MapWrapper<K, V> : IDictionary<K, V>, IEnumerable<KeyValuePair<K, V>>, ICloneable
	{
		/// <summary>
		/// 默认增长因子
		/// </summary>
		protected static readonly float DEFAULT_LOAD_FACTOR = 0.75f;
		/// <summary>
		/// 默认初始大小
		/// </summary>
		protected static readonly int DEFAULT_INITIAL_CAPACITY = 1 << 4; // aka 16

		private IDictionary<K, V> raw;

		/// <summary>
		/// 构造
		/// </summary>
		/// <param name="raw">被包装的Map</param>
		public MapWrapper(IDictionary<K, V> raw)
		{
			this.raw = raw;
		}

		/// <summary>
		/// 获取原始的Map
		/// </summary>
		/// <returns>Map</returns>
		public IDictionary<K, V> GetRaw()
		{
			return this.raw;
		}

		public int Count => raw.Count;

		public bool IsReadOnly => raw.IsReadOnly;

		public ICollection<K> Keys => raw.Keys;

		public ICollection<V> Values => raw.Values;

		public V this[K key]
		{
			get => raw[key];
			set => raw[key] = value;
		}

		public void Add(K key, V value)
		{
			raw.Add(key, value);
		}

		public void Add(KeyValuePair<K, V> item)
		{
			raw.Add(item);
		}

		public void Clear()
		{
			raw.Clear();
		}

		public bool Contains(KeyValuePair<K, V> item)
		{
			return raw.Contains(item);
		}

		public bool ContainsKey(K key)
		{
			return raw.ContainsKey(key);
		}

		public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex)
		{
			raw.CopyTo(array, arrayIndex);
		}

		public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
		{
			return raw.GetEnumerator();
		}

		public bool Remove(K key)
		{
			return raw.Remove(key);
		}

		public bool Remove(KeyValuePair<K, V> item)
		{
			return raw.Remove(item);
		}

		public bool TryGetValue(K key, out V value)
		{
			return raw.TryGetValue(key, out value);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return raw.GetEnumerator();
		}

		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}
			MapWrapper<K, V> that = (MapWrapper<K, V>)obj;
			return raw.SequenceEqual(that.raw);
		}

		public override int GetHashCode()
		{
			return raw.GetHashCode();
		}

		public override string ToString()
		{
			return raw.ToString();
		}

		public object Clone()
		{
			MapWrapper<K, V> clone = new MapWrapper<K, V>(new Dictionary<K, V>(raw));
			return clone;
		}

		/// <summary>
		/// 添加多个键值对
		/// </summary>
		/// <param name="items">键值对集合</param>
		public void AddRange(IDictionary<K, V> items)
		{
			if (items != null)
			{
				foreach (var item in items)
				{
					raw.Add(item);
				}
			}
		}
	}
}