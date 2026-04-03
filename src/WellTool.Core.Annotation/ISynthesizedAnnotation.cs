using System;
using System.Reflection;

namespace WellTool.Core.Annotation
{
    /// <summary>
    /// 合成注解接口
    /// </summary>
    public interface ISynthesizedAnnotation : Annotation
    {
        /// <summary>
        /// 获取根对象
        /// </summary>
        object GetRoot();

        /// <summary>
        /// 获取被合成的注解实例
        /// </summary>
        Annotation GetAnnotation();

        /// <summary>
        /// 返回注解类型
        /// </summary>
        Type AnnotationType();

        /// <summary>
        /// 检查是否存在指定属性
        /// </summary>
        bool HasAttribute(string attributeName);

        /// <summary>
        /// 获取属性值
        /// </summary>
        object GetAttributeValue(string attributeName);

        /// <summary>
        /// 获取垂直距离（注解与根对象的层级数）
        /// </summary>
        int GetVerticalDistance();

        /// <summary>
        /// 获取水平距离（扫描到的注解数）
        /// </summary>
        int GetHorizontalDistance();
    }
}