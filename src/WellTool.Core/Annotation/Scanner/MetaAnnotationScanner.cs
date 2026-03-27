using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WellTool.Core.Annotation;
using WellTool.Core.Collection;

namespace WellTool.Core.Annotation.Scanner
{
    /// <summary>
    /// 扫描注解类上存在的注解，支持处理枚举实例或枚举类型
    /// 需要注意，当待解析是枚举类时，有可能与<see cref="TypeAnnotationScanner"/>冲突
    /// </summary>
    public class MetaAnnotationScanner : IAnnotationScanner
    {
        /// <summary>
        /// 获取当前注解的元注解后，是否继续递归扫描的元注解的元注解
        /// </summary>
        private readonly bool includeSupperMetaAnnotation;

        /// <summary>
        /// 构造一个元注解扫描器
        /// </summary>
        /// <param name="includeSupperMetaAnnotation">获取当前注解的元注解后，是否继续递归扫描的元注解的元注解</param>
        public MetaAnnotationScanner(bool includeSupperMetaAnnotation)
        {
            this.includeSupperMetaAnnotation = includeSupperMetaAnnotation;
        }

        /// <summary>
        /// 构造一个元注解扫描器，默认在扫描当前注解上的元注解后，并继续递归扫描元注解
        /// </summary>
        public MetaAnnotationScanner()
            : this(true)
        {
        }

        /// <summary>
        /// 判断是否支持扫描该注解元素，仅当注解元素是<see cref="Attribute"/>类型时返回<see langword="true"/>
        /// </summary>
        /// <param name="annotatedEle">可注解元素，可以是Type、Method、Field、Constructor等</param>
        /// <returns>是否支持扫描该注解元素</returns>
        public bool Support(object annotatedEle)
        {
            return annotatedEle is Type type && typeof(Attribute).IsAssignableFrom(type);
        }

        /// <summary>
        /// 获取注解元素上的全部注解。调用该方法前，需要确保调用<see cref="Support(object)"/>返回为true
        /// </summary>
        /// <param name="annotatedEle">可注解元素，可以是Type、Method、Field、Constructor等</param>
        /// <returns>注解列表</returns>
        public List<Attribute> GetAnnotations(object annotatedEle)
        {
            var annotations = new List<Attribute>();
            Scan(
                (index, annotation) => annotations.Add(annotation), annotatedEle,
                annotation => annotation.GetType() != annotatedEle
            );
            return annotations;
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
        /// 按广度优先扫描指定注解上的元注解，对扫描到的注解与层级索引进行操作
        /// </summary>
        /// <param name="consumer">当前层级索引与操作</param>
        /// <param name="annotatedEle">可注解元素，可以是Type、Method、Field、Constructor等</param>
        /// <param name="filter">过滤器</param>
        public void Scan(Action<int, Attribute> consumer, object annotatedEle, System.Func<Attribute, bool> filter)
        {
            filter = filter ?? (a => true);
            var accessed = new HashSet<Type>();
            var deque = CollUtil.NewLinkedList(new List<Type> { (Type)annotatedEle });
            int distance = 0;
            do
            {
                var annotationTypes = deque.First.Value;
                deque.RemoveFirst();
                foreach (var type in annotationTypes)
                {
                    var metaAnnotations = type.GetCustomAttributes(true)
                        .Cast<Attribute>()
                        .Where(filter)
                        .ToList();
                    foreach (var metaAnnotation in metaAnnotations)
                    {
                        consumer(distance, metaAnnotation);
                    }
                    accessed.Add(type);
                    var next = metaAnnotations.Select(a => a.GetType())
                        .Where(t => !accessed.Contains(t))
                        .ToList();
                    if (next.Count > 0)
                    {
                        deque.AddLast(next);
                    }
                }
                distance++;
            } while (includeSupperMetaAnnotation && deque.Count > 0);
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