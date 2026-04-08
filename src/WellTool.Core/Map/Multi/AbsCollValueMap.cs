using System;
using System.Collections.Generic;
using WellTool.Core.Map;

namespace WellTool.Core.Map.Multi
{
	/// <summary>
	/// 常量定义
	/// </summary>
	public static class MapConstants
	{
		/// <summary>
		/// 默认初始容量
		/// </summary>
		public const int DEFAULT_INITIAL_CAPACITY = 16;

		/// <summary>
		/// 默认加载因子
		/// </summary>
		public const float DEFAULT_LOAD_FACTOR = 0.75f;

		/// <summary>
		/// 默认集合初始容量
		/// </summary>
		public const int DEFAULT_COLLECTION_INITIAL_CAPACITY = 10;
	}

	/// <summary>
	/// 值作为集合的Map实现，通过调用putValue可以在相同key时加入多个值，多个值用集合表示
	/// </summary>
	/// <typeparam name="K">键类型</typeparam>
	/// <typeparam name="V">值类型</typeparam>
	/// <typeparam name="C">集合类型</typeparam>
	public abstract class AbsCollValueMap<K, V, C> : MapWrapper<K, C> where C : ICollection<V>
	{
		/// <summary>
		/// 默认集合初始大小
		/// </summary>
		protected static readonly int DEFAULT_COLLECTION_INITIAL_CAPACITY = 3;

		/// <summary>
		/// 构造
		/// </summary>
		public AbsCollValueMap() : this(MapConstants.DEFAULT_INITIAL_CAPACITY)
		{
		}

		/// <summary>
		/// 构造
		/// </summary>
		/// <param name="initialCapacity">初始大小</param>
		public AbsCollValueMap(int initialCapacity) : this(initialCapacity, MapConstants.DEFAULT_LOAD_FACTOR)
		{
		}

		/// <summary>
		/// 构造
		/// </summary>
		/// <param name="m">Map</param>
		public AbsCollValueMap(IDictionary<K, C> m) : this(MapConstants.DEFAULT_LOAD_FACTOR, m)
		{
		}

		/// <summary>
		/// 构造
		/// </summary>
		/// <param name="loadFactor">加载因子</param>
		/// <param name="m">Map</param>
		public AbsCollValueMap(float loadFactor, IDictionary<K, C> m) : this(m.Count, loadFactor)
		{
			this.AddRange(m);
		}

		/// <summary>
		/// 构造
		/// </summary>
		/// <param name="initialCapacity">初始大小</param>
		/// <param name="loadFactor">加载因子</param>
		public AbsCollValueMap(int initialCapacity, float loadFactor) : base(new Dictionary<K, C>(initialCapacity))
		{
		}

		/// <summary>
		/// 放入所有value
		/// </summary>
		/// <param name="m">valueMap</param>
		public void PutAllValues(IDictionary<K, ICollection<V>> m)
		{
			if (m != null)
			{
				foreach (var entry in m)
				{
					if (entry.Value != null)
					{
						foreach (var value in entry.Value)
						{
							PutValue(entry.Key, value);
						}
					}
				}
			}
		}

		/// <summary>
		/// 放入Value<br>
		/// 如果键对应值列表有值，加入，否则创建一个新列表后加入
		/// </summary>
		/// <param name="key">键</param>
		/// <param name="value">值</param>
		public void PutValue(K key, V value)
		{
			C collection;
			if (!TryGetValue(key, out collection!))
			{
				collection = CreateCollection();
				this[key] = collection;
			}
			collection.Add(value);
		}

		/// <summary>
		/// 获取值
		/// </summary>
		/// <param name="key">键</param>
		/// <param name="index">第几个值的索引，越界返回null</param>
		/// <returns>值或null</returns>
		public V Get(K key, int index)
		{
			if (!TryGetValue(key, out var collection))
			{
				return default(V);
			}
			if (collection == null || index < 0 || index >= collection.Count)
			{
				return default(V);
			}
			int i = 0;
			foreach (var item in collection)
			{
				if (i == index)
				{
					return item;
				}
				i++;
			}
			return default(V);
		}

		/// <summary>
		/// 移除value集合中的某个值
		/// </summary>
		/// <param name="key">键</param>
		/// <param name="value">集合中的某个值</param>
		/// <returns>是否删除成功</returns>
		public bool RemoveValue(K key, V value)
		{
			if (!TryGetValue(key, out var collection))
			{
				return false;
			}
			return collection != null && collection.Remove(value);
		}

		/// <summary>
		/// 移除value集合中的某些值
		/// </summary>
		/// <param name="key">键</param>
		/// <param name="values">集合中的某些值</param>
		/// <returns>是否删除成功</returns>
		public bool RemoveValues(K key, ICollection<V> values)
		{
			if (!TryGetValue(key, out var collection))
			{
				return false;
			}
			return collection != null && values.All(value => collection.Remove(value));
		}

		/// <summary>
		/// 创建集合<br>
		/// 此方法用于创建在putValue后追加值所在的集合，子类实现此方法创建不同类型的集合
		/// </summary>
		/// <returns>ICollection</returns>
		protected abstract C CreateCollection();
	}
}