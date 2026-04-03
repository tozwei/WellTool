using System;
using System.Text;

namespace WellTool.Core.Util;

/// <summary>
/// ID工具类
/// </summary>
public static class IdUtil
{
	private static readonly Random _random = new Random();

	/// <summary>
	/// 生成UUID（无横线）
	/// </summary>
	public static string FastUUID() => Guid.NewGuid().ToString("N");

	/// <summary>
	/// 生成UUID
	/// </summary>
	public static string UUID() => Guid.NewGuid().ToString();

	/// <summary>
	/// 生成简单的UUID
	/// </summary>
	public static string SimpleUUID() => Guid.NewGuid().ToString("N");

	/// <summary>
	/// 生成雪花ID
	/// </summary>
	public static long SnowflakeId() => Snowflake.Instance.NextId();

	/// <summary>
	/// 生成雪花ID字符串
	/// </summary>
	public static string SnowflakeIdStr() => Snowflake.Instance.NextId().ToString();

	/// <summary>
	/// 生成ObjectId
	/// </summary>
	public static string ObjectId() => ObjectId.NewObjectId();

	/// <summary>
	/// 生成时间戳ID
	/// </summary>
	public static string TimeId() => $"{DateTime.Now:yyyyMMddHHmmssfff}{_random.Next(1000, 9999)}";

	/// <summary>
	/// 生成随机ID
	/// </summary>
	/// <param name="length">长度</param>
	public static string RandomId(int length = 16)
	{
		var sb = new StringBuilder(length);
		var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
		for (int i = 0; i < length; i++)
		{
			sb.Append(chars[_random.Next(chars.Length)]);
		}
		return sb.ToString();
	}
}

/// <summary>
/// 雪花ID生成器
/// </summary>
public class Snowflake
{
	/// <summary>
	/// 单例
	/// </summary>
	public static readonly Snowflake Instance = new Snowflake();

	private long _lastId = -1;
	private readonly object _lock = new object();

	private const long TWEPOCH = 1288834974657L;
	private const int WORKER_ID_BITS = 5;
	private const int DATACENTER_ID_BITS = 5;
	private const int SEQUENCE_BITS = 12;

	private const long MAX_WORKER_ID = ~(-1L << WORKER_ID_BITS);
	private const long MAX_DATACENTER_ID = ~(-1L << DATACENTER_ID_BITS);

	private const int WORKER_ID_SHIFT = SEQUENCE_BITS;
	private const int DATACENTER_ID_SHIFT = SEQUENCE_BITS + WORKER_ID_BITS;
	private const int TIMESTAMP_LEFT_SHIFT = SEQUENCE_BITS + WORKER_ID_BITS + DATACENTER_ID_BITS;
	private const long SEQUENCE_MASK = ~(-1L << SEQUENCE_BITS);

	private readonly long _workerId;
	private readonly long _datacenterId;
	private long _sequence;

	private Snowflake(long workerId = 1, long datacenterId = 1)
	{
		if (workerId > MAX_WORKER_ID || workerId < 0)
			throw new ArgumentException($"worker Id can't be greater than {MAX_WORKER_ID} or less than 0");
		if (datacenterId > MAX_DATACENTER_ID || datacenterId < 0)
			throw new ArgumentException($"datacenter Id can't be greater than {MAX_DATACENTER_ID} or less than 0");

		_workerId = workerId;
		_datacenterId = datacenterId;
	}

	/// <summary>
	/// 生成下一个ID
	/// </summary>
	public long NextId()
	{
		lock (_lock)
		{
			var timestamp = TimeGen();

			if (timestamp < _lastId)
			{
				throw new Exception($"Clock moved backwards. Refusing to generate id for {_lastId - timestamp} milliseconds");
			}

			if (_lastId == timestamp)
			{
				_sequence = (_sequence + 1) & SEQUENCE_MASK;
				if (_sequence == 0)
				{
					timestamp = TilNextMillis(_lastId);
				}
			}
			else
			{
				_sequence = 0;
			}

			_lastId = timestamp;

			return ((timestamp - TWEPOCH) << TIMESTAMP_LEFT_SHIFT) |
				   (_datacenterId << DATACENTER_ID_SHIFT) |
				   (_workerId << WORKER_ID_SHIFT) |
				   _sequence;
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

	private long TimeGen() => DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
}

/// <summary>
/// ObjectId工具类
/// </summary>
public class ObjectId
{
	private static readonly Random _random = new Random();

	/// <summary>
	/// 生成新的ObjectId
	/// </summary>
	public static string NewObjectId()
	{
		var timestamp = DateTime.UtcNow;
		var time = BitConverter.GetBytes(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
		var machine = BitConverter.GetBytes(_random.Next(0, 0xFFFFFF));
		var pid = BitConverter.GetBytes((short)_random.Next(0, 0xFFFF));
		var increment = BitConverter.GetBytes(_random.Next(0, 0xFFFFFF));

		Array.Reverse(time);
		Array.Reverse(increment);

		var result = new byte[12];
		Array.Copy(time, 0, result, 0, 4);
		Array.Copy(machine, 0, result, 4, 3);
		Array.Copy(pid, 0, result, 7, 2);
		Array.Copy(increment, 1, result, 9, 3);

		return BitConverter.ToString(result).Replace("-", "").ToLower();
	}
}
