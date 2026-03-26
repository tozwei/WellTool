using System;

namespace WellTool.Core.Annotation
{
	/// <summary>
	/// 别名注解，使用此注解的字段、方法、参数等会有一个别名，用于Bean拷贝、Bean转Map等
	/// </summary>
	/// <author>Looly</author>
	/// <since>5.1.1</since>
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Parameter)]
	public class AliasAttribute : Attribute
	{
		/// <summary>
		/// 别名值，即使用此注解要替换成的别名名称
		/// </summary>
		/// <returns>别名值</returns>
		public string Value { get; set; }

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="value">别名值</param>
		public AliasAttribute(string value)
		{
			Value = value;
		}
	}
}