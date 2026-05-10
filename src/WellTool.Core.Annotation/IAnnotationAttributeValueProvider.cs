namespace WellTool.Core.Annotation;

using System;

public interface IAnnotationAttributeValueProvider
{
	object? GetAttributeValue(string attributeName, Type attributeType);
}
