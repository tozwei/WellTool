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
		// 简化实现
		return 0;
	}
}
