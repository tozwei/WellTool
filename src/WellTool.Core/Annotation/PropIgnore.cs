using System;

namespace WellTool.Core.Annotation
{
	/// <summary>
	/// 属性忽略注解，使用此注解的字段等会被忽略，主要用于Bean拷贝、Bean转Map等<br>
	/// 此注解应用于字段时，忽略读取和设置属性值，应用于setXXX方法忽略设置值，应用于getXXX忽略读取值
	/// </summary>
	/// <author>Looly</author>
	/// <since>5.4.2</since>
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Parameter)]
	public class PropIgnoreAttribute : Attribute
	{
	}
}