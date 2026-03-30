using System;
using System.Collections.Generic;
using System.Reflection;

namespace WellTool.Core.Bean.Copier
{
	/// <summary>
	/// Map属性拷贝到Map中的拷贝器
	/// </summary>
	/// <typeparam name="T">目标Map类型</typeparam>
	/// <since>5.8.0</since>
	public class MapToMapCopier<T> : AbsCopier<IDictionary<object, object>, T> where T : IDictionary<object, object>
	{
		// 提前获取目标值真实类型
		private readonly Type[] _targetTypeArguments;

		/// <summary>
		/// 构造
		/// </summary>
		/// <param name="source">来源Map</param>
		/// <param name="target">目标Map对象</param>
		/// <param name="targetType">目标泛型类型</param>
		/// <param name="copyOptions">拷贝选项</param>
		public MapToMapCopier(IDictionary<object, object> source, T target, Type targetType, CopyOptions copyOptions) : base(source, target, copyOptions)
		{
			_targetTypeArguments = GetTypeArguments(targetType);
		}

		/// <summary>
		/// 执行拷贝
		/// </summary>
		/// <returns>目标对象</returns>
		public override T Copy()
		{
			foreach (var entry in Source)
			{
				object sKey = entry.Key;
				object sValue = entry.Value;

				if (sKey == null)
				{
					continue;
				}

				if (sKey is string sKeyStr)
				{
					sKey = CopyOptions.EditFieldName(sKeyStr);
					// 对key做转换，转换后为null的跳过
					if (sKey == null)
					{
						continue;
					}
				}

				// 忽略不需要拷贝的 key
				if (!CopyOptions.TestKeyFilter(sKey))
				{
					continue;
				}

				// 非覆盖模式下，如果目标值存在，则跳过
				if (!CopyOptions.Override && Target.ContainsKey(sKey))
				{
					continue;
				}

				// 尝试转换源值
				if (_targetTypeArguments != null && _targetTypeArguments.Length > 1)
				{
					sValue = CopyOptions.ConvertField(_targetTypeArguments[1], sValue);
				}

				// 自定义值
				sValue = CopyOptions.EditFieldValue(sKey.ToString(), sValue);

				// 忽略空值
				if (CopyOptions.IgnoreNullValue && sValue == null)
				{
					continue;
				}

				// 目标赋值
				Target[sKey] = sValue;
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