using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace WellTool.Core.Lang.Caller;

/// <summary>
/// 基于StackTrace的调用者获取实现
/// </summary>
public static class StackTraceCaller
{
	/// <summary>
	/// 获取调用者信息
	/// </summary>
	/// <param name="depth">深度，0表示直接调用者</param>
	/// <returns>调用者信息</returns>
	public static CallerInfo GetCallerInfo(int depth = 0)
	{
		var frames = new StackTrace(true).GetFrames();
		if (frames == null || frames.Length < depth + 2)
			return new CallerInfo();

		var frame = frames[depth + 2];
		var method = frame?.GetMethod();

		return new CallerInfo
		{
			CallerType = method?.DeclaringType,
			CallerMethod = method?.Name,
			CallerFilePath = frame?.GetFileName(),
			CallerLineNumber = frame?.GetFileLineNumber() ?? 0,
			CallerColumnNumber = frame?.GetFileColumnNumber() ?? 0
		};
	}

	/// <summary>
	/// 获取调用者的类
	/// </summary>
	/// <param name="depth">深度</param>
	/// <returns>调用者类</returns>
	public static Type? GetCallerType(int depth = 0)
	{
		var frames = new StackTrace(true).GetFrames();
		if (frames == null || frames.Length < depth + 2)
			return null;

		return frames[depth + 2]?.GetMethod()?.DeclaringType;
	}

	/// <summary>
	/// 获取调用者的方法名
	/// </summary>
	/// <param name="depth">深度</param>
	/// <returns>方法名</returns>
	public static string? GetCallerMethodName(int depth = 0)
	{
		var frames = new StackTrace(true).GetFrames();
		if (frames == null || frames.Length < depth + 2)
			return null;

		return frames[depth + 2]?.GetMethod()?.Name;
	}

	/// <summary>
	/// 获取调用者的文件名
	/// </summary>
	/// <param name="depth">深度</param>
	/// <returns>文件名</returns>
	public static string? GetCallerFileName(int depth = 0)
	{
		var frames = new StackTrace(true).GetFrames();
		if (frames == null || frames.Length < depth + 2)
			return null;

		return frames[depth + 2]?.GetFileName();
	}

	/// <summary>
	/// 获取调用者的行号
	/// </summary>
	/// <param name="depth">深度</param>
	/// <returns>行号</returns>
	public static int GetCallerLineNumber(int depth = 0)
	{
		var frames = new StackTrace(true).GetFrames();
		if (frames == null || frames.Length < depth + 2)
			return 0;

		return frames[depth + 2]?.GetFileLineNumber() ?? 0;
	}
}

/// <summary>
/// 调用者信息
/// </summary>
public class CallerInfo
{
	/// <summary>
	/// 调用者的类型
	/// </summary>
	public Type? CallerType { get; set; }

	/// <summary>
	/// 调用者的方法名
	/// </summary>
	public string? CallerMethod { get; set; }

	/// <summary>
	/// 调用者的文件名
	/// </summary>
	public string? CallerFilePath { get; set; }

	/// <summary>
	/// 调用者的行号
	/// </summary>
	public int CallerLineNumber { get; set; }

	/// <summary>
	/// 调用者的列号
	/// </summary>
	public int CallerColumnNumber { get; set; }

	/// <summary>
	/// 获取调用者的类名
	/// </summary>
	public string? CallerClassName => CallerType?.Name;

	/// <summary>
	/// 获取调用者的完整类名
	/// </summary>
	public string? CallerClassFullName => CallerType?.FullName;

	/// <inheritdoc />
	public override string ToString()
	{
		if (CallerType == null)
			return "Unknown";

		var location = CallerFilePath != null
			? $"{CallerFilePath}({CallerLineNumber},{CallerColumnNumber})"
			: "Unknown Location";

		return $"{CallerClassFullName}.{CallerMethod} at {location}";
	}
}
