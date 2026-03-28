using System;
using System.Reflection;

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

			// 获取源对象的属性描述映射（使用大小写不敏感的映射，确保能找到所有属性）
			var sourcePropDescMap = BeanUtil.GetBeanDesc(Source.GetType()).GetPropMap(true);
			// 获取目标对象的属性描述映射（使用大小写不敏感的映射，确保能找到所有属性）
			var targetPropDescMap = BeanUtil.GetBeanDesc(actualEditable).GetPropMap(true);

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

				// 检查目标字段可写性
				var tDesc = targetPropDescMap.TryGetValue(sFieldName, out var propDesc) ? propDesc : CopyOptions.FindPropDesc(targetPropDescMap, sFieldName);
				if (tDesc == null || !tDesc.IsWritable(CopyOptions.TransientSupport))
				{
					// 字段不可写，跳过之
					continue;
				}
				sFieldName = tDesc.FieldName;

				// 检查源对象属性是否过滤属性
				object sValue = sDesc.GetValue(Source);
				if (!CopyOptions.TestPropertyFilter(sDesc.Field, sValue))
				{
					continue;
				}

				// 获取目标字段真实类型并转换源值
				Type fieldType = GetActualType(_targetType, tDesc.FieldType);
				object newValue = CopyOptions.ConvertField(fieldType, sValue);

				// 自定义值
				newValue = CopyOptions.EditFieldValue(sFieldName, newValue);

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