using LevelEnum = WellTool.Log.Level.Level;
using ITraceLog = WellTool.Log.Level.ITraceLog;
using IDebugLog = WellTool.Log.Level.IDebugLog;
using IInfoLog = WellTool.Log.Level.IInfoLog;
using IWarnLog = WellTool.Log.Level.IWarnLog;
using IErrorLog = WellTool.Log.Level.IErrorLog;

namespace WellTool.Log
{
	/// <summary>
	/// 日志统一接口
	/// </summary>
	public interface ILog : ITraceLog, IDebugLog, IInfoLog, IWarnLog, IErrorLog
	{
		/// <summary>
		/// 日志对象的Name
		/// </summary>
		/// <returns>日志对象的Name</returns>
		string GetName();

		/// <summary>
		/// 是否开启指定日志
		/// </summary>
		/// <param name="level">日志级别</param>
		/// <returns>是否开启指定级别</returns>
		bool IsEnabled(LevelEnum level);

		/// <summary>
		/// 打印指定级别的日志
		/// </summary>
		/// <param name="level">级别</param>
		/// <param name="format">消息模板</param>
		/// <param name="arguments">参数</param>
		void Log(LevelEnum level, string format, params object[] arguments);

		/// <summary>
		/// 打印 指定级别的日志
		/// </summary>
		/// <param name="level">级别</param>
		/// <param name="t">错误对象</param>
		/// <param name="format">消息模板</param>
		/// <param name="arguments">参数</param>
		void Log(LevelEnum level, Exception t, string format, params object[] arguments);

		/// <summary>
		/// 打印 ERROR 等级的日志
		/// </summary>
		/// <param name="fqcn">完全限定类名(Fully Qualified Class Name)，用于定位日志位置</param>
		/// <param name="level">级别</param>
		/// <param name="t">错误对象</param>
		/// <param name="format">消息模板</param>
		/// <param name="arguments">参数</param>
		void Log(string fqcn, LevelEnum level, Exception t, string format, params object[] arguments);
	}

	/// <summary>
	/// 日志统一接口的静态工具类
	/// </summary>
	public static class Log
	{
		/// <summary>
		/// 获得Log
		/// </summary>
		/// <param name="clazz">日志发出的类</param>
		/// <returns>Log</returns>
		public static ILog Get(Type clazz)
		{
			return LogFactory.Get(clazz);
		}

		/// <summary>
		/// 获得Log
		/// </summary>
		/// <param name="name">自定义的日志发出者名称</param>
		/// <returns>Log</returns>
		public static ILog Get(string name)
		{
			return LogFactory.Get(name);
		}

		/// <summary>
		/// 获得日志，自动判定日志发出者
		/// </summary>
		/// <returns>Log</returns>
		public static ILog Get()
		{
			return LogFactory.Get();
		}
	}
}