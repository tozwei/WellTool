using System;

namespace WellTool.Core.Annotation
{
    /// <summary>
    /// 表示一组被聚合在一起的注解对象
    /// </summary>
    public interface IAggregateAnnotation : Annotation
    {
        /// <summary>
        /// 在聚合中是否存在的指定类型注解对象
        /// </summary>
        /// <param name="annotationType">注解类型</param>
        /// <returns>是否</returns>
        bool IsAnnotationPresent(Type annotationType);

        /// <summary>
        /// 获取聚合中的全部注解对象
        /// </summary>
        /// <returns>注解对象数组</returns>
        Annotation[] GetAnnotations();
    }
}