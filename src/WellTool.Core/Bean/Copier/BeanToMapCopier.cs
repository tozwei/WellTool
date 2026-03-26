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

			// 获取源对象的属性描述映射
			var sourcePropDescMap = BeanUtil.GetBeanDesc(actualEditable).GetPropMap(CopyOptions.IgnoreCase);
			foreach (var entry in sourcePropDescMap)
			{
				string sFieldName = entry.Key;
				var sDesc = entry.Value;

				if (string.IsNullOrEmpty(sFieldName) || !sDesc.IsReadable(CopyOptions.TransientSupport))
				{
					// 字段空或不可读，跳过
					continue;
				}

				sFieldName = CopyOptions.EditFieldName(sFieldName);
				// 对key做转换，转换后为null的跳过
				if (string.IsNullOrEmpty(sFieldName))
				{
					continue;
				}

				// 忽略不需要拷贝的 key
				if (!CopyOptions.TestKeyFilter(sFieldName))
				{
					continue;
				}

				// 检查源对象属性是否过滤属性
				object sValue = sDesc.GetValue(Source);
				if (!CopyOptions.TestPropertyFilter(sDesc.Field, sValue))
				{
					continue;
				}

				// 尝试转换源值
				if (_targetTypeArguments != null && _targetTypeArguments.Length > 1)
				{
					sValue = CopyOptions.ConvertField(_targetTypeArguments[1], sValue);
				}

				// 自定义值
				sValue = CopyOptions.EditFieldValue(sFieldName, sValue);

				// 目标赋值
				if (sValue != null || !CopyOptions.IgnoreNullValue)
				{
					Target[sFieldName] = sValue;
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