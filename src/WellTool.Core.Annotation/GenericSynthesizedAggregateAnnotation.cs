namespace WellTool.Core.Annotation;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class GenericSynthesizedAggregateAnnotation : SynthesizedAnnotation, IAggregateAnnotation
{
	private readonly ISynthesizedAnnotation[] _synthesizedAnnotations;
	private readonly ISynthesizedAnnotationAttributeProcessor[] _attributeProcessors;
	private readonly ISynthesizedAnnotationPostProcessor[] _postProcessors;

	public GenericSynthesizedAggregateAnnotation(object root, Attribute annotation, int verticalDistance,
		int horizontalDistance, ISynthesizedAnnotation[] synthesizedAnnotations,
		ISynthesizedAnnotationAttributeProcessor[] attributeProcessors,
		ISynthesizedAnnotationPostProcessor[] postProcessors)
		: base(root, annotation, verticalDistance, horizontalDistance)
	{
		_synthesizedAnnotations = synthesizedAnnotations ?? Array.Empty<ISynthesizedAnnotation>();
		_attributeProcessors = attributeProcessors ?? Array.Empty<ISynthesizedAnnotationAttributeProcessor>();
		_postProcessors = postProcessors ?? Array.Empty<ISynthesizedAnnotationPostProcessor>();
	}

	public bool IsAnnotationPresent(Type annotationType)
	{
		return _synthesizedAnnotations.Any(sa => annotationType.IsAssignableFrom(sa.AnnotationType()));
	}

	public Attribute[] GetAnnotations()
	{
		return _synthesizedAnnotations.Select(sa => sa.GetAnnotation()).ToArray();
	}

	public override object GetAttributeValue(string attributeName)
	{
		var attribute = GetAttribute(attributeName);
		return attribute?.GetValue();
	}

	public IAnnotationAttribute GetAttribute(string attributeName)
	{
		var synthesizedAnnotation = SelectSynthesizedAnnotation(attributeName);
		if (synthesizedAnnotation == null)
		{
			return null;
		}

		var attribute = CreateAnnotationAttribute(synthesizedAnnotation, attributeName);
		return ProcessAttribute(attribute, synthesizedAnnotation);
	}

	protected virtual ISynthesizedAnnotation SelectSynthesizedAnnotation(string attributeName)
	{
		foreach (var postProcessor in _postProcessors)
		{
			var selector = postProcessor.Selector();
			if (selector != null)
			{
				var selected = selector.Select(_synthesizedAnnotations);
				if (selected != null)
				{
					return selected;
				}
			}
		}
		return _synthesizedAnnotations.FirstOrDefault();
	}

	protected virtual IAnnotationAttribute CreateAnnotationAttribute(ISynthesizedAnnotation synthesizedAnnotation, string attributeName)
	{
		var annotation = synthesizedAnnotation.GetAnnotation();
		var method = annotation.GetType().GetMethod(attributeName);
		if (method == null)
		{
			return null;
		}
		return new SimpleAnnotationAttribute(annotation, method);
	}

	protected virtual IAnnotationAttribute ProcessAttribute(IAnnotationAttribute attribute, ISynthesizedAnnotation synthesizedAnnotation)
	{
		if (attribute == null || synthesizedAnnotation == null)
		{
			return attribute;
		}

		foreach (var processor in _attributeProcessors.OrderBy(p => p.Order))
		{
			attribute = processor.Process(attribute, synthesizedAnnotation);
			if (attribute == null)
			{
				break;
			}
		}

		return attribute;
	}

	private class SimpleAnnotationAttribute : IAnnotationAttribute
	{
		private readonly Attribute _annotation;
		private readonly MethodInfo _method;

		public SimpleAnnotationAttribute(Attribute annotation, MethodInfo method)
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

		public bool IsWrapped() => false;
	}
}
