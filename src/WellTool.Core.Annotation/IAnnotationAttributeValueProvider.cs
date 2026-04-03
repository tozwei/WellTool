using System;

namespace WellTool.Core.Annotation
{
    /// <summary>
    /// 注解属性值提供者接口
    /// </summary>
    public interface IAnnotationAttributeValueProvider
    {
        /// <summary>
        /// 获取注解属性值
        /// </summary>
        /// <param name="attributeName">属性名称</param>
        /// <param name="attributeType">属性类型</param>
        /// <returns>注解属性值</returns>
        object GetAttributeValue(string attributeName, Type attributeType);
    }
}