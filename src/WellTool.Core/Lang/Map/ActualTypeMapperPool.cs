namespace WellTool.Core.lang.map;

using System;
using System.Collections.Concurrent;

/// <summary>
/// 类型映射池
/// </summary>
public static class ActualTypeMapperPool
{
	private static readonly ConcurrentDictionary<Type, Type> Cache = new();

	/// <summary>
	/// 获取实际类型
	/// </summary>
	/// <param name="type">类型</param>
	/// <returns>实际类型</returns>
	public static Type Get(Type type)
	{
		return Cache.GetOrAdd(type, t =>
		{
			if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
				return Nullable.GetUnderlyingType(t) ?? t;
			return t;
		});
	}

	/// <summary>
	/// 清理缓存
	/// </summary>
	public static void Clear()
	{
		Cache.Clear();
	}
}

/// <summary>
/// Lookup工厂
/// </summary>
public static class LookupFactory
{
	/// <summary>
	/// 创建查找器
	/// </summary>
	/// <typeparam name="TKey">键类型</typeparam>
	/// <typeparam name="TValue">值类型</typeparam>
	/// <returns>查找器</returns>
	public static ILookup<TKey, TValue> Create<TKey, TValue>()
	{
		return new Lookup<TKey, TValue>();
	}
}

/// <summary>
/// 简单Lookup实现
/// </summary>
public class Lookup<TKey, TValue> : ILookup<TKey, TValue>
{
	private readonly System.Collections.Generic.Dictionary<TKey, System.Collections.Generic.List<TValue>> _dict = new();

	public void Add(TKey key, TValue value)
	{
		if (!_dict.ContainsKey(key))
			_dict[key] = new System.Collections.Generic.List<TValue>();
		_dict[key].Add(value);
	}

	public System.Collections.Generic.IEnumerable<TValue> this[TKey key] => _dict.ContainsKey(key) ? _dict[key] : Array.Empty<TValue>();

	public int Count => _dict.Count;
}

/// <summary>
/// Lookup接口
/// </summary>
public interface ILookup<TKey, TValue>
{
	System.Collections.Generic.IEnumerable<TValue> this[TKey key] { get; }
	int Count { get; }
}

/// <summary>
/// MethodHandle工具类
/// </summary>
public static class MethodHandleUtil
{
	/// <summary>
	/// 获取方法句柄
	/// </summary>
	/// <param name="method">方法信息</param>
	/// <returns>委托</returns>
	public static Delegate GetMethodHandle(System.Reflection.MethodInfo method)
	{
		var delegateType = GetDelegateType(method);
		return Delegate.CreateDelegate(delegateType, method);
	}

	private static Type GetDelegateType(System.Reflection.MethodInfo method)
	{
		var parameters = method.GetParameters();
		var paramTypes = new Type[parameters.Length + 1];
		paramTypes[0] = method.DeclaringType;
		for (int i = 0; i < parameters.Length; i++)
			paramTypes[i + 1] = parameters[i].ParameterType;

		if (method.ReturnType == typeof(void))
			return System.Linq.Expressions.Expression.GetActionType(paramTypes);
		return System.Linq.Expressions.Expression.GetFuncType(paramTypes);
	}
}
