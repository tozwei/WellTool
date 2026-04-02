using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;

namespace WellTool.Poi.Excel;

/// <summary>
/// Excel工作簿工具类
/// </summary>
public static class WorkbookUtil
{
	/// <summary>
	/// 创建或加载工作簿
	/// </summary>
	/// <param name="excelFile">Excel文件</param>
	/// <returns>工作簿</returns>
	public static IWorkbook CreateBook(FileInfo excelFile)
	{
		if (excelFile == null)
			return CreateBook(true);

		if (!excelFile.Exists)
			return CreateBook(excelFile.Extension.Equals(".xlsx", StringComparison.OrdinalIgnoreCase));

		using var fs = excelFile.OpenRead();
		return WorkbookFactory.Create(fs);
	}

	/// <summary>
	/// 创建或加载工作簿
	/// </summary>
	/// <param name="inStream">输入流</param>
	/// <returns>工作簿</returns>
	public static IWorkbook CreateBook(Stream inStream)
	{
		return WorkbookFactory.Create(inStream);
	}

	/// <summary>
	/// 创建空白工作簿
	/// </summary>
	/// <param name="isXlsx">是否为xlsx格式</param>
	/// <returns>工作簿</returns>
	public static IWorkbook CreateBook(bool isXlsx)
	{
		// NPOI中WorkbookFactory.Create需要传入一个流或文件
		// 创建空白工作簿使用反射或直接实例化
		if (isXlsx)
		{
			var xssfType = Type.GetType("NPOI.XSSF.UserModel.XSSFWorkbook, NPOI.OOXML");
			return (IWorkbook)Activator.CreateInstance(xssfType!);
		}
		else
		{
			var hssfType = Type.GetType("NPOI.HSSF.UserModel.HSSFWorkbook, NPOI");
			return (IWorkbook)Activator.CreateInstance(hssfType!);
		}
	}

	/// <summary>
	/// 获取或创建Sheet
	/// </summary>
	/// <param name="book">工作簿</param>
	/// <param name="sheetName">Sheet名称</param>
	/// <returns>Sheet</returns>
	public static ISheet GetOrCreateSheet(IWorkbook book, string sheetName)
	{
		if (book == null)
			return null!;

		sheetName ??= "Sheet1";
		var sheet = book.GetSheet(sheetName);
		return sheet ?? book.CreateSheet(sheetName);
	}

	/// <summary>
	/// 判断Sheet是否为空
	/// </summary>
	/// <param name="sheet">Sheet</param>
	/// <returns>是否为空</returns>
	public static bool IsEmpty(ISheet sheet)
	{
		return sheet == null || (sheet.LastRowNum == 0 && sheet.PhysicalNumberOfRows == 0);
	}

	/// <summary>
	/// 写出工作簿
	/// </summary>
	/// <param name="book">工作簿</param>
	/// <param name="outStream">输出流</param>
	public static void WriteBook(IWorkbook book, Stream outStream)
	{
		book.Write(outStream);
	}
}
