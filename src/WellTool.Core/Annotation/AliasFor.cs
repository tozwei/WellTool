using System;

namespace WellTool.Core.Annotation
{
	/// <summary>
	/// <p>{@link Link}的子注解。表示“原始属性”将作为“关联属性”的别名。
	/// <ul>
	///     <li>当“原始属性”为默认值时，获取“关联属性”将返回“关联属性”本身的值；</li>
	///     <li>当“原始属性”不为默认值时，获取“关联属性”将返回“原始属性”的值；</li>
	/// </ul>
	/// <b>注意，该注解与{@link Link}、{@link ForceAliasFor}或{@link MirrorFor}一起使用时，将只有被声明在最上面的注解会生效</b>
	/// </summary>
	/// <author>huangchengxing</author>
	/// <seealso cref="LinkAttribute"/>
	/// <seealso cref="RelationType.ALIAS_FOR"/>
	[LinkAttribute(Type = RelationType.ALIAS_FOR)]
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
	public class AliasForAttribute : Attribute
	{
		/// <summary>
		/// 产生关联的注解类型，当不指定时，默认指注释的属性所在的类
		/// </summary>
		/// <returns>注解类型</returns>
		[LinkAttribute(Annotation = typeof(LinkAttribute), Attribute = "Attribute", Type = RelationType.FORCE_ALIAS_FOR)]
		public Type Annotation { get; set; } = typeof(Attribute);

		/// <summary>
		/// {@link #annotation()}指定注解中关联的属性
		/// </summary>
		/// <returns>关联属性</returns>
		[LinkAttribute(Annotation = typeof(LinkAttribute), Attribute = "Attribute", Type = RelationType.FORCE_ALIAS_FOR)]
		public string Attribute { get; set; } = "";
	}
}