using System.Diagnostics;

namespace WellTool.Core.Lang.Caller;

/// <summary>
/// 基于StackTrace的Caller实现
/// </summary>
public class StackTraceCallerImpl : Caller
{
	/// <inheritdoc />
	public override Type GetCaller(int depth)
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
