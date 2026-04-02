using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WellTool.Poi.Excel;

/// <summary>
/// Excel行工具类
/// </summary>
public static class RowUtil
{
	/// <summary>
	/// 获取或创建行
	/// </summary>
	/// <param name="sheet">Sheet</param>
	/// <param name="rowIndex">行索引</param>
	/// <returns>行</returns>
	public static IRow GetOrCreateRow(ISheet sheet, int rowIndex)
	{
		var row = sheet.GetRow(rowIndex);
		return row ?? sheet.CreateRow(rowIndex);
	}

	/// <summary>
	/// 删除行
	/// </summary>
	/// <param name="row">要删除的行</param>
	public static void RemoveRow(IRow row)
	{
		if (row == null)
			return;

		var sheet = row.Sheet;
		var rowIndex = row.RowNum;
		var lastRow = sheet.LastRowNum;

		if (rowIndex >= 0 && rowIndex < lastRow)
		{
			sheet.ShiftRows(rowIndex + 1, lastRow, -1);
		}

		if (rowIndex == lastRow)
		{
			sheet.RemoveRow(row);
		}
	}
}
