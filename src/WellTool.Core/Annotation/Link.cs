using System;

namespace WellTool.Core.Annotation
{
	/// <summary>
	/// <p>用于在同一注解中，或具有一定关联的不同注解的属性中，表明这些属性之间具有特定的关联关系。
	/// 在通过{@link SynthesizedAggregateAnnotation}获取合成注解后，合成注解获取属性值时会根据该注解进行调整。<br>
	/// 
	/// <p>该注解存在三个字注解：{@link MirrorFor}、{@link ForceAliasFor}或{@link AliasFor}，
	/// 使用三个子注解等同于{@link Link}。但是需要注意的是，
	/// 当注解中的属性同时存在多个{@link Link}或基于{@link Link}的子注解时，
	/// 仅有声明在被注解的属性最上方的注解会生效，其余注解都将被忽略。
	/// 
	/// <b>注意：该注解的优先级低于{@link Alias}</b>
	/// </summary>
	/// <author>huangchengxing</author>
	/// <seealso cref="SynthesizedAggregateAnnotation"/>
	/// <seealso cref="RelationType"/>
	/// <seealso cref="AliasForAttribute"/>
	/// <seealso cref="MirrorForAttribute"/>
	/// <seealso cref="ForceAliasForAttribute"/>
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Property)]
	public class LinkAttribute : Attribute
	{
		/// <summary>
		/// 产生关联的注解类型，当不指定时，默认指注释的属性所在的类
		/// </summary>
		/// <returns>关联的注解类型</returns>
		public Type Annotation { get; set; } = typeof(Attribute);

		/// <summary>
		/// {@link #annotation()}指定注解中关联的属性
		/// </summary>
		/// <returns>属性名</returns>
		public string Attribute { get; set; } = "";

		/// <summary>
		/// {@link #attribute()}指定属性与当前注解的属性建的关联关系类型
		/// </summary>
		/// <returns>关系类型</returns>
		public RelationType Type { get; set; } = RelationType.MIRROR_FOR;
	}
}