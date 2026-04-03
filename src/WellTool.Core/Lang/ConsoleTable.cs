using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WellTool.Core.Lang;

/// <summary>
/// 控制台表格
/// </summary>
public class ConsoleTable
{
	private readonly List<string[]> _rows = new();
	private string[] _headers;
	private readonly List<int> _columnWidths = new();

	/// <summary>
	/// 设置表头
	/// </summary>
	/// <param name="headers">表头</param>
	/// <returns>this</returns>
	public ConsoleTable SetHeaders(params string[] headers)
	{
		_headers = headers;
		return this;
	}

	/// <summary>
	/// 添加行
	/// </summary>
	/// <param name="row">行数据</param>
	/// <returns>this</returns>
	public ConsoleTable AddRow(params string[] row)
	{
		_rows.Add(row);
		return this;
	}

	/// <summary>
	/// 添加行
	/// </summary>
	/// <param name="row">行数据</param>
	/// <returns>this</returns>
	public ConsoleTable AddRow(IEnumerable<object> row)
	{
		_rows.Add(row.Select(o => o?.ToString() ?? "").ToArray());
		return this;
	}

	/// <summary>
	/// 输出表格到控制台
	/// </summary>
	public void Print()
	{
		CalculateColumnWidths();
		PrintLine(GetSeparator());
		PrintLine(GetRow(_headers));
		PrintLine(GetSeparator());
		foreach (var row in _rows)
		{
			PrintLine(GetRow(row));
		}
		PrintLine(GetSeparator());
	}

	/// <summary>
	/// 转换为字符串
	/// </summary>
	public override string ToString()
	{
		CalculateColumnWidths();
		var sb = new StringBuilder();
		sb.AppendLine(GetSeparator());
		sb.AppendLine(GetRow(_headers));
		sb.AppendLine(GetSeparator());
		foreach (var row in _rows)
		{
			sb.AppendLine(GetRow(row));
		}
		sb.Append(GetSeparator());
		return sb.ToString();
	}

	private void CalculateColumnWidths()
	{
		if (_headers == null) return;

		_columnWidths.Clear();
		for (int i = 0; i < _headers.Length; i++)
		{
			int maxWidth = _headers[i]?.Length ?? 0;
			foreach (var row in _rows)
			{
				if (row != null && i < row.Length)
				{
					maxWidth = System.Math.Max(maxWidth, row[i]?.Length ?? 0);
				}
			}
			_columnWidths.Add(maxWidth + 2);
		}
	}

	private string GetSeparator()
	{
		var sb = new StringBuilder("+");
		foreach (var width in _columnWidths)
		{
			sb.Append(new string('-', width));
			sb.Append("+");
		}
		return sb.ToString();
	}

	private string GetRow(string[] cells)
	{
		if (cells == null) return "";

		var sb = new StringBuilder("|");
		for (int i = 0; i < _columnWidths.Count; i++)
		{
			var cell = i < cells.Length ? cells[i] ?? "" : "";
			sb.Append(" ");
			sb.Append(cell.PadRight(_columnWidths[i] - 1));
			sb.Append("|");
		}
		return sb.ToString();
	}

	private void PrintLine(string line)
	{
		System.Console.WriteLine(line);
	}
}
