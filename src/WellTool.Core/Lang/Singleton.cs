using System;
using WellTool.Core.Lang.Func;
using WellTool.Core.Map;
using WellTool.Core.Util;

namespace WellTool.Core.Lang;

/// <summary>
/// 单例类
/// 提供单例对象的统一管理，当调用get方法时，如果对象池中存在此对象，返回此对象，否则创建新对象返回
/// </summary>
public static class Singleton
{
	private static readonly SafeConcurrentHashMap<string, object> POOL = new();

	/// <summary>
	/// 获得指定类的单例对象
	/// </summary>
	/// <typeparam name="T">单例对象类型</typeparam>
	/// <param name="clazz">类</param>
	/// <param name="params">构造方法参数</param>
	/// <returns>单例对象</returns>
	public static T Get<T>(Type clazz, params object[] @params) where T : class
	{
		AssertUtil.NotNull(clazz, "Class must be not null !");
		string key = BuildKey(clazz.FullName, @params);
		return Get<T>(key, () => ReflectUtil.NewInstance<T>(clazz, @params));
	}

	/// <summary>
	/// 获得指定类的单例对象
	/// </summary>
	/// <typeparam name="T">单例对象类型</typeparam>
	/// <param name="key">自定义键</param>
	/// <param name="supplier">单例对象的创建函数</param>
	/// <returns>单例对象</returns>
	public static T Get<T>(string key, Func0<T> supplier)
	{
		if (!POOL.TryGetValue(key, out var result) || result == null)
		{
			result = supplier.CallWithRuntimeException();
			POOL[key] = result;
		}
		return (T)result;
	}

	/// <summary>
	/// 获得指定类的单例对象
	/// </summary>
	/// <typeparam name="T">单例对象类型</typeparam>
	/// <param name="className">类名</param>
	/// <param name="params">构造参数</param>
	/// <returns>单例对象</returns>
	public static T Get<T>(string className, params object[] @params) where T : class
	{
		AssertUtil.NotBlank(className, "Class name must be not blank !");
		Type clazz = ClassUtil.LoadClass(className);
		return Get<T>(clazz, @params);
	}

	/// <summary>
	/// 将已有对象放入单例中，其Class做为键
	/// </summary>
	/// <param name="obj">对象</param>
	public static void Put(object obj)
	{
		AssertUtil.NotNull(obj, "Bean object must be not null !");
		Put(obj.GetType().FullName, obj);
	}

	/// <summary>
	/// 将已有对象放入单例中，key做为键
	/// </summary>
	/// <param name="key">键</param>
	/// <param name="obj">对象</param>
	public static void Put(string key, object obj)
	{
		POOL[key] = obj;
	}

	/// <summary>
	/// 移除指定Singleton对象
	/// </summary>
	/// <param name="clazz">类</param>
	public static void Remove(Type clazz)
	{
		if (clazz != null)
		{
			Remove(clazz.FullName);
		}
	}

	/// <summary>
	/// 移除指定Singleton对象
	/// </summary>
	/// <param name="key">键</param>
	public static void Remove(string key)
	{
		POOL.TryRemove(key, out _);
	}

	/// <summary>
	/// 清除所有Singleton对象
	/// </summary>
	public static void Destroy()
	{
		POOL.Clear();
	}

	/// <summary>
	/// 构建key
	/// </summary>
	/// <param name="className">类名</param>
	/// <param name="params">参数列表</param>
	/// <returns>key</returns>
	private static string BuildKey(string className, params object[] @params)
	{
		if (ArrayUtil.IsEmpty(@params))
		{
			return className;
		}
		return $"{className}#{string.Join("_", @params)}";
	}
}
