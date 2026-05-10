namespace WellTool.Core.Annotation;

using System;

public class GenericSynthesizedAnnotation : SynthesizedAnnotation
{
	public GenericSynthesizedAnnotation(object root, Attribute annotation, int verticalDistance, int horizontalDistance)
		: base(root, annotation, verticalDistance, horizontalDistance)
	{
	}

	public T? GetAnnotation<T>() where T : Attribute
	{
		return _annotation as T;
	}

	public object GetAttributeValue(string attributeName, Type attributeType)
	{
		var value = GetAttributeValue(attributeName);
		return Convert.ChangeType(value, attributeType);
	}

	public T GetAttributeValue<T>(string attributeName)
	{
		var value = GetAttributeValue(attributeName);
		return (T)Convert.ChangeType(value, typeof(T));
	}
}
