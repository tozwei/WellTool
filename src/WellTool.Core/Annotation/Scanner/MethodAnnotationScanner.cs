using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WellTool.Core.Collection;
using WellTool.Core.Lang;

namespace WellTool.Core.Annotation.Scanner
{
    /// <summary>
    /// 扫描<see cref="MethodInfo"/>上的注解
    /// </summary>
    public class MethodAnnotationScanner : AbstractTypeAnnotationScanner<MethodAnnotationScanner>, IAnnotationScanner
    {
        /// <summary>
        /// 构造一个类注解扫描器，仅扫描该方法上直接声明的注解
        /// </summary>
        public MethodAnnotationScanner()
            : this(false)
        {
        }

        /// <summary>
        /// 构造一个类注解扫描器
        /// </summary>
        /// <param name="scanSameSignatureMethod">是否扫描类层级结构中具有相同方法签名的方法</param>
        public MethodAnnotationScanner(bool scanSameSignatureMethod)
            : this(scanSameSignatureMethod, targetClass => true, CollUtil.NewLinkedHashSet<Type>())
        {
        }

        /// <summary>
        /// 构造一个方法注解扫描器
        /// </summary>
        /// <param name="scanSameSignatureMethod">是否扫描类层级结构中具有相同方法签名的方法</param>
        /// <param name="filter">过滤器</param>
        /// <param name="excludeTypes">不包含的类型</param>
        public MethodAnnotationScanner(bool scanSameSignatureMethod, System.Func<Type, bool> filter, HashSet<Type> excludeTypes)
            : base(scanSameSignatureMethod, scanSameSignatureMethod, filter, excludeTypes)
        {
        }

        /// <summary>
        /// 构造一个方法注解扫描器
        /// </summary>
        /// <param name="includeSuperClass">是否允许扫描父类中具有相同方法签名的方法</param>
        /// <param name="includeInterfaces">是否允许扫描父接口中具有相同方法签名的方法</param>
        /// <param name="filter">过滤器</param>
        /// <param name="excludeTypes">不包含的类型</param>
        public MethodAnnotationScanner(bool includeSuperClass, bool includeInterfaces, System.Func<Type, bool> filter, HashSet<Type> excludeTypes)
            : base(includeSuperClass, includeInterfaces, filter, excludeTypes)
        {
        }

        /// <summary>
        /// 判断是否支持扫描该注解元素，仅当注解元素是<see cref="MethodInfo"/>时返回<see langword="true"/>
        /// </summary>
        /// <param name="annotatedEle">可注解元素，可以是Type、Method、Field、Constructor等</param>
        /// <returns>是否支持扫描该注解元素</returns>
        public bool Support(object annotatedEle)
        {
            return annotatedEle is MethodInfo;
        }

        /// <summary>
        /// 获取注解元素上的全部注解。调用该方法前，需要确保调用Support返回为true
        /// </summary>
        /// <param name="annotatedEle">可注解元素，可以是Type、Method、Field、Constructor等</param>
        /// <returns>注解列表</returns>
        public List<Attribute> GetAnnotations(object annotatedEle)
        {
            var method = (MethodInfo)annotatedEle;
            var annotations = method.GetCustomAttributes(true);
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
        public void ScanIfSupport(Action<int, Attribute> consumer, object annotatedEle, System.Func<Attribute, bool> filter)
        {
            if (Support(annotatedEle))
            {
                Scan(consumer, annotatedEle, filter);
            }
        }

        /// <summary>
        /// 获取声明该方法的类
        /// </summary>
        /// <param name="annotatedElement">注解元素</param>
        /// <returns>要递归的类型</returns>
        /// <seealso cref="MethodInfo.DeclaringType"/>
        protected override Type GetClassFormAnnotatedElement(MemberInfo annotatedElement)
        {
            return ((MethodInfo)annotatedElement).DeclaringType;
        }

        /// <summary>
        /// 若父类/父接口中方法具有相同的方法签名，则返回该方法上的注解
        /// </summary>
        /// <param name="source">原始方法</param>
        /// <param name="index">类的层级索引</param>
        /// <param name="targetClass">类</param>
        /// <returns>最终所需的目标注解</returns>
        protected override Attribute[] GetAnnotationsFromTargetClass(MemberInfo source, int index, Type targetClass)
        {
            var sourceMethod = (MethodInfo)source;
            return targetClass.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(superMethod => HasSameSignature(sourceMethod, superMethod))
                .SelectMany(superMethod => superMethod.GetCustomAttributes(true))
                .Cast<Attribute>()
                .ToArray();
        }

        /// <summary>
        /// 设置是否扫描类层级结构中具有相同方法签名的方法
        /// </summary>
        /// <param name="scanSuperMethodIfOverride">是否扫描类层级结构中具有相同方法签名的方法</param>
        /// <returns>当前实例</returns>
        public MethodAnnotationScanner SetScanSameSignatureMethod(bool scanSuperMethodIfOverride)
        {
            SetIncludeInterfaces(scanSuperMethodIfOverride);
            SetIncludeSuperClass(scanSuperMethodIfOverride);
            return this;
        }

        /// <summary>
        /// 该方法是否具备与扫描的方法相同的方法签名
        /// </summary>
        private bool HasSameSignature(MethodInfo sourceMethod, MethodInfo superMethod)
        {
            if (!sourceMethod.Name.Equals(superMethod.Name))
            {
                return false;
            }
            var sourceParameterTypes = sourceMethod.GetParameters().Select(p => p.ParameterType).ToArray();
            var targetParameterTypes = superMethod.GetParameters().Select(p => p.ParameterType).ToArray();
            if (sourceParameterTypes.Length != targetParameterTypes.Length)
            {
                return false;
            }
            for (int i = 0; i < sourceParameterTypes.Length; i++)
            {
                if (sourceParameterTypes[i] != targetParameterTypes[i])
                {
                    return false;
                }
            }
            return superMethod.ReturnType.IsAssignableFrom(sourceMethod.ReturnType);
        }

        /// <summary>
        /// 是否允许扫描父类
        /// </summary>
        /// <param name="includeSuperClass">是否允许扫描父类</param>
        /// <returns>当前实例</returns>
        public new MethodAnnotationScanner SetIncludeSuperClass(bool includeSuperClass)
        {
            return base.SetIncludeSuperClass(includeSuperClass);
        }

        /// <summary>
        /// 是否允许扫描父接口
        /// </summary>
        /// <param name="includeInterfaces">是否允许扫描父类</param>
        /// <returns>当前实例</returns>
        public new MethodAnnotationScanner SetIncludeInterfaces(bool includeInterfaces)
        {
            return base.SetIncludeInterfaces(includeInterfaces);
        }
    }
}