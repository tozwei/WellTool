namespace WellTool.Core.Annotation;

using System;
using System.Collections.Generic;

public abstract class AbstractWrappedAnnotationAttribute : IAnnotationAttribute
{
	protected readonly IAnnotationAttribute _original;
	protected readonly IAnnotationAttribute _linked;

	protected AbstractWrappedAnnotationAttribute(IAnnotationAttribute original, IAnnotationAttribute linked)
	{
		if (original == null)
			throw new ArgumentNullException(nameof(original));
		if (linked == null)
			throw new ArgumentNullException(nameof(linked));

		_original = original;
		_linked = linked;
	}

	public virtual IAnnotationAttribute GetOriginal() => _original;

	public virtual IAnnotationAttribute GetLinked() => _linked;

	public virtual IAnnotationAttribute GetNonWrappedOriginal()
	{
		IAnnotationAttribute curr = null;
		IAnnotationAttribute next = _original;
		while (next != null)
		{
			curr = next;
			next = curr.IsWrapped() ? ((AbstractWrappedAnnotationAttribute)curr).GetOriginal() : null;
		}
		return curr;
	}

	public virtual IEnumerable<IAnnotationAttribute> GetAllLinkedNonWrappedAttributes()
	{
		var leafAttributes = new List<IAnnotationAttribute>();
		CollectLeafAttribute(this, leafAttributes);
		return leafAttributes;
	}

	private void CollectLeafAttribute(IAnnotationAttribute curr, List<IAnnotationAttribute> leafAttributes)
	{
		if (curr == null)
		{
			return;
		}
		if (!curr.IsWrapped())
		{
			leafAttributes.Add(curr);
			return;
		}
		var wrappedAttribute = (AbstractWrappedAnnotationAttribute)curr;
		CollectLeafAttribute(wrappedAttribute.GetOriginal(), leafAttributes);
		CollectLeafAttribute(wrappedAttribute.GetLinked(), leafAttributes);
	}

	public virtual Type GetAnnotationType() => _original.GetAnnotationType();

	public virtual string GetAttributeName() => _original.GetAttributeName();

	public virtual Type GetAttributeType() => _original.GetAttributeType();

	public virtual object? GetValue() => _original.GetValue();

	public virtual T? GetAnnotation<T>() where T : Attribute => _original.GetAnnotation<T>();

	public virtual bool IsValueEquivalentToDefaultValue() => _original.IsValueEquivalentToDefaultValue();

	public bool IsWrapped() => true;
}
