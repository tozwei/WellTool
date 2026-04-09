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
				var declaringType = method?.DeclaringType;
				// 找到第一个不是 CallerUtil 类的类型
				if (declaringType != null && declaringType != typeof(CallerUtil))
				{
					return declaringType;
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
			if (startIndex + depth < frames.Length)
			{
				var method = frames[startIndex + depth].GetMethod();
				return method?.DeclaringType;
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
		return CallerUtil.GetCaller(3);
	}
}
