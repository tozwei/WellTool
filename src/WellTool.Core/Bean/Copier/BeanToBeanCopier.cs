using System;
using System.Reflection;
using WellTool.Core.Bean;

namespace WellTool.Core.Bean.Copier
{
	/// <summary>
	/// Bean属性拷贝到Bean中的拷贝器
	/// </summary>
	/// <typeparam name="T">目标Bean类型</typeparam>
	/// <since>5.8.0</since>
	public class BeanToBeanCopier<T> : AbsCopier<object, T>
	{
		/// <summary>
		/// 目标的类型（用于泛型类注入）
		/// </summary>
		private readonly Type _targetType;

		/// <summary>
		/// 构造
		/// </summary>
		/// <param name="source">来源对象</param>
		/// <param name="target">目标Bean对象</param>
		/// <param name="targetType">目标泛型类型</param>
		/// <param name="copyOptions">拷贝选项</param>
		public BeanToBeanCopier(object source, T target, Type targetType, CopyOptions copyOptions) : base(source, target, copyOptions)
		{
			_targetType = targetType;
		}

		/// <summary>
		/// 执行拷贝
		/// </summary>
		/// <returns>目标对象</returns>
		public override T Copy()
		{
			// 获取源对象和目标对象的Bean描述
			var sourceBeanDesc = BeanUtil.GetBeanDesc(Source.GetType());
			var targetBeanDesc = BeanUtil.GetBeanDesc(Target.GetType());

			// 遍历源对象的所有属性
			foreach (var sourcePropDesc in sourceBeanDesc.PropDescs.Values)
			{
				// 检查属性是否可读
				if (!sourcePropDesc.HasGetter)
				{
					continue;
				}

				// 获取源属性的值
				object value = sourcePropDesc.GetValue(Source);

				// 查找目标对象中对应的属性
				var targetPropDesc = targetBeanDesc.GetPropDesc(sourcePropDesc.FieldName);
				if (targetPropDesc == null || !targetPropDesc.HasSetter)
				{
					// 尝试通过源属性的Alias查找（不区分大小写）
					string sourceAlias = GetSourcePropAlias(sourcePropDesc);
					if (!string.IsNullOrEmpty(sourceAlias))
					{
						targetPropDesc = targetBeanDesc.GetPropDesc(sourceAlias, true);
					}
					
					// 尝试通过目标属性的Alias查找（目标属性的Alias等于源属性的FieldName）
					if (targetPropDesc == null || !targetPropDesc.HasSetter)
					{
						targetPropDesc = FindTargetPropByAlias(targetBeanDesc, sourcePropDesc.FieldName);
					}
					
					if (targetPropDesc == null || !targetPropDesc.HasSetter)
					{
						// 目标属性不存在或不可写，跳过
						continue;
					}
				}

				// 设置目标属性的值，考虑IgnoreNullValue选项
				targetPropDesc.SetValue(Target, value, CopyOptions.IgnoreNullValue, CopyOptions.IgnoreError, CopyOptions.Override);
			}

			return Target;
		}

		/// <summary>
		/// 获取源属性的Alias
		/// </summary>
		/// <param name="sourcePropDesc">源属性描述</param>
		/// <returns>Alias值</returns>
		private string GetSourcePropAlias(PropDesc sourcePropDesc)
		{
			// 先从Property获取Alias注解
			var property = sourcePropDesc.Property;
			if (property != null)
			{
				// 查找具有Value属性的Alias注解
				var attributes = property.GetCustomAttributes(true);
				foreach (var attribute in attributes)
				{
					var attributeType = attribute.GetType();
					if ((attributeType.Name == "AliasAttribute" || attributeType.Name == "Alias") && 
						attributeType.GetProperty("Value") != null)
					{
						var valueProperty = attributeType.GetProperty("Value");
						var aliasValue = valueProperty.GetValue(attribute) as string;
						if (!string.IsNullOrEmpty(aliasValue))
						{
							return aliasValue;
						}
					}
				}
			}
			
			// 再从Field获取Alias注解
			var field = sourcePropDesc.Field;
			if (field != null)
			{
				// 查找具有Value属性的Alias注解
				var attributes = field.GetCustomAttributes(true);
				foreach (var attribute in attributes)
				{
					var attributeType = attribute.GetType();
					if ((attributeType.Name == "AliasAttribute" || attributeType.Name == "Alias") && 
						attributeType.GetProperty("Value") != null)
					{
						var valueProperty = attributeType.GetProperty("Value");
						var aliasValue = valueProperty.GetValue(attribute) as string;
						if (!string.IsNullOrEmpty(aliasValue))
						{
							return aliasValue;
						}
					}
				}
			}
			return null;
		}

		/// <summary>
		/// 通过Alias查找目标属性
		/// </summary>
		/// <param name="targetBeanDesc">目标对象的Bean描述</param>
		/// <param name="sourceFieldName">源字段名</param>
		/// <returns>目标属性描述</returns>
		private PropDesc FindTargetPropByAlias(BeanDesc targetBeanDesc, string sourceFieldName)
		{
			// 遍历目标对象的所有属性，查找带有Alias注解的属性
			foreach (var targetPropDesc in targetBeanDesc.PropDescs.Values)
			{
				// 先检查Property的Alias注解
				var property = targetPropDesc.Property;
				if (property != null)
				{
					// 查找具有Value属性的Alias注解
					var attributes = property.GetCustomAttributes(true);
					foreach (var attribute in attributes)
					{
						var attributeType = attribute.GetType();
						if ((attributeType.Name == "AliasAttribute" || attributeType.Name == "Alias") && 
							attributeType.GetProperty("Value") != null)
						{
							var valueProperty = attributeType.GetProperty("Value");
							var aliasValue = valueProperty.GetValue(attribute) as string;
							if (string.Equals(aliasValue, sourceFieldName, StringComparison.OrdinalIgnoreCase))
							{
								return targetPropDesc;
							}
						}
					}
				}
				
				// 再检查Field的Alias注解
				var field = targetPropDesc.Field;
				if (field != null)
				{
					// 查找具有Value属性的Alias注解
					var attributes = field.GetCustomAttributes(true);
					foreach (var attribute in attributes)
					{
						var attributeType = attribute.GetType();
						if ((attributeType.Name == "AliasAttribute" || attributeType.Name == "Alias") && 
							attributeType.GetProperty("Value") != null)
						{
							var valueProperty = attributeType.GetProperty("Value");
							var aliasValue = valueProperty.GetValue(attribute) as string;
							if (string.Equals(aliasValue, sourceFieldName, StringComparison.OrdinalIgnoreCase))
							{
								return targetPropDesc;
							}
						}
					}
				}
			}
			return null;
		}

		/// <summary>
		/// 获取实际类型
		/// </summary>
		/// <param name="targetType">目标类型</param>
		/// <param name="fieldType">字段类型</param>
		/// <returns>实际类型</returns>
		private Type GetActualType(Type targetType, Type fieldType)
		{
			// 这里简化处理，实际项目中可以根据需要实现更复杂的类型解析
			return fieldType;
		}
	}
}