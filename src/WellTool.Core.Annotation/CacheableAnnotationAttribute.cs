namespace WellTool.Core.Annotation;

using System;

public class CacheableAnnotationAttribute : IAnnotationAttribute
{
	private readonly IAnnotationAttribute _annotationAttribute;
	private object? _value;
	private bool _valueCached;

	public CacheableAnnotationAttribute(IAnnotationAttribute annotationAttribute)
	{
		_annotationAttribute = annotationAttribute ?? throw new ArgumentNullException(nameof(annotationAttribute));
	}

	public Type GetAnnotationType() => _annotationAttribute.GetAnnotationType();

	public string GetAttributeName() => _annotationAttribute.GetAttributeName();

	public Type GetAttributeType() => _annotationAttribute.GetAttributeType();

	public object? GetValue()
	{
		if (!_valueCached)
		{
			_value = _annotationAttribute.GetValue();
			_valueCached = true;
		}
		return _value;
	}

	public T? GetAnnotation<T>() where T : Attribute => _annotationAttribute.GetAnnotation<T>();

	public bool IsValueEquivalentToDefaultValue() => _annotationAttribute.IsValueEquivalentToDefaultValue();

	public bool IsWrapped() => _annotationAttribute.IsWrapped();
}
