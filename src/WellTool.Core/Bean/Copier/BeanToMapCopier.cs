using System;
using System.Collections.Generic;
using System.Reflection;

namespace WellTool.Core.Bean.Copier
{
	/// <summary>
	/// Bean属性拷贝到Map中的拷贝器
	/// </summary>
	/// <since>5.8.0</since>
	public class BeanToMapCopier : AbsCopier<object, IDictionary<object, object>>
	{
		// 提前获取目标值真实类型
		private readonly Type[] _targetTypeArguments;

		/// <summary>
		/// 构造
		/// </summary>
		/// <param name="source">来源对象</param>
		/// <param name="target">目标Map对象</param>
		/// <param name="targetType">目标泛型类型</param>
		/// <param name="copyOptions">拷贝选项</param>
		public BeanToMapCopier(object source, IDictionary<object, object> target, Type targetType, CopyOptions copyOptions) : base(source, target, copyOptions)
		{
			_targetTypeArguments = GetTypeArguments(targetType);
		}

		/// <summary>
		/// 执行拷贝
		/// </summary>
		/// <returns>目标对象</returns>
		public override IDictionary<object, object> Copy()
		{
			Type actualEditable = Source.GetType();
			if (CopyOptions.Editable != null)
			{
				// 检查限制类是否为source的父类或接口
				if (!CopyOptions.Editable.IsAssignableFrom(actualEditable))
				{
					throw new ArgumentException($"Source class [{actualEditable.Name}] not assignable to Editable class [{CopyOptions.Editable.Name}]");
				}
				actualEditable = CopyOptions.Editable;
			}

			// 获取源对象的属性描述映射（大小写不敏感，确保能获取到所有别名）
			var sourcePropDescMap = BeanUtil.GetBeanDesc(actualEditable).GetPropMap(true);
			// 使用一个字典来跟踪已经处理过的原始字段，避免重复处理值
			var processedOriginalFields = new HashSet<string>();
			
			// 遍历所有属性描述
			foreach (var entry in sourcePropDescMap)
			{
				string sFieldName = entry.Key;
				var sDesc = entry.Value;

				// 跳过空字段或不可读字段
				if (string.IsNullOrEmpty(sFieldName) || !sDesc.IsReadable(CopyOptions.TransientSupport))
				{
					continue;
				}

				// 跳过已经处理过的字段
				if (processedOriginalFields.Contains(sDesc.FieldName))
				{
					continue;
				}

				// 对于原始字段（字段名与PropDesc的FieldName相同），标记为已处理
				if (sFieldName == sDesc.FieldName)
				{
					processedOriginalFields.Add(sDesc.FieldName);
				}

				// 获取属性值
				object sValue = sDesc.GetValue(Source);

				// 转换字段名（如果需要）
				string targetFieldName = sFieldName;
				if (CopyOptions.FieldNameEditor != null)
				{
					targetFieldName = CopyOptions.EditFieldName(sFieldName);
				}
				else if (sFieldName == sDesc.FieldName)
				{
					// 对于原始字段，使用小写作为默认字段名
					targetFieldName = sDesc.FieldName.ToLower();
				}

				// 对key做转换，转换后为null的跳过
				if (!string.IsNullOrEmpty(targetFieldName))
				{
					// 忽略不需要拷贝的 key
					if (CopyOptions.TestKeyFilter(targetFieldName))
					{
						// 检查源对象属性是否过滤属性
						if (CopyOptions.TestPropertyFilter(sDesc.Field, sValue))
						{
							// 尝试转换源值
							if (_targetTypeArguments != null && _targetTypeArguments.Length > 1)
							{
								sValue = CopyOptions.ConvertField(_targetTypeArguments[1], sValue);
							}

							// 自定义值
							sValue = CopyOptions.EditFieldValue(targetFieldName, sValue);

							// 目标赋值
							if (sValue != null || !CopyOptions.IgnoreNullValue)
							{
								Target[targetFieldName] = sValue;
							}
						}
					}
				}
			}

			return Target;
		}

		/// <summary>
		/// 获取类型参数
		/// </summary>
		/// <param name="type">类型</param>
		/// <returns>类型参数数组</returns>
		private Type[] GetTypeArguments(Type type)
		{
			if (type.IsGenericType)
			{
				return type.GetGenericArguments();
			}
			return null;
		}
	}
}