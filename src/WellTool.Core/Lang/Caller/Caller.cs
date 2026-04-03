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
		return GetCaller(2);
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
		if (frames != null && frames.Length > depth)
		{
			var method = frames[depth].GetMethod();
			return method?.DeclaringType;
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
