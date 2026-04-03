namespace WellTool.Core.Annotation;

using System;
using System.Reflection;

/// <summary>
/// 表示注解的某个属性，等同于绑定的调用对象的Method方法。
/// 在SynthesizedAggregateAnnotation的解析以及取值过程中，
/// 可以通过设置SynthesizedAnnotation的注解属性，
/// 从而使得可以从一个注解对象中属性获取另一个注解对象的属性值
/// </summary>
public interface IAnnotationAttribute
{
	/// <summary>
	/// 获取声明属性的注解类
	/// </summary>
	/// <returns>声明注解的注解类</returns>
	Type GetAnnotationType();

	/// <summary>
	/// 获取属性名称
	/// </summary>
	/// <returns>属性名称</returns>
	string GetAttributeName();

	/// <summary>
	/// 获取属性类型
	/// </summary>
	/// <returns>属性类型</returns>
	Type GetAttributeType();

	/// <summary>
	/// 获取注解属性
	/// </summary>
	/// <returns>注解属性值</returns>
	object? GetValue();

	/// <summary>
	/// 获取属性上的注解
	/// </summary>
	/// <typeparam name="T">注解类型</typeparam>
	/// <param name="annotationType">注解类型</param>
	/// <returns>注解对象</returns>
	T? GetAnnotation<T>() where T : Attribute;

	/// <summary>
	/// 该注解属性的值是否等于默认值
	/// </summary>
	/// <returns>是否等于默认值</returns>
	bool IsValueEquivalentToDefaultValue();
}
