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

			// 清除缓存，确保使用最新的BeanDesc
			BeanDescCache.Clear();

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

			// 如果没有指定拷贝选项，创建默认的
			if (copyOptions == null)
			{
				copyOptions = Copier.CopyOptions.Create();
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

		/// <summary>
		/// 检查一个类型是否是Bean
		/// </summary>
		/// <param name="type">类型</param>
		/// <returns>是否是Bean</returns>
		public static bool IsBean(Type type)
		{
			if (type == null || type.IsPrimitive || type.IsEnum || type == typeof(string) || type == typeof(DateTime) || type.IsValueType)
			{
				return false;
			}

			// 检查是否有公共的无参构造函数
			var constructor = type.GetConstructor(Type.EmptyTypes);
			if (constructor == null)
			{
				return false;
			}

			// 检查是否有公共的属性或方法
			var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
			var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance);

			return properties.Length > 0 || methods.Length > 0;
		}

		/// <summary>
		/// 填充Bean属性
		/// </summary>
		/// <typeparam name="T">Bean类型</typeparam>
		/// <param name="source">源对象</param>
		/// <param name="target">目标Bean</param>
		/// <returns>目标Bean</returns>
		public static T FillBean<T>(object source, T target)
		{
			return CopyProperties(source, target);
		}

		/// <summary>
		/// 忽略大小写填充Bean属性
		/// </summary>
		/// <typeparam name="T">Bean类型</typeparam>
		/// <param name="map">源Map</param>
		/// <param name="target">目标Bean</param>
		/// <returns>目标Bean</returns>
		public static T FillBeanWithMapIgnoreCase<T>(Dictionary<string, object> map, T target)
		{
			if (map == null || target == null)
			{
				return target;
			}

			var beanDesc = GetBeanDesc(target.GetType());
			foreach (var entry in map)
			{
				var propDesc = beanDesc.GetPropDesc(entry.Key, true);
				if (propDesc != null && propDesc.HasSetter)
				{
					propDesc.SetValue(target, entry.Value);
				}
			}

			return target;
		}

		/// <summary>
		/// 将对象转换为指定类型的Bean
		/// </summary>
		/// <typeparam name="T">目标类型</typeparam>
		/// <param name="source">源对象</param>
		/// <returns>目标Bean</returns>
		public static T ToBean<T>(object source)
		{
			if (source == null)
			{
				return default;
			}

			if (source is Dictionary<string, object> map)
			{
				return MapToBean<T>(map);
			}

			var target = Activator.CreateInstance<T>();
			return CopyProperties(source, target);
		}

		/// <summary>
		/// 将对象转换为指定类型的Bean
		/// </summary>
		/// <param name="source">源对象</param>
		/// <param name="targetType">目标类型</param>
		/// <returns>目标Bean</returns>
		public static object ToBean(object source, Type targetType)
		{
			return ToBean(source, targetType, null);
		}

		/// <summary>
		/// 将对象转换为指定类型的Bean
		/// </summary>
		/// <param name="source">源对象</param>
		/// <param name="targetType">目标类型</param>
		/// <param name="copyOptions">拷贝选项</param>
		/// <returns>目标Bean</returns>
		public static object ToBean(object source, Type targetType, Copier.CopyOptions copyOptions)
		{
			if (source == null)
			{
				return null;
			}

			var target = Activator.CreateInstance(targetType);
			if (source is Dictionary<string, object> map)
			{
				var objectMap = map.ToDictionary(k => (object)k.Key, v => v.Value);
				var copier = new Copier.MapToBeanCopier(objectMap, target, targetType, copyOptions);
				copier.Copy();
			}
			else if (source is IDictionary<object, object> objectMap)
			{
				var copier = new Copier.MapToBeanCopier(objectMap, target, targetType, copyOptions);
				copier.Copy();
			}
			else
			{
				CopyProperties(source, target, copyOptions);
			}
			return target;
		}

		/// <summary>
		/// 忽略错误将对象转换为指定类型的Bean
		/// </summary>
		/// <param name="source">源对象</param>
		/// <param name="targetType">目标类型</param>
		/// <returns>目标Bean</returns>
		public static object ToBeanIgnoreError(object source, Type targetType)
		{
			var copyOptions = Copier.CopyOptions.Create().SetIgnoreError(true);
			return ToBean(source, targetType, copyOptions);
		}

		/// <summary>
		/// 忽略大小写将对象转换为指定类型的Bean
		/// </summary>
		/// <param name="source">源对象</param>
		/// <param name="targetType">目标类型</param>
		/// <param name="ignoreError">是否忽略错误</param>
		/// <returns>目标Bean</returns>
		public static object ToBeanIgnoreCase(object source, Type targetType, bool ignoreError)
		{
			var copyOptions = Copier.CopyOptions.Create().SetIgnoreCase(true);
			if (ignoreError)
			{
				copyOptions.SetIgnoreError(true);
			}
			return ToBean(source, targetType, copyOptions);
		}

		/// <summary>
		/// 获取Bean属性值
		/// </summary>
		/// <param name="bean">Bean对象</param>
		/// <param name="propertyName">属性名</param>
		/// <returns>属性值</returns>
		public static object GetProperty(object bean, string propertyName)
		{
			if (bean == null || string.IsNullOrEmpty(propertyName))
			{
				return null;
			}

			var beanDesc = GetBeanDesc(bean.GetType());
			var propDesc = beanDesc.GetPropDesc(propertyName);
			return propDesc?.GetValue(bean);
		}

		/// <summary>
		/// 获取Bean属性描述符
		/// </summary>
		/// <param name="type">Bean类型</param>
		/// <returns>属性描述符数组</returns>
		public static PropertyInfo[] GetPropertyDescriptors(Type type)
		{
			if (type == null)
			{
				return new PropertyInfo[0];
			}

			return type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
		}

		/// <summary>
		/// 修剪Bean中的字符串字段
		/// </summary>
		/// <param name="bean">Bean对象</param>
		/// <returns>修剪后的Bean</returns>
		public static object TrimStrFields(object bean)
		{
			if (bean == null)
			{
				return null;
			}

			var beanDesc = GetBeanDesc(bean.GetType());
			foreach (var propDesc in beanDesc.PropDescs.Values)
			{
				if (propDesc.PropertyType == typeof(string) && propDesc.HasGetter && propDesc.HasSetter)
				{
					var value = propDesc.GetValue(bean) as string;
					if (value != null)
					{
						propDesc.SetValue(bean, value.Trim());
					}
				}
			}

			return bean;
		}

		/// <summary>
		/// 设置Bean属性值
		/// </summary>
		/// <param name="bean">Bean对象</param>
		/// <param name="propertyName">属性名</param>
		/// <param name="value">属性值</param>
		public static void SetProperty(object bean, string propertyName, object value)
		{
			if (bean == null || string.IsNullOrEmpty(propertyName))
			{
				return;
			}

			var beanDesc = GetBeanDesc(bean.GetType());
			var propDesc = beanDesc.GetPropDesc(propertyName);
			if (propDesc != null && propDesc.HasSetter)
			{
				propDesc.SetValue(bean, value);
			}
		}

		/// <summary>
		/// 复制到列表
		/// </summary>
		/// <typeparam name="T">目标类型</typeparam>
		/// <param name="sourceList">源列表</param>
		/// <returns>目标列表</returns>
		public static List<T> CopyToList<T>(IEnumerable<object> sourceList)
		{
			if (sourceList == null)
			{
				return new List<T>();
			}

			var result = new List<T>();
			foreach (var source in sourceList)
			{
				result.Add(ToBean<T>(source));
			}
			return result;
		}

		/// <summary>
		/// 检查公共字段是否相等
		/// </summary>
		/// <param name="bean1">第一个Bean</param>
		/// <param name="bean2">第二个Bean</param>
		/// <returns>是否相等</returns>
		public static bool IsCommonFieldsEqual(object bean1, object bean2)
		{
			if (bean1 == null || bean2 == null)
			{
				return bean1 == bean2;
			}

			if (bean1.GetType() != bean2.GetType())
			{
				return false;
			}

			var beanDesc = GetBeanDesc(bean1.GetType());
			foreach (var propDesc in beanDesc.PropDescs.Values)
			{
				if (propDesc.HasGetter)
				{
					var value1 = propDesc.GetValue(bean1);
					var value2 = propDesc.GetValue(bean2);
					if (!Equals(value1, value2))
					{
						return false;
					}
				}
			}

			return true;
		}

		/// <summary>
		/// 检查是否有getter方法
		/// </summary>
		/// <param name="type">类型</param>
		/// <param name="propertyName">属性名</param>
		/// <returns>是否有getter方法</returns>
		public static bool HasGetter(Type type, string propertyName)
		{
			if (type == null || string.IsNullOrEmpty(propertyName))
			{
				return false;
			}

			var beanDesc = GetBeanDesc(type);
			var propDesc = beanDesc.GetPropDesc(propertyName);
			return propDesc != null && propDesc.HasGetter;
		}
	}
}