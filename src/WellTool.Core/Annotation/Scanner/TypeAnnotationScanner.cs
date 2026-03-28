using System;
using System.Collections.Generic;
using System.Reflection;
using WellTool.Core.Collection;

namespace WellTool.Core.Annotation.Scanner
{
    /// <summary>
    /// 扫描<see cref="Type"/>上的注解
    /// </summary>
    public class TypeAnnotationScanner : AbstractTypeAnnotationScanner<TypeAnnotationScanner>, IAnnotationScanner
    {
        /// <summary>
        /// 构造一个类注解扫描器
        /// </summary>
        /// <param name="includeSupperClass">是否允许扫描父类</param>
        /// <param name="includeInterfaces">是否允许扫描父接口</param>
        /// <param name="filter">过滤器</param>
        /// <param name="excludeTypes">不包含的类型</param>
        public TypeAnnotationScanner(bool includeSupperClass, bool includeInterfaces, System.Func<Type, bool> filter, HashSet<Type> excludeTypes)
            : base(includeSupperClass, includeInterfaces, filter, excludeTypes)
        {
        }

        /// <summary>
        /// 构建一个类注解扫描器，默认允许扫描指定元素的父类以及父接口
        /// </summary>
        public TypeAnnotationScanner()
            : this(true, true, t => true, CollUtil.NewLinkedHashSet<Type>())
        {
        }

        /// <summary>
        /// 判断是否支持扫描该注解元素，仅当注解元素是<see cref="Type"/>时返回<see langword="true"/>
        /// </summary>
        /// <param name="annotatedEle">可注解元素，可以是Type、Method、Field、Constructor等</param>
        /// <returns>是否支持扫描该注解元素</returns>
        public bool Support(object annotatedEle)
        {
            return annotatedEle is Type;
        }

        /// <summary>
        /// 获取注解元素上的全部注解。调用该方法前，需要确保调用Support返回为true
        /// </summary>
        /// <param name="annotatedEle">可注解元素，可以是Type、Method、Field、Constructor等</param>
        /// <returns>注解列表</returns>
        public List<Attribute> GetAnnotations(object annotatedEle)
        {
            var type = (Type)annotatedEle;
            var annotations = type.GetCustomAttributes(true);
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
        /// 将注解元素转为<see cref="Type"/>
        /// </summary>
        /// <param name="annotatedElement">注解元素</param>
        /// <returns>要递归的类型</returns>
        protected override Type GetClassFormAnnotatedElement(MemberInfo annotatedElement)
        {
            return annotatedElement as Type;
        }

        /// <summary>
        /// 获取<see cref="Type.GetCustomAttributes(bool)"/>
        /// </summary>
        /// <param name="source">最初的注解元素</param>
        /// <param name="index">类的层级索引</param>
        /// <param name="targetClass">类</param>
        /// <returns>类上直接声明的注解</returns>
        protected override Attribute[] GetAnnotationsFromTargetClass(MemberInfo source, int index, Type targetClass)
        {
            return targetClass.GetCustomAttributes(true) as Attribute[];
        }

        /// <summary>
        /// 是否允许扫描父类
        /// </summary>
        /// <param name="includeSuperClass">是否允许扫描父类</param>
        /// <returns>当前实例</returns>
        public new TypeAnnotationScanner SetIncludeSuperClass(bool includeSuperClass)
        {
            return base.SetIncludeSuperClass(includeSuperClass);
        }

        /// <summary>
        /// 是否允许扫描父接口
        /// </summary>
        /// <param name="includeInterfaces">是否允许扫描父类</param>
        /// <returns>当前实例</returns>
        public new TypeAnnotationScanner SetIncludeInterfaces(bool includeInterfaces)
        {
            return base.SetIncludeInterfaces(includeInterfaces);
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
    }
}