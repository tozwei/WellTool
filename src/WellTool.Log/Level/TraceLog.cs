namespace WellTool.Log.Level
{
	/// <summary>
	/// TRACE级别日志接口
	/// </summary>
	public interface ITraceLog
	{
		/// <summary>
		/// TRACE 等级是否开启
		/// </summary>
		/// <returns>是否开启</returns>
		bool IsTraceEnabled();

		/// <summary>
		/// 打印 TRACE 等级的日志
		/// </summary>
		/// <param name="t">错误对象</param>
		void Trace(Exception t);

		/// <summary>
		/// 打印 TRACE 等级的日志
		/// </summary>
		/// <param name="format">消息模板</param>
		/// <param name="arguments">参数</param>
		void Trace(string format, params object[] arguments);

		/// <summary>
		/// 打印 TRACE 等级的日志
		/// </summary>
		/// <param name="t">错误对象</param>
		/// <param name="format">消息模板</param>
		/// <param name="arguments">参数</param>
		void Trace(Exception t, string format, params object[] arguments);
	
		/// <summary>
		/// 打印 TRACE 等级的日志
		/// </summary>
		/// <param name="fqcn">完全限定类名(Fully Qualified Class Name)，用于定位日志位置</param>
		/// <param name="t">错误对象</param>
		/// <param name="format">消息模板</param>
		/// <param name="arguments">参数</param>
		void Trace(string fqcn, Exception t, string format, params object[] arguments);
	}
}