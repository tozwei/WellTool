namespace WellTool.Core.util.console;

using System;

/// <summary>
/// ANSI颜色包装器
/// </summary>
public class AnsiColorWrapper
{
	/// <summary>
	/// 前景色代码
	/// </summary>
	public int ForeCode { get; }

	/// <summary>
	/// 背景色代码
	/// </summary>
	public int BackCode { get; }

	/// <summary>
	/// 是否为亮色
	/// </summary>
	public bool Bright { get; }

	public AnsiColorWrapper(int foreCode, int backCode, bool bright = false)
	{
		ForeCode = foreCode;
		BackCode = backCode;
		Bright = bright;
	}

	/// <summary>
	/// 获取ANSI转义序列
	/// </summary>
	public string ToAnsiString()
	{
		var fore = Bright ? ForeCode + 60 : ForeCode;
		var back = Bright ? BackCode + 60 : BackCode;
		return $"\x1b[{fore};{back}m";
	}
}

/// <summary>
/// 前景或背景颜色选项
/// </summary>
public enum ForeOrBack
{
	Fore,
	Back
}

/// <summary>
/// ANSI颜色常量
/// </summary>
public static class AnsiColors
{
	public const int BLACK = 30;
	public const int RED = 31;
	public const int GREEN = 32;
	public const int YELLOW = 33;
	public const int BLUE = 34;
	public const int MAGENTA = 35;
	public const int CYAN = 36;
	public const int WHITE = 37;
}

/// <summary>
/// 无参无返回的函数接口
/// </summary>
public delegate void VoidFunc();

/// <summary>
/// 单参数无返回的函数接口
/// </summary>
public delegate void VoidFunc<T>(T arg);
