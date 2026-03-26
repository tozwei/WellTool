using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics.CodeAnalysis;

namespace WellTool.Core.Annotation
{
	/// <summary>
	/// 注解工具类<br>
	/// 快速获取注解对象、注解值等工具封装
	/// </summary>
	/// <author>looly</author>
	/// <since>4.0.9</since>
	public static class AnnotationUtil
	{
		/// <summary>
		/// 元注解
		/// </summary>
		private static readonly HashSet<Type> META_ANNOTATIONS = new HashSet<Type>
		{
			typeof(AttributeUsageAttribute),
			typeof(ObsoleteAttribute),
			typeof(SuppressMessageAttribute)
		};

		/// <summary>
		/// 是否为C#自带的元注解。<br>
		/// </summary>
		/// <param name="attributeType">注解类型</param>
		/// <returns>是否为C#自带的元注解</returns>
		public static bool IsJdkMetaAnnotation(Type attributeType)
		{
			return META_ANNOTATIONS.Contains(attributeType);
		}

		/// <summary>
		/// 是否不为C#自带的元注解。<br>
		/// </summary>
		/// <param name="attributeType">注解类型</param>
		/// <returns>是否为C#自带的元注解</returns>
		public static bool IsNotJdkMateAnnotation(Type attributeType)
		{
			return !IsJdkMetaAnnotation(attributeType);
		}

		/// <summary>
		/// 获取指定注解
		/// </summary>
		/// <typeparam name="T">注解类型</typeparam>
		/// <param name="member">成员信息，可以是Type、MethodInfo、FieldInfo、ConstructorInfo等</param>
		/// <returns>注解对象</returns>
		public static T GetAttribute<T>(MemberInfo member) where T : Attribute
		{
			if (member == null)
			{
				return null;
			}
			return member.GetCustomAttribute<T>();
		}

		/// <summary>
		/// 获取指定注解
		/// </summary>
		/// <typeparam name="T">注解类型</typeparam>
		/// <param name="member">成员信息，可以是Type、MethodInfo、FieldInfo、ConstructorInfo等</param>
		/// <param name="inherit">是否继承</param>
		/// <returns>注解对象</returns>
		public static T GetAttribute<T>(MemberInfo member, bool inherit) where T : Attribute
		{
			if (member == null)
			{
				return null;
			}
			return member.GetCustomAttribute<T>(inherit);
		}

		/// <summary>
		/// 获取指定注解数组
		/// </summary>
		/// <typeparam name="T">注解类型</typeparam>
		/// <param name="member">成员信息，可以是Type、MethodInfo、FieldInfo、ConstructorInfo等</param>
		/// <returns>注解对象数组</returns>
		public static T[] GetAttributes<T>(MemberInfo member) where T : Attribute
		{
			if (member == null)
			{
				return new T[0];
			}
			return member.GetCustomAttributes<T>().ToArray();
		}

		/// <summary>
		/// 获取指定注解数组
		/// </summary>
		/// <typeparam name="T">注解类型</typeparam>
		/// <param name="member">成员信息，可以是Type、MethodInfo、FieldInfo、ConstructorInfo等</param>
		/// <param name="inherit">是否继承</param>
		/// <returns>注解对象数组</returns>
		public static T[] GetAttributes<T>(MemberInfo member, bool inherit) where T : Attribute
		{
			if (member == null)
			{
				return new T[0];
			}
			return member.GetCustomAttributes<T>(inherit).ToArray();
		}

		/// <summary>
		/// 检查是否包含指定注解
		/// </summary>
		/// <typeparam name="T">注解类型</typeparam>
		/// <param name="member">成员信息，可以是Type、MethodInfo、FieldInfo、ConstructorInfo等</param>
		/// <returns>是否包含指定注解</returns>
		public static bool HasAttribute<T>(MemberInfo member) where T : Attribute
		{
			return GetAttribute<T>(member) != null;
		}

		/// <summary>
		/// 检查是否包含指定注解
		/// </summary>
		/// <typeparam name="T">注解类型</typeparam>
		/// <param name="member">成员信息，可以是Type、MethodInfo、FieldInfo、ConstructorInfo等</param>
		/// <param name="inherit">是否继承</param>
		/// <returns>是否包含指定注解</returns>
		public static bool HasAttribute<T>(MemberInfo member, bool inherit) where T : Attribute
		{
			return GetAttribute<T>(member, inherit) != null;
		}

		/// <summary>
		/// 检查是否包含指定注解<br>
		/// 注解类传入全名，通过Type.GetType加载，避免不存在的注解导致的异常
		/// </summary>
		/// <param name="member">成员信息，可以是Type、MethodInfo、FieldInfo、ConstructorInfo等</param>
		/// <param name="attributeTypeName">注解类型完整类名</param>
		/// <returns>是否包含指定注解</returns>
		public static bool HasAttribute(MemberInfo member, string attributeTypeName)
		{
			if (member == null)
			{
				return false;
			}

			try
			{
				var attributeType = Type.GetType(attributeTypeName);
				if (attributeType != null)
				{
					return member.GetCustomAttributes(attributeType, true).Length > 0;
				}
			}
			catch
			{
				// ignore
			}
			return false;
		}

		/// <summary>
		/// 获取指定注解属性的值<br>
		/// 如果无指定的属性方法返回null
		/// </summary>
		/// <typeparam name="T">注解值类型</typeparam>
		/// <param name="member">成员信息，可以是Type、MethodInfo、FieldInfo、ConstructorInfo等</param>
		/// <param name="attributeType">注解类型</param>
		/// <param name="propertyName">属性名</param>
		/// <returns>注解对象</returns>
		public static T GetAttributeValue<T>(MemberInfo member, Type attributeType, string propertyName)
		{
			var attribute = member.GetCustomAttribute(attributeType);
			if (attribute == null)
			{
				return default;
			}

			var property = attributeType.GetProperty(propertyName);
			if (property == null)
			{
				return default;
			}

			return (T)property.GetValue(attribute);
		}

		/// <summary>
		/// 获取指定注解中所有属性值<br>
		/// </summary>
		/// <param name="member">成员信息，可以是Type、MethodInfo、FieldInfo、ConstructorInfo等</param>
		/// <param name="attributeType">注解类型</param>
		/// <returns>注解对象</returns>
		public static Dictionary<string, object> GetAttributeValueMap(MemberInfo member, Type attributeType)
		{
			var attribute = member.GetCustomAttribute(attributeType);
			if (attribute == null)
			{
				return null;
			}

			var properties = attributeType.GetProperties()
				.Where(p => p.CanRead && p.GetIndexParameters().Length == 0)
				.Where(p => p.Name != "TypeId"); // 跳过TypeId属性

			var result = new Dictionary<string, object>();
			foreach (var property in properties)
			{
				result[property.Name] = property.GetValue(attribute);
			}
			return result;
		}

		/// <summary>
		/// 获取注解类可以用来修饰哪些程序元素
		/// </summary>
		/// <param name="attributeType">注解类</param>
		/// <returns>注解修饰的程序元素</returns>
		public static AttributeTargets GetTargetType(Type attributeType)
		{
			var attributeUsage = attributeType.GetCustomAttribute<AttributeUsageAttribute>();
			if (attributeUsage == null)
			{
				return AttributeTargets.All;
			}
			return attributeUsage.ValidOn;
		}

		/// <summary>
		/// 是否可以被继承，默认为 false
		/// </summary>
		/// <param name="attributeType">注解类</param>
		/// <returns>是否可以被继承</returns>
		public static bool IsInherited(Type attributeType)
		{
			var attributeUsage = attributeType.GetCustomAttribute<AttributeUsageAttribute>();
			return attributeUsage != null && attributeUsage.Inherited;
		}

		/// <summary>
		/// 方法是否为注解属性方法。 <br>
		/// 方法无参数，且有返回值的方法认为是注解属性的方法。
		/// </summary>
		/// <param name="method">方法</param>
		static bool IsAttributeMethod(MethodInfo method)
		{
			return method.GetParameters().Length == 0 && method.ReturnType != typeof(void);
		}
	}
}