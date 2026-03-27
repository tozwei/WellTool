using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WellTool.Core.Map.Multi
{
	/// <summary>
	/// 将行的键作为主键的{@link Table}实现<br>
	/// 此结构为: 行=(列=值)
	/// </summary>
	/// <typeparam name="R">行类型</typeparam>
	/// <typeparam name="C">列类型</typeparam>
	/// <typeparam name="V">值类型</typeparam>
	public class RowKeyTable<R, C, V> : AbsTable<R, C, V>
	{
		readonly IDictionary<R, IDictionary<C, V>> raw;
		/// <summary>
		/// 列的Map创建器，用于定义Table中Value对应Map类型
		/// </summary>
		readonly Func<IDictionary<C, V>> columnBuilder;

		//region 构造

		/// <summary>
		/// 构造
		/// </summary>
		public RowKeyTable() : this(new Dictionary<R, IDictionary<C, V>>())
		{
		}

		/// <summary>
		/// 构造
		/// </summary>
		/// <param name="isLinked">是否有序，有序则使用{@link LinkedDictionary}作为原始Map</param>
		public RowKeyTable(bool isLinked) : this(
			isLinked ? new Dictionary<R, IDictionary<C, V>>() : new Dictionary<R, IDictionary<C, V>>(),
			() => isLinked ? new Dictionary<C, V>() : new Dictionary<C, V>()
		)
		{
		}

		/// <summary>
		/// 构造
		/// </summary>
		/// <param name="raw">原始Map</param>
		public RowKeyTable(IDictionary<R, IDictionary<C, V>> raw) : this(raw, () => new Dictionary<C, V>())
		{
		}

		/// <summary>
		/// 构造
		/// </summary>
		/// <param name="raw">原始Map</param>
		/// <param name="columnMapBuilder">列的map创建器</param>
		public RowKeyTable(IDictionary<R, IDictionary<C, V>> raw, Func<IDictionary<C, V>> columnMapBuilder)
		{
			this.raw = raw;
			this.columnBuilder = columnMapBuilder ?? (() => new Dictionary<C, V>());
		}
		//endregion

		public override IDictionary<R, IDictionary<C, V>> RowMap()
		{
			return raw;
		}

		public override V Put(R rowKey, C columnKey, V value)
		{
			if (!raw.TryGetValue(rowKey, out var columnMap))
			{
				columnMap = columnBuilder();
				raw[rowKey] = columnMap;
			}
			var oldValue = columnMap.TryGetValue(columnKey, out var result) ? result : default(V);
			columnMap[columnKey] = value;
			return oldValue;
		}

		public override V Remove(R rowKey, C columnKey)
		{
			var map = ((Table<R, C, V>)this).GetRow(rowKey);
			if (map == null)
			{
				return default(V);
			}
			var value = map.TryGetValue(columnKey, out var result) ? result : default(V);
			map.Remove(columnKey);
			if (map.Count == 0)
			{
				raw.Remove(rowKey);
			}
			return value;
		}

		public override bool IsEmpty()
		{
			return raw.Count == 0;
		}

		public override void Clear()
		{
			raw.Clear();
		}

		//region columnMap
		public override IDictionary<C, IDictionary<R, V>> ColumnMap()
		{
			if (columnMap == null)
			{
				columnMap = new ColumnMapImpl(this);
			}
			return columnMap;
		}

		private IDictionary<C, IDictionary<R, V>> columnMap;

		private class ColumnMapImpl : IDictionary<C, IDictionary<R, V>>
		{
			private readonly RowKeyTable<R, C, V> table;

			public ColumnMapImpl(RowKeyTable<R, C, V> table)
			{
				this.table = table;
			}

			public IEnumerator<KeyValuePair<C, IDictionary<R, V>>> GetEnumerator()
			{
				return table.GetAllColumnKeys().Select(c => new KeyValuePair<C, IDictionary<R, V>>(c, table.GetColumn(c))).GetEnumerator();
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}

			public void Add(KeyValuePair<C, IDictionary<R, V>> item)
			{
				throw new NotSupportedException();
			}

			public void Clear()
			{
				throw new NotSupportedException();
			}

			public bool Contains(KeyValuePair<C, IDictionary<R, V>> item)
			{
				throw new NotSupportedException();
			}

			public void CopyTo(KeyValuePair<C, IDictionary<R, V>>[] array, int arrayIndex)
			{
				throw new NotSupportedException();
			}

			public bool Remove(KeyValuePair<C, IDictionary<R, V>> item)
			{
				throw new NotSupportedException();
			}

			public int Count => table.GetAllColumnKeys().Count();

			public bool IsReadOnly => true;

			public void Add(C key, IDictionary<R, V> value)
			{
				throw new NotSupportedException();
			}

			public bool ContainsKey(C key)
			{
				return ((Table<R, C, V>)table).ContainsColumn(key);
			}

			public bool Remove(C key)
			{
				throw new NotSupportedException();
			}

			public bool TryGetValue(C key, out IDictionary<R, V> value)
			{
				value = table.GetColumn(key);
				return true;
			}

			public IDictionary<R, V> this[C key]
			{
				get => table.GetColumn(key);
				set => throw new NotSupportedException();
			}

			public ICollection<C> Keys => table.GetAllColumnKeys().ToList();

			public ICollection<IDictionary<R, V>> Values => table.GetAllColumnKeys().Select(table.GetColumn).ToList();
		}
		//endregion

		//region columnKeySet
		private ICollection<C> columnKeySet;

		public IEnumerable<C> GetAllColumnKeys()
		{
			var seen = new HashSet<C>();
			foreach (var map in raw.Values)
			{
				foreach (var key in map.Keys)
				{
					if (seen.Add(key))
					{
						yield return key;
					}
				}
			}
		}
		//endregion

		//region getColumn
		public IDictionary<R, V> GetColumn(C columnKey)
		{
			return new Column(this, columnKey);
		}

		private class Column : IDictionary<R, V>
		{
			private readonly RowKeyTable<R, C, V> table;
			readonly C columnKey;

			public Column(RowKeyTable<R, C, V> table, C columnKey)
			{
				this.table = table;
				this.columnKey = columnKey;
			}

			public IEnumerator<KeyValuePair<R, V>> GetEnumerator()
			{
				foreach (var entry in table.raw)
				{
					if (entry.Value.TryGetValue(columnKey, out var value))
					{
						yield return new KeyValuePair<R, V>(entry.Key, value);
					}
				}
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}

			public void Add(KeyValuePair<R, V> item)
			{
				table.Put(item.Key, columnKey, item.Value);
			}

			public void Clear()
			{
				foreach (var entry in table.raw)
				{
					entry.Value.Remove(columnKey);
					if (entry.Value.Count == 0)
					{
						table.raw.Remove(entry.Key);
					}
				}
			}

			public bool Contains(KeyValuePair<R, V> item)
			{
				return TryGetValue(item.Key, out var value) && Equals(value, item.Value);
			}

			public void CopyTo(KeyValuePair<R, V>[] array, int arrayIndex)
			{
				int i = arrayIndex;
				foreach (var entry in this)
				{
					array[i++] = entry;
				}
			}

			public bool Remove(KeyValuePair<R, V> item)
			{
				if (Contains(item))
				{
					table.Remove(item.Key, columnKey);
					return true;
				}
				return false;
			}

			public int Count => table.raw.Sum(row => row.Value.Count);

			public bool IsReadOnly => false;

			public void Add(R key, V value)
			{
				table.Put(key, columnKey, value);
			}

			public bool ContainsKey(R key)
			{
				var map = ((Table<R, C, V>)table).GetRow(key);
				return map != null && map.ContainsKey(columnKey);
			}

			public bool Remove(R key)
			{
				return table.Remove(key, columnKey) != null;
			}

			public bool TryGetValue(R key, out V value)
			{
				var map = ((Table<R, C, V>)table).GetRow(key);
				if (map != null && map.TryGetValue(columnKey, out value))
				{
					return true;
				}
				value = default(V);
				return false;
			}

			public V this[R key]
			{
				get
				{
					if (TryGetValue(key, out var value))
					{
						return value;
					}
					throw new KeyNotFoundException();
				}
				set => table.Put(key, columnKey, value);
			}

			public ICollection<R> Keys => this.Select(entry => entry.Key).ToList();

			public ICollection<V> Values => this.Select(entry => entry.Value).ToList();
		}
		//endregion
	}
}