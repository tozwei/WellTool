using System;
using System.Collections.Generic;
using System.Reflection;

namespace WellTool.Core.Bean
{
	/// <summary>
	/// Bean工具类，提供Bean的各种操作方法
	/// </summary>
	/// <author>looly</author>
	public static class BeanUtil
	{
		/// <summary>
		/// 获取Bean的属性描述
		/// </summary>
		/// <param name="type">Bean类型</param>
		/// <returns>Bean描述</returns>
		public static BeanDesc GetBeanDesc(Type type)
		{
			return BeanDescCache.GetBeanDesc(type);
		}

		/// <summary>
		/// 拷贝Bean属性
		/// </summary>
		/// <typeparam name="T">目标类型</typeparam>
		/// <param name="source">源对象</param>
		/// <param name="target">目标对象</param>
		/// <returns>目标对象</returns>
		public static T CopyProperties<T>(object source, T target)
		{
			return CopyProperties(source, target, null);
		}

		/// <summary>
		/// 拷贝Bean属性
		/// </summary>
		/// <typeparam name="T">目标类型</typeparam>
		/// <param name="source">源对象</param>
		/// <param name="target">目标对象</param>
		/// <param name="copyOptions">拷贝选项</param>
		/// <returns>目标对象</returns>
		public static T CopyProperties<T>(object source, T target, Copier.CopyOptions copyOptions)
		{
			if (source == null || target == null)
			{
				return target;
			}

			var copier = Copier.BeanCopier<T>.Create(source, target, copyOptions);
			return copier.Copy();
		}

		/// <summary>
		/// 将Bean转换为Map
		/// </summary>
		/// <param name="source">源对象</param>
		/// <returns>Map</returns>
		public static Dictionary<string, object> BeanToMap(object source)
		{
			return BeanToMap(source, null);
		}

		/// <summary>
		/// 将Bean转换为Map
		/// </summary>
		/// <param name="source">源对象</param>
		/// <param name="copyOptions">拷贝选项</param>
		/// <returns>Map</returns>
		public static Dictionary<string, object> BeanToMap(object source, Copier.CopyOptions copyOptions)
		{
			if (source == null)
			{
				return null;
			}

			var target = new Dictionary<object, object>();
			var copier = new Copier.BeanToMapCopier(source, target, target.GetType(), copyOptions);
			copier.Copy();
			return target.ToDictionary(k => k.Key?.ToString() ?? string.Empty, k => k.Value);
		}

		/// <summary>
		/// 将Map转换为Bean
		/// </summary>
		/// <typeparam name="T">目标类型</typeparam>
		/// <param name="map">源Map</param>
		/// <returns>Bean对象</returns>
		public static T MapToBean<T>(Dictionary<string, object> map)
		{
			return MapToBean<T>(map, null);
		}

		/// <summary>
		/// 将Map转换为Bean
		/// </summary>
		/// <typeparam name="T">目标类型</typeparam>
		/// <param name="map">源Map</param>
		/// <param name="copyOptions">拷贝选项</param>
		/// <returns>Bean对象</returns>
		public static T MapToBean<T>(Dictionary<string, object> map, Copier.CopyOptions copyOptions)
		{
			if (map == null)
			{
				return default;
			}

			var target = Activator.CreateInstance<T>();
			var objectMap = map.ToDictionary(k => (object)k.Key, v => v.Value);
			var copier = new Copier.MapToBeanCopier<T>(objectMap, target, typeof(T), copyOptions);
			copier.Copy();
			return target;
		}
	}
}