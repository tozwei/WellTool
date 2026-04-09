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
      var trace = new StackTrace(true);
		var frames = trace.GetFrames();
		if (frames == null)
		{
			return null;
		}

		// 收集非框架调用者
		var callers = new System.Collections.Generic.List<System.Type>();
		foreach (var f in frames)
		{
			var method = f?.GetMethod();
			if (method == null) continue;
			var decl = method.DeclaringType;
			if (decl == null) continue;
			var name = decl.FullName ?? string.Empty;
			if (decl == typeof(CallerUtil)) continue;
			if (name.StartsWith("System.")) continue;
			if (name.StartsWith("Microsoft.VisualStudio.TestPlatform.")) continue;
			if (name.StartsWith("Xunit.")) continue;
			callers.Add(decl);
		}

		if (callers.Count == 0) return null;
		if (depth < 0) depth = 0;
		if (depth >= callers.Count) depth = callers.Count - 1;
		return callers[depth];
	}
}
