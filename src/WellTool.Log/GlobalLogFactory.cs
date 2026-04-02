namespace WellTool.Log
{
	/// <summary>
	/// 全局日志工厂
	/// </summary>
	public static class GlobalLogFactory
	{
		/// <summary>
		/// 当前日志工厂
		/// </summary>
		private static volatile LogFactory currentFactory;

		/// <summary>
		/// 锁对象
		/// </summary>
		private static readonly object lockObj = new object();

		/// <summary>
		/// 获取当前日志工厂
		/// </summary>
		/// <returns>当前日志工厂</returns>
		public static LogFactory Get()
		{
			if (currentFactory == null)
			{
				lock (lockObj)
				{
					if (currentFactory == null)
					{
						try
						{
							currentFactory = LogFactory.Create();
						}
						catch
						{
							// 如果创建失败，使用控制台日志工厂
							currentFactory = new WellTool.Log.Dialect.Console.ConsoleLogFactory();
						}
						if (currentFactory == null)
						{
							// 确保返回非null的日志工厂
							currentFactory = new WellTool.Log.Dialect.Console.ConsoleLogFactory();
						}
					}
				}
			}
			if (currentFactory == null)
			{
				// 确保返回非null的日志工厂
				currentFactory = new WellTool.Log.Dialect.Console.ConsoleLogFactory();
			}
			return currentFactory;
		}

		/// <summary>
		/// 设置当前日志工厂
		/// </summary>
		/// <param name="logFactoryClass">日志工厂类</param>
		/// <returns>设置的日志工厂</returns>
		public static LogFactory Set(Type logFactoryClass)
		{
			try
			{
				return Set((LogFactory)Activator.CreateInstance(logFactoryClass));
			}
			catch (Exception e)
			{
				throw new ArgumentException("Can not instance LogFactory class!", e);
			}
		}

		/// <summary>
		/// 设置当前日志工厂
		/// </summary>
		/// <param name="logFactory">日志工厂</param>
		/// <returns>设置的日志工厂</returns>
		public static LogFactory Set(LogFactory logFactory)
		{
			try
			{
				var log = logFactory.GetLog(typeof(GlobalLogFactory));
				if (log != null)
				{
					log.Debug("Custom Use [{}] Logger.", logFactory.GetName());
				}
			}
			catch
			{
				// 忽略异常
			}
			currentFactory = logFactory;
			return currentFactory;
		}

		/// <summary>
		/// 重置当前日志工厂为默认状态
		/// </summary>
		public static void Reset()
		{
			currentFactory = null;
		}
	}
}