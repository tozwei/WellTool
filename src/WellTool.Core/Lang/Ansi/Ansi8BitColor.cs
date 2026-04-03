namespace WellTool.Core.Lang.Ansi;

/// <summary>
/// 8位ANSI颜色
/// </summary>
public class Ansi8BitColor
{
	/// <summary>
	/// 标准颜色（0-15）
	/// </summary>
	public static readonly Ansi8BitColor[] StandardColors = new Ansi8BitColor[16];

	/// <summary>
	/// 216色（16-231）
	/// </summary>
	public static readonly Ansi8BitColor[] HighContrastColors = new Ansi8BitColor[216];

	/// <summary>
	/// 灰度色（232-255）
	/// </summary>
	public static readonly Ansi8BitColor[] GrayscaleColors = new Ansi8BitColor[24];

	static Ansi8BitColor()
	{
		// 初始化标准颜色
		StandardColors[0] = new Ansi8BitColor(0, 0, 0);      // 黑色
		StandardColors[1] = new Ansi8BitColor(128, 0, 0);    // 红色
		StandardColors[2] = new Ansi8BitColor(0, 128, 0);   // 绿色
		StandardColors[3] = new Ansi8BitColor(128, 128, 0); // 黄色
		StandardColors[4] = new Ansi8BitColor(0, 0, 128);    // 蓝色
		StandardColors[5] = new Ansi8BitColor(128, 0, 128); // 品红
		StandardColors[6] = new Ansi8BitColor(0, 128, 128); // 青色
		StandardColors[7] = new Ansi8BitColor(192, 192, 192);// 白色

		StandardColors[8] = new Ansi8BitColor(128, 128, 128);// 亮黑色
		StandardColors[9] = new Ansi8BitColor(255, 0, 0);   // 亮红色
		StandardColors[10] = new Ansi8BitColor(0, 255, 0);  // 亮绿色
		StandardColors[11] = new Ansi8BitColor(255, 255, 0); // 亮黄色
		StandardColors[12] = new Ansi8BitColor(0, 0, 255);  // 亮蓝色
		StandardColors[13] = new Ansi8BitColor(255, 0, 255); // 亮品红
		StandardColors[14] = new Ansi8BitColor(0, 255, 255); // 亮青色
		StandardColors[15] = new Ansi8BitColor(255, 255, 255);// 亮白色

		// 初始化216色
		for (var i = 0; i < 6; i++)
		{
			for (var j = 0; j < 6; j++)
			{
				for (var k = 0; k < 6; k++)
				{
					var index = i * 36 + j * 6 + k;
					HighContrastColors[index] = new Ansi8BitColor(
						(byte)(i * 51), (byte)(j * 51), (byte)(k * 51));
				}
			}
		}

		// 初始化灰度色
		for (var i = 0; i < 24; i++)
		{
			var gray = (byte)(i * 10 + 8);
			GrayscaleColors[i] = new Ansi8BitColor(gray, gray, gray);
		}
	}

	/// <summary>
	/// 颜色代码（0-255）
	/// </summary>
	public byte Code { get; }

	/// <summary>
	/// 红色分量
	/// </summary>
	public byte R { get; }

	/// <summary>
	/// 绿色分量
	/// </summary>
	public byte G { get; }

	/// <summary>
	/// 蓝色分量
	/// </summary>
	public byte B { get; }

	/// <summary>
	/// 构造
	/// </summary>
	public Ansi8BitColor(byte r, byte g, byte b)
	{
		R = r;
		G = g;
		B = b;
		Code = FindClosestColor(r, g, b);
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="code">颜色代码（0-255）</param>
	public Ansi8BitColor(byte code)
	{
		Code = code;
		if (code < 16)
		{
			var color = StandardColors[code];
			R = color.R; G = color.G; B = color.B;
		}
		else if (code < 232)
		{
			var idx = code - 16;
			R = (byte)((idx / 36) * 51);
			G = (byte)(((idx / 6) % 6) * 51);
			B = (byte)((idx % 6) * 51);
		}
		else
		{
			var gray = (byte)((code - 232) * 10 + 8);
			R = G = B = gray;
		}
	}

	/// <summary>
	/// 获取前景色ANSI字符串
	/// </summary>
	public string ToForegroundAnsi() => $"\x1b[38;5;{Code}m";

	/// <summary>
	/// 获取背景色ANSI字符串
	/// </summary>
	public string ToBackgroundAnsi() => $"\x1b[48;5;{Code}m";

	private static byte FindClosestColor(byte r, byte g, byte b)
	{
		var closestCode = (byte)0;
		var minDistance = int.MaxValue;

		for (var i = 0; i < 256; i++)
		{
			var color = new Ansi8BitColor((byte)i);
			var distance = Square(color.R - r) + Square(color.G - g) + Square(color.B - b);
			if (distance < minDistance)
			{
				minDistance = distance;
				closestCode = (byte)i;
			}
		}

		return closestCode;
	}

	private static int Square(int value) => value * value;
}
