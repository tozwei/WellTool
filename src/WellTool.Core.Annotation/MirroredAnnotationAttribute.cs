namespace WellTool.Core.Annotation;

using System;

public class MirroredAnnotationAttribute : AbstractWrappedAnnotationAttribute
{
	public MirroredAnnotationAttribute(IAnnotationAttribute original, IAnnotationAttribute linked)
		: base(original, linked)
	{
	}

	public override object? GetValue()
	{
		return _linked.GetValue();
	}
}
