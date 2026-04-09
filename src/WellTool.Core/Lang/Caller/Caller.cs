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
		// 遍历所有帧，找到第一个不是当前类且不是系统类的类型
		for (int i = 0; i < frames.Length; i++)
		{
			var method = frames[i].GetMethod();
			if (method != null)
			{
				var declaringType = method.DeclaringType;
				if (declaringType != null && 
				    declaringType != typeof(CallerUtil) &&
				    !declaringType.FullName.StartsWith("System.") &&
				    !declaringType.FullName.StartsWith("Microsoft.VisualStudio.TestPlatform.") &&
				    !declaringType.FullName.StartsWith("Xunit."))
				{
					return declaringType;
				}
			}
		}
	}
	// 如果没有找到合适的调用者，返回 null
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
            // 收集所有非框架的调用者类型
			var callers = new System.Collections.Generic.List<System.Type>();
			for (int i = 0; i < frames.Length; i++)
			{
				var method = frames[i].GetMethod();
				if (method == null) continue;
				var declaringType = method.DeclaringType;
				if (declaringType == null) continue;
				var fullName = declaringType.FullName ?? string.Empty;
				if (declaringType == typeof(CallerUtil)) continue;
				if (fullName.StartsWith("System.")) continue;
				if (fullName.StartsWith("Microsoft.VisualStudio.TestPlatform.")) continue;
				if (fullName.StartsWith("Xunit.")) continue;
				callers.Add(declaringType);
			}

			if (callers.Count == 0)
			{
				return null;
			}

			// 如果指定的深度超出范围，返回最近的（最接近调用点的）可用调用者
			if (depth < 0) depth = 0;
			if (depth >= callers.Count) depth = callers.Count - 1;
			return callers[depth];
	}
	// 如果没有找到合适的调用者，返回 null
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
