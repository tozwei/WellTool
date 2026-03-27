using System;
using System.Collections.Generic;
using System.Linq;

namespace WellTool.Core.Map.Multi
{
	/// <summary>
	/// 表格数据结构定义<br>
	/// 此结构类似于Guava的Table接口，使用两个键映射到一个值，类似于表格结构。
	/// </summary>
	/// <typeparam name="R">行键类型</typeparam>
	/// <typeparam name="C">列键类型</typeparam>
	/// <typeparam name="V">值类型</typeparam>
	public interface Table<R, C, V> : IEnumerable<Table<R, C, V>.Cell>
	{
		/// <summary>
		/// 是否包含指定行列的映射<br>
		/// 行和列任意一个不存在都会返回{@code false}，如果行和列都存在，值为{@code null}，也会返回{@code true}
		/// </summary>
		/// <param name="rowKey">行键</param>
		/// <param name="columnKey">列键</param>
		/// <returns>是否包含映射</returns>
		bool Contains(R rowKey, C columnKey)
		{
			var row = GetRow(rowKey);
			return row != null && row.ContainsKey(columnKey);
		}

		//region Row

		/// <summary>
		/// 行是否存在
		/// </summary>
		/// <param name="rowKey">行键</param>
		/// <returns>行是否存在</returns>
		bool ContainsRow(R rowKey)
		{
			var rowMap = RowMap();
			return rowMap != null && rowMap.ContainsKey(rowKey);
		}

		/// <summary>
		/// 获取行
		/// </summary>
		/// <param name="rowKey">行键</param>
		/// <returns>行映射，返回的键为列键，值为表格的值</returns>
		IDictionary<C, V> GetRow(R rowKey)
		{
			var rowMap = RowMap();
			return rowMap != null && rowMap.TryGetValue(rowKey, out var map) ? map : null;
		}

		/// <summary>
		/// 返回所有行的key，行的key不可重复
		/// </summary>
		/// <returns>行键</returns>
		ICollection<R> RowKeySet()
		{
			var rowMap = RowMap();
			return rowMap != null ? rowMap.Keys : new List<R>();
		}

		/// <summary>
		/// 返回行列对应的Map
		/// </summary>
		/// <returns>map，键为行键，值为列和值的对应map</returns>
		IDictionary<R, IDictionary<C, V>> RowMap();
		//endregion

		//region Column

		/// <summary>
		/// 列是否存在
		/// </summary>
		/// <param name="columnKey">列键</param>
		/// <returns>列是否存在</returns>
		bool ContainsColumn(C columnKey)
		{
			var columnMap = ColumnMap();
			return columnMap != null && columnMap.ContainsKey(columnKey);
		}

		/// <summary>
		/// 获取列
		/// </summary>
		/// <param name="columnKey">列键</param>
		/// <returns>列映射，返回的键为行键，值为表格的值</returns>
		IDictionary<R, V> GetColumn(C columnKey)
		{
			var columnMap = ColumnMap();
			return columnMap != null && columnMap.TryGetValue(columnKey, out var map) ? map : null;
		}

		/// <summary>
		/// 返回所有列的key，列的key不可重复
		/// </summary>
		/// <returns>列set</returns>
		ICollection<C> ColumnKeySet()
		{
			var columnMap = ColumnMap();
			return columnMap != null ? columnMap.Keys : new List<C>();
		}

		/// <summary>
		/// 返回所有列的key，列的key如果实现Map是可重复key，则返回对应不去重的List。
		/// </summary>
		/// <returns>列set</returns>
		IList<C> ColumnKeys()
		{
			var columnMap = ColumnMap();
			if (columnMap == null || columnMap.Count == 0)
			{
				return new List<C>();
			}

			var result = new List<C>(columnMap.Count);
			foreach (var entry in columnMap)
			{
				result.Add(entry.Key);
			}
			return result;
		}

		/// <summary>
		/// 返回列-行对应的map
		/// </summary>
		/// <returns>map，键为列键，值为行和值的对应map</returns>
		IDictionary<C, IDictionary<R, V>> ColumnMap();
		//endregion

		//region value

		/// <summary>
		/// 指定值是否存在
		/// </summary>
		/// <param name="value">值</param>
		/// <returns>值</returns>
		bool ContainsValue(V value)
		{
			var rows = RowMap()?.Values;
			if (rows != null)
			{
				foreach (var row in rows)
				{
					if (row.Values.Any(v => Equals(v, value)))
					{
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>
		/// 获取指定值
		/// </summary>
		/// <param name="rowKey">行键</param>
		/// <param name="columnKey">列键</param>
		/// <returns>值，如果值不存在，返回{@code null}</returns>
		V Get(R rowKey, C columnKey)
		{
			var row = GetRow(rowKey);
			return row != null && row.TryGetValue(columnKey, out var value) ? value : default(V);
		}

		/// <summary>
		/// 所有行列值的集合
		/// </summary>
		/// <returns>值的集合</returns>
		ICollection<V> Values();
		//endregion

		/// <summary>
		/// 所有单元格集合
		/// </summary>
		/// <returns>单元格集合</returns>
		ISet<Cell> CellSet();

		/// <summary>
		/// 为表格指定行列赋值，如果不存在，创建之，存在则替换之，返回原值
		/// </summary>
		/// <param name="rowKey">行键</param>
		/// <param name="columnKey">列键</param>
		/// <param name="value">值</param>
		/// <returns>原值，不存在返回{@code null}</returns>
		V Put(R rowKey, C columnKey, V value);

		/// <summary>
		/// 批量加入
		/// </summary>
		/// <param name="table">其他table</param>
		void PutAll(Table<R, C, V> table)
		{
			if (table != null)
			{
				foreach (var cell in table.CellSet())
				{
					Put(cell.RowKey, cell.ColumnKey, cell.Value);
				}
			}
		}

		/// <summary>
		/// 移除指定值
		/// </summary>
		/// <param name="rowKey">行键</param>
		/// <param name="columnKey">列键</param>
		/// <returns>移除的值，如果值不存在，返回{@code null}</returns>
		V Remove(R rowKey, C columnKey);

		/// <summary>
		/// 表格是否为空
		/// </summary>
		/// <returns>是否为空</returns>
		bool IsEmpty();

		/// <summary>
		/// 表格大小，一般为单元格的个数
		/// </summary>
		/// <returns>表格大小</returns>
		int Size()
		{
			var rowMap = RowMap();
			if (rowMap == null || rowMap.Count == 0)
			{
				return 0;
			}
			int size = 0;
			foreach (var map in rowMap.Values)
			{
				size += map.Count;
			}
			return size;
		}

		/// <summary>
		/// 清空表格
		/// </summary>
		void Clear();

		/// <summary>
		/// 遍历表格的单元格，处理值
		/// </summary>
		/// <param name="action">单元格值处理器</param>
		void ForEach(Action<R, C, V> action)
		{
			foreach (var cell in this)
			{
				action(cell.RowKey, cell.ColumnKey, cell.Value);
			}
		}

		/// <summary>
		/// 单元格，用于表示一个单元格的行、列和值
		/// </summary>
		interface Cell
		{
			/// <summary>
			/// 获取行键
			/// </summary>
			/// <returns>行键</returns>
			R RowKey { get; }

			/// <summary>
			/// 获取列键
			/// </summary>
			/// <returns>列键</returns>
			C ColumnKey { get; }

			/// <summary>
			/// 获取值
			/// </summary>
			/// <returns>值</returns>
			V Value { get; }
		}
	}
}