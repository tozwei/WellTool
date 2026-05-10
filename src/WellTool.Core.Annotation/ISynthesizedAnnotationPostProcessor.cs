namespace WellTool.Core.Annotation;

using System;

public interface ISynthesizedAnnotationPostProcessor
{
	ISynthesizedAnnotation PostProcess(ISynthesizedAnnotation synthesizedAnnotation);

	ISynthesizedAnnotationSelector Selector();
}
