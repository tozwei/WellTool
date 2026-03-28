using System;
using System.Collections.Generic;
using System.Reflection;

namespace WellTool.Core.Bean
{
	/// <summary>
	/// Bean描述类，用于缓存Bean的属性信息
	/// </summary>
	/// <author>looly</author>
	public class BeanDesc
	{
		private readonly Type _type;
		private readonly Dictionary<string, PropDesc> _propMap;
		private readonly Dictionary<string, PropDesc> _propMapIgnoreCase;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="type">Bean类型</param>
		public BeanDesc(Type type)
		{
			_type = type;
			_propMap = new Dictionary<string, PropDesc>();
			_propMapIgnoreCase = new Dictionary<string, PropDesc>(StringComparer.OrdinalIgnoreCase);

			// 初始化属性描述
			InitPropMap();
		}

		/// <summary>
		/// 初始化属性映射
		/// </summary>
		private void InitPropMap()
		{
			// 获取所有字段
			var fields = _type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
			foreach (var field in fields)
			{
				// 跳过静态字段和常量
				if (field.IsStatic || field.IsLiteral)
				{
					continue;
				}

				var propDesc = new PropDesc(field);
				_propMap[propDesc.FieldName] = propDesc;
				_propMapIgnoreCase[propDesc.FieldName] = propDesc;

				// 处理Alias注解
				try
				{
					// 尝试获取所有自定义属性
					var attributes = field.GetCustomAttributes(false);
					foreach (var attribute in attributes)
					{
						var attributeTypeName = attribute.GetType().Name;
						if (attributeTypeName == "AliasAttribute" || attributeTypeName == "Alias")
						{
							var valueProperty = attribute.GetType().GetProperty("Value");
							if (valueProperty != null)
							{
								var aliasValue = valueProperty.GetValue(attribute) as string;
								if (!string.IsNullOrEmpty(aliasValue))
								{
									_propMap[aliasValue] = propDesc;
									_propMapIgnoreCase[aliasValue] = propDesc;
								}
							}
							break;
						}
					}
				}
				catch (AmbiguousMatchException)
				{
					// 忽略AmbiguousMatchException，继续处理下一个字段
				}
			}

			// 获取所有属性
			var properties = _type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
			foreach (var property in properties)
			{
				// 跳过静态属性
				if (property.GetMethod?.IsStatic ?? property.SetMethod?.IsStatic ?? false)
				{
					continue;
				}

				var propDesc = new PropDesc(property);
				_propMap[propDesc.FieldName] = propDesc;
				_propMapIgnoreCase[propDesc.FieldName] = propDesc;

				// 处理Alias注解
				try
				{
					// 尝试获取所有自定义属性
					var attributes = property.GetCustomAttributes(false);
					foreach (var attribute in attributes)
					{
						var attributeTypeName = attribute.GetType().Name;
						if (attributeTypeName == "AliasAttribute" || attributeTypeName == "Alias")
						{
							var valueProperty = attribute.GetType().GetProperty("Value");
							if (valueProperty != null)
							{
								var aliasValue = valueProperty.GetValue(attribute) as string;
								if (!string.IsNullOrEmpty(aliasValue))
								{
									_propMap[aliasValue] = propDesc;
									_propMapIgnoreCase[aliasValue] = propDesc;
								}
							}
							break;
						}
					}
				}
				catch (AmbiguousMatchException)
				{
					// 忽略AmbiguousMatchException，继续处理下一个属性
				}
			}
		}

		/// <summary>
		/// 获取属性描述映射
		/// </summary>
		/// <param name="ignoreCase">是否忽略大小写</param>
		/// <returns>属性描述映射</returns>
		public Dictionary<string, PropDesc> GetPropMap(bool ignoreCase)
		{
			return ignoreCase ? _propMapIgnoreCase : _propMap;
		}

		/// <summary>
		/// 获取属性描述
		/// </summary>
		/// <param name="fieldName">字段名</param>
		/// <returns>属性描述</returns>
		public PropDesc GetPropDesc(string fieldName)
		{
			return _propMap.TryGetValue(fieldName, out var propDesc) ? propDesc : null;
		}

		/// <summary>
		/// 获取属性描述（忽略大小写）
		/// </summary>
		/// <param name="fieldName">字段名</param>
		/// <returns>属性描述</returns>
		public PropDesc GetPropDescIgnoreCase(string fieldName)
		{
			return _propMapIgnoreCase.TryGetValue(fieldName, out var propDesc) ? propDesc : null;
		}

		/// <summary>
		/// 获取属性描述
		/// </summary>
		/// <param name="fieldName">字段名</param>
		/// <param name="ignoreCase">是否忽略大小写</param>
		/// <returns>属性描述</returns>
		public PropDesc GetPropDesc(string fieldName, bool ignoreCase)
		{
			return ignoreCase ? GetPropDescIgnoreCase(fieldName) : GetPropDesc(fieldName);
		}

		/// <summary>
		/// 获取Bean类型
		/// </summary>
		/// <returns>Bean类型</returns>
		public Type Type => _type;

		/// <summary>
		/// 获取所有属性描述
		/// </summary>
		/// <returns>属性描述映射</returns>
		public Dictionary<string, PropDesc> PropDescs => _propMap;
	}
}