using System;

namespace WellTool.Poi.Excel.Sax;

/// <summary>
/// 读取停止异常
/// </summary>
public class StopReadException : Exception
{
	/// <summary>
	/// 构造
	/// </summary>
	public StopReadException() : base("Stop read by user.")
	{
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="message">消息</param>
	public StopReadException(string message) : base(message)
	{
	}
}
