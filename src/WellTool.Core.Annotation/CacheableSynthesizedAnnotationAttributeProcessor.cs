namespace WellTool.Core.Annotation;

using System;

public class CacheableSynthesizedAnnotationAttributeProcessor : AbstractSynthesizedAnnotationAttributeProcessor
{
	private readonly ISynthesizedAnnotationAttributeProcessor _processor;

	public CacheableSynthesizedAnnotationAttributeProcessor(ISynthesizedAnnotationAttributeProcessor processor)
	{
		_processor = processor ?? throw new ArgumentNullException(nameof(processor));
	}

	public override int Order => _processor.Order;

	public override IAnnotationAttribute Process(IAnnotationAttribute annotationAttribute, ISynthesizedAnnotation synthesizedAnnotation)
	{
		return _processor.Process(annotationAttribute, synthesizedAnnotation);
	}
}
