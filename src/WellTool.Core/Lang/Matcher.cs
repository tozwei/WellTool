namespace WellTool.Core.Lang;

/// <summary>
/// 匹配器接口
/// </summary>
/// <typeparam name="T">匹配对象类型</typeparam>
public interface IMatcher<T>
{
	/// <summary>
	/// 匹配方法
	/// </summary>
	/// <param name="t">待匹配对象</param>
	/// <returns>是否匹配</returns>
	bool Match(T t);
}

/// <summary>
/// 匹配器抽象类
/// </summary>
/// <typeparam name="T">匹配对象类型</typeparam>
public abstract class Matcher<T> : IMatcher<T>
{
	/// <summary>
	/// 匹配方法
	/// </summary>
	/// <param name="t">待匹配对象</param>
	/// <returns>是否匹配</returns>
	public abstract bool Match(T t);
}

/// <summary>
/// Lambda匹配器
/// </summary>
/// <typeparam name="T">匹配对象类型</typeparam>
public class LambdaMatcher<T> : Matcher<T>
{
	private readonly System.Func<T, bool> _matcher;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="matcher">匹配函数</param>
	public LambdaMatcher(System.Func<T, bool> matcher)
	{
		_matcher = matcher;
	}

	/// <summary>
	/// 匹配方法
	/// </summary>
	/// <param name="t">待匹配对象</param>
	/// <returns>是否匹配</returns>
	public override bool Match(T t)
	{
		return _matcher?.Invoke(t) ?? false;
	}
}
