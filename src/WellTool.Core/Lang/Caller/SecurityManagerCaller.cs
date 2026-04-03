using System;
using System.Security;

namespace WellTool.Core.Lang.Caller;

/// <summary>
/// 基于SecurityManager的调用者
/// </summary>
public class SecurityManagerCaller : Caller
{
	public override Type GetCaller()
	{
		try
		{
			var trace = new System.Diagnostics.StackTrace(true);
			var frames = trace.GetFrames();
			if (frames != null && frames.Length > 2)
			{
				// 跳过前两个帧（当前方法和调用者）
				for (int i = 2; i < frames.Length; i++)
				{
					var method = frames[i].GetMethod();
					if (method?.DeclaringType != typeof(SecurityManagerCaller))
					{
						return method?.DeclaringType;
					}
				}
			}
		}
		catch (SecurityException)
		{
		}
		return null;
	}
}
