namespace WellTool.Core.Annotation;

using System;
using System.Collections.Concurrent;

public abstract class AbstractSynthesizedAnnotationAttributeProcessor : ISynthesizedAnnotationAttributeProcessor
{
	private readonly ConcurrentDictionary<IAnnotationAttribute, IAnnotationAttribute> _processedCache = new ConcurrentDictionary<IAnnotationAttribute, IAnnotationAttribute>();

	public abstract IAnnotationAttribute Process(IAnnotationAttribute annotationAttribute, ISynthesizedAnnotation synthesizedAnnotation);

	public abstract int Order { get; }

	protected virtual IAnnotationAttribute ProcessInternal(IAnnotationAttribute annotationAttribute, ISynthesizedAnnotation synthesizedAnnotation)
	{
		return annotationAttribute;
	}

	public IAnnotationAttribute ProcessWithCache(IAnnotationAttribute annotationAttribute, ISynthesizedAnnotation synthesizedAnnotation)
	{
		if (annotationAttribute == null)
		{
			return null;
		}

		return _processedCache.GetOrAdd(annotationAttribute, key => Process(key, synthesizedAnnotation));
	}

	protected virtual bool ShouldProcess(IAnnotationAttribute annotationAttribute, ISynthesizedAnnotation synthesizedAnnotation)
	{
		return true;
	}
}
