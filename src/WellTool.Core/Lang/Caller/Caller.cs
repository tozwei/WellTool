namespace WellTool.Core.Lang.Caller;

/// <summary>
/// 调用者接口
/// </summary>
public interface ICaller
{
	/// <summary>
	/// 获取调用者的类
	/// </summary>
	/// <returns>调用者的类</returns>
	System.Type GetCaller();
}

/// <summary>
/// 调用者工具类
/// </summary>
public static class CallerUtil
{
	/// <summary>
	/// 获取调用者的类
	/// </summary>
	/// <returns>调用者的类</returns>
	public static System.Type GetCaller()
{
	var trace = new System.Diagnostics.StackTrace(true);
	var frames = trace.GetFrames();
	if (frames != null)
	{
		// 跳过前两个帧：GetCaller() 方法和调用它的方法
		for (int i = 2; i < frames.Length; i++)
		{
			var method = frames[i].GetMethod();
			if (method != null)
			{
				var declaringType = method.DeclaringType;
				// 找到第一个不是 CallerUtil 类、不是系统运行时类、不是xunit类的类型
				if (declaringType != null && 
					declaringType != typeof(CallerUtil) && 
					!declaringType.FullName.StartsWith("System.") &&
					!declaringType.FullName.StartsWith("Xunit."))
				{
					return declaringType;
				}
			}
		}
	}
	return null;
}

	/// <summary>
	/// 获取调用者的类
	/// </summary>
	/// <param name="depth">深度</param>
	/// <returns>调用者的类</returns>
	public static System.Type GetCaller(int depth)
{
	var trace = new System.Diagnostics.StackTrace(true);
	var frames = trace.GetFrames();
	if (frames != null)
	{
		// 跳过前两个帧：GetCaller() 方法和调用它的方法
		int startIndex = 2;
		for (int i = startIndex; i < frames.Length; i++)
		{
			var method = frames[i].GetMethod();
			if (method != null)
			{
				var declaringType = method.DeclaringType;
				// 找到第一个不是 CallerUtil 类、不是系统运行时类、不是xunit类的类型
				if (declaringType != null && 
					declaringType != typeof(CallerUtil) && 
					!declaringType.FullName.StartsWith("System.") &&
					!declaringType.FullName.StartsWith("Xunit."))
				{
					if (depth <= 0)
					{
						return declaringType;
					}
					depth--;
				}
			}
		}
	}
	return null;
}
}

/// <summary>
/// Caller基类
/// </summary>
public abstract class Caller : ICaller
{
	/// <summary>
	/// 获取调用者的类
	/// </summary>
	/// <returns>调用者的类</returns>
	public abstract System.Type GetCaller();
}

/// <summary>
/// 基于堆栈跟踪的调用者
/// </summary>
public class StackTraceCaller : Caller
{
	public override System.Type GetCaller()
{
	return CallerUtil.GetCaller(0);
}
}
