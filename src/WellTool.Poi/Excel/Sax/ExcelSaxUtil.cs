using System;
using System.IO;
using WellTool.Poi.Excel.Sax.Handler;

namespace WellTool.Poi.Excel.Sax;

/// <summary>
/// SAX工具类
/// </summary>
public static class ExcelSaxUtil
{
	/// <summary>
	/// 创建SAX读取器
	/// </summary>
	/// <param name="isXlsx">是否为xlsx</param>
	/// <param name="rowHandler">行处理器</param>
	/// <returns>SAX读取器</returns>
	public static IExcelSaxReader CreateSaxReader(bool isXlsx, IRowHandler rowHandler)
	{
		return isXlsx
			? new Excel07SaxReader(rowHandler)
			: new Excel03SaxReader(rowHandler);
	}

	/// <summary>
	/// 计算空单元格数
	/// </summary>
	public static int CountNullCell(string preRef, string refCoord)
	{
		// 实现计算空单元格数的功能
		if (string.IsNullOrEmpty(preRef) || string.IsNullOrEmpty(refCoord))
		{
			return 0;
		}

		try
		{
			// 解析前一个单元格引用
			var preCell = ParseCellReference(preRef);
			// 解析当前单元格引用
			var currentCell = ParseCellReference(refCoord);

			// 计算行差和列差
			int rowDiff = currentCell.Row - preCell.Row;
			int colDiff = currentCell.Column - preCell.Column;

			// 如果是同一行，计算列差
			if (rowDiff == 0)
			{
				return Math.Max(0, colDiff - 1);
			}
			// 如果是同一列，计算行差
			else if (colDiff == 0)
			{
				return Math.Max(0, rowDiff - 1);
			}
			// 否则，计算矩形区域中的单元格数
			else
			{
				// 计算行数和列数
				int rows = Math.Abs(rowDiff) + 1;
				int cols = Math.Abs(colDiff) + 1;
				// 总单元格数减去两个端点
				return rows * cols - 2;
			}
		}
		catch
		{
			// 解析失败，返回 0
			return 0;
		}
	}

	/// <summary>
	/// 解析单元格引用
	/// </summary>
	/// <param name="cellReference">单元格引用，如 "A1"</param>
	/// <returns>单元格坐标</returns>
	private static (int Row, int Column) ParseCellReference(string cellReference)
	{
		// 分离列字母和行数字
		int i = 0;
		while (i < cellReference.Length && char.IsLetter(cellReference[i]))
		{
			i++;
		}

		string columnPart = cellReference.Substring(0, i);
		string rowPart = cellReference.Substring(i);

		// 解析列号（A=1, B=2, ..., Z=26, AA=27, 等等）
		int column = 0;
		foreach (char c in columnPart)
		{
			column = column * 26 + (char.ToUpper(c) - 'A' + 1);
		}

		// 解析行号
		int row = int.Parse(rowPart);

		return (row, column);
	}
}
