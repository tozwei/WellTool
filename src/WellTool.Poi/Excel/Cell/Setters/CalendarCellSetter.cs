using NPOI.SS.UserModel;
using System;

namespace WellTool.Poi.Excel.Cell.Setters;

/// <summary>
/// 日历单元格设置器
/// </summary>
internal class CalendarCellSetter : ICellSetter
{
	private readonly DateTime _value;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="value">日期时间值</param>
	internal CalendarCellSetter(DateTime value)
	{
		_value = value;
	}

	/// <summary>
	/// 设置单元格值
	/// </summary>
	/// <param name="cell">单元格</param>
	public void SetValue(ICell cell)
	{
		cell.SetCellValue(_value);
	}
}
