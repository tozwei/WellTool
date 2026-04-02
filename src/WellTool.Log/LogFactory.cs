using System.Collections.Concurrent;
using System.Reflection;
using WellTool.Log.Dialect.Console;
using WellTool.Log.Dialect.Commons;
using WellTool.Log.Dialect.Jdk;

namespace WellTool.Log
{
	/// <summary>
	/// 日志工厂类
	/// </summary>
	public abstract class LogFactory
	{
		/// <summary>
		/// 日志框架名，用于打印当前所用日志框架
		/// </summary>
		protected string name;
		/// <summary>
		/// 日志对象缓存
		/// </summary>
		private readonly ConcurrentDictionary<object, ILog> logCache;

		/// <summary>
		/// 构造
		/// </summary>
		/// <param name="name">日志框架名</param>
		public LogFactory(string name)
		{
			this.name = name;
			logCache = new ConcurrentDictionary<object, ILog>();
		}

		/// <summary>
		/// 获取日志框架名，用于打印当前所用日志框架
		/// </summary>
		/// <returns>日志框架名</returns>
		public string GetName()
		{
			return this.name;
		}

		/// <summary>
		/// 获得日志对象
		/// </summary>
		/// <param name="name">日志对象名</param>
		/// <returns>日志对象</returns>
		public ILog GetLog(string name)
		{
			var log = logCache.GetOrAdd(name, o => CreateLog((string)o));
			if (log == null)
			{
				// 如果获取失败，使用控制台日志
				log = new WellTool.Log.Dialect.Console.ConsoleLog(name);
				logCache.TryAdd(name, log);
			}
			return log;
		}

		/// <summary>
		/// 获得日志对象
		/// </summary>
		/// <param name="clazz">日志对应类</param>
		/// <returns>日志对象</returns>
		public ILog GetLog(Type clazz)
		{
			var log = logCache.GetOrAdd(clazz, o => CreateLog((Type)o));
			if (log == null)
			{
				// 如果获取失败，使用控制台日志
				log = new WellTool.Log.Dialect.Console.ConsoleLog(clazz);
				logCache.TryAdd(clazz, log);
			}
			return log;
		}

		/// <summary>
		/// 创建日志对象
		/// </summary>
		/// <param name="name">日志对象名</param>
		/// <returns>日志对象</returns>
		public abstract ILog CreateLog(string name);

		/// <summary>
		/// 创建日志对象
		/// </summary>
		/// <param name="clazz">日志对应类</param>
		/// <returns>日志对象</returns>
		public abstract ILog CreateLog(Type clazz);

		/// <summary>
		/// 检查日志实现是否存在<br>
		/// 此方法仅用于检查所提供的日志相关类是否存在，当传入的日志类类不存在时抛出ClassNotFoundException<br>
		/// 此方法的作用是在detectLogFactory方法自动检测所用日志时，如果实现类不存在，调用此方法会自动抛出异常，从而切换到下一种日志的检测。
		/// </summary>
		/// <param name="logClassName">日志实现相关类</param>
		protected virtual void CheckLogExist(Type logClassName)
		{
			// 不做任何操作
		}

		// ------------------------------------------------------------------------- Static start

		/// <summary>
		/// 当前使用的日志工厂
		/// </summary>
		private static LogFactory currentLogFactory;

		/// <summary>
		/// 当前使用的日志工厂
		/// </summary>
		/// <returns>当前使用的日志工厂</returns>
		public static LogFactory GetCurrentLogFactory()
		{
			if (currentLogFactory == null)
			{
				try
				{
					currentLogFactory = Create();
				}
				catch
				{
					// 如果创建失败，使用控制台日志工厂
					currentLogFactory = new ConsoleLogFactory();
				}
			}
			if (currentLogFactory == null)
			{
				// 确保返回非null的日志工厂
				currentLogFactory = new ConsoleLogFactory();
			}
			return currentLogFactory;
		}

		/// <summary>
		/// 自定义日志实现
		/// </summary>
		/// <param name="logFactoryClass">日志工厂类</param>
		/// <returns>自定义的日志工厂类</returns>
		public static LogFactory SetCurrentLogFactory(Type logFactoryClass)
		{
			currentLogFactory = (LogFactory)Activator.CreateInstance(logFactoryClass);
			return currentLogFactory;
		}

		/// <summary>
		/// 自定义日志实现
		/// </summary>
		/// <param name="logFactory">日志工厂类对象</param>
		/// <returns>自定义的日志工厂类</returns>
		public static LogFactory SetCurrentLogFactory(LogFactory logFactory)
		{
			currentLogFactory = logFactory;
			return currentLogFactory;
		}

		/// <summary>
		/// 获得日志对象
		/// </summary>
		/// <param name="name">日志对象名</param>
		/// <returns>日志对象</returns>
		public static ILog Get(string name)
		{
			var log = GetCurrentLogFactory().GetLog(name);
			if (log == null)
			{
				// 如果获取失败，使用控制台日志
				log = new WellTool.Log.Dialect.Console.ConsoleLog(name);
			}
			return log;
		}

		/// <summary>
		/// 获得日志对象
		/// </summary>
		/// <param name="clazz">日志对应类</param>
		/// <returns>日志对象</returns>
		public static ILog Get(Type clazz)
		{
			var log = GetCurrentLogFactory().GetLog(clazz);
			if (log == null)
			{
				// 如果获取失败，使用控制台日志
				log = new WellTool.Log.Dialect.Console.ConsoleLog(clazz);
			}
			return log;
		}

		/// <summary>
		/// 获得调用者的日志
		/// </summary>
		/// <returns>日志对象</returns>
		public static ILog Get()
		{
			var caller = GetCallerCaller();
			return Get(caller);
		}

		/// <summary>
		/// 决定日志实现
		/// <p>
		/// 依次按照顺序检查日志库的jar是否被引入，如果未引入任何日志库，则检查ClassPath下的logging.properties，存在则使用JdkLogFactory，否则使用ConsoleLogFactory
		/// </p>
		/// </summary>
		/// <returns>日志实现类</returns>
		public static LogFactory Create()
		{
			var factory = DoCreate();
			if (factory == null)
			{
				factory = new ConsoleLogFactory();
			}
			try
			{
				var log = factory.GetLog(typeof(LogFactory));
				if (log != null)
				{
					log.Debug("Use [{}] Logger As Default.", factory.name);
				}
			}
			catch
			{
				// 忽略异常，确保即使日志记录失败也能返回日志工厂
			}
			return factory;
		}

		/// <summary>
		/// 决定日志实现
		/// <p>
		/// 依次按照顺序检查日志库的dll是否被引入，如果未引入任何日志库，则检查是否存在System.Diagnostics.TraceListener，存在则使用JdkLogFactory，否则使用ConsoleLogFactory
		/// </p>
		/// </summary>
		/// <returns>日志实现类</returns>
		private static LogFactory DoCreate()
		{
			// 首先尝试通过ServiceLoader机制加载日志实现
			var factory = LoadFromServiceLoader();
			if (factory != null)
			{
				return factory;
			}
			
			// 依次检查各种日志实现
			
			// 检查Log4Net
			try
			{
				factory = CreateLog4NetLogFactory();
				if (factory != null)
				{
					return factory;
				}
			}
			catch (Exception)
			{
				// 忽略异常，继续检查下一个
			}
			
			// 检查NLog
			try
			{
				factory = CreateNLogFactory();
				if (factory != null)
				{
					return factory;
				}
			}
			catch (Exception)
			{
				// 忽略异常，继续检查下一个
			}
			
			// 检查Serilog
			try
			{
				factory = CreateSerilogFactory();
				if (factory != null)
				{
					return factory;
				}
			}
			catch (Exception)
			{
				// 忽略异常，继续检查下一个
			}
			
			// 检查Apache Commons Logging
			try
			{
				factory = CreateApacheCommonsLogFactory();
				if (factory != null)
				{
					return factory;
				}
			}
			catch (Exception)
			{
				// 忽略异常，继续检查下一个
			}
			
			// 检查System.Diagnostics (JDK Logging)
			try
			{
				factory = CreateJdkLogFactory();
				if (factory != null)
				{
					return factory;
				}
			}
			catch (Exception)
			{
				// 忽略异常，继续检查下一个
			}
			
			// 默认使用ConsoleLogFactory
			return new ConsoleLogFactory();
		}

		/// <summary>
		/// 通过ServiceLoader机制加载日志实现
		/// </summary>
		/// <returns>日志实现类</returns>
		private static LogFactory LoadFromServiceLoader()
		{
			try
			{
				// 扫描所有程序集，查找实现了LogFactory的类型
				var assemblies = AppDomain.CurrentDomain.GetAssemblies();
				foreach (var assembly in assemblies)
				{
					try
					{
						var types = assembly.GetTypes();
						foreach (var type in types)
						{
							// 查找继承自LogFactory的非抽象类
							if (type.IsSubclassOf(typeof(LogFactory)) && !type.IsAbstract)
							{
								// 尝试创建实例
								var factory = (LogFactory)Activator.CreateInstance(type);
								if (factory != null)
								{
									return factory;
								}
							}
						}
					}
					catch (Exception)
					{
						// 忽略异常，继续检查下一个程序集
					}
				}
			}
			catch (Exception)
			{
				// 忽略异常，返回null
			}
			return null;
		}

		/// <summary>
		/// 创建Log4Net日志工厂
		/// </summary>
		/// <returns>Log4Net日志工厂</returns>
		private static LogFactory CreateLog4NetLogFactory()
		{
			// 检查Log4Net是否存在
			try
			{
				// 尝试加载log4net程序集
				var log4NetAssembly = System.Reflection.Assembly.Load("log4net");
				if (log4NetAssembly != null)
				{
					// 检查是否存在ILog接口
					var logType = log4NetAssembly.GetType("log4net.ILog");
					if (logType != null)
					{
						// 创建Log4NetLogFactory实例
						return new WellTool.Log.Dialect.Log4Net.Log4NetLogFactory();
					}
				}
			}
			catch (Exception)
			{
				// 忽略异常
			}
			return null;
		}

		/// <summary>
		/// 创建NLog日志工厂
		/// </summary>
		/// <returns>NLog日志工厂</returns>
		private static LogFactory CreateNLogFactory()
		{
			// 检查NLog是否存在
			try
			{
				// 尝试加载NLog程序集
				var nlogAssembly = System.Reflection.Assembly.Load("NLog");
				if (nlogAssembly != null)
				{
					// 检查是否存在Logger类
					var loggerType = nlogAssembly.GetType("NLog.Logger");
					if (loggerType != null)
					{
						// 创建NLogLogFactory实例
						return new WellTool.Log.Dialect.NLog.NLogLogFactory();
					}
				}
			}
			catch (Exception)
			{
				// 忽略异常
			}
			return null;
		}

		/// <summary>
		/// 创建Serilog日志工厂
		/// </summary>
		/// <returns>Serilog日志工厂</returns>
		private static LogFactory CreateSerilogFactory()
		{
			// 检查Serilog是否存在
			try
			{
				// 尝试加载Serilog程序集
				var serilogAssembly = System.Reflection.Assembly.Load("Serilog");
				if (serilogAssembly != null)
				{
					// 检查是否存在ILogger接口
					var loggerType = serilogAssembly.GetType("Serilog.ILogger");
					if (loggerType != null)
					{
						// 创建SerilogLogFactory实例
						return new WellTool.Log.Dialect.Serilog.SerilogLogFactory();
					}
				}
			}
			catch (Exception)
			{
				// 忽略异常
			}
			return null;
		}

		/// <summary>
		/// 创建Apache Commons Logging日志工厂
		/// </summary>
		/// <returns>Apache Commons Logging日志工厂</returns>
		private static LogFactory CreateApacheCommonsLogFactory()
		{
			// 检查Apache Commons Logging是否存在
			try
			{
				// 尝试加载Apache.Common.Logging程序集
				var commonsLogAssembly = System.Reflection.Assembly.Load("Apache.Common.Logging");
				if (commonsLogAssembly != null)
				{
					// 检查是否存在ILog接口
					var logType = commonsLogAssembly.GetType("Apache.Common.Logging.ILog");
					if (logType != null)
					{
						// 创建ApacheCommonsLogFactory实例
						return new ApacheCommonsLogFactory();
					}
				}
			}
			catch (Exception)
			{
				// 忽略异常
			}
			return null;
		}

		/// <summary>
		/// 创建Jdk日志工厂
		/// </summary>
		/// <returns>Jdk日志工厂</returns>
		private static LogFactory CreateJdkLogFactory()
		{
			// 检查System.Diagnostics是否可用
			try
			{
				// 尝试使用System.Diagnostics.Trace
				System.Diagnostics.Trace.WriteLine("Test");
				// 创建JdkLogFactory实例
				return new JdkLogFactory();
			}
			catch (Exception)
			{
				return null;
			}
		}

		/// <summary>
		/// 获取调用者的调用者
		/// </summary>
		/// <returns>调用者的调用者类型</returns>
		private static Type GetCallerCaller()
		{
			var stackTrace = new System.Diagnostics.StackTrace();
			for (int i = 0; i < stackTrace.FrameCount; i++)
			{
				var frame = stackTrace.GetFrame(i);
				var method = frame.GetMethod();
				var type = method.DeclaringType;
				if (type != typeof(Log) && type != typeof(LogFactory))
				{
					return type;
				}
			}
			return typeof(LogFactory);
		}
		// ------------------------------------------------------------------------- Static end
	}
}