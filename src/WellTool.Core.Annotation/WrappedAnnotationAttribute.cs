namespace WellTool.Core.Annotation;

using System;

public class WrappedAnnotationAttribute : AbstractWrappedAnnotationAttribute, IWrappedAnnotationAttribute
{
	public WrappedAnnotationAttribute(IAnnotationAttribute original, IAnnotationAttribute linked)
		: base(original, linked)
	{
	}

	public override object? GetValue()
	{
		return _linked.GetValue();
	}

	public override bool IsValueEquivalentToDefaultValue()
	{
		return _linked.IsValueEquivalentToDefaultValue();
	}
}
