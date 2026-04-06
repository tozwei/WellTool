using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WellTool.Core.Map.Multi
{
	/// <summary>
	/// 抽象{@link Table}接口实现<br>
	/// 默认实现了：
	/// <ul>
	///     <li>{@link #Equals(Object)}</li>
	///     <li>{@link #GetHashCode()}</li>
	///     <li>{@link #ToString()}</li>
	///     <li>{@link #Values()}</li>
	///     <li>{@link #CellSet()}</li>
	///     <li>{@link #GetEnumerator()}</li>
	/// </ul>
	/// </summary>
	/// <typeparam name="R">行类型</typeparam>
	/// <typeparam name="C">列类型</typeparam>
	/// <typeparam name="V">值类型</typeparam>
	public abstract class AbsTable<R, C, V> : Table<R, C, V>
	{
		public bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			else if (obj is Table<R, C, V> table)
			{
				return ((Table<R, C, V>)this).CellSet().SetEquals(table.CellSet());
			}
			else
			{
				return false;
			}
		}

		public int GetHashCode()
		{
			return ((Table<R, C, V>)this).CellSet().GetHashCode();
		}

		public string ToString()
		{
			return RowMap().ToString();
		}

		//region values
		ICollection<V> Table<R, C, V>.Values()
		{
			if (values == null)
			{
				values = new ValuesCollection(this);
			}
			return values;
		}

		private ICollection<V>? values;
		private class ValuesCollection : ICollection<V>
		{
			private readonly AbsTable<R, C, V> table;

			public ValuesCollection(AbsTable<R, C, V> table)
			{
				this.table = table;
			}

			public IEnumerator<V> GetEnumerator()
			{
				foreach (var cell in ((Table<R, C, V>)table).CellSet())
				{
					yield return cell.Value;
				}
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}

			public void Add(V item)
			{
				throw new NotSupportedException();
			}

			public void Clear()
			{
				table.Clear();
			}

			public bool Contains(V item)
			{
				return ((Table<R, C, V>)table).ContainsValue(item);
			}

			public void CopyTo(V[] array, int arrayIndex)
			{
				int i = arrayIndex;
				foreach (var cell in ((Table<R, C, V>)table).CellSet())
				{
					array[i++] = cell.Value;
				}
			}

			public bool Remove(V item)
			{
				throw new NotSupportedException();
			}

			public int Count => ((Table<R, C, V>)table).Size();

			public bool IsReadOnly => true;
		}
		//endregion

		//region cellSet
		ISet<Table<R, C, V>.Cell> Table<R, C, V>.CellSet()
		{
			if (cellSet == null)
			{
				cellSet = new CellSetImpl(this);
			}
			return cellSet;
		}

		private ISet<Table<R, C, V>.Cell>? cellSet;

		private class CellSetImpl : ISet<Table<R, C, V>.Cell>
		{
			private readonly AbsTable<R, C, V> table;
			private readonly HashSet<Table<R, C, V>.Cell> backingSet;

			public CellSetImpl(AbsTable<R, C, V> table)
			{
				this.table = table;
				this.backingSet = new HashSet<Table<R, C, V>.Cell>();
				// 初始化时填充所有单元格
				foreach (var rowEntry in table.RowMap())
				{
					foreach (var columnEntry in rowEntry.Value)
					{
						backingSet.Add(new SimpleCell<R, C, V>(rowEntry.Key, columnEntry.Key, columnEntry.Value));
					}
				}
			}

			public IEnumerator<Table<R, C, V>.Cell> GetEnumerator()
			{
				return backingSet.GetEnumerator();
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}

			public bool Add(Table<R, C, V>.Cell item)
			{
				return backingSet.Add(item);
			}

			void ICollection<Table<R, C, V>.Cell>.Add(Table<R, C, V>.Cell item)
			{
				backingSet.Add(item);
			}

			public void Clear()
			{
				backingSet.Clear();
				table.Clear();
			}

			public bool Contains(Table<R, C, V>.Cell item)
			{
				return backingSet.Contains(item);
			}

			public void CopyTo(Table<R, C, V>.Cell[] array, int arrayIndex)
			{
				backingSet.CopyTo(array, arrayIndex);
			}

			public bool Remove(Table<R, C, V>.Cell item)
			{
				if (backingSet.Remove(item))
				{
					table.Remove(item.RowKey, item.ColumnKey);
					return true;
				}
				return false;
			}

			public int Count => backingSet.Count;

			public bool IsReadOnly => false;

			public bool AddIfNotPresent(Table<R, C, V>.Cell item)
			{
				if (!backingSet.Contains(item))
				{
					backingSet.Add(item);
					table.Put(item.RowKey, item.ColumnKey, item.Value);
					return true;
				}
				return false;
			}

			public void ExceptWith(IEnumerable<Table<R, C, V>.Cell> other)
			{
				backingSet.ExceptWith(other);
			}

			public void IntersectWith(IEnumerable<Table<R, C, V>.Cell> other)
			{
				backingSet.IntersectWith(other);
			}

			public bool IsProperSubsetOf(IEnumerable<Table<R, C, V>.Cell> other)
			{
				return backingSet.IsProperSubsetOf(other);
			}

			public bool IsProperSupersetOf(IEnumerable<Table<R, C, V>.Cell> other)
			{
				return backingSet.IsProperSupersetOf(other);
			}

			public bool IsSubsetOf(IEnumerable<Table<R, C, V>.Cell> other)
			{
				return backingSet.IsSubsetOf(other);
			}

			public bool IsSupersetOf(IEnumerable<Table<R, C, V>.Cell> other)
			{
				return backingSet.IsSupersetOf(other);
			}

			public bool Overlaps(IEnumerable<Table<R, C, V>.Cell> other)
			{
				return backingSet.Overlaps(other);
			}

			public bool SetEquals(IEnumerable<Table<R, C, V>.Cell> other)
			{
				return backingSet.SetEquals(other);
			}

			public void SymmetricExceptWith(IEnumerable<Table<R, C, V>.Cell> other)
			{
				backingSet.SymmetricExceptWith(other);
			}

			public void UnionWith(IEnumerable<Table<R, C, V>.Cell> other)
			{
				backingSet.UnionWith(other);
			}
		}
		//endregion

		//region iterator
		public IEnumerator<Table<R, C, V>.Cell> GetEnumerator()
		{
			return new CellIterator(this);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		/// <summary>
		/// 基于{@link Cell}的{@link IEnumerator}实现
		/// </summary>
		private class CellIterator : IEnumerator<Table<R, C, V>.Cell>
		{
			private readonly AbsTable<R, C, V> table;
			private IEnumerator<KeyValuePair<R, IDictionary<C, V>>> rowIterator;
			private KeyValuePair<R, IDictionary<C, V>> rowEntry;
			private IEnumerator<KeyValuePair<C, V>> columnIterator;

			public CellIterator(AbsTable<R, C, V> table)
			{
				this.table = table;
				this.rowIterator = table.RowMap().GetEnumerator();
				this.columnIterator = Enumerable.Empty<KeyValuePair<C, V>>().GetEnumerator();
			}

			public bool MoveNext()
			{
				if (!columnIterator.MoveNext())
				{
					if (!rowIterator.MoveNext())
					{
						return false;
					}
					rowEntry = rowIterator.Current;
					columnIterator = rowEntry.Value.GetEnumerator();
					return columnIterator.MoveNext();
				}
				return true;
			}

			public void Reset()
			{
				rowIterator = table.RowMap().GetEnumerator();
				columnIterator = Enumerable.Empty<KeyValuePair<C, V>>().GetEnumerator();
			}

			public Table<R, C, V>.Cell Current
			{
				get
				{
					var columnEntry = columnIterator.Current;
					return new SimpleCell<R, C, V>(rowEntry.Key, columnEntry.Key, columnEntry.Value);
				}
			}

			object IEnumerator.Current => Current;

			public void Dispose()
			{
				rowIterator.Dispose();
				columnIterator.Dispose();
			}
		}
		//endregion

		/// <summary>
		/// 简单{@link Cell} 实现
		/// </summary>
		/// <typeparam name="R">行类型</typeparam>
		/// <typeparam name="C">列类型</typeparam>
		/// <typeparam name="V">值类型</typeparam>
		private class SimpleCell<R, C, V> : Table<R, C, V>.Cell
		{
			private readonly R rowKey;
			private readonly C columnKey;
			private readonly V value;

			public SimpleCell(R rowKey, C columnKey, V value)
			{
				this.rowKey = rowKey;
				this.columnKey = columnKey;
				this.value = value;
			}

			public R RowKey => rowKey;

			public C ColumnKey => columnKey;

			public V Value => value;

			public override bool Equals(object obj)
			{
				if (obj == this)
				{
					return true;
				}
				if (obj is Table<R, C, V>.Cell other)
				{
					return Equals(rowKey, other.RowKey)
						&& Equals(columnKey, other.ColumnKey)
						&& Equals(value, other.Value);
				}
				return false;
			}

			public override int GetHashCode()
			{
				return HashCode.Combine(rowKey, columnKey, value);
			}

			public override string ToString()
			{
				return $"({rowKey},{columnKey})={value}";
			}
		}

		// 抽象方法，需要子类实现
		public abstract IDictionary<R, IDictionary<C, V>> RowMap();
		public abstract IDictionary<C, IDictionary<R, V>> ColumnMap();
		public abstract V Put(R rowKey, C columnKey, V value);
		public abstract V Remove(R rowKey, C columnKey);
		public abstract bool IsEmpty();
		public abstract void Clear();
	}
}