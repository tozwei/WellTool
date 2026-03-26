namespace WellTool.Socket;

/// <summary>
/// Socket通讯配置
/// </summary>
[Serializable]
public class SocketConfig
{
	/// <summary>
	/// CPU核心数
	/// </summary>
	private static readonly int CpuCount = Environment.ProcessorCount;

	/// <summary>
	/// 共享线程池大小，此线程池用于接收和处理用户连接
	/// </summary>
	public int ThreadPoolSize { get; set; } = CpuCount;

	/// <summary>
	/// 读取超时时长，小于等于0表示默认（毫秒）
	/// </summary>
	public long ReadTimeout { get; set; }

	/// <summary>
	/// 写出超时时长，小于等于0表示默认（毫秒）
	/// </summary>
	public long WriteTimeout { get; set; }

	/// <summary>
	/// 读取缓存大小，默认8192
	/// </summary>
	public int ReadBufferSize { get; set; } = 8192;

	/// <summary>
	/// 写出缓存大小，默认8192
	/// </summary>
	public int WriteBufferSize { get; set; } = 8192;
}