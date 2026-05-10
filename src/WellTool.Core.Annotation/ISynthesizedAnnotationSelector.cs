namespace WellTool.Core.Annotation;

using System;

public interface ISynthesizedAnnotationSelector
{
	ISynthesizedAnnotation Select(ISynthesizedAnnotation[] synthesizedAnnotations);
}
