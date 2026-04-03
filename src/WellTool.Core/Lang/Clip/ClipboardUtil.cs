namespace WellTool.Core.lang.clip;

using System;

/// <summary>
/// 剪贴板工具类
/// </summary>
public class ClipboardUtil
{
	/// <summary>
	/// 获取剪贴板文本
	/// </summary>
	/// <returns>剪贴板文本</returns>
	public static string? GetText()
	{
		try
		{
			if (System.Windows.Forms.Clipboard.ContainsText())
			{
				return System.Windows.Forms.Clipboard.GetText();
			}
		}
		catch
		{
			// Ignore
		}
		return null;
	}

	/// <summary>
	/// 设置剪贴板文本
	/// </summary>
	/// <param name="text">文本</param>
	public static void SetText(string text)
	{
		try
		{
			System.Windows.Forms.Clipboard.SetText(text);
		}
		catch
		{
			// Ignore
		}
	}

	/// <summary>
	/// 清空剪贴板
	/// </summary>
	public static void Clear()
	{
		try
		{
			System.Windows.Forms.Clipboard.Clear();
		}
		catch
		{
			// Ignore
		}
	}
}
