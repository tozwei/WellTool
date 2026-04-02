using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using WellTool.Poi.Excel.Sax.Handler;

namespace WellTool.Poi.Excel.Sax;

/// <summary>
/// Excel03 SAX读取器
/// </summary>
public class Excel03SaxReader : IExcelSaxReader
{
	/// <summary>
	/// RID前缀
	/// </summary>
	public static string RidPrefix => "rId";

	/// <inheritdoc/>
	string IExcelSaxReader.RidPrefix => RidPrefix;

	/// <summary>
	/// 行处理器
	/// </summary>
	private readonly IRowHandler _rowHandler;

	/// <summary>
	/// 是否已停止读取
	/// </summary>
	private bool _isStopped;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="rowHandler">行处理器</param>
	public Excel03SaxReader(IRowHandler rowHandler)
	{
		_rowHandler = rowHandler ?? throw new ArgumentNullException(nameof(rowHandler));
	}

	/// <inheritdoc/>
	public IExcelSaxReader Read(FileInfo file, string idOrRidOrSheetName)
	{
		using var fs = file.OpenRead();
		return Read(fs, idOrRidOrSheetName);
	}

	/// <inheritdoc/>
	public IExcelSaxReader Read(Stream inStream, string idOrRidOrSheetName)
	{
		_isStopped = false;
		var workbook = WorkbookFactory.Create(inStream);
		try
		{
			var sheetIndex = ResolveSheetIndex(workbook, idOrRidOrSheetName);
			var sheet = workbook.GetSheetAt(sheetIndex);

			ProcessRows(sheet, sheetIndex);
		}
		finally
		{
			workbook.Close();
		}

		return this;
	}

	/// <summary>
	/// 解析Sheet索引
	/// </summary>
	private int ResolveSheetIndex(IWorkbook workbook, string idOrRidOrSheetName)
	{
		// 尝试解析为数字索引
		if (int.TryParse(idOrRidOrSheetName, out var index))
		{
			return Math.Max(0, Math.Min(index, workbook.NumberOfSheets - 1));
		}

		// 按名称查找
		for (int i = 0; i < workbook.NumberOfSheets; i++)
		{
			if (workbook.GetSheetName(i).Equals(idOrRidOrSheetName, StringComparison.OrdinalIgnoreCase))
			{
				return i;
			}
		}

		return 0;
	}

	/// <summary>
	/// 处理行
	/// </summary>
	private void ProcessRows(ISheet sheet, int sheetIndex)
	{
		foreach (IRow row in sheet)
		{
			if (row == null || _isStopped) continue;

			var rowData = new List<object>();
			for (int i = 0; i < row.LastCellNum; i++)
			{
				var cell = row.GetCell(i);
				rowData.Add(cell != null ? GetCellValue(cell) : null!);
			}

			_rowHandler.Handle(sheetIndex, row.RowNum, rowData);
		}
	}

	/// <summary>
	/// 停止读取
	/// </summary>
	public void Stop()
	{
		_isStopped = true;
	}

	/// <summary>
	/// 获取单元格值
	/// </summary>
	private object GetCellValue(ICell cell)
	{
		return cell.CellType switch
		{
			CellType.Numeric => DateUtil.IsCellDateFormatted(cell) ? cell.DateCellValue : cell.NumericCellValue,
			CellType.String => cell.StringCellValue,
			CellType.Boolean => cell.BooleanCellValue,
			CellType.Formula => GetFormulaValue(cell),
			CellType.Error => cell.ErrorCellValue,
			CellType.Blank => string.Empty,
			_ => string.Empty
		};
	}

	/// <summary>
	/// 获取公式值
	/// </summary>
	private object GetFormulaValue(ICell cell)
	{
		try
		{
			return cell.CachedFormulaResultType switch
			{
				CellType.Numeric => cell.NumericCellValue,
				CellType.String => cell.StringCellValue,
				_ => cell.StringCellValue
			};
		}
		catch
		{
			return string.Empty;
		}
	}
}
