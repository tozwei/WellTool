namespace WellTool.Log.Level
{
	/// <summary>
	/// INFO级别日志接口
	/// </summary>
	public interface IInfoLog
	{
		/// <summary>
		/// INFO 等级是否开启
		/// </summary>
		/// <returns>是否开启</returns>
		bool IsInfoEnabled();

		/// <summary>
		/// 打印 INFO 等级的日志
		/// </summary>
		/// <param name="t">错误对象</param>
		void Info(Exception t);

		/// <summary>
		/// 打印 INFO 等级的日志
		/// </summary>
		/// <param name="format">消息模板</param>
		/// <param name="arguments">参数</param>
		void Info(string format, params object[] arguments);

		/// <summary>
		/// 打印 INFO 等级的日志
		/// </summary>
		/// <param name="t">错误对象</param>
		/// <param name="format">消息模板</param>
		/// <param name="arguments">参数</param>
		void Info(Exception t, string format, params object[] arguments);
	
		/// <summary>
		/// 打印 INFO 等级的日志
		/// </summary>
		/// <param name="fqcn">完全限定类名(Fully Qualified Class Name)，用于定位日志位置</param>
		/// <param name="t">错误对象</param>
		/// <param name="format">消息模板</param>
		/// <param name="arguments">参数</param>
		void Info(string fqcn, Exception t, string format, params object[] arguments);
	}
}