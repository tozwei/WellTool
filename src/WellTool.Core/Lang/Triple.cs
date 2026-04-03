using System;

namespace WellTool.Core.Lang;

/// <summary>
/// 三元组封装
/// </summary>
/// <typeparam name="L">左类型</typeparam>
/// <typeparam name="M">中类型</typeparam>
/// <typeparam name="R">右类型</typeparam>
public class Triple<L, M, R>
{
	/// <summary>
	/// 左值
	/// </summary>
	public L Left { get; set; }

	/// <summary>
	/// 中值
	/// </summary>
	public M Middle { get; set; }

	/// <summary>
	/// 右值
	/// </summary>
	public R Right { get; set; }

	/// <summary>
	/// 构造
	/// </summary>
	public Triple()
	{
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="left">左值</param>
	/// <param name="middle">中值</param>
	/// <param name="right">右值</param>
	public Triple(L left, M middle, R right)
	{
		Left = left;
		Middle = middle;
		Right = right;
	}

	/// <summary>
	/// 创建三元组
	/// </summary>
	public static Triple<L, M, R> Of(L left, M middle, R right) => new Triple<L, M, R>(left, middle, right);

	/// <summary>
	/// 转换为元组
	/// </summary>
	public (L, M, R) ToTuple() => (Left, Middle, Right);

	public override bool Equals(object obj)
	{
		if (obj is Triple<L, M, R> other)
		{
			return Equals(Left, other.Left) && Equals(Middle, other.Middle) && Equals(Right, other.Right);
		}
		return false;
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(Left, Middle, Right);
	}

	public override string ToString()
	{
		return $"({Left}, {Middle}, {Right})";
	}
}
