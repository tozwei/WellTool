namespace WellTool.Core.Annotation;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public interface IAnnotationScanner
{
	bool Support(object element);
	IList<Attribute> GetAnnotations(object element);
}

public class AnnotationScanner : IAnnotationScanner
{
	public static readonly AnnotationScanner NOTHING = new EmptyScanner();

	public static readonly AnnotationScanner DIRECTLY = new GenericScanner(false, false, false);

	private readonly bool _scanMeta;
	private readonly bool _scanSuperclass;
	private readonly bool _scanInterface;

	public AnnotationScanner(bool scanMeta, bool scanSuperclass, bool scanInterface)
	{
		_scanMeta = scanMeta;
		_scanSuperclass = scanSuperclass;
		_scanInterface = scanInterface;
	}

	public virtual bool Support(object element) => element != null;

	public virtual IList<Attribute> GetAnnotations(object element)
	{
		var annotations = new List<Attribute>();
		var type = element as Type;
		if (type != null)
		{
			var attrs = type.GetCustomAttributes(true).OfType<Attribute>();
			annotations.AddRange(attrs);
		}
		return annotations;
	}

	private class EmptyScanner : AnnotationScanner
	{
		public EmptyScanner() : base(false, false, false) { }
		public override bool Support(object element) => false;
		public override IList<Attribute> GetAnnotations(object element) => new List<Attribute>();
	}

	private class GenericScanner : AnnotationScanner
	{
		public GenericScanner(bool scanMeta, bool scanSuperclass, bool scanInterface) 
			: base(scanMeta, scanSuperclass, scanInterface) { }
	}
}
