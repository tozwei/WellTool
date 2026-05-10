namespace WellTool.Core.Annotation;

using System;
using System.Reflection;

public interface IAnnotationAttribute
{
	Type GetAnnotationType();

	string GetAttributeName();

	Type GetAttributeType();

	object? GetValue();

	T? GetAnnotation<T>() where T : Attribute;

	bool IsValueEquivalentToDefaultValue();

	bool IsWrapped();
}
