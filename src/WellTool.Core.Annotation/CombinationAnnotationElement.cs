namespace WellTool.Core.Annotation;

using System;
using System.Collections.Generic;

public class CombinationAnnotationElement
{
	private readonly List<Attribute> _annotations = new List<Attribute>();

	public CombinationAnnotationElement Add(Attribute annotation)
	{
		if (annotation != null)
		{
			_annotations.Add(annotation);
		}
		return this;
	}

	public CombinationAnnotationElement AddAll(IEnumerable<Attribute> annotations)
	{
		if (annotations != null)
		{
			_annotations.AddRange(annotations);
		}
		return this;
	}

	public Attribute[] ToArray()
	{
		return _annotations.ToArray();
	}

	public List<Attribute> ToList()
	{
		return new List<Attribute>(_annotations);
	}

	public int Size()
	{
		return _annotations.Count;
	}

	public bool IsEmpty()
	{
		return _annotations.Count == 0;
	}

	public bool Contains(Type annotationType)
	{
		foreach (var annotation in _annotations)
		{
			if (annotationType.IsAssignableFrom(annotation.GetType()))
			{
				return true;
			}
		}
		return false;
	}

	public Attribute FindAnnotation(Type annotationType)
	{
		foreach (var annotation in _annotations)
		{
			if (annotationType.IsAssignableFrom(annotation.GetType()))
			{
				return annotation;
			}
		}
		return null;
	}
}
