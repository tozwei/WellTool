using System;

namespace WellTool.Core.Lang;

/// <summary>
/// 控制台工具类
/// </summary>
public static class Console
{
	/// <summary>
	/// 打印一行
	/// </summary>
	/// <param name="message">消息</param>
	public static void PrintLine(object message)
	{
		System.Console.WriteLine(message);
	}

	/// <summary>
	/// 打印
	/// </summary>
	/// <param name="message">消息</param>
	public static void Print(object message)
	{
		System.Console.Write(message);
	}

	/// <summary>
	/// 打印错误
	/// </summary>
	/// <param name="message">消息</param>
	public static void PrintError(object message)
	{
		System.Console.Error.WriteLine(message);
	}

	/// <summary>
	/// 读取一行
	/// </summary>
	/// <returns>输入行</returns>
	public static string ReadLine()
	{
		return System.Console.ReadLine();
	}

	/// <summary>
	/// 读取密码
	/// </summary>
	/// <returns>密码</returns>
	public static string ReadPassword()
	{
		var password = new System.Text.StringBuilder();
		ConsoleKeyInfo key;
		while ((key = System.Console.ReadKey(true)).Key != ConsoleKey.Enter)
		{
			if (key.Key == ConsoleKey.Backspace && password.Length > 0)
			{
				password.Length--;
				System.Console.Write("\b \b");
			}
			else if (key.KeyChar != '\0')
			{
				password.Append(key.KeyChar);
				System.Console.Write("*");
			}
		}
		System.Console.WriteLine();
		return password.ToString();
	}

	/// <summary>
	/// 读取按键
	/// </summary>
	/// <returns>按键</returns>
	public static ConsoleKeyInfo ReadKey()
	{
		return System.Console.ReadKey();
	}

	/// <summary>
	/// 清除屏幕
	/// </summary>
	public static void Clear()
	{
		System.Console.Clear();
	}

	/// <summary>
	/// 设置前景色
	/// </summary>
	/// <param name="color">颜色</param>
	public static void SetForegroundColor(ConsoleColor color)
	{
		System.Console.ForegroundColor = color;
	}

	/// <summary>
	/// 设置背景色
	/// </summary>
	/// <param name="color">颜色</param>
	public static void SetBackgroundColor(ConsoleColor color)
	{
		System.Console.BackgroundColor = color;
	}

	/// <summary>
	/// 重置颜色
	/// </summary>
	public static void ResetColor()
	{
		System.Console.ResetColor();
	}
}
