using System;
using System.Collections.Generic;
using System.Reflection;

namespace WellTool.Core.Bean.Copier
{
	/// <summary>
	/// Bean拷贝，提供：
	/// 
	/// <pre>
	///     1. Bean 转 Bean
	///     2. Bean 转 Map
	///     3. Map  转 Bean
	///     4. Map  转 Map
	/// </pre>
	/// </summary>
	/// <typeparam name="T">目标对象类型</typeparam>
	/// <author>looly</author>
	/// <since>3.2.3</since>
	public class BeanCopier<T> : ICopier<T>
	{
		private readonly ICopier<T> _copier;

		/// <summary>
		/// 创建BeanCopier
		/// </summary>
		/// <typeparam name="T">目标Bean类型</typeparam>
		/// <param name="source">来源对象，可以是Bean或者Map</param>
		/// <param name="target">目标Bean对象</param>
		/// <param name="copyOptions">拷贝属性选项</param>
		/// <returns>BeanCopier</returns>
		public static BeanCopier<T> Create(object source, T target, CopyOptions copyOptions)
		{
			return Create(source, target, target.GetType(), copyOptions);
		}

		/// <summary>
		/// 创建BeanCopier
		/// </summary>
		/// <typeparam name="T">目标Bean类型</typeparam>
		/// <param name="source">来源对象，可以是Bean或者Map</param>
		/// <param name="target">目标Bean对象</param>
		/// <param name="destType">目标的泛型类型，用于标注有泛型参数的Bean对象</param>
		/// <param name="copyOptions">拷贝属性选项</param>
		/// <returns>BeanCopier</returns>
		public static BeanCopier<T> Create(object source, T target, Type destType, CopyOptions copyOptions)
		{
			return new BeanCopier<T>(source, target, destType, copyOptions);
		}

		/// <summary>
		/// 构造
		/// </summary>
		/// <param name="source">来源对象，可以是Bean或者Map</param>
		/// <param name="target">目标Bean对象</param>
		/// <param name="targetType">目标的泛型类型，用于标注有泛型参数的Bean对象</param>
		/// <param name="copyOptions">拷贝属性选项</param>
		public BeanCopier(object source, T target, Type targetType, CopyOptions copyOptions)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source", "Source bean must be not null!");
			}
			if (target == null)
			{
				throw new ArgumentNullException("target", "Target bean must be not null!");
			}

			ICopier<T> copier;
			if (source is IDictionary<object, object> mapSource)
			{
				if (target is IDictionary<object, object> mapTarget)
				{
					copier = (ICopier<T>)new MapToMapCopier(mapSource, mapTarget, targetType, copyOptions);
				}
				else
				{
					copier = new MapToBeanCopier<T>(mapSource, target, targetType, copyOptions);
				}
			}
			else if (source is IValueProvider<string> valueProvider)
			{
				copier = new ValueProviderToBeanCopier<T>(valueProvider, target, targetType, copyOptions);
			}
			else
			{
				if (target is IDictionary<object, object> mapTarget)
				{
					copier = (ICopier<T>)new BeanToMapCopier(source, mapTarget, targetType, copyOptions);
				}
				else
				{
					copier = new BeanToBeanCopier<T>(source, target, targetType, copyOptions);
				}
			}
			_copier = copier;
		}

		/// <summary>
		/// 执行拷贝
		/// </summary>
		/// <returns>目标对象</returns>
		public T Copy()
		{
			return _copier.Copy();
		}
	}
}