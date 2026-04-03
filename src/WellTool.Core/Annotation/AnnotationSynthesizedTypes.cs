namespace WellTool.Core.Annotation;

/// <summary>
/// 聚合注解
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field)]
public class AggregateAnnotation : Attribute
{
}

/// <summary>
/// 通用合成聚合注解
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field)]
public class GenericSynthesizedAggregateAnnotation : Attribute
{
}

/// <summary>
/// 通用合成注解
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field)]
public class GenericSynthesizedAnnotation : Attribute
{
}

/// <summary>
/// 合成聚合注解
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field)]
public class SynthesizedAggregateAnnotation : Attribute
{
}

/// <summary>
/// 合成注解
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field)]
public class SynthesizedAnnotation : Attribute
{
}

/// <summary>
/// 注解属性值提供者
/// </summary>
public interface IAnnotationAttributeProvider
{
	/// <summary>
	/// 获取属性值
	/// </summary>
	/// <param name="name">属性名</param>
	/// <returns>属性值</returns>
	object? GetAttribute(string name);
}

/// <summary>
/// 别名注解后处理器
/// </summary>
public class AliasAnnotationPostProcessor : IAnnotationPostProcessor
{
	public Attribute? PostProcess(Attribute annotation) => annotation;
}

/// <summary>
/// 别名链接注解后处理器
/// </summary>
public class AliasLinkAnnotationPostProcessor : IAnnotationPostProcessor
{
	public Attribute? PostProcess(Attribute annotation) => annotation;
}

/// <summary>
/// 镜像链接注解后处理器
/// </summary>
public class MirrorLinkAnnotationPostProcessor : IAnnotationPostProcessor
{
	public Attribute? PostProcess(Attribute annotation) => annotation;
}

/// <summary>
/// 合成注解属性处理器
/// </summary>
public class SynthesizedAnnotationAttributeProcessor : IAnnotationPostProcessor
{
	public Attribute? PostProcess(Attribute annotation) => annotation;
}

/// <summary>
/// 合成注解后处理器
/// </summary>
public class SynthesizedAnnotationPostProcessor : IAnnotationPostProcessor
{
	public Attribute? PostProcess(Attribute annotation) => annotation;
}

/// <summary>
/// 可缓存的注解合成属性处理器
/// </summary>
public class CacheableSynthesizedAnnotationAttributeProcessor : IAnnotationPostProcessor
{
	public Attribute? PostProcess(Attribute annotation) => annotation;
}

/// <summary>
/// 注解后处理器接口
/// </summary>
public interface IAnnotationPostProcessor
{
	/// <summary>
	/// 后处理注解
	/// </summary>
	/// <param name="annotation">注解</param>
	/// <returns>处理后的注解</returns>
	Attribute? PostProcess(Attribute annotation);
}

/// <summary>
/// 合成注解代理
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class SynthesizedAnnotationProxy : Attribute
{
}

/// <summary>
/// 合成注解选择器
/// </summary>
public class SynthesizedAnnotationSelector
{
	/// <summary>
	/// 选择注解
	/// </summary>
	/// <param name="candidates">候选注解列表</param>
	/// <returns>选中的注解</returns>
	public Attribute? Select(System.Collections.Generic.IEnumerable<Attribute> candidates)
	{
		Attribute? selected = null;
		foreach (var candidate in candidates)
		{
			if (selected == null)
			{
				selected = candidate;
			}
		}
		return selected;
	}
}
