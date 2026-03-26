using System;
using System.Collections.Concurrent;

namespace WellTool.Core.Bean
{
	/// <summary>
	/// Bean描述缓存，用于缓存Bean的属性信息，避免重复反射
	/// </summary>
	/// <author>looly</author>
	public static class BeanDescCache
	{
		private static readonly ConcurrentDictionary<Type, BeanDesc> _cache = new ConcurrentDictionary<Type, BeanDesc>();

		/// <summary>
		/// 获取Bean描述
		/// </summary>
		/// <param name="type">Bean类型</param>
		/// <returns>Bean描述</returns>
		public static BeanDesc GetBeanDesc(Type type)
		{
			return _cache.GetOrAdd(type, t => new BeanDesc(t));
		}

		/// <summary>
		/// 清除缓存
		/// </summary>
		public static void Clear()
		{
			_cache.Clear();
		}

		/// <summary>
		/// 移除指定类型的缓存
		/// </summary>
		/// <param name="type">Bean类型</param>
		public static void Remove(Type type)
		{
			_cache.TryRemove(type, out _);
		}

		/// <summary>
		/// 获取缓存大小
		/// </summary>
		/// <returns>缓存大小</returns>
		public static int Size => _cache.Count;
	}
}