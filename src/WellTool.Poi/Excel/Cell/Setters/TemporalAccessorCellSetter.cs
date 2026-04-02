using NPOI.SS.UserModel;
using System;

namespace WellTool.Poi.Excel.Cell.Setters;

/// <summary>
/// 时间访问器单元格设置器
/// </summary>
public class TemporalAccessorCellSetter : ICellSetter
{
	private readonly object _value;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="value">值</param>
	public TemporalAccessorCellSetter(object value)
	{
		_value = value;
	}

	/// <summary>
	/// 设置单元格值
	/// </summary>
	/// <param name="cell">单元格</param>
	public void SetValue(ICell cell)
	{
		switch (_value)
		{
			case DateTime dt:
				cell.SetCellValue(dt);
				break;
			case DateTimeOffset dto:
				cell.SetCellValue(dto.DateTime);
				break;
		}
	}
}
