namespace WellTool.Socket.Nio;

/// <summary>
/// SelectionKey Operation的枚举封装
/// </summary>
public enum Operation
{
	/// <summary>
	/// 读操作
	/// </summary>
	Read = 1,
	/// <summary>
	/// 写操作
	/// </summary>
	Write = 4,
	/// <summary>
	/// 连接操作
	/// </summary>
	Connect = 8,
	/// <summary>
	/// 接受连接操作
	/// </summary>
	Accept = 16
}