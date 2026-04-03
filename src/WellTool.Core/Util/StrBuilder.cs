using System;
using System.Text;

namespace WellDone.Core.Util;

/// <summary>
/// StrBuilder工具类
/// </summary>
public class StrBuilder
{
	private readonly StringBuilder _sb;

	/// <summary>
	/// 构造
	/// </summary>
	public StrBuilder()
	{
		_sb = new StringBuilder();
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="capacity">初始容量</param>
	public StrBuilder(int capacity)
	{
		_sb = new StringBuilder(capacity);
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="str">初始字符串</param>
	public StrBuilder(string str)
	{
		_sb = new StringBuilder(str);
	}

	/// <summary>
	/// 追加
	/// </summary>
	/// <param name="str">字符串</param>
	/// <returns>this</returns>
	public StrBuilder Append(string str)
	{
		_sb.Append(str);
		return this;
	}

	/// <summary>
	/// 追加
	/// </summary>
	/// <param name="obj">对象</param>
	/// <returns>this</returns>
	public StrBuilder Append(object obj)
	{
		_sb.Append(obj);
		return this;
	}

	/// <summary>
	/// 追加换行
	/// </summary>
	/// <returns>this</returns>
	public StrBuilder AppendLine()
	{
		_sb.AppendLine();
		return this;
	}

	/// <summary>
	/// 追加并换行
	/// </summary>
	/// <param name="str">字符串</param>
	/// <returns>this</returns>
	public StrBuilder AppendLine(string str)
	{
		_sb.AppendLine(str);
		return this;
	}

	/// <summary>
	/// 插入
	/// </summary>
	/// <param name="index">位置</param>
	/// <param name="str">字符串</param>
	/// <returns>this</returns>
	public StrBuilder Insert(int index, string str)
	{
		_sb.Insert(index, str);
		return this;
	}

	/// <summary>
	/// 替换
	/// </summary>
	/// <param name="oldStr">旧字符串</param>
	/// <param name="newStr">新字符串</param>
	/// <returns>this</returns>
	public StrBuilder Replace(string oldStr, string newStr)
	{
		_sb.Replace(oldStr, newStr);
		return this;
	}

	/// <summary>
	/// 清空
	/// </summary>
	/// <returns>this</returns>
	public StrBuilder Clear()
	{
		_sb.Clear();
		return this;
	}

	/// <summary>
	/// 长度
	/// </summary>
	public int Length => _sb.Length;

	/// <summary>
	/// 是否为空
	/// </summary>
	public bool IsEmpty => _sb.Length == 0;

	public override string ToString() => _sb.ToString();
}
