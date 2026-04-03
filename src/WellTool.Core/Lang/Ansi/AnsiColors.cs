namespace WellTool.Core.Lang.Ansi;

/// <summary>
/// ANSI颜色工具类
/// </summary>
public static class AnsiColors
{
	/// <summary>
	/// 获取前景色
	/// </summary>
	/// <param name="color">颜色</param>
	/// <returns>ANSI字符串</returns>
	public static string Foreground(AnsiColor color)
	{
		return AnsiColorImpl.Foreground(color).ToAnsiString();
	}

	/// <summary>
	/// 获取背景色
	/// </summary>
	/// <param name="color">颜色</param>
	/// <returns>ANSI字符串</returns>
	public static string Background(AnsiBackground color)
	{
		return AnsiColorImpl.Background(color).ToAnsiString();
	}

	/// <summary>
	/// 获取RGB前景色
	/// </summary>
	/// <param name="r">红色</param>
	/// <param name="g">绿色</param>
	/// <param name="b">蓝色</param>
	/// <returns>ANSI字符串</returns>
	public static string ForegroundRgb(byte r, byte g, byte b)
	{
		return $"\x1b[38;2;{r};{g};{b}m";
	}

	/// <summary>
	/// 获取RGB背景色
	/// </summary>
	/// <param name="r">红色</param>
	/// <param name="g">绿色</param>
	/// <param name="b">蓝色</param>
	/// <returns>ANSI字符串</returns>
	public static string BackgroundRgb(byte r, byte g, byte b)
	{
		return $"\x1b[48;2;{r};{g};{b}m";
	}

	/// <summary>
	/// 获取256色前景色
	/// </summary>
	/// <param name="code">颜色代码（0-255）</param>
	/// <returns>ANSI字符串</returns>
	public static string Foreground256(byte code)
	{
		return $"\x1b[38;5;{code}m";
	}

	/// <summary>
	/// 获取256色背景色
	/// </summary>
	/// <param name="code">颜色代码（0-255）</param>
	/// <returns>ANSI字符串</returns>
	public static string Background256(byte code)
	{
		return $"\x1b[48;5;{code}m";
	}

	/// <summary>
	/// 重置样式
	/// </summary>
	public const string Reset = "\x1b[0m";

	/// <summary>
	/// 加粗
	/// </summary>
	public const string Bold = "\x1b[1m";

	/// <summary>
	/// 弱化
	/// </summary>
	public const string Faint = "\x1b[2m";

	/// <summary>
	/// 斜体
	/// </summary>
	public const string Italic = "\x1b[3m";

	/// <summary>
	/// 下划线
	/// </summary>
	public const string Underline = "\x1b[4m";

	/// <summary>
	/// 反色
	/// </summary>
	public const string Reverse = "\x1b[7m";

	/// <summary>
	/// 删除线
	/// </summary>
	public const string Strikethrough = "\x1b[9m";

	/// <summary>
	/// 构建带样式的字符串
	/// </summary>
	/// <param name="text">文本</param>
	/// <param name="foreground">前景色</param>
	/// <param name="background">背景色（可选）</param>
	/// <param name="style">样式（可选）</param>
	/// <returns>带样式的字符串</returns>
	public static string Colorize(string text, AnsiColor? foreground = null, AnsiBackground? background = null, AnsiStyle? style = null)
	{
		var sb = new System.Text.StringBuilder();

		if (style != null)
			sb.Append($"{(int)style}m").Insert(1, "\x1b[");

		if (foreground != null)
			sb.Append(Foreground(foreground));

		if (background != null)
			sb.Append(Background(background));

		sb.Append(text);
		sb.Append(Reset);

		return sb.ToString();
	}
}
