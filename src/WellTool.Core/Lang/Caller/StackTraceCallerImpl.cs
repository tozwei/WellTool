using System.Diagnostics;

namespace WellTool.Core.Lang.Caller;

/// <summary>
/// 基于StackTrace的Caller实现
/// </summary>
public class StackTraceCallerImpl : Caller
{
	/// <inheritdoc />
	public override System.Type GetCaller()
	{
		return GetCaller(0);
	}

	/// <summary>
	/// 获取指定深度的调用者的类
	/// </summary>
	/// <param name="depth">深度</param>
	/// <returns>调用者的类</returns>
	public System.Type GetCaller(int depth)
	{
		var frames = new StackTrace(true).GetFrames();
		if (frames == null || frames.Length < depth + 3) // +3 because of the additional stack frames
		{
			throw new IndexOutOfRangeException("Stack depth insufficient");
		}

		var frame = frames[depth + 3];
		var method = frame?.GetMethod();
		var declaringType = method?.DeclaringType;

		if (declaringType == null)
		{
			throw new IndexOutOfRangeException("Cannot determine caller type");
		}

		return declaringType;
	}
}
