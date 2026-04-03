namespace WellTool.Core.Lang.Ansi;

/// <summary>
/// ANSI元素接口
/// </summary>
public interface IAnsiElement
{
	/// <summary>
	/// 获取ANSI转义序列
	/// </summary>
	string ToAnsiString();
}

/// <summary>
/// ANSI样式枚举
/// </summary>
public enum AnsiStyle
{
	/// <summary>粗体</summary>
	Bold = 1,
	/// <summary>弱化</summary>
	Faint = 2,
	/// <summary>斜体</summary>
	Italic = 3,
	/// <summary>下划线</summary>
	Underline = 4,
	/// <summary>慢闪烁</summary>
	SlowBlink = 5,
	/// <summary>快速闪烁</summary>
	RapidBlink = 6,
	/// <summary>反色</summary>
	Reverse = 7,
	/// <summary>隐藏</summary>
	Hidden = 8,
	/// <summary>删除线</summary>
	Strikethrough = 9
}

/// <summary>
/// ANSI颜色实现
/// </summary>
public class AnsiColorImpl : IAnsiElement
{
	/// <summary>
	/// 前景色模式
	/// </summary>
	public const int ForegroundOffset = 30;

	/// <summary>
	/// 背景色模式
	/// </summary>
	public const int BackgroundOffset = 40;

	/// <summary>
	/// 256色前景色模式
	/// </summary>
	public const int Foreground256Offset = 38;

	/// <summary>
	/// 256色背景色模式
	/// </summary>
	public const int Background256Offset = 48;

	/// <summary>
	/// RGB颜色前景色模式
	/// </summary>
	public const int ForegroundRgbOffset = 38;

	/// <summary>
	/// RGB颜色背景色模式
	/// </summary>
	public const int BackgroundRgbOffset = 48;

	private readonly int _code;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="code">颜色代码</param>
	public AnsiColorImpl(int code)
	{
		_code = code;
	}

	/// <inheritdoc />
	public string ToAnsiString() => $"\x1b[{_code}m";

	/// <summary>
	/// 创建前景色
	/// </summary>
	/// <param name="color">颜色类</param>
	/// <returns>ANSI元素</returns>
	public static IAnsiElement Foreground(AnsiColor color)
	{
		return new AnsiColorImpl(color.GetCode());
	}

	/// <summary>
	/// 创建背景色
	/// </summary>
	/// <param name="color">颜色类</param>
	/// <returns>ANSI元素</returns>
	public static IAnsiElement Background(AnsiBackground color)
	{
		return new AnsiColorImpl(color.GetCode());
	}

	/// <summary>
	/// 创建256色前景色
	/// </summary>
	/// <param name="code">256色代码</param>
	/// <returns>ANSI元素</returns>
	public static IAnsiElement Foreground256(int code)
	{
		return new AnsiColorImpl($"{Foreground256Offset};5;{code}");
	}

	/// <summary>
	/// 创建RGB前景色
	/// </summary>
	/// <param name="r">红色</param>
	/// <param name="g">绿色</param>
	/// <param name="b">蓝色</param>
	/// <returns>ANSI元素</returns>
	public static IAnsiElement ForegroundRgb(byte r, byte g, byte b)
	{
		return new AnsiColorImpl($"{ForegroundRgbOffset};2;{r};{g};{b}");
	}

	/// <summary>
	/// 创建RGB背景色
	/// </summary>
	/// <param name="r">红色</param>
	/// <param name="g">绿色</param>
	/// <param name="b">蓝色</param>
	/// <returns>ANSI元素</returns>
	public static IAnsiElement BackgroundRgb(byte r, byte g, byte b)
	{
		return new AnsiColorImpl($"{BackgroundRgbOffset};2;{r};{g};{b}");
	}

	/// <summary>
	/// 构造（用于RGB字符串格式）
	/// </summary>
	/// <param name="codeStr">代码字符串</param>
	public AnsiColorImpl(string codeStr)
	{
		_code = 0; // 标记为特殊格式
		_codeStr = codeStr;
	}

	private readonly string? _codeStr;
}

/// <summary>
/// ANSI样式实现
/// </summary>
public class AnsiStyleImpl : IAnsiElement
{
	/// <summary>
	/// 重置
	/// </summary>
	public const int Reset = 0;

	private readonly int _styleCode;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="styleCode">样式代码</param>
	public AnsiStyleImpl(AnsiStyle style)
	{
		_styleCode = (int)style;
	}

	/// <inheritdoc />
	public string ToAnsiString() => $"\x1b[{_styleCode}m";
}
