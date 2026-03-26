using System;
using System.Reflection;

namespace WellTool.Core.Bean
{
	/// <summary>
	/// 属性描述类，用于描述Bean的属性信息
	/// </summary>
	/// <author>looly</author>
	public class PropDesc
	{
		private readonly FieldInfo _field;
		private readonly PropertyInfo _property;
		private readonly string _fieldName;
		private readonly Type _fieldType;

		/// <summary>
		/// 构造函数（基于字段）
		/// </summary>
		/// <param name="field">字段信息</param>
		public PropDesc(FieldInfo field)
		{
			_field = field;
			_fieldName = field.Name;
			_fieldType = field.FieldType;
		}

		/// <summary>
		/// 构造函数（基于属性）
		/// </summary>
		/// <param name="property">属性信息</param>
		public PropDesc(PropertyInfo property)
		{
			_property = property;
			_fieldName = property.Name;
			_fieldType = property.PropertyType;
		}

		/// <summary>
		/// 获取字段名
		/// </summary>
		/// <returns>字段名</returns>
		public string FieldName => _fieldName;

		/// <summary>
		/// 获取字段类型
		/// </summary>
		/// <returns>字段类型</returns>
		public Type FieldType => _fieldType;

		/// <summary>
		/// 获取字段信息
		/// </summary>
		/// <returns>字段信息</returns>
		public FieldInfo Field => _field;

		/// <summary>
		/// 获取属性信息
		/// </summary>
		/// <returns>属性信息</returns>
		public PropertyInfo Property => _property;

		/// <summary>
		/// 判断字段是否可读
		/// </summary>
		/// <param name="transientSupport">是否支持transient关键字</param>
		/// <returns>是否可读</returns>
		public bool IsReadable(bool transientSupport)
		{
			if (_field != null)
			{
				// 检查字段是否被transient修饰
				if (transientSupport && _field.IsInitOnly)
				{
					return false;
				}
				return true;
			}

			if (_property != null)
			{
				return _property.CanRead;
			}

			return false;
		}

		/// <summary>
		/// 判断字段是否可写
		/// </summary>
		/// <param name="transientSupport">是否支持transient关键字</param>
		/// <returns>是否可写</returns>
		public bool IsWritable(bool transientSupport)
		{
			if (_field != null)
			{
				// 检查字段是否被transient修饰
				if (transientSupport && _field.IsInitOnly)
				{
					return false;
				}
				return true;
			}

			if (_property != null)
			{
				return _property.CanWrite;
			}

			return false;
		}

		/// <summary>
		/// 获取字段值
		/// </summary>
		/// <param name="obj">对象</param>
		/// <returns>字段值</returns>
		public object GetValue(object obj)
		{
			if (_field != null)
			{
				return _field.GetValue(obj);
			}

			if (_property != null && _property.CanRead)
			{
				return _property.GetValue(obj);
			}

			return null;
		}

		/// <summary>
		/// 设置字段值
		/// </summary>
		/// <param name="obj">对象</param>
		/// <param name="value">值</param>
		/// <param name="ignoreNullValue">是否忽略空值</param>
		/// <param name="ignoreError">是否忽略错误</param>
		/// <param name="override">是否覆盖</param>
		public void SetValue(object obj, object value, bool ignoreNullValue, bool ignoreError, bool @override)
		{
			// 忽略空值
			if (ignoreNullValue && value == null)
			{
				return;
			}

			// 非覆盖模式下，如果目标值不为null，则跳过
			if (!@override && GetValue(obj) != null)
			{
				return;
			}

			try
			{
				if (_field != null)
				{
					_field.SetValue(obj, value);
				}
				else if (_property != null && _property.CanWrite)
				{
					_property.SetValue(obj, value);
				}
			}
			catch (Exception ex)
			{
				if (!ignoreError)
				{
					throw;
				}
			}
		}
	}
}