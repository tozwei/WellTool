using NPOI.SS.UserModel;
using System;
using System.IO;

namespace WellTool.Poi.Excel;

/// <summary>
/// Excel内容提取器
/// </summary>
public static class ExcelExtractorUtil
{
	/// <summary>
	/// 将Excel读取为文本
	/// </summary>
	/// <param name="workbook">工作簿</param>
	/// <param name="withSheetName">是否包含Sheet名称</param>
	/// <returns>文本内容</returns>
	public static string ReadAsText(IWorkbook workbook, bool withSheetName)
	{
		throw new NotImplementedException("ExcelExtractorUtil needs to be implemented with NPOI.Extractor");
	}
}
