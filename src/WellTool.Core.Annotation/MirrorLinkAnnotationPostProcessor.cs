namespace WellTool.Core.Annotation;

using System;

public class MirrorLinkAnnotationPostProcessor : AbstractLinkAnnotationPostProcessor
{
	public MirrorLinkAnnotationPostProcessor() : base(typeof(MirrorForAttribute))
	{
	}

	public override ISynthesizedAnnotation PostProcess(ISynthesizedAnnotation synthesizedAnnotation)
	{
		return synthesizedAnnotation;
	}
}
