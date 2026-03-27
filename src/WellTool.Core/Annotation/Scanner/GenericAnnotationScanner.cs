using System;
using System.Collections.Generic;
using System.Reflection;

namespace WellTool.Core.Annotation.Scanner
{
    /// <summary>
    /// 通用注解扫描器，支持按不同的层级结构扫描可注解元素上的注解
    /// </summary>
    public class GenericAnnotationScanner : IAnnotationScanner
    {
        /// <summary>
        /// 类型扫描器
        /// </summary>
        private readonly IAnnotationScanner typeScanner;

        /// <summary>
        /// 方法扫描器
        /// </summary>
        private readonly IAnnotationScanner methodScanner;

        /// <summary>
        /// 元注解扫描器
        /// </summary>
        private readonly IAnnotationScanner metaScanner;

        /// <summary>
        /// 普通元素扫描器
        /// </summary>
        private readonly IAnnotationScanner elementScanner;

        /// <summary>
        /// 通用注解扫描器支持扫描所有类型的可注解元素
        /// </summary>
        /// <param name="annotatedEle">可注解元素，可以是Type、Method、Field、Constructor等</param>
        /// <returns>是否支持扫描该注解元素</returns>
        public bool Support(object annotatedEle)
        {
            return true;
        }

        /// <summary>
        /// 构造一个通用注解扫描器
        /// </summary>
        /// <param name="enableScanMetaAnnotation">是否扫描注解上的元注解</param>
        /// <param name="enableScanSupperClass">是否扫描父类</param>
        /// <param name="enableScanSupperInterface">是否扫描父接口</param>
        public GenericAnnotationScanner(
            bool enableScanMetaAnnotation,
            bool enableScanSupperClass,
            bool enableScanSupperInterface)
        {
            this.metaScanner = enableScanMetaAnnotation ? new MetaAnnotationScanner() : new EmptyAnnotationScanner();
            this.typeScanner = new TypeAnnotationScanner(
                enableScanSupperClass, enableScanSupperInterface, a => true, new HashSet<Type>()
            );
            this.methodScanner = new MethodAnnotationScanner(
                enableScanSupperClass, enableScanSupperInterface, a => true, new HashSet<Type>()
            );
            this.elementScanner = new ElementAnnotationScanner();
        }

        /// <summary>
        /// 获取注解元素上的全部注解
        /// </summary>
        /// <param name="annotatedEle">可注解元素，可以是Type、Method、Field、Constructor等</param>
        /// <returns>注解列表</returns>
        public List<Attribute> GetAnnotations(object annotatedEle)
        {
            var annotations = new List<Attribute>();
            Scan((index, annotation) => annotations.Add(annotation), annotatedEle, null);
            return annotations;
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
            filter = filter ?? (a => true);
            if (annotatedEle == null)
            {
                return;
            }

            // 注解元素是类型
            if (annotatedEle is Type type)
            {
                ScanElements(typeScanner, consumer, type, filter);
            }
            // 注解元素是方法
            else if (annotatedEle is MethodInfo method)
            {
                ScanElements(methodScanner, consumer, method, filter);
            }
            // 注解元素是其他类型
            else
            {
                ScanElements(elementScanner, consumer, annotatedEle, filter);
            }
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

        /// <summary>
        /// 扫描注解类的层级结构（若存在），然后对获取到的注解和注解对应的层级索引进行处理
        /// </summary>
        /// <param name="scanner">使用的扫描器</param>
        /// <param name="consumer">对获取到的注解和注解对应的层级索引的处理</param>
        /// <param name="annotatedEle">可注解元素，可以是Type、Method、Field、Constructor等</param>
        /// <param name="filter">注解过滤器，无法通过过滤器的注解不会被处理。该参数允许为空。</param>
        private void ScanElements(
            IAnnotationScanner scanner,
            Action<int, Attribute> consumer,
            object annotatedEle,
            Func<Attribute, bool> filter)
        {
            // 扫描类上注解
            var classAnnotations = new Dictionary<int, List<Attribute>>();
            scanner.Scan((index, annotation) => {
                if (filter(annotation))
                {
                    if (!classAnnotations.ContainsKey(index))
                    {
                        classAnnotations[index] = new List<Attribute>();
                    }
                    classAnnotations[index].Add(annotation);
                }
            }, annotatedEle, filter);

            // 扫描元注解
            foreach (var entry in classAnnotations)
            {
                var index = entry.Key;
                var annotations = entry.Value;
                foreach (var annotation in annotations)
                {
                    consumer(index, annotation);
                    metaScanner.Scan(consumer, annotation.GetType(), filter);
                }
            }
        }
    }
}