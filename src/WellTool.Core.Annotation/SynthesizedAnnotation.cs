namespace WellTool.Core.Annotation;

using System;
using System.Reflection;

public class SynthesizedAnnotation : ISynthesizedAnnotation
{
	protected readonly object _root;
	protected readonly Attribute _annotation;
	protected readonly int _verticalDistance;
	protected readonly int _horizontalDistance;

	public SynthesizedAnnotation(object root, Attribute annotation, int verticalDistance, int horizontalDistance)
	{
		_root = root ?? throw new ArgumentNullException(nameof(root));
		_annotation = annotation ?? throw new ArgumentNullException(nameof(annotation));
		_verticalDistance = verticalDistance;
		_horizontalDistance = horizontalDistance;
	}

	public object GetRoot() => _root;

	public Attribute GetAnnotation() => _annotation;

	public Type AnnotationType() => _annotation.GetType();

	public virtual bool HasAttribute(string attributeName)
	{
		var method = AnnotationType().GetMethod(attributeName);
		return method != null;
	}

	public virtual object GetAttributeValue(string attributeName)
	{
		var method = AnnotationType().GetMethod(attributeName);
		if (method == null)
		{
			throw new ArgumentException($"Attribute '{attributeName}' not found on annotation type {AnnotationType().Name}");
		}
		return method.Invoke(_annotation, null);
	}

	public int GetVerticalDistance() => _verticalDistance;

	public int GetHorizontalDistance() => _horizontalDistance;

	public override bool Equals(object obj)
	{
		if (obj == this) return true;
		if (obj is not SynthesizedAnnotation other) return false;

		return Equals(_root, other._root) &&
			   Equals(_annotation, other._annotation) &&
			   _verticalDistance == other._verticalDistance &&
			   _horizontalDistance == other._horizontalDistance;
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(_root, _annotation, _verticalDistance, _horizontalDistance);
	}
}
