namespace WellTool.Core.Annotation;

using System;
using System.Collections.Generic;
using System.Reflection;

public class SynthesizedAnnotationProxy
{
	private readonly IAnnotationAttributeValueProvider _attributeValueProvider;
	private readonly ISynthesizedAnnotation _synthesizedAnnotation;
	private readonly Dictionary<string, MethodInfo> _methods = new Dictionary<string, MethodInfo>();

	public static T Create<T>(Type annotationType, IAnnotationAttributeValueProvider attributeValueProvider, ISynthesizedAnnotation synthesizedAnnotation) where T : class
	{
		return Create(annotationType, attributeValueProvider, synthesizedAnnotation) as T;
	}

	public static Attribute Create(Type annotationType, IAnnotationAttributeValueProvider attributeValueProvider, ISynthesizedAnnotation synthesizedAnnotation)
	{
		var proxy = new SynthesizedAnnotationProxy(attributeValueProvider, synthesizedAnnotation);
		return (Attribute)Activator.CreateInstance(annotationType);
	}

	private SynthesizedAnnotationProxy(IAnnotationAttributeValueProvider attributeValueProvider, ISynthesizedAnnotation synthesizedAnnotation)
	{
		_attributeValueProvider = attributeValueProvider;
		_synthesizedAnnotation = synthesizedAnnotation;
		LoadMethods();
	}

	private void LoadMethods()
	{
		var synthesizedType = typeof(ISynthesizedAnnotation);
		foreach (var method in synthesizedType.GetMethods())
		{
			_methods[method.Name] = method;
		}

		var annotationType = _synthesizedAnnotation.AnnotationType();
		foreach (var method in annotationType.GetMethods())
		{
			if (method.DeclaringType == typeof(Attribute) || method.DeclaringType == typeof(object))
				continue;

			var methodName = method.Name;
			if (!_methods.ContainsKey(methodName))
			{
				_methods[methodName] = method;
			}
		}
	}

	public object Invoke(object proxy, MethodInfo method, object[] args)
	{
		var methodName = method.Name;

		if (methodName == "ToString")
		{
			return ProxyToString();
		}
		if (methodName == "GetHashCode")
		{
			return GetHashCode();
		}
		if (methodName == "Equals")
		{
			return Equals(args[0]);
		}

		if (_methods.TryGetValue(methodName, out var targetMethod))
		{
			return targetMethod.Invoke(_synthesizedAnnotation, args);
		}

		if (_attributeValueProvider != null)
		{
			var value = _attributeValueProvider.GetAttributeValue(methodName, method.ReturnType);
			if (value != null)
			{
				return value;
			}
		}

		return method.ReturnType.IsValueType ? Activator.CreateInstance(method.ReturnType) : null;
	}

	private string ProxyToString()
	{
		var annotationType = _synthesizedAnnotation.AnnotationType();
		return $"[@{annotationType.Name}(...)]";
	}

	public interface ISyntheticProxyAnnotation
	{
		ISynthesizedAnnotation GetSynthesizedAnnotation();
	}
}
