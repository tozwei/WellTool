namespace WellTool.Core.Annotation;

using System;

public interface IWrappedAnnotationAttribute : IAnnotationAttribute
{
	IAnnotationAttribute GetOriginal();

	IAnnotationAttribute GetLinked();

	IAnnotationAttribute GetNonWrappedOriginal();
}
