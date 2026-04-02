using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using System;

namespace WellTool.Poi.Excel;

/// <summary>
/// 样式集合
/// </summary>
[Serializable]
public class StyleSet
{
	private readonly IWorkbook _workbook;
	private readonly ICellStyle _headCellStyle;
	private readonly ICellStyle _cellStyle;
	private readonly ICellStyle _cellStyleForNumber;
	private readonly ICellStyle _cellStyleForDate;
	private readonly ICellStyle _cellStyleForHyperlink;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="workbook">工作簿</param>
	public StyleSet(IWorkbook workbook)
	{
		_workbook = workbook;
		_headCellStyle = workbook.CreateCellStyle();
		_cellStyle = workbook.CreateCellStyle();
		_cellStyleForNumber = workbook.CreateCellStyle();
		_cellStyleForNumber.DataFormat = 2; // 0.00
		_cellStyleForDate = workbook.CreateCellStyle();
		_cellStyleForDate.DataFormat = 22; // m/d/yy h:mm
		_cellStyleForHyperlink = workbook.CreateCellStyle();

		// 设置标题样式
		var font = workbook.CreateFont();
		font.IsBold = true;
		_headCellStyle.SetFont(font);
	}

	/// <summary>
	/// 头部样式
	/// </summary>
	public ICellStyle HeadCellStyle => _headCellStyle;

	/// <summary>
	/// 默认样式
	/// </summary>
	public ICellStyle CellStyle => _cellStyle;

	/// <summary>
	/// 数字样式
	/// </summary>
	public ICellStyle CellStyleForNumber => _cellStyleForNumber;

	/// <summary>
	/// 日期样式
	/// </summary>
	public ICellStyle CellStyleForDate => _cellStyleForDate;

	/// <summary>
	/// 链接样式
	/// </summary>
	public ICellStyle CellStyleForHyperlink => _cellStyleForHyperlink;

	/// <summary>
	/// 设置边框
	/// </summary>
	public StyleSet SetBorder(BorderStyle borderSize, short colorIndex)
	{
		_headCellStyle.BorderBottom = borderSize;
		_headCellStyle.BorderLeft = borderSize;
		_headCellStyle.BorderRight = borderSize;
		_headCellStyle.BorderTop = borderSize;
		return this;
	}

	/// <summary>
	/// 设置对齐
	/// </summary>
	public StyleSet SetAlign(HorizontalAlignment halign, VerticalAlignment valign)
	{
		_headCellStyle.Alignment = halign;
		_headCellStyle.VerticalAlignment = valign;
		return this;
	}

	/// <summary>
	/// 设置背景色
	/// </summary>
	public StyleSet SetBackgroundColor(IndexedColors color)
	{
		_cellStyle.FillForegroundColor = color.Index;
		return this;
	}

	/// <summary>
	/// 设置换行
	/// </summary>
	public StyleSet SetWrapText()
	{
		_cellStyle.WrapText = true;
		return this;
	}
}
