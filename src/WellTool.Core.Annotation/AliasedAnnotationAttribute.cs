namespace WellTool.Core.Annotation;

using System;

public class AliasedAnnotationAttribute : AbstractWrappedAnnotationAttribute
{
	public AliasedAnnotationAttribute(IAnnotationAttribute original, IAnnotationAttribute linked)
		: base(original, linked)
	{
	}

	public override object? GetValue()
	{
		return _linked.GetValue();
	}
}
