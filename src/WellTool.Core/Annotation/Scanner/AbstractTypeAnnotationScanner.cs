using System;
using System.Collections.Generic;
using System.Reflection;
using WellTool.Core.Annotation;
using WellTool.Core.Collection;
using WellTool.Core.Lang;
using WellTool.Core.Map;

namespace WellTool.Core.Annotation.Scanner
{
    /// <summary>
    /// 为需要从类的层级结构中获取注解的<see cref="IAnnotationScanner"/>提供基本实现
    /// </summary>
    public abstract class AbstractTypeAnnotationScanner<T> : IAnnotationScanner where T : AbstractTypeAnnotationScanner<T>
    {
        /// <summary>
        /// 是否允许扫描父类
        /// </summary>
        private bool includeSuperClass;

        /// <summary>
        /// 是否允许扫描父接口
        /// </summary>
        private bool includeInterfaces;

        /// <summary>
        /// 过滤器，若类型无法通过该过滤器，则该类型及其树结构将直接不被查找
        /// </summary>
        private System.Func<Type, bool> filter;

        /// <summary>
        /// 排除的类型，以上类型及其树结构将直接不被查找
        /// </summary>
        private readonly HashSet<Type> excludeTypes;

        /// <summary>
        /// 转换器
        /// </summary>
        private readonly List<System.Func<Type, Type>> converters;

        /// <summary>
        /// 是否有转换器
        /// </summary>
        private bool hasConverters;

        /// <summary>
        /// 当前实例
        /// </summary>
        private readonly T typedThis;

        /// <summary>
        /// 构造一个类注解扫描器
        /// </summary>
        /// <param name="includeSuperClass">是否允许扫描父类</param>
        /// <param name="includeInterfaces">是否允许扫描父接口</param>
        /// <param name="filter">过滤器</param>
        /// <param name="excludeTypes">不包含的类型</param>
        protected AbstractTypeAnnotationScanner(bool includeSuperClass, bool includeInterfaces, System.Func<Type, bool> filter, HashSet<Type> excludeTypes)
        {
            Assert.NotNull(filter, "filter must not null");
            Assert.NotNull(excludeTypes, "excludeTypes must not null");
            this.includeSuperClass = includeSuperClass;
            this.includeInterfaces = includeInterfaces;
            this.filter = filter;
            this.excludeTypes = excludeTypes;
            this.converters = new List<System.Func<Type, Type>>();
            this.typedThis = (T)this;
        }

        /// <summary>
        /// 是否允许扫描父类
        /// </summary>
        /// <returns>是否允许扫描父类</returns>
        public bool IsIncludeSuperClass()
        {
            return includeSuperClass;
        }

        /// <summary>
        /// 是否允许扫描父接口
        /// </summary>
        /// <returns>是否允许扫描父接口</returns>
        public bool IsIncludeInterfaces()
        {
            return includeInterfaces;
        }

        /// <summary>
        /// 设置过滤器，若类型无法通过该过滤器，则该类型及其树结构将直接不被查找
        /// </summary>
        /// <param name="filter">过滤器</param>
        /// <returns>当前实例</returns>
        public T SetFilter(System.Func<Type, bool> filter)
        {
            Assert.NotNull(filter, "filter must not null");
            this.filter = filter;
            return typedThis;
        }

        /// <summary>
        /// 添加不扫描的类型，该类型及其树结构将直接不被查找
        /// </summary>
        /// <param name="excludeTypes">不扫描的类型</param>
        /// <returns>当前实例</returns>
        public T AddExcludeTypes(params Type[] excludeTypes)
        {
            CollUtil.AddAll(this.excludeTypes, excludeTypes);
            //foreach (var type in excludeTypes)
            //{
            //    this.excludeTypes.Add(type);
            //}
            return typedThis;
        }

        /// <summary>
        /// 添加转换器
        /// </summary>
        /// <param name="converter">转换器</param>
        /// <returns>当前实例</returns>
        public T AddConverters(System.Func<Type, Type> converter)
        {
            Assert.NotNull(converter, "converter must not null");
            this.converters.Add(converter);
            if (!this.hasConverters)
            {
                this.hasConverters = CollUtil.IsNotEmpty<System.Func<Type, Type>>(this.converters);
            }
            return typedThis;
        }

        /// <summary>
        /// 是否允许扫描父类
        /// </summary>
        /// <param name="includeSuperClass">是否</param>
        /// <returns>当前实例</returns>
        protected T SetIncludeSuperClass(bool includeSuperClass)
        {
            this.includeSuperClass = includeSuperClass;
            return typedThis;
        }

        /// <summary>
        /// 是否允许扫描父接口
        /// </summary>
        /// <param name="includeInterfaces">是否</param>
        /// <returns>当前实例</returns>
        protected T SetIncludeInterfaces(bool includeInterfaces)
        {
            this.includeInterfaces = includeInterfaces;
            return typedThis;
        }

        /// <summary>
        /// 则根据广度优先递归扫描类的层级结构，并对层级结构中类/接口声明的层级索引和它们声明的注解对象进行处理
        /// </summary>
        /// <param name="consumer">对获取到的注解和注解对应的层级索引的处理</param>
        /// <param name="annotatedEle">注解元素</param>
        /// <param name="filter">注解过滤器，无法通过过滤器的注解不会被处理。该参数允许为空。</param>
        public void Scan(Action<int, Attribute> consumer, object annotatedEle, Func<Attribute, bool> filter)
        {
            filter = filter ?? (a => true);
            var memberInfo = annotatedEle as MemberInfo;
            var sourceClass = GetClassFormAnnotatedElement(memberInfo);
            var classDeque = CollUtil.NewLinkedList(new List<Type> { sourceClass });
            var accessedTypes = new HashSet<Type>();
            int index = 0;
            while (classDeque.Count > 0)
            {
                var currClassList = classDeque.First.Value;
                classDeque.RemoveFirst();
                var nextClassQueue = new List<Type>();
                foreach (var targetClass in currClassList)
                {
                    var convertedClass = Convert(targetClass);
                    // 过滤不需要处理的类
                    if (IsNotNeedProcess(accessedTypes, convertedClass))
                    {
                        continue;
                    }
                    accessedTypes.Add(convertedClass);
                    // 扫描父类
                    ScanSuperClassIfNecessary(nextClassQueue, convertedClass);
                    // 扫描接口
                    ScanInterfaceIfNecessary(nextClassQueue, convertedClass);
                    // 处理层级索引和注解
                    var targetAnnotations = GetAnnotationsFromTargetClass(memberInfo, index, convertedClass);
                    foreach (var annotation in targetAnnotations)
                    {
                        if (!AnnotationUtil.IsJdkMetaAnnotation(annotation.GetType()) && filter(annotation))
                        {
                            consumer(index, annotation);
                        }
                    }
                    index++;
                }
                if (CollUtil.IsNotEmpty<Type>(nextClassQueue))
                {
                    classDeque.AddLast(nextClassQueue);
                }
            }
        }

        /// <summary>
        /// 从要搜索的注解元素上获得要递归的类型
        /// </summary>
        /// <param name="annotatedElement">注解元素</param>
        /// <returns>要递归的类型</returns>
        protected abstract Type GetClassFormAnnotatedElement(MemberInfo annotatedElement);

        /// <summary>
        /// 从类上获取最终所需的目标注解
        /// </summary>
        /// <param name="source">最初的注解元素</param>
        /// <param name="index">类的层级索引</param>
        /// <param name="targetClass">类</param>
        /// <returns>最终所需的目标注解</returns>
        protected abstract Attribute[] GetAnnotationsFromTargetClass(MemberInfo source, int index, Type targetClass);

        /// <summary>
        /// 当前类是否不需要处理
        /// </summary>
        /// <param name="accessedTypes">访问类型</param>
        /// <param name="targetClass">目标类型</param>
        /// <returns>是否不需要处理</returns>
        protected bool IsNotNeedProcess(HashSet<Type> accessedTypes, Type targetClass)
        {
            return targetClass == null
                    || accessedTypes.Contains(targetClass)
                    || excludeTypes.Contains(targetClass)
                    || !filter(targetClass);
        }

        /// <summary>
        /// 若<see cref="includeInterfaces"/>为<see langword="true"/>，则将目标类的父接口也添加到nextClasses
        /// </summary>
        /// <param name="nextClasses">下一个类集合</param>
        /// <param name="targetClass">目标类型</param>
        protected void ScanInterfaceIfNecessary(List<Type> nextClasses, Type targetClass)
        {
            if (includeInterfaces)
            {
                var interfaces = targetClass.GetInterfaces();
                if (interfaces.Length > 0)
                {
                    CollUtil.AddAll(nextClasses, interfaces);
                }
            }
        }

        /// <summary>
        /// 若<see cref="includeSuperClass"/>为<see langword="true"/>，则将目标类的父类也添加到nextClasses
        /// </summary>
        /// <param name="nextClassQueue">下一个类队列</param>
        /// <param name="targetClass">目标类型</param>
        protected void ScanSuperClassIfNecessary(List<Type> nextClassQueue, Type targetClass)
        {
            if (includeSuperClass)
            {
                var superClass = targetClass.BaseType;
                if (superClass != typeof(object) && superClass != null)
                {
                    nextClassQueue.Add(superClass);
                }
            }
        }

        /// <summary>
        /// 若存在转换器，则使用转换器对目标类进行转换
        /// </summary>
        /// <param name="target">目标类</param>
        /// <returns>转换后的类</returns>
        protected Type Convert(Type target)
        {
            if (hasConverters)
            {
                foreach (var converter in converters)
                {
                    target = converter(target);
                }
            }
            return target;
        }

        /// <summary>
        /// 若类型为代理类，则尝试转换为原始被代理类
        /// </summary>
        public static System.Func<Type, Type> JdkProxyClassConverter
        {
            get
            {
                return ConvertType;
            }
        }

        private static Type ConvertType(Type sourceClass)
        {
            return sourceClass.IsInterface ? sourceClass : ConvertType(sourceClass.BaseType);
        }

        /// <summary>
        /// 判断是否支持扫描该注解元素
        /// </summary>
        /// <param name="annotatedEle">可注解元素，可以是Type、Method、Field、Constructor等</param>
        /// <returns>是否支持扫描该注解元素</returns>
        public bool Support(object annotatedEle)
        {
            return annotatedEle is MemberInfo;
        }

        /// <summary>
        /// 获取注解元素上的全部注解。调用该方法前，需要确保调用Support返回为true
        /// </summary>
        /// <param name="annotatedEle">可注解元素，可以是Type、Method、Field、Constructor等</param>
        /// <returns>注解列表</returns>
        public List<Attribute> GetAnnotations(object annotatedEle)
        {
            var result = new List<Attribute>();
            Scan((index, annotation) => result.Add(annotation), annotatedEle, null);
            return result;
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