using System;
using System.IO;

namespace WellTool.Poi.Excel;

/// <summary>
/// Excel文件工具类
/// </summary>
public static class ExcelFileUtil
{
	/// <summary>
	/// 是否为XLS格式
	/// </summary>
	/// <param name="file">文件</param>
	/// <returns>是否为XLS</returns>
	public static bool IsXls(FileInfo file)
	{
		if (!file.Exists)
			return false;

		try
		{
			using var fs = file.OpenRead();
			var header = new byte[8];
			fs.Read(header, 0, 8);
			// OLE2 magic number: D0 CF 11 E0
			return header[0] == 0xD0 && header[1] == 0xCF && header[2] == 0x11 && header[3] == 0xE0;
		}
		catch
		{
			return false;
		}
	}

	/// <summary>
	/// 是否为XLSX格式
	/// </summary>
	/// <param name="file">文件</param>
	/// <returns>是否为XLSX</returns>
	public static bool IsXlsx(FileInfo file)
	{
		if (!file.Exists)
			return false;

		try
		{
			using var fs = file.OpenRead();
			var header = new byte[4];
			fs.Read(header, 0, 4);
			// PK (ZIP) magic number
			return header[0] == 0x50 && header[1] == 0x4B;
		}
		catch
		{
			return false;
		}
	}
}
