using System;
using System.Collections.Generic;

namespace WellTool.Core.Map.Multi
{
	/// <summary>
	/// 值作为集合的Map实现，通过调用putValue可以在相同key时加入多个值，多个值用集合表示<br>
	/// 此类可以通过传入函数自定义集合类型的创建规则
	/// </summary>
	/// <typeparam name="K">键类型</typeparam>
	/// <typeparam name="V">值类型</typeparam>
	public class CollectionValueMap<K, V> : AbsCollValueMap<K, V, ICollection<V>>
	{
		private readonly Func<ICollection<V>> collectionCreateFunc;

		/// <summary>
		/// 构造
		/// </summary>
		public CollectionValueMap() : this(MapConstants.DEFAULT_INITIAL_CAPACITY)
		{
		}

		/// <summary>
		/// 构造
		/// </summary>
		/// <param name="initialCapacity">初始大小</param>
		public CollectionValueMap(int initialCapacity) : this(initialCapacity, MapConstants.DEFAULT_LOAD_FACTOR)
		{
		}

		/// <summary>
		/// 构造
		/// </summary>
		/// <param name="m">Map</param>
		public CollectionValueMap(IDictionary<K, ICollection<V>> m) : this(MapConstants.DEFAULT_LOAD_FACTOR, m)
		{
		}

		/// <summary>
		/// 构造
		/// </summary>
		/// <param name="loadFactor">加载因子</param>
		/// <param name="m">Map</param>
		public CollectionValueMap(float loadFactor, IDictionary<K, ICollection<V>> m) : this(loadFactor, m, () => new List<V>())
		{
		}

		/// <summary>
		/// 构造
		/// </summary>
		/// <param name="initialCapacity">初始大小</param>
		/// <param name="loadFactor">加载因子</param>
		public CollectionValueMap(int initialCapacity, float loadFactor) : this(initialCapacity, loadFactor, () => new List<V>())
		{
		}

		/// <summary>
		/// 构造
		/// </summary>
		/// <param name="loadFactor">加载因子</param>
		/// <param name="m">Map</param>
		/// <param name="collectionCreateFunc">Map中值的集合创建函数</param>
		public CollectionValueMap(float loadFactor, IDictionary<K, ICollection<V>> m, Func<ICollection<V>> collectionCreateFunc) : this(m.Count, loadFactor, collectionCreateFunc)
		{
			this.AddRange(m);
		}

		/// <summary>
		/// 构造
		/// </summary>
		/// <param name="initialCapacity">初始大小</param>
		/// <param name="loadFactor">加载因子</param>
		/// <param name="collectionCreateFunc">Map中值的集合创建函数</param>
		public CollectionValueMap(int initialCapacity, float loadFactor, Func<ICollection<V>> collectionCreateFunc) : base(new Dictionary<K, ICollection<V>>(initialCapacity))
		{
			this.collectionCreateFunc = collectionCreateFunc;
		}

		protected override ICollection<V> CreateCollection()
		{
			return collectionCreateFunc();
		}
	}
}