using System;
using System.Reflection;

namespace WellTool.Core.Annotation
{
    /// <summary>
    /// 注解属性接口
    /// </summary>
    public interface IAnnotationAttribute
    {
        /// <summary>
        /// 获取注解对象
        /// </summary>
        Annotation GetAnnotation();

        /// <summary>
        /// 获取注解属性对应的方法
        /// </summary>
        MethodInfo GetAttribute();

        /// <summary>
        /// 获取声明属性的注解类
        /// </summary>
        Type GetAnnotationType();

        /// <summary>
        /// 获取属性名称
        /// </summary>
        string GetAttributeName();

        /// <summary>
        /// 获取属性值
        /// </summary>
        object GetValue();

        /// <summary>
        /// 该注解属性的值是否等于默认值
        /// </summary>
        bool IsValueEquivalentToDefaultValue();

        /// <summary>
        /// 获取属性类型
        /// </summary>
        Type GetAttributeType();

        /// <summary>
        /// 当前注解属性是否已经被包装
        /// </summary>
        bool IsWrapped();
    }
}