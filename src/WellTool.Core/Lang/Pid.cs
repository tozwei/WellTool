using System;
using System.Diagnostics;

namespace WellTool.Core.Lang;

/// <summary>
/// 进程ID单例封装
/// </summary>
public static class Pid
{
	private static readonly int INSTANCE;

	static Pid()
	{
		INSTANCE = GetPid();
	}

	/// <summary>
	/// 获取PID值
	/// </summary>
	public static int Get()
	{
		return INSTANCE;
	}

	/// <summary>
	/// 获取当前进程ID
	/// </summary>
	private static int GetPid()
	{
		return Process.GetCurrentProcess().Id;
	}
}
