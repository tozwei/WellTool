using NPOI.SS.UserModel;
using System;

namespace WellTool.Poi.Excel.Cell;

/// <summary>
/// 公式单元格值
/// </summary>
public class FormulaCellValue
{
	/// <summary>
	/// 公式
	/// </summary>
	public string Formula { get; }

	/// <summary>
	/// 结果
	/// </summary>
	public object Result { get; }

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="formula">公式</param>
	public FormulaCellValue(string formula)
	{
		Formula = formula;
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="formula">公式</param>
	/// <param name="result">结果</param>
	public FormulaCellValue(string formula, object result)
	{
		Formula = formula;
		Result = result;
	}

	/// <summary>
	/// 设置单元格公式
	/// </summary>
	/// <param name="cell">单元格</param>
	public void SetValue(ICell cell)
	{
		cell.SetCellFormula(Formula);
	}

	/// <summary>
	/// 获取结果字符串
	/// </summary>
	public override string ToString()
	{
		return Result?.ToString() ?? string.Empty;
	}
}
