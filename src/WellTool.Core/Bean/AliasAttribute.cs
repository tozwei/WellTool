using System;

namespace WellTool.Core.Bean
{
	/// <summary>
	/// 字段别名注解
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
	public class AliasAttribute : Attribute
	{
		/// <summary>
		/// 别名值
		/// </summary>
		public string Value { get; }

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="value">别名值</param>
		public AliasAttribute(string value)
		{
			Value = value;
		}
	}

	/// <summary>
	/// 字段别名注解（简写）
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
	public class Alias : AliasAttribute
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="value">别名值</param>
		public Alias(string value) : base(value)
		{}
	}
}