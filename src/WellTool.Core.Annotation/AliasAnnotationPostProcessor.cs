namespace WellTool.Core.Annotation;

using System;

public class AliasAnnotationPostProcessor : AbstractLinkAnnotationPostProcessor
{
	public AliasAnnotationPostProcessor() : base(typeof(AliasForAttribute))
	{
	}

	public override ISynthesizedAnnotation PostProcess(ISynthesizedAnnotation synthesizedAnnotation)
	{
		return synthesizedAnnotation;
	}
}
