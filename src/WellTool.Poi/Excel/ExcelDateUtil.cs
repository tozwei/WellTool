using NPOI.SS.UserModel;
using NPOI.SS.Formula;
using System;

namespace WellTool.Poi.Excel;

/// <summary>
/// Excel日期工具类
/// </summary>
public static class ExcelDateUtil
{
	/// <summary>
	/// 特殊日期格式索引
	/// </summary>
	private static readonly int[] CustomFormats = { 28, 30, 31, 32, 33, 55, 56, 57, 58 };

	/// <summary>
	/// 判断是否为日期格式
	/// </summary>
	/// <param name="cell">单元格</param>
	/// <returns>是否为日期格式</returns>
	public static bool IsDateFormat(ICell cell)
	{
		if (cell == null || cell.CellStyle == null)
			return false;

		var formatIndex = cell.CellStyle.DataFormat;
		var formatString = cell.CellStyle.GetDataFormatString() ?? string.Empty;

		return IsDateFormat(formatIndex, formatString);
	}

	/// <summary>
	/// 判断是否为日期格式
	/// </summary>
	/// <param name="formatIndex">格式索引</param>
	/// <param name="formatString">格式字符串</param>
	/// <returns>是否为日期格式</returns>
	public static bool IsDateFormat(int formatIndex, string formatString)
	{
		// 检查特殊格式
		if (Array.IndexOf(CustomFormats, formatIndex) >= 0)
			return true;

		// 检查周、星期等
		if (!string.IsNullOrEmpty(formatString) &&
			(formatString.Contains("周") || formatString.Contains("星期")))
			return true;

		return DateUtil.IsADateFormat(formatIndex, formatString);
	}
}
