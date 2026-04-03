using System;
using System.Collections.Generic;

namespace WellTool.Core.Util;

/// <summary>
/// 服务加载器工具类
/// </summary>
public static class ServiceLoaderUtil
{
	private static readonly Dictionary<Type, List<Type>> _services = new();

	/// <summary>
	/// 注册服务实现
	/// </summary>
	/// <typeparam name="T">服务接口</typeparam>
	/// <typeparam name="TImpl">实现类</typeparam>
	public static void Register<T, TImpl>() where TImpl : T, new()
	{
		Register(typeof(T), typeof(TImpl));
	}

	/// <summary>
	/// 注册服务实现
	/// </summary>
	/// <param name="serviceType">服务接口</param>
	/// <param name="implType">实现类</param>
	public static void Register(Type serviceType, Type implType)
	{
		if (!_services.ContainsKey(serviceType))
		{
			_services[serviceType] = new List<Type>();
		}
		if (!_services[serviceType].Contains(implType))
		{
			_services[serviceType].Add(implType);
		}
	}

	/// <summary>
	/// 获取服务实现
	/// </summary>
	/// <typeparam name="T">服务接口</typeparam>
	/// <returns>实现实例</returns>
	public static T GetService<T>() where T : class, new()
	{
		var serviceType = typeof(T);
		if (_services.ContainsKey(serviceType) && _services[serviceType].Count > 0)
		{
			var implType = _services[serviceType][0];
			return Activator.CreateInstance(implType) as T;
		}
		return new T();
	}

	/// <summary>
	/// 获取所有服务实现
	/// </summary>
	/// <typeparam name="T">服务接口</typeparam>
	/// <returns>实现实例列表</returns>
	public static IEnumerable<T> GetServices<T>() where T : class, new()
	{
		var serviceType = typeof(T);
		if (_services.ContainsKey(serviceType))
		{
			foreach (var implType in _services[serviceType])
			{
				yield return Activator.CreateInstance(implType) as T;
			}
		}
	}
}
