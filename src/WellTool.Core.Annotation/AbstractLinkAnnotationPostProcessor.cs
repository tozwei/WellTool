namespace WellTool.Core.Annotation;

using System;

public interface IAnnotationPostProcessor
{
	Attribute? PostProcess(Attribute annotation);
}

public abstract class SynthesizedAnnotationPostProcessor : ISynthesizedAnnotationPostProcessor
{
	public abstract ISynthesizedAnnotation PostProcess(ISynthesizedAnnotation synthesizedAnnotation);

	public virtual ISynthesizedAnnotationSelector Selector()
	{
		return SynthesizedAnnotationSelector.Instance;
	}
}

public abstract class AbstractLinkAnnotationPostProcessor : SynthesizedAnnotationPostProcessor
{
	protected readonly Type _linkAnnotationType;

	protected AbstractLinkAnnotationPostProcessor(Type linkAnnotationType)
	{
		_linkAnnotationType = linkAnnotationType ?? throw new ArgumentNullException(nameof(linkAnnotationType));
	}

	protected virtual bool IsLinkAnnotation(Attribute annotation)
	{
		return annotation != null && _linkAnnotationType.IsAssignableFrom(annotation.GetType());
	}

	protected virtual bool IsLinkAnnotation(Type annotationType)
	{
		return annotationType != null && _linkAnnotationType.IsAssignableFrom(annotationType);
	}
}
