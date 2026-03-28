using System;
using System.Collections.Generic;
using System.Reflection;

namespace WellTool.Core.Annotation.Scanner
{
    /// <summary>
    /// 扫描<see cref="MemberInfo"/>上的注解，不支持处理层级对象
    /// </summary>
    public class ElementAnnotationScanner : IAnnotationScanner
    {
        /// <summary>
        /// 判断是否支持扫描该注解元素，仅当注解元素不为空时返回<see langword="true"/>
        /// </summary>
        /// <param name="annotatedEle">可注解元素，可以是Type、Method、Field、Constructor等</param>
        /// <returns>是否支持扫描该注解元素</returns>
        public bool Support(object annotatedEle)
        {
            return annotatedEle != null;
        }

        /// <summary>
        /// 获取注解元素上的全部注解。调用该方法前，需要确保调用Support返回为true
        /// </summary>
        /// <param name="annotatedEle">可注解元素，可以是Type、Method、Field、Constructor等</param>
        /// <returns>注解列表</returns>
        public List<Attribute> GetAnnotations(object annotatedEle)
        {
            var memberInfo = (MemberInfo)annotatedEle;
            var annotations = memberInfo.GetCustomAttributes(true);
            var attributeArray = annotations as Attribute[];
            return attributeArray != null ? new List<Attribute>(attributeArray) : new List<Attribute>();
        }

        /// <summary>
        /// 若Support返回true，则调用并返回GetAnnotations结果，否则返回空列表
        /// </summary>
        /// <param name="annotatedEle">可注解元素，可以是Type、Method、Field、Constructor等</param>
        /// <returns>注解列表</returns>
        public List<Attribute> GetAnnotationsIfSupport(object annotatedEle)
        {
            if (Support(annotatedEle))
            {
                return GetAnnotations(annotatedEle);
            }
            return new List<Attribute>();
        }

        /// <summary>
        /// 扫描<see cref="MemberInfo"/>上直接声明的注解，调用前需要确保调用<see cref="Support(object)"/>返回为true
        /// </summary>
        /// <param name="consumer">对获取到的注解和注解对应的层级索引的处理</param>
        /// <param name="annotatedEle">可注解元素，可以是Type、Method、Field、Constructor等</param>
        /// <param name="filter">注解过滤器，无法通过过滤器的注解不会被处理。该参数允许为空。</param>
        public void Scan(Action<int, Attribute> consumer, object annotatedEle, System.Func<Attribute, bool> filter)
        {
            filter = filter ?? (a => true);
            var memberInfo = (MemberInfo)annotatedEle;
            foreach (var annotation in memberInfo.GetCustomAttributes(true))
            {
                var attr = annotation as Attribute;
                if (attr != null && filter(attr))
                {
                    consumer(0, attr);
                }
            }
        }

        /// <summary>
        /// 若Support返回true，则调用Scan
        /// </summary>
        /// <param name="consumer">对获取到的注解和注解对应的层级索引的处理</param>
        /// <param name="annotatedEle">可注解元素，可以是Type、Method、Field、Constructor等</param>
        /// <param name="filter">注解过滤器，无法通过过滤器的注解不会被处理。该参数允许为空。</param>
        public void ScanIfSupport(Action<int, Attribute> consumer, object annotatedEle, System.Func<Attribute, bool> filter)
        {
            if (Support(annotatedEle))
            {
                Scan(consumer, annotatedEle, filter);
            }
        }
    }
}