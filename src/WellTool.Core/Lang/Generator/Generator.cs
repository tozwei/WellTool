namespace WellTool.Core.Lang.Generator;

/// <summary>
/// ID生成器接口
/// </summary>
public interface IGenerator
{
	/// <summary>
	/// 生成下一个ID
	/// </summary>
	/// <returns>ID字符串</returns>
	string Next();
}

/// <summary>
/// ObjectId生成器
/// </summary>
public class ObjectIdGenerator : IGenerator
{
	/// <inheritdoc />
	public string Next() => ObjectId.Next();
}

/// <summary>
/// UUID生成器
/// </summary>
public class UUIDGenerator : IGenerator
{
	/// <inheritdoc />
	public string Next() => Guid.NewGuid().ToString("N");
}

/// <summary>
/// Snowflake生成器
/// </summary>
public class SnowflakeGenerator : IGenerator
{
	private readonly Snowflake _snowflake;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="datacenterId">数据中心ID</param>
	/// <param name="machineId">机器ID</param>
	public SnowflakeGenerator(long datacenterId, long machineId)
	{
		_snowflake = new Snowflake(datacenterId, machineId);
	}

	/// <inheritdoc />
	public string Next() => _snowflake.NextId().ToString();
}
