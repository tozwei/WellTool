using System;
using System.Reflection;

namespace WellTool.Core.Bean.Copier
{
	/// <summary>
	/// ValueProvider属性拷贝到Bean中的拷贝器
	/// </summary>
	/// <typeparam name="T">目标Bean类型</typeparam>
	/// <since>5.8.0</since>
	public class ValueProviderToBeanCopier<T> : AbsCopier<IValueProvider<string>, T>
	{
		/// <summary>
		/// 目标的类型（用于泛型类注入）
		/// </summary>
		private readonly Type _targetType;

		/// <summary>
		/// 构造
		/// </summary>
		/// <param name="source">来源ValueProvider</param>
		/// <param name="target">目标Bean对象</param>
		/// <param name="targetType">目标泛型类型</param>
		/// <param name="copyOptions">拷贝选项</param>
		public ValueProviderToBeanCopier(IValueProvider<string> source, T target, Type targetType, CopyOptions copyOptions) : base(source, target, copyOptions)
		{
			_targetType = targetType;
		}

		/// <summary>
		/// 执行拷贝
		/// </summary>
		/// <returns>目标对象</returns>
		public override T Copy()
		{
			Type actualEditable = Target.GetType();
			if (CopyOptions.Editable != null)
			{
				// 检查限制类是否为target的父类或接口
				if (!CopyOptions.Editable.IsAssignableFrom(actualEditable))
				{
					throw new ArgumentException($"Target class [{actualEditable.Name}] not assignable to Editable class [{CopyOptions.Editable.Name}]");
				}
				actualEditable = CopyOptions.Editable;
			}

			// 获取目标对象的属性描述映射
			var targetPropDescMap = BeanUtil.GetBeanDesc(actualEditable).GetPropMap(CopyOptions.IgnoreCase);

			foreach (var entry in targetPropDescMap)
			{
				string fieldName = entry.Key;
				var tDesc = entry.Value;

				if (string.IsNullOrEmpty(fieldName) || !tDesc.IsWritable(CopyOptions.TransientSupport))
				{
					// 字段空或不可写，跳过
					continue;
				}

				fieldName = CopyOptions.EditFieldName(fieldName);
				// 对key做转换，转换后为null的跳过
				if (string.IsNullOrEmpty(fieldName))
				{
					continue;
				}

				// 忽略不需要拷贝的 key
				if (!CopyOptions.TestKeyFilter(fieldName))
				{
					continue;
				}

				// 检查目标字段可写性
				if (!tDesc.IsWritable(CopyOptions.TransientSupport))
				{
					// 字段不可写，跳过之
					continue;
				}

				// 检查目标是否过滤属性
				if (!CopyOptions.TestPropertyFilter(tDesc.Field, null))
				{
					continue;
				}

				// 检查ValueProvider是否包含该key
				if (!Source.ContainsKey(fieldName))
				{
					continue;
				}

				// 获取目标字段真实类型并从ValueProvider获取值
				Type fieldType = GetActualType(_targetType, tDesc.FieldType);
				object sValue = Source.Value(fieldName, fieldType);

				// 转换源值
				object newValue = CopyOptions.ConvertField(fieldType, sValue);

				// 自定义值
				newValue = CopyOptions.EditFieldValue(fieldName, newValue);

				// 目标赋值
				tDesc.SetValue(Target, newValue, CopyOptions.IgnoreNullValue, CopyOptions.IgnoreError, CopyOptions.Override);
			}

			return Target;
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