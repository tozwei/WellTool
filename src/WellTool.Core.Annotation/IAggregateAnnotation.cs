namespace WellTool.Core.Annotation;

using System;

public interface IAggregateAnnotation
{
	bool IsAnnotationPresent(Type annotationType);

	Attribute[] GetAnnotations();
}
