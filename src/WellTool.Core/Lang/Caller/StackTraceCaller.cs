//using System;
//using System.Diagnostics;

//namespace WellTool.Core.Lang.Caller;

///// <summary>
///// 基于堆栈跟踪的调用者
///// </summary>
//public class StackTraceCaller : Caller
//{
//	public override Type GetCaller()
//	{
//		var trace = new StackTrace(true);
//		var frames = trace.GetFrames();
//		if (frames != null && frames.Length > 2)
//		{
//			// 跳过前两个帧
//			for (int i = 2; i < frames.Length; i++)
//			{
//				var method = frames[i].GetMethod();
//				if (method?.DeclaringType != typeof(StackTraceCaller))
//				{
//					return method?.DeclaringType;
//				}
//			}
//		}
//		return null;
//	}
//}
