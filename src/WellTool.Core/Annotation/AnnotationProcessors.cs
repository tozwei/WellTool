namespace WellTool.Core.Annotation.processors;

using System;
using System.Collections.Generic;

/// <summary>
/// 合成注解代理
/// </summary>
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
	/// <param name="candidates">候选注解</param>
	/// <returns>选中的注解</returns>
	public Attribute? Select(IEnumerable<Attribute> candidates)
	{
		Attribute? selected = null;
		foreach (var candidate in candidates)
		{
			if (selected == null || GetPriority(candidate) > GetPriority(selected))
			{
				selected = candidate;
			}
		}
		return selected;
	}

	private int GetPriority(Attribute annotation)
	{
		var type = annotation.GetType();
		var priorityProp = type.GetProperty("Priority");
		return priorityProp != null ? (int)(priorityProp.GetValue(annotation) ?? 0) : 0;
	}
}

/// <summary>
/// 别名注解后处理器
/// </summary>
public class AliasAnnotationPostProcessor : IAnnotationPostProcessor
{
	public Attribute? PostProcess(Attribute annotation)
	{
		return annotation;
	}
}

/// <summary>
/// 别名链接注解后处理器
/// </summary>
public class AliasLinkAnnotationPostProcessor : IAnnotationPostProcessor
{
	public Attribute? PostProcess(Attribute annotation)
	{
		return annotation;
	}
}

/// <summary>
/// 镜像链接注解后处理器
/// </summary>
public class MirrorLinkAnnotationPostProcessor : IAnnotationPostProcessor
{
	public Attribute? PostProcess(Attribute annotation)
	{
		return annotation;
	}
}

/// <summary>
/// 合成注解属性处理器
/// </summary>
public class SynthesizedAnnotationAttributeProcessor : IAnnotationPostProcessor
{
	public Attribute? PostProcess(Attribute annotation)
	{
		return annotation;
	}
}

/// <summary>
/// 合成注解后处理器
/// </summary>
public class SynthesizedAnnotationPostProcessor : IAnnotationPostProcessor
{
	public Attribute? PostProcess(Attribute annotation)
	{
		return annotation;
	}
}

/// <summary>
/// 通用合成聚合注解
/// </summary>
public class GenericSynthesizedAggregateAnnotation : Attribute
{
}

/// <summary>
/// 通用合成注解
/// </summary>
public class GenericSynthesizedAnnotation : Attribute
{
}

/// <summary>
/// 合成聚合注解
/// </summary>
public class SynthesizedAggregateAnnotation : Attribute
{
}

/// <summary>
/// 合成注解
/// </summary>
public class SynthesizedAnnotation : Attribute
{
}
