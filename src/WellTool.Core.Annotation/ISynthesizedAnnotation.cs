namespace WellTool.Core.Annotation;

using System;

public interface ISynthesizedAnnotation
{
	object GetRoot();

	Attribute GetAnnotation();

	Type AnnotationType();

	bool HasAttribute(string attributeName);

	object GetAttributeValue(string attributeName);

	int GetVerticalDistance();

	int GetHorizontalDistance();
}
