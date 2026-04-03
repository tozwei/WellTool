namespace WellTool.Core.Annotation.synthesized;

using System;

/// <summary>
/// 注解属性值提供者接口
/// </summary>
public interface IAnnotationAttributeValueProvider
{
	/// <summary>
	/// 获取属性值
	/// </summary>
	/// <param name="attributeName">属性名</param>
	/// <returns>属性值</returns>
	object? GetAttributeValue(string attributeName);
}

/// <summary>
/// 聚合注解接口
/// </summary>
public interface IAggregateAnnotation
{
	/// <summary>
	/// 获取所有注解
	/// </summary>
	/// <returns>注解列表</returns>
	Attribute[] GetAnnotations();
}

/// <summary>
/// 注解合成器基类
/// </summary>
public abstract class AbstractAnnotationSynthesizer : IAnnotationSynthesizer
{
	public abstract Attribute? Synthesize(Type annotationType, object annotation);
}

/// <summary>
/// 注解合成器接口
/// </summary>
public interface IAnnotationSynthesizer
{
	/// <summary>
	/// 合成注解
	/// </summary>
	/// <param name="annotationType">注解类型</param>
	/// <param name="annotation">注解</param>
	/// <returns>合成后的注解</returns>
	Attribute? Synthesize(Type annotationType, object annotation);
}

/// <summary>
/// 链接注解后处理器基类
/// </summary>
public abstract class AbstractLinkAnnotationPostProcessor : IAnnotationPostProcessor
{
	public abstract Attribute? PostProcess(Attribute annotation);
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
/// 包装注解属性基类
/// </summary>
public abstract class AbstractWrappedAnnotationAttribute : IAnnotationAttribute
{
	protected readonly Attribute _annotation;
	protected readonly System.Reflection.MethodInfo _method;

	protected AbstractWrappedAnnotationAttribute(Attribute annotation, System.Reflection.MethodInfo method)
	{
		_annotation = annotation;
		_method = method;
	}

	public Type GetAnnotationType() => _annotation.GetType();
	public string GetAttributeName() => _method.Name;
	public Type GetAttributeType() => _method.ReturnType;

	public virtual object? GetValue()
{
	return _method.Invoke(_annotation, null);
}

	public T? GetAnnotation<T>() where T : Attribute
	{
		return _method.GetCustomAttribute<T>();
	}

	public abstract bool IsValueEquivalentToDefaultValue();
}

/// <summary>
/// 别名注解属性
/// </summary>
public class AliasedAnnotationAttribute : AbstractWrappedAnnotationAttribute
{
	private readonly string _aliasFor;

	public AliasedAnnotationAttribute(Attribute annotation, System.Reflection.MethodInfo method, string aliasFor)
		: base(annotation, method)
	{
		_aliasFor = aliasFor;
	}

	public override bool IsValueEquivalentToDefaultValue()
	{
		var value = GetValue();
		var aliasedMethod = _annotation.GetType().GetMethod(_aliasFor);
		var aliasedValue = aliasedMethod?.Invoke(_annotation, null);
		return Equals(value, aliasedValue);
	}
}

/// <summary>
/// 强制别名注解属性
/// </summary>
public class ForceAliasedAnnotationAttribute : AbstractWrappedAnnotationAttribute
{
	private readonly string _aliasFor;

	public ForceAliasedAnnotationAttribute(Attribute annotation, System.Reflection.MethodInfo method, string aliasFor)
		: base(annotation, method)
	{
		_aliasFor = aliasFor;
	}

	public override bool IsValueEquivalentToDefaultValue()
	{
		return false;
	}
}

/// <summary>
/// 镜像注解属性
/// </summary>
public class MirroredAnnotationAttribute : AbstractWrappedAnnotationAttribute
{
	private readonly string _mirrorLink;

	public MirroredAnnotationAttribute(Attribute annotation, System.Reflection.MethodInfo method, string mirrorLink)
		: base(annotation, method)
	{
		_mirrorLink = mirrorLink;
	}

	public override bool IsValueEquivalentToDefaultValue()
	{
		var value = GetValue();
		var mirrorMethod = _annotation.GetType().GetMethod(_mirrorLink);
		var mirrorValue = mirrorMethod?.Invoke(_annotation, null);
		return Equals(value, mirrorValue);
	}
}

/// <summary>
/// 包装注解属性
/// </summary>
public class WrappedAnnotationAttribute : AbstractWrappedAnnotationAttribute
{
	public WrappedAnnotationAttribute(Attribute annotation, System.Reflection.MethodInfo method)
		: base(annotation, method)
	{
	}

	public override bool IsValueEquivalentToDefaultValue()
	{
		var value = GetValue();
		return Equals(value, _method.GetCustomAttribute<Attribute>());
	}
}

/// <summary>
/// 可缓存的注解属性
/// </summary>
public class CacheableAnnotationAttribute : AbstractWrappedAnnotationAttribute
{
	private object? _cachedValue;

	public CacheableAnnotationAttribute(Attribute annotation, System.Reflection.MethodInfo method)
		: base(annotation, method)
	{
	}

	public override object? GetValue()
	{
		return _cachedValue ?? (_cachedValue = base.GetValue());
	}

	public override bool IsValueEquivalentToDefaultValue()
	{
		return Equals(GetValue(), _method.GetCustomAttribute<Attribute>());
	}
}

/// <summary>
/// 可缓存的注解合成属性处理器
/// </summary>
public class CacheableSynthesizedAnnotationAttributeProcessor : IAnnotationPostProcessor
{
	public Attribute? PostProcess(Attribute annotation)
	{
		return annotation;
	}
}

/// <summary>
/// 组合注解元素
/// </summary>
public class CombinationAnnotationElement
{
	public Type Type { get; }
	public Attribute Annotation { get; }

	public CombinationAnnotationElement(Type type, Attribute annotation)
	{
		Type = type;
		Annotation = annotation;
	}
}
