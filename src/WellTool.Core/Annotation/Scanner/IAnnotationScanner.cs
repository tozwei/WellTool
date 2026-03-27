using System;
using System.Collections.Generic;
using System.Linq;

namespace WellTool.Core.Annotation.Scanner
{
    /// <summary>
    /// 注解扫描器，用于从支持的可注解元素上获取所需注解
    /// </summary>
    public interface IAnnotationScanner
    {
        // ============================ 预置的扫描器实例 ============================

        /// <summary>
        /// 不扫描任何注解
        /// </summary>
        static readonly IAnnotationScanner NOTHING = new EmptyAnnotationScanner();

        /// <summary>
        /// 扫描元素本身直接声明的注解，包括父类带有Inherited、被传递到元素上的注解的扫描器
        /// </summary>
        static readonly IAnnotationScanner DIRECTLY = new GenericAnnotationScanner(false, false, false);

        /// <summary>
        /// 扫描元素本身直接声明的注解，包括父类带有Inherited、被传递到元素上的注解，以及这些注解的元注解的扫描器
        /// </summary>
        static readonly IAnnotationScanner DIRECTLY_AND_META_ANNOTATION = new GenericAnnotationScanner(true, false, false);

        /// <summary>
        /// 扫描元素本身以及父类的层级结构中声明的注解的扫描器
        /// </summary>
        static readonly IAnnotationScanner SUPERCLASS = new GenericAnnotationScanner(false, true, false);

        /// <summary>
        /// 扫描元素本身以及父类的层级结构中声明的注解，以及这些注解的元注解的扫描器
        /// </summary>
        static readonly IAnnotationScanner SUPERCLASS_AND_META_ANNOTATION = new GenericAnnotationScanner(true, true, false);

        /// <summary>
        /// 扫描元素本身以及父接口的层级结构中声明的注解的扫描器
        /// </summary>
        static readonly IAnnotationScanner INTERFACE = new GenericAnnotationScanner(false, false, true);

        /// <summary>
        /// 扫描元素本身以及父接口的层级结构中声明的注解，以及这些注解的元注解的扫描器
        /// </summary>
        static readonly IAnnotationScanner INTERFACE_AND_META_ANNOTATION = new GenericAnnotationScanner(true, false, true);

        /// <summary>
        /// 扫描元素本身以及父类、父接口的层级结构中声明的注解的扫描器
        /// </summary>
        static readonly IAnnotationScanner TYPE_HIERARCHY = new GenericAnnotationScanner(false, true, true);

        /// <summary>
        /// 扫描元素本身以及父接口、父接口的层级结构中声明的注解，以及这些注解的元注解的扫描器
        /// </summary>
        static readonly IAnnotationScanner TYPE_HIERARCHY_AND_META_ANNOTATION = new GenericAnnotationScanner(true, true, true);

        // ============================ 静态方法 ============================

        /// <summary>
        /// 给定一组扫描器，使用第一个支持处理该类型元素的扫描器获取元素上可能存在的注解
        /// </summary>
        /// <param name="annotatedEle">可注解元素，可以是Type、Method、Field、Constructor等</param>
        /// <param name="scanners">注解扫描器</param>
        /// <returns>注解列表</returns>
        static List<Attribute> ScanByAnySupported(object annotatedEle, params IAnnotationScanner[] scanners)
        {
            if (annotatedEle == null || scanners == null || scanners.Length == 0)
            {
                return new List<Attribute>();
            }
            return scanners
                .FirstOrDefault(scanner => scanner.Support(annotatedEle))?
                .GetAnnotations(annotatedEle) ?? new List<Attribute>();
        }

        /// <summary>
        /// 根据指定的扫描器，扫描元素上可能存在的注解
        /// </summary>
        /// <param name="annotatedEle">可注解元素，可以是Type、Method、Field、Constructor等</param>
        /// <param name="scanners">注解扫描器</param>
        /// <returns>注解列表</returns>
        static List<Attribute> ScanByAllSupported(object annotatedEle, params IAnnotationScanner[] scanners)
        {
            if (annotatedEle == null || scanners == null || scanners.Length == 0)
            {
                return new List<Attribute>();
            }
            return scanners
                .SelectMany(scanner => scanner.GetAnnotationsIfSupport(annotatedEle))
                .ToList();
        }

        // ============================ 抽象方法 ============================

        /// <summary>
        /// 判断是否支持扫描该注解元素
        /// </summary>
        /// <param name="annotatedEle">可注解元素，可以是Type、Method、Field、Constructor等</param>
        /// <returns>是否支持扫描该注解元素</returns>
        bool Support(object annotatedEle);

        /// <summary>
        /// 获取注解元素上的全部注解。调用该方法前，需要确保调用Support返回为true
        /// </summary>
        /// <param name="annotatedEle">可注解元素，可以是Type、Method、Field、Constructor等</param>
        /// <returns>注解列表</returns>
        List<Attribute> GetAnnotations(object annotatedEle);

        /// <summary>
        /// 若Support返回true，则调用并返回GetAnnotations结果，否则返回空列表
        /// </summary>
        /// <param name="annotatedEle">可注解元素，可以是Type、Method、Field、Constructor等</param>
        /// <returns>注解列表</returns>
        List<Attribute> GetAnnotationsIfSupport(object annotatedEle);

        /// <summary>
        /// 扫描注解元素的层级结构（若存在），然后对获取到的注解和注解对应的层级索引进行处理。
        /// 调用该方法前，需要确保调用Support返回为true
        /// </summary>
        /// <param name="consumer">对获取到的注解和注解对应的层级索引的处理</param>
        /// <param name="annotatedEle">可注解元素，可以是Type、Method、Field、Constructor等</param>
        /// <param name="filter">注解过滤器，无法通过过滤器的注解不会被处理。该参数允许为空。</param>
        void Scan(Action<int, Attribute> consumer, object annotatedEle, Func<Attribute, bool> filter);

        /// <summary>
        /// 若Support返回true，则调用Scan
        /// </summary>
        /// <param name="consumer">对获取到的注解和注解对应的层级索引的处理</param>
        /// <param name="annotatedEle">可注解元素，可以是Type、Method、Field、Constructor等</param>
        /// <param name="filter">注解过滤器，无法通过过滤器的注解不会被处理。该参数允许为空。</param>
        void ScanIfSupport(Action<int, Attribute> consumer, object annotatedEle, Func<Attribute, bool> filter);
    }
}