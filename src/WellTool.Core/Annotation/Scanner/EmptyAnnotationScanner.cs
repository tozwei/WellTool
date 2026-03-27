using System;
using System.Collections.Generic;

namespace WellTool.Core.Annotation.Scanner
{
    /// <summary>
    /// 默认不扫描任何元素的扫描器
    /// </summary>
    public class EmptyAnnotationScanner : IAnnotationScanner
    {
        /// <summary>
        /// 判断是否支持扫描该注解元素
        /// </summary>
        /// <param name="annotatedEle">可注解元素，可以是Type、Method、Field、Constructor等</param>
        /// <returns>是否支持扫描该注解元素</returns>
        public bool Support(object annotatedEle)
        {
            return true;
        }

        /// <summary>
        /// 获取注解元素上的全部注解
        /// </summary>
        /// <param name="annotatedEle">可注解元素，可以是Type、Method、Field、Constructor等</param>
        /// <returns>注解列表</returns>
        public List<Attribute> GetAnnotations(object annotatedEle)
        {
            return new List<Attribute>();
        }

        /// <summary>
        /// 若Support返回true，则调用并返回GetAnnotations结果，否则返回空列表
        /// </summary>
        /// <param name="annotatedEle">可注解元素，可以是Type、Method、Field、Constructor等</param>
        /// <returns>注解列表</returns>
        public List<Attribute> GetAnnotationsIfSupport(object annotatedEle)
        {
            return Support(annotatedEle) ? GetAnnotations(annotatedEle) : new List<Attribute>();
        }

        /// <summary>
        /// 扫描注解元素的层级结构（若存在），然后对获取到的注解和注解对应的层级索引进行处理
        /// </summary>
        /// <param name="consumer">对获取到的注解和注解对应的层级索引的处理</param>
        /// <param name="annotatedEle">可注解元素，可以是Type、Method、Field、Constructor等</param>
        /// <param name="filter">注解过滤器，无法通过过滤器的注解不会被处理。该参数允许为空。</param>
        public void Scan(Action<int, Attribute> consumer, object annotatedEle, Func<Attribute, bool> filter)
        {
            // do nothing
        }

        /// <summary>
        /// 若Support返回true，则调用Scan
        /// </summary>
        /// <param name="consumer">对获取到的注解和注解对应的层级索引的处理</param>
        /// <param name="annotatedEle">可注解元素，可以是Type、Method、Field、Constructor等</param>
        /// <param name="filter">注解过滤器，无法通过过滤器的注解不会被处理。该参数允许为空。</param>
        public void ScanIfSupport(Action<int, Attribute> consumer, object annotatedEle, Func<Attribute, bool> filter)
        {
            if (Support(annotatedEle))
            {
                Scan(consumer, annotatedEle, filter);
            }
        }
    }
}