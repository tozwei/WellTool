namespace WellTool.Core.Annotation;

using System;

public interface ISynthesizedAnnotationAttributeProcessor
{
	IAnnotationAttribute Process(IAnnotationAttribute annotationAttribute, ISynthesizedAnnotation synthesizedAnnotation);

	int Order { get; }
}
