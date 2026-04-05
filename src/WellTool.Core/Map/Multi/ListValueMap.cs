using System;
using System.Collections.Generic;

namespace WellTool.Core.Map.Multi
{
	/// <summary>
	/// 值作为集合List的Map实现，通过调用putValue可以在相同key时加入多个值，多个值用集合表示
	/// </summary>
	/// <typeparam name="K">键类型</typeparam>
	/// <typeparam name="V">值类型</typeparam>
	public class ListValueMap<K, V> : AbsCollValueMap<K, V, List<V>>
	{
		/// <summary>
		/// 构造
		/// </summary>
		public ListValueMap() : this(MapConstants.DEFAULT_INITIAL_CAPACITY)
		{
		}

		/// <summary>
		/// 构造
		/// </summary>
		/// <param name="initialCapacity">初始大小</param>
		public ListValueMap(int initialCapacity) : this(initialCapacity, MapConstants.DEFAULT_LOAD_FACTOR)
		{
		}

		/// <summary>
		/// 构造
		/// </summary>
		/// <param name="m">Map</param>
		public ListValueMap(IDictionary<K, ICollection<V>> m) : this(MapConstants.DEFAULT_LOAD_FACTOR, m)
		{
		}

		/// <summary>
		/// 构造
		/// </summary>
		/// <param name="loadFactor">加载因子</param>
		/// <param name="m">Map</param>
		public ListValueMap(float loadFactor, IDictionary<K, ICollection<V>> m) : this(m.Count, loadFactor)
		{
			this.PutAllValues(m);
		}

		/// <summary>
		/// 构造
		/// </summary>
		/// <param name="initialCapacity">初始大小</param>
		/// <param name="loadFactor">加载因子</param>
		public ListValueMap(int initialCapacity, float loadFactor) : base(new Dictionary<K, List<V>>(initialCapacity))
		{
		}

		protected override List<V> CreateCollection()
		{
			return new List<V>(MapConstants.DEFAULT_COLLECTION_INITIAL_CAPACITY);
		}
	}
}