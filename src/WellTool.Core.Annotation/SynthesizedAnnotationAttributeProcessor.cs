namespace WellTool.Core.Annotation;

using System;

public class SynthesizedAnnotationAttributeProcessor : AbstractSynthesizedAnnotationAttributeProcessor
{
	public override int Order => 0;

	public override IAnnotationAttribute Process(IAnnotationAttribute annotationAttribute, ISynthesizedAnnotation synthesizedAnnotation)
	{
		return annotationAttribute;
	}
}
