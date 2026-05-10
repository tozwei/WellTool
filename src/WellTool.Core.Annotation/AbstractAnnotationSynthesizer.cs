namespace WellTool.Core.Annotation;

using System;

public interface IAnnotationSynthesizer
{
	Attribute? Synthesize(Type annotationType, object annotation);
}

public abstract class AbstractAnnotationSynthesizer : IAnnotationSynthesizer
{
	protected ISynthesizedAnnotationAttributeProcessor[] _attributeProcessors;
	protected ISynthesizedAnnotationPostProcessor[] _postProcessors;

	public abstract Attribute? Synthesize(Type annotationType, object annotation);

	public void SetAttributeProcessors(params ISynthesizedAnnotationAttributeProcessor[] processors)
	{
		_attributeProcessors = processors;
	}

	public void SetPostProcessors(params ISynthesizedAnnotationPostProcessor[] processors)
	{
		_postProcessors = processors;
	}

	protected virtual ISynthesizedAnnotation CreateSynthesizedAnnotation(object root, Attribute annotation,
		int verticalDistance, int horizontalDistance, ISynthesizedAnnotation[] synthesizedAnnotations)
	{
		return new GenericSynthesizedAggregateAnnotation(root, annotation, verticalDistance, horizontalDistance,
			synthesizedAnnotations, _attributeProcessors, _postProcessors);
	}

	protected virtual ISynthesizedAnnotation[] PostProcess(ISynthesizedAnnotation[] synthesizedAnnotations)
	{
		if (_postProcessors == null || _postProcessors.Length == 0)
		{
			return synthesizedAnnotations;
		}

		return synthesizedAnnotations.Select(sa =>
		{
			foreach (var processor in _postProcessors)
			{
				sa = processor.PostProcess(sa);
			}
			return sa;
		}).ToArray();
	}
}
