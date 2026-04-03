using System;

namespace WellTool.Core.Lang.Caller;

/// <summary>
/// 调用者信息
/// </summary>
public abstract class Caller
{
	/// <summary>
	/// 获得调用者
	/// </summary>
	/// <returns>调用者</returns>
	public Type GetCaller()
	{
		return GetCaller(1);
	}

	/// <summary>
	/// 获得调用者的调用者
	/// </summary>
	/// <returns>调用者的调用者</returns>
	public Type GetCallerCaller()
	{
		return GetCaller(2);
	}

	/// <summary>
	/// 获得调用者，指定第几级调用者<br>
	/// 调用者层级关系：
	/// 
	/// <pre>
	/// 0 CallerUtil
	/// 1 调用CallerUtil中方法的类
	/// 2 调用者的调用者
	/// ...
	/// </pre>
	/// </summary>
	/// <param name="depth">层级。0表示CallerUtil本身，1表示调用CallerUtil的类，2表示调用者的调用者，依次类推</param>
	/// <returns>第几级调用者</returns>
	public abstract Type GetCaller(int depth);

	/// <summary>
	/// 是否被指定类调用
	/// </summary>
	/// <param name="clazz">调用者类</param>
	/// <returns>是否被调用</returns>
	public bool IsCalledBy(Type clazz)
	{
		if (clazz == null)
		{
			return false;
		}

		Type caller;
		try
		{
			// 从1开始，0是CallerUtil本身
			for (int i = 1; ; i++)
			{
				caller = GetCaller(i);
				if (caller == null)
				{
					return false;
				}
				if (clazz.Equals(caller))
				{
					return true;
				}
			}
		}
		catch (Exception)
		{
			return false;
		}
	}
}