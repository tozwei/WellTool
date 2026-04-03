namespace WellTool.Core.Annotation;

using System;
using System.Collections.Generic;
using System.Linq;

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
/// 注解扫描器
/// </summary>
public class AnnotationScanner : IAnnotationScanner
{
	/// <summary>
	/// 不扫描任何注解
	/// </summary>
	public static readonly AnnotationScanner NOTHING = new EmptyScanner();

	/// <summary>
	/// 扫描元素本身直接声明的注解
	/// </summary>
	public static readonly AnnotationScanner DIRECTLY = new GenericScanner(false, false, false);

	private readonly bool _scanMeta;
	private readonly bool _scanSuperclass;
	private readonly bool _scanInterface;

	public AnnotationScanner(bool scanMeta, bool scanSuperclass, bool scanInterface)
	{
		_scanMeta = scanMeta;
		_scanSuperclass = scanSuperclass;
		_scanInterface = scanInterface;
	}

	public virtual bool Support(object element) => element != null;

public virtual IList<Attribute> GetAnnotations(object element)
	{
		var annotations = new List<Attribute>();
		var type = element as Type;
		if (type != null)
		{
			var attrs = type.GetCustomAttributes(true).OfType<Attribute>();
			annotations.AddRange(attrs);
		}
		return annotations;
	}

	private class EmptyScanner : AnnotationScanner
	{
		public EmptyScanner() : base(false, false, false) { }
		public override bool Support(object element) => false;
		public override IList<Attribute> GetAnnotations(object element) => new List<Attribute>();
	}

	private class GenericScanner : AnnotationScanner
	{
		public GenericScanner(bool scanMeta, bool scanSuperclass, bool scanInterface) 
			: base(scanMeta, scanSuperclass, scanInterface) { }
	}
}

public interface IAnnotationScanner
{
	bool Support(object element);
	IList<Attribute> GetAnnotations(object element);
}

/// <summary>
/// 别名注解属性
/// </summary>
public class AliasedAnnotationAttribute : IAnnotationAttribute
{
	private readonly Attribute _annotation;
	private readonly System.Reflection.MethodInfo _method;
	private readonly string _aliasFor;

	public AliasedAnnotationAttribute(Attribute annotation, System.Reflection.MethodInfo method, string aliasFor)
	{
		_annotation = annotation;
		_method = method;
		_aliasFor = aliasFor;
	}

	public Type GetAnnotationType() => _annotation.GetType();
	public string GetAttributeName() => _method.Name;
	public Type GetAttributeType() => _method.ReturnType;

	public object? GetValue() => _method.Invoke(_annotation, null);

	public T? GetAnnotation<T>() where T : Attribute => _method.GetCustomAttribute<T>();

	public bool IsValueEquivalentToDefaultValue()
	{
		var value = GetValue();
		var aliasedMethod = _annotation.GetType().GetMethod(_aliasFor);
		var aliasedValue = aliasedMethod?.Invoke(_annotation, null);
		return Equals(value, aliasedValue);
	}
}

/// <summary>
/// 包装注解属性
/// </summary>
public class WrappedAnnotationAttribute : IAnnotationAttribute
{
	private readonly Attribute _annotation;
	private readonly System.Reflection.MethodInfo _method;

	public WrappedAnnotationAttribute(Attribute annotation, System.Reflection.MethodInfo method)
	{
		_annotation = annotation;
		_method = method;
	}

	public Type GetAnnotationType() => _annotation.GetType();
	public string GetAttributeName() => _method.Name;
	public Type GetAttributeType() => _method.ReturnType;

	public object? GetValue() => _method.Invoke(_annotation, null);

	public T? GetAnnotation<T>() where T : Attribute => _method.GetCustomAttribute<T>();

	public bool IsValueEquivalentToDefaultValue() => false;
}

/// <summary>
/// 镜像注解属性
/// </summary>
public class MirroredAnnotationAttribute : IAnnotationAttribute
{
	private readonly Attribute _annotation;
	private readonly System.Reflection.MethodInfo _method;
	private readonly string _mirrorLink;

	public MirroredAnnotationAttribute(Attribute annotation, System.Reflection.MethodInfo method, string mirrorLink)
	{
		_annotation = annotation;
		_method = method;
		_mirrorLink = mirrorLink;
	}

	public Type GetAnnotationType() => _annotation.GetType();
	public string GetAttributeName() => _method.Name;
	public Type GetAttributeType() => _method.ReturnType;

	public object? GetValue() => _method.Invoke(_annotation, null);

	public T? GetAnnotation<T>() where T : Attribute => _method.GetCustomAttribute<T>();

	public bool IsValueEquivalentToDefaultValue()
	{
		var value = GetValue();
		var mirrorMethod = _annotation.GetType().GetMethod(_mirrorLink);
		var mirrorValue = mirrorMethod?.Invoke(_annotation, null);
		return Equals(value, mirrorValue);
	}
}

/// <summary>
/// 强制别名注解属性
/// </summary>
public class ForceAliasedAnnotationAttribute : IAnnotationAttribute
{
	private readonly Attribute _annotation;
	private readonly System.Reflection.MethodInfo _method;

	public ForceAliasedAnnotationAttribute(Attribute annotation, System.Reflection.MethodInfo method)
	{
		_annotation = annotation;
		_method = method;
	}

	public Type GetAnnotationType() => _annotation.GetType();
	public string GetAttributeName() => _method.Name;
	public Type GetAttributeType() => _method.ReturnType;

	public object? GetValue() => _method.Invoke(_annotation, null);

	public T? GetAnnotation<T>() where T : Attribute => _method.GetCustomAttribute<T>();

	public bool IsValueEquivalentToDefaultValue() => false;
}

/// <summary>
/// 可缓存的注解属性
/// </summary>
public class CacheableAnnotationAttribute : IAnnotationAttribute
{
	private readonly Attribute _annotation;
	private readonly System.Reflection.MethodInfo _method;
	private object? _cachedValue;

	public CacheableAnnotationAttribute(Attribute annotation, System.Reflection.MethodInfo method)
	{
		_annotation = annotation;
		_method = method;
	}

	public Type GetAnnotationType() => _annotation.GetType();
	public string GetAttributeName() => _method.Name;
	public Type GetAttributeType() => _method.ReturnType;

	public object? GetValue() => _cachedValue ??= _method.Invoke(_annotation, null);

	public T? GetAnnotation<T>() where T : Attribute => _method.GetCustomAttribute<T>();

	public bool IsValueEquivalentToDefaultValue() => Equals(GetValue(), _method.GetCustomAttribute<Attribute>());
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
