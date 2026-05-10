namespace WellTool.Core.Annotation;

using System;

public class AliasLinkAnnotationPostProcessor : AbstractLinkAnnotationPostProcessor
{
	public AliasLinkAnnotationPostProcessor() : base(typeof(AliasForAttribute))
	{
	}

	public override ISynthesizedAnnotation PostProcess(ISynthesizedAnnotation synthesizedAnnotation)
	{
		return synthesizedAnnotation;
	}
}
