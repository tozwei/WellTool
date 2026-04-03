using System;

namespace WellTool.Core.Lang.Copier;

/// <summary>
/// 源到目标拷贝器基类
/// </summary>
/// <typeparam name="S">源类型</typeparam>
/// <typeparam name="D">目标类型</typeparam>
[Serializable]
public abstract class SrcToDestCopier<S, D> : ISrcToDestCopier<S, D>
{
	/// <summary>
	/// 源对象
	/// </summary>
	public S Src { get; set; }

	/// <summary>
	/// 目标对象
	/// </summary>
	public D Dest { get; set; }

	/// <summary>
	/// 拷贝过滤器
	/// </summary>
	protected Func<S, bool> CopyFilter { get; set; }

	/// <summary>
	/// 执行拷贝
	/// </summary>
	/// <param name="src">源对象</param>
	/// <param name="dest">目标对象</param>
	/// <returns>目标对象</returns>
	public abstract D Copy(S src, D dest);

	/// <summary>
	/// 执行拷贝
	/// </summary>
	/// <returns>目标对象</returns>
	public virtual D Copy()
	{
		return Copy(Src, Dest);
	}

	/// <summary>
	/// 设置拷贝过滤器
	/// </summary>
	/// <param name="filter">过滤器</param>
	/// <returns>this</returns>
	public virtual SrcToDestCopier<S, D> SetCopyFilter(Func<S, bool> filter)
	{
		CopyFilter = filter;
		return this;
	}
}
