using System;

namespace WellTool.Core.Annotation
{
	/// <summary>
	/// <p>{@link Link}的子注解。表示“原始属性”将强制作为“关联属性”的别名。效果等同于在“原始属性”上添加{@link Alias}注解，
	/// 任何情况下，获取“关联属性”的值都将直接返回“原始属性”的值
	/// <b>注意，该注解与{@link Link}、{@link AliasFor}或{@link MirrorFor}一起使用时，将只有被声明在最上面的注解会生效</b>
	/// </summary>
	/// <author>huangchengxing</author>
	/// <seealso cref="LinkAttribute"/>
	/// <seealso cref="RelationType.FORCE_ALIAS_FOR"/>
	[LinkAttribute(Type = RelationType.FORCE_ALIAS_FOR)]
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
	public class ForceAliasForAttribute : Attribute
	{
		/// <summary>
		/// 产生关联的注解类型，当不指定时，默认指注释的属性所在的类
		/// </summary>
		/// <returns>关联注解类型</returns>
		[LinkAttribute(Annotation = typeof(LinkAttribute), Attribute = "Attribute", Type = RelationType.FORCE_ALIAS_FOR)]
		public Type Annotation { get; set; } = typeof(Attribute);

		/// <summary>
		/// {@link #annotation()}指定注解中关联的属性
		/// </summary>
		/// <returns>关联的属性</returns>
		[LinkAttribute(Annotation = typeof(LinkAttribute), Attribute = "Attribute", Type = RelationType.FORCE_ALIAS_FOR)]
		public string Attribute { get; set; } = "";
	}
}