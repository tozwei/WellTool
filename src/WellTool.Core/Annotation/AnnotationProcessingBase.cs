using System.Reflection;

namespace WellTool.Core.Annotation;

/// <summary>
/// 注解合成器基类
/// </summary>
public abstract class AbstractAnnotationSynthesizer
{
	/// <summary>
	/// 合成注解
	/// </summary>
	/// <param name="annotationType">注解类型</param>
	/// <param name="annotation">注解</param>
	/// <returns>合成后的注解</returns>
	protected abstract Attribute? SynthesizeInternal(Type annotationType, Attribute annotation);

	/// <summary>
	/// 合成注解
	/// </summary>
	/// <param name="annotationType">注解类型</param>
	/// <param name="annotation">注解</param>
	/// <returns>合成后的注解</returns>
	public Attribute? Synthesize(Type annotationType, object annotation)
	{
		if (annotation is Attribute attr)
		{
			return SynthesizeInternal(annotationType, attr);
		}
		return null;
	}
}

/// <summary>
/// 链接注解后处理器基类
/// </summary>
public abstract class AbstractLinkAnnotationPostProcessor : IAnnotationPostProcessor
{
	/// <summary>
	/// 后处理注解
	/// </summary>
	/// <param name="annotation">注解</param>
	/// <returns>处理后的注解</returns>
	protected abstract Attribute? PostProcessInternal(Attribute annotation);

	/// <summary>
	/// 后处理注解
	/// </summary>
	/// <param name="annotation">注解</param>
	/// <returns>处理后的注解</returns>
	public Attribute? PostProcess(Attribute annotation)
	{
		return PostProcessInternal(annotation);
	}
}

/// <summary>
/// 包装注解属性基类
/// </summary>
public abstract class AbstractWrappedAnnotationAttribute
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

	public object? GetValue() => _method.Invoke(_annotation, null);

	public T? GetAnnotation<T>() where T : Attribute => _method.GetCustomAttribute<T>();

	public virtual bool IsValueEquivalentToDefaultValue() => false;
}
