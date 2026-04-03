namespace WellTool.Core.Lang.Generator;

/// <summary>
/// 对象生成器接口
/// </summary>
/// <typeparam name="T">生成对象类型</typeparam>
public interface IObjectGenerator<T>
{
	/// <summary>
	/// 生成对象
	/// </summary>
	/// <returns>生成的对象</returns>
	T Generate();
}

/// <summary>
/// 基于Func的对象生成器
/// </summary>
/// <typeparam name="T">生成对象类型</typeparam>
public class ObjectGenerator<T> : IObjectGenerator<T>
{
	private readonly Func<T> _generator;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="generator">生成函数</param>
	public ObjectGenerator(Func<T> generator)
	{
		_generator = generator ?? throw new ArgumentNullException(nameof(generator));
	}

	/// <inheritdoc />
	public T Generate() => _generator();
}

/// <summary>
/// 对象生成器工具类
/// </summary>
public static class ObjectGenerators
{
	/// <summary>
	/// 创建对象生成器
	/// </summary>
	/// <typeparam name="T">生成对象类型</typeparam>
	/// <param name="generator">生成函数</param>
	/// <returns>生成器</returns>
	public static IObjectGenerator<T> Create<T>(Func<T> generator) => new ObjectGenerator<T>(generator);

	/// <summary>
	/// 创建基于类型的生成器
	/// </summary>
	/// <typeparam name="T">生成对象类型</typeparam>
	/// <returns>生成器</returns>
	public static IObjectGenerator<T> CreateDefault<T>() where T : new() => new ObjectGenerator<T>(() => new T());

	/// <summary>
	/// 创建基于Activator的生成器
	/// </summary>
	/// <typeparam name="T">生成对象类型</typeparam>
	/// <returns>生成器</returns>
	public static IObjectGenerator<T> CreateActivator<T>() => new ObjectGenerator<T>(() => Activator.CreateInstance<T>());
}
