using System;
using System.Collections.Generic;
using System.Reflection;

namespace WellTool.Core.Annotation
{
	/// <summary>
	/// 注解代理<br>
	/// 通过代理指定注解，可以自定义调用注解的方法逻辑，如支持{@link Alias} 注解
	/// </summary>
	/// <typeparam name="T">注解类型</typeparam>
	/// <since>5.7.23</since>
	public class AnnotationProxy<T> where T : Attribute
	{
		private readonly T _annotation;
		private readonly Type _type;
		private readonly Dictionary<string, object> _attributes;

		/// <summary>
		/// 构造
		/// </summary>
		/// <param name="annotation">注解</param>
		public AnnotationProxy(T annotation)
		{
			_annotation = annotation;
			_type = typeof(T);
			_attributes = InitAttributes();
		}

		/// <summary>
		/// 获取注解类型
		/// </summary>
		/// <returns>注解类型</returns>
		public Type AnnotationType()
		{
			return _type;
		}

		/// <summary>
		/// 调用注解方法
		/// </summary>
		/// <param name="methodName">方法名</param>
		/// <returns>方法返回值</returns>
		public object Invoke(string methodName)
		{
			// 查找方法
			var method = _type.GetMethod(methodName);
			if (method == null)
			{
				throw new ArgumentException($"No method found: {methodName}");
			}

			// 检查是否有AliasAttribute
			var aliasAttr = method.GetCustomAttribute<AliasAttribute>();
			if (aliasAttr != null && !string.IsNullOrEmpty(aliasAttr.Value))
			{
				if (_attributes.ContainsKey(aliasAttr.Value))
				{
					return _attributes[aliasAttr.Value];
				}
				throw new ArgumentException($"No method for alias: [{aliasAttr.Value}]");
			}

			// 从缓存中获取值
			if (_attributes.ContainsKey(methodName))
			{
				return _attributes[methodName];
			}

			// 调用方法
			return method.Invoke(_annotation, null);
		}

		/// <summary>
		/// 初始化注解的属性<br>
		/// 此方法预先调用所有注解的方法，将注解方法值缓存于attributes中
		/// </summary>
		/// <returns>属性（方法结果）映射</returns>
		private Dictionary<string, object> InitAttributes()
		{
			var methods = _type.GetMethods();
			var attributes = new Dictionary<string, object>(methods.Length);

			foreach (var method in methods)
			{
				// 只处理无参方法
				if (method.GetParameters().Length == 0)
				{
					attributes[method.Name] = method.Invoke(_annotation, null);
				}
			}

			return attributes;
		}
	}
}