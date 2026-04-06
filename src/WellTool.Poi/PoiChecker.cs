using System;

namespace WellTool.Poi;

/// <summary>
/// POI引入检查器
/// </summary>
public static class PoiChecker
{
	/// <summary>
	/// 没有引入POI的错误消息
	/// </summary>
	public const string NoPoiErrorMsg = "You need to add dependency of 'NPOI' to your project, and version >= 2.5.1";

	/// <summary>
	/// 检查NPOI包的引入情况
	/// </summary>
	public static void CheckPoiImport()
	{
		try
		{
			var type = Type.GetType("NPOI.SS.UserModel.Workbook, NPOI");
			if (type == null)
			{
				throw new InvalidOperationException(NoPoiErrorMsg);
			}
		}
		catch (Exception e) when (e is TypeLoadException or InvalidOperationException)
		{
			throw new InvalidOperationException(NoPoiErrorMsg, e);
		}
	}

	/// <summary>
	/// 检查NPOI是否可用
	/// </summary>
	/// <returns>是否可用</returns>
	public static bool IsPoiAvailable()
	{
		try
		{
			var type = Type.GetType("NPOI.SS.UserModel.Workbook, NPOI");
			return type != null;
		}
		catch
		{
			return false;
		}
	}
}
