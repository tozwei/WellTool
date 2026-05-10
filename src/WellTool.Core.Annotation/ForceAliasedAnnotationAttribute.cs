namespace WellTool.Core.Annotation;

using System;

public class ForceAliasedAnnotationAttribute : AliasedAnnotationAttribute
{
	public ForceAliasedAnnotationAttribute(IAnnotationAttribute original, IAnnotationAttribute linked)
		: base(original, linked)
	{
	}
}
