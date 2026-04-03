using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace WellTool.Core.Lang.Caller;

/// <summary>
/// 基于SecurityManager的调用者获取实现
/// 注意：在.NET中SecurityManager已被弃用，此实现仅作参考
/// </summary>
public static class SecurityManagerCaller
{
	/// <summary>
	/// 获取调用者类
	/// </summary>
	/// <param name="depth">深度，0表示直接调用者</param>
	/// <returns>调用者类型</returns>
	public static Type? GetCallerClass(int depth = 0)
	{
		// .NET中没有SecurityManager的等价物，使用StackTrace替代
		var frames = new StackTrace(true).GetFrames();
		if (frames == null || frames.Length < depth + 2)
			return null;

		// 跳过当前方法和直接调用者
		return frames[depth + 2]?.GetMethod()?.DeclaringType;
	}

	/// <summary>
	/// 获取调用者的类名
	/// </summary>
	/// <param name="depth">深度</param>
	/// <returns>类名</returns>
	public static string? GetCallerClassName(int depth = 0)
	{
		return GetCallerClass(depth)?.Name;
	}

	/// <summary>
	/// 获取调用者的完整类型名
	/// </summary>
	/// <param name="depth">深度</param>
	/// <returns>完整类型名</returns>
	public static string? GetCallerClassFullName(int depth = 0)
	{
		return GetCallerClass(depth)?.FullName;
	}
}
