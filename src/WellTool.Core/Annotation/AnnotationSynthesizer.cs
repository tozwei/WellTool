using System;
using System.Collections.Generic;
using System.Reflection;

namespace WellTool.Core.Annotation
{
    /// <summary>
    /// 注解合成器<br>
    /// 用于合成注解，支持注解的别名和关联处理
    /// </summary>
    public class AnnotationSynthesizer
    {
        /// <summary>
        /// 合成注解
        /// </summary>
        /// <typeparam name="T">注解类型</typeparam>
        /// <param name="member">成员信息</param>
        /// <returns>合成后的注解</returns>
        public static T Synthesize<T>(MemberInfo member) where T : Attribute
        {
            // 先尝试获取直接注解
            var annotation = AnnotationUtil.GetAttribute<T>(member);
            
            // 如果没有直接注解，尝试从其他注解中合成
            if (annotation == null)
            {
                annotation = SynthesizeFromMetaAnnotations<T>(member);
            }
            
            // 处理注解的别名和关联
            if (annotation != null)
            {
                annotation = ProcessAliasAndLink<T>(annotation);
            }
            
            return annotation;
        }

        /// <summary>
        /// 从元注解中合成注解
        /// </summary>
        /// <typeparam name="T">注解类型</typeparam>
        /// <param name="member">成员信息</param>
        /// <returns>合成后的注解</returns>
        private static T SynthesizeFromMetaAnnotations<T>(MemberInfo member) where T : Attribute
        {
            var otherAnnotations = member.GetCustomAttributes(false);
            foreach (var otherAnnotation in otherAnnotations)
            {
                // 检查其他注解是否有目标注解
                var otherAnnotationType = otherAnnotation.GetType();
                var metaAnnotation = AnnotationUtil.GetAttribute<T>(otherAnnotationType);
                if (metaAnnotation != null)
                {
                    // 创建新的注解实例
                    var constructor = typeof(T).GetConstructor(Type.EmptyTypes);
                    if (constructor != null)
                    {
                        var instance = constructor.Invoke(null) as T;
                        // 复制元注解的属性值
                        CopyAnnotationProperties(metaAnnotation, instance);
                        return instance;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 处理注解的别名和关联
        /// </summary>
        /// <typeparam name="T">注解类型</typeparam>
        /// <param name="annotation">注解</param>
        /// <returns>处理后的注解</returns>
        private static T ProcessAliasAndLink<T>(T annotation) where T : Attribute
        {
            // 这里可以实现更复杂的别名和关联处理逻辑
            // 目前简单实现，后续可以扩展
            return annotation;
        }

        /// <summary>
        /// 复制注解属性值
        /// </summary>
        /// <param name="source">源注解</param>
        /// <param name="target">目标注解</param>
        private static void CopyAnnotationProperties(Attribute source, Attribute target)
        {
            var sourceType = source.GetType();
            var targetType = target.GetType();
            
            var properties = sourceType.GetProperties()
                .Where(p => p.CanRead && p.CanWrite)
                .Where(p => p.Name != "TypeId");
            
            foreach (var property in properties)
            {
                var targetProperty = targetType.GetProperty(property.Name);
                if (targetProperty != null && targetProperty.CanWrite)
                {
                    var value = property.GetValue(source);
                    targetProperty.SetValue(target, value);
                }
            }
        }
    }
}