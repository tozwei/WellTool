using System;
using System.Threading;

namespace WellTool.Core.Lang;

/// <summary>
/// Twitter的Snowflake 算法
/// 分布式系统中，有一些需要使用全局唯一ID的场景，有些时候我们希望能使用一种简单一些的ID，并且希望ID能够按照时间有序生成。
/// </summary>
[Serializable]
public class Snowflake
{
	/// <summary>
	/// 默认的起始时间，为Thu, 04 Nov 2010 01:42:54 GMT
	/// </summary>
	public static long DEFAULT_TWEPOCH = 1288834974657L;
	/// <summary>
	/// 默认回拨时间，2S
	/// </summary>
	public static long DEFAULT_TIME_OFFSET = 2000L;

	private static readonly long WORKER_ID_BITS = 5L;
	public static readonly long MAX_WORKER_ID = ~(-1L << (int)WORKER_ID_BITS);
	private static readonly long DATA_CENTER_ID_BITS = 5L;
	public static readonly long MAX_DATA_CENTER_ID = ~(-1L << (int)DATA_CENTER_ID_BITS);
	private static readonly long SEQUENCE_BITS = 12L;
	private static readonly long WORKER_ID_SHIFT = SEQUENCE_BITS;
	private static readonly long DATA_CENTER_ID_SHIFT = SEQUENCE_BITS + WORKER_ID_BITS;
	private static readonly long TIMESTAMP_LEFT_SHIFT = SEQUENCE_BITS + WORKER_ID_BITS + DATA_CENTER_ID_BITS;
	private static readonly long SEQUENCE_MASK = ~(-1L << (int)SEQUENCE_BITS);

	private readonly long _twepoch;
	private readonly long _workerId;
	private readonly long _dataCenterId;
	private readonly bool _useSystemClock;
	private readonly long _timeOffset;
	private readonly long _randomSequenceLimit;

	private long _sequence = 0L;
	private long _lastTimestamp = -1L;

	/// <summary>
	/// 构造，使用自动生成的工作节点ID和数据中心ID
	/// </summary>
	public Snowflake() : this(1)
	{
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="workerId">终端ID</param>
	public Snowflake(long workerId) : this(workerId, 1)
	{
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="workerId">终端ID</param>
	/// <param name="dataCenterId">数据中心ID</param>
	public Snowflake(long workerId, long dataCenterId) : this(workerId, dataCenterId, false)
	{
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="workerId">终端ID</param>
	/// <param name="dataCenterId">数据中心ID</param>
	/// <param name="isUseSystemClock">是否使用系统时钟获取当前时间戳</param>
	public Snowflake(long workerId, long dataCenterId, bool isUseSystemClock)
	{
		_twepoch = DEFAULT_TWEPOCH;
		_workerId = Math.Max(0, Math.Min(workerId, MAX_WORKER_ID));
		_dataCenterId = Math.Max(0, Math.Min(dataCenterId, MAX_DATA_CENTER_ID));
		_useSystemClock = isUseSystemClock;
		_timeOffset = DEFAULT_TIME_OFFSET;
		_randomSequenceLimit = 0;
	}

	/// <summary>
	/// 根据Snowflake的ID，获取机器id
	/// </summary>
	/// <param name="id">snowflake算法生成的id</param>
	/// <returns>所属机器的id</returns>
	public long GetWorkerId(long id)
	{
		return (id >> (int)WORKER_ID_SHIFT) & ~(-1L << (int)WORKER_ID_BITS);
	}

	/// <summary>
	/// 根据Snowflake的ID，获取数据中心id
	/// </summary>
	/// <param name="id">snowflake算法生成的id</param>
	/// <returns>所属数据中心</returns>
	public long GetDataCenterId(long id)
	{
		return (id >> (int)DATA_CENTER_ID_SHIFT) & ~(-1L << (int)DATA_CENTER_ID_BITS);
	}

	/// <summary>
	/// 根据Snowflake的ID，获取生成时间
	/// </summary>
	/// <param name="id">snowflake算法生成的id</param>
	/// <returns>生成的时间</returns>
	public long GetGenerateDateTime(long id)
	{
		return ((id >> (int)TIMESTAMP_LEFT_SHIFT) & ~(-1L << 41)) + _twepoch;
	}

	/// <summary>
	/// 下一个ID
	/// </summary>
	/// <returns>ID</returns>
	public long NextId()
	{
		lock (this)
		{
			long timestamp = GenTime();
			if (timestamp < _lastTimestamp)
			{
				if (_lastTimestamp - timestamp < _timeOffset)
				{
					timestamp = _lastTimestamp;
				}
				else
				{
					throw new Exception($"Clock moved backwards. Refusing to generate id for {_lastTimestamp - timestamp}ms");
				}
			}

			if (timestamp == _lastTimestamp)
			{
				_sequence = (_sequence + 1) & SEQUENCE_MASK;
				if (_sequence == 0)
				{
					timestamp = TilNextMillis(_lastTimestamp);
				}
			}
			else
			{
				_sequence = 0L;
			}

			_lastTimestamp = timestamp;

			return ((timestamp - _twepoch) << (int)TIMESTAMP_LEFT_SHIFT)
					| (_dataCenterId << (int)DATA_CENTER_ID_SHIFT)
					| (_workerId << (int)WORKER_ID_SHIFT)
					| _sequence;
		}
	}

	/// <summary>
	/// 下一个ID（字符串形式）
	/// </summary>
	/// <returns>ID字符串形式</returns>
	public string NextIdStr()
	{
		return NextId().ToString();
	}

	/// <summary>
	/// 循环等待下一个时间
	/// </summary>
	/// <param name="lastTimestamp">上次记录的时间</param>
	/// <returns>下一个时间</returns>
	private long TilNextMillis(long lastTimestamp)
	{
		long timestamp = GenTime();
		while (timestamp == lastTimestamp)
		{
			timestamp = GenTime();
		}
		if (timestamp < lastTimestamp)
		{
			throw new Exception($"Clock moved backwards. Refusing to generate id for {lastTimestamp - timestamp}ms");
		}
		return timestamp;
	}

	/// <summary>
	/// 生成时间戳
	/// </summary>
	/// <returns>时间戳</returns>
	private long GenTime()
	{
		return _useSystemClock ? DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() : DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
	}
}
