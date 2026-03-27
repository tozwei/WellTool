using System;
using System.Collections.Generic;

namespace WellTool.Core.Map.Multi
{
	/// <summary>
	/// 值作为集合Set的Map实现，通过调用putValue可以在相同key时加入多个值，多个值用集合表示
	/// </summary>
	/// <typeparam name="K">键类型</typeparam>
	/// <typeparam name="V">值类型</typeparam>
	public class SetValueMap<K, V> : AbsCollValueMap<K, V, HashSet<V>>
	{
		/// <summary>
		/// 构造
		/// </summary>
		public SetValueMap() : this(DEFAULT_INITIAL_CAPACITY)
		{
		}

		/// <summary>
		/// 构造
		/// </summary>
		/// <param name="initialCapacity">初始大小</param>
		public SetValueMap(int initialCapacity) : this(initialCapacity, DEFAULT_LOAD_FACTOR)
		{
		}

		/// <summary>
		/// 构造
		/// </summary>
		/// <param name="m">Map</param>
		public SetValueMap(IDictionary<K, ICollection<V>> m) : this(DEFAULT_LOAD_FACTOR, m)
		{
		}

		/// <summary>
		/// 构造
		/// </summary>
		/// <param name="loadFactor">加载因子</param>
		/// <param name="m">Map</param>
		public SetValueMap(float loadFactor, IDictionary<K, ICollection<V>> m) : this(m.Count, loadFactor)
		{
			this.PutAllValues(m);
		}

		/// <summary>
		/// 构造
		/// </summary>
		/// <param name="initialCapacity">初始大小</param>
		/// <param name="loadFactor">加载因子</param>
		public SetValueMap(int initialCapacity, float loadFactor) : base(new Dictionary<K, HashSet<V>>(initialCapacity))
		{
		}

		protected override HashSet<V> CreateCollection()
		{
			return new HashSet<V>();
		}
	}
}