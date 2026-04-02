namespace WellTool.Log.Level
{
	/// <summary>
	/// WARN级别日志接口
	/// </summary>
	public interface IWarnLog
	{
		/// <summary>
		/// WARN 等级是否开启
		/// </summary>
		/// <returns>是否开启</returns>
		bool IsWarnEnabled();

		/// <summary>
		/// 打印 WARN 等级的日志
		/// </summary>
		/// <param name="t">错误对象</param>
		void Warn(Exception t);

		/// <summary>
		/// 打印 WARN 等级的日志
		/// </summary>
		/// <param name="format">消息模板</param>
		/// <param name="arguments">参数</param>
		void Warn(string format, params object[] arguments);

		/// <summary>
		/// 打印 WARN 等级的日志
		/// </summary>
		/// <param name="t">错误对象</param>
		/// <param name="format">消息模板</param>
		/// <param name="arguments">参数</param>
		void Warn(Exception t, string format, params object[] arguments);
	
		/// <summary>
		/// 打印 WARN 等级的日志
		/// </summary>
		/// <param name="fqcn">完全限定类名(Fully Qualified Class Name)，用于定位日志位置</param>
		/// <param name="t">错误对象</param>
		/// <param name="format">消息模板</param>
		/// <param name="arguments">参数</param>
		void Warn(string fqcn, Exception t, string format, params object[] arguments);
	}
}