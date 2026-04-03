namespace WellTool.Core.lang.id;

/// <summary>
/// ID生成器接口
/// </summary>
public interface IdGenerator
{
	/// <summary>
	/// 生成ID
	/// </summary>
	/// <returns>ID</returns>
	string NextId();
}

/// <summary>
/// UUID生成器
/// </summary>
public class UUIDGenerator : IdGenerator
{
	public string NextId()
	{
		return Guid.NewGuid().ToString("N");
	}
}

/// <summary>
/// Snowflake雪花算法生成器
/// </summary>
public class SnowflakeGenerator : IdGenerator
{
	private readonly long _workerId;
	private readonly long _datacenterId;
	private long _sequence;
	private readonly long _twepoch;

	private const long WorkerIdShift = 12;
	private const long DatacenterIdShift = 17;
	private const long TimestampLeftShift = 22;
	private const long SequenceMask = 4095;

	private long _lastTimestamp = -1;

	public SnowflakeGenerator(long workerId, long datacenterId)
	{
		if (workerId > 31 || workerId < 0)
			throw new ArgumentException("worker Id can't be greater than 31 or less than 0");
		if (datacenterId > 31 || datacenterId < 0)
			throw new ArgumentException("datacenter Id can't be greater than 31 or less than 0");

		_workerId = workerId;
		_datacenterId = datacenterId;
		_twepoch = 1288834974657L;
	}

	public string NextId()
	{
		lock (this)
		{
			var timestamp = TimeGen();
			if (timestamp < _lastTimestamp)
				throw new Exception("Clock moved backwards!");

			if (_lastTimestamp == timestamp)
			{
				_sequence = (_sequence + 1) & SequenceMask;
				if (_sequence == 0)
					timestamp = TilNextMillis(_lastTimestamp);
			}
			else
			{
				_sequence = 0;
			}

			_lastTimestamp = timestamp;
			var id = ((timestamp - _twepoch) << TimestampLeftShift)
				| (_datacenterId << DatacenterIdShift)
				| (_workerId << WorkerIdShift)
				| _sequence;
			return id.ToString();
		}
	}

	private long TilNextMillis(long lastTimestamp)
	{
		var timestamp = TimeGen();
		while (timestamp <= lastTimestamp)
		{
			timestamp = TimeGen();
		}
		return timestamp;
	}

	private long TimeGen()
	{
		return DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
	}
}

/// <summary>
/// ObjectId生成器
/// </summary>
public class ObjectIdGenerator : IdGenerator
{
	private static readonly object _lock = new object();
	private static int _counter;

	public string NextId()
	{
		lock (_lock)
		{
			var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
			var counter = _counter++;
			return $"{timestamp:x}{_counter:x8}";
		}
	}
}
