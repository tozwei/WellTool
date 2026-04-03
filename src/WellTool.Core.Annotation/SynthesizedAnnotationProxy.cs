using System;
using System.Collections.Generic;
using System.Reflection;

namespace WellTool.Core.Annotation
{
    /// <summary>
    /// 合成注解代理
    /// </summary>
    public class SynthesizedAnnotationProxy : System.Reflection.IInvocationHandler
    {
        private readonly IAnnotationAttributeValueProvider _attributeValueProvider;
        private readonly ISynthesizedAnnotation _synthesizedAnnotation;
        private readonly Dictionary<string, MethodInfo> _methods = new Dictionary<string, MethodInfo>();

        /// <summary>
        /// 创建注解代理
        /// </summary>
        /// <typeparam name="T">注解类型</typeparam>
        /// <param name="annotationType">注解类型</param>
        /// <param name="attributeValueProvider">属性值提供器</param>
        /// <param name="synthesizedAnnotation">合成注解</param>
        /// <returns>注解代理</returns>
        public static T Create<T>(Type annotationType, IAnnotationAttributeValueProvider attributeValueProvider, ISynthesizedAnnotation synthesizedAnnotation) where T : class
        {
            return Create(annotationType, attributeValueProvider, synthesizedAnnotation) as T;
        }

        /// <summary>
        /// 创建注解代理
        /// </summary>
        /// <param name="annotationType">注解类型</param>
        /// <param name="attributeValueProvider">属性值提供器</param>
        /// <param name="synthesizedAnnotation">合成注解</param>
        /// <returns>注解代理</returns>
        public static Annotation Create(Type annotationType, IAnnotationAttributeValueProvider attributeValueProvider, ISynthesizedAnnotation synthesizedAnnotation)
        {
            var proxy = new SynthesizedAnnotationProxy(attributeValueProvider, synthesizedAnnotation);
            return (Annotation)Proxy.NewProxyInstance(
                new[] { annotationType, typeof(ISyntheticProxyAnnotation) },
                proxy);
        }

        private SynthesizedAnnotationProxy(IAnnotationAttributeValueProvider attributeValueProvider, ISynthesizedAnnotation synthesizedAnnotation)
        {
            _attributeValueProvider = attributeValueProvider;
            _synthesizedAnnotation = synthesizedAnnotation;
            LoadMethods();
        }

        private void LoadMethods()
        {
            // 加载合成注解接口方法
            var synthesizedType = typeof(ISynthesizedAnnotation);
            foreach (var method in synthesizedType.GetMethods())
            {
                _methods[method.Name] = method;
            }

            // 加载注解属性方法
            var annotationType = _synthesizedAnnotation.AnnotationType();
            foreach (var method in annotationType.GetMethods())
            {
                if (method.DeclaringType == typeof(Annotation) || method.DeclaringType == typeof(object))
                    continue;
                
                var methodName = method.Name;
                if (!_methods.ContainsKey(methodName))
                {
                    _methods[methodName] = method;
                }
            }
        }

        public object Invoke(object proxy, MethodInfo method, object[] args)
        {
            var methodName = method.Name;
            
            // 处理Object方法
            if (methodName == "toString")
            {
                return ProxyToString();
            }
            if (methodName == "hashCode")
            {
                return GetHashCode();
            }
            if (methodName == "equals")
            {
                return Equals(args[0]);
            }
            
            // 检查是否为合成注解接口方法
            if (_methods.TryGetValue(methodName, out var targetMethod))
            {
                return targetMethod.Invoke(_synthesizedAnnotation, args);
            }
            
            // 获取注解属性值
            if (_attributeValueProvider != null)
            {
                var value = _attributeValueProvider.GetAttributeValue(methodName, method.ReturnType);
                if (value != null)
                {
                    return value;
                }
            }
            
            // 返回默认值
            return method.ReturnType.IsValueType ? Activator.CreateInstance(method.ReturnType) : null;
        }

        private string ProxyToString()
        {
            var annotationType = _synthesizedAnnotation.AnnotationType();
            return $"[@{annotationType.Name}(...)]";
        }

        /// <summary>
        /// 标记接口，用于标识代理注解
        /// </summary>
        public interface ISyntheticProxyAnnotation
        {
            /// <summary>
            /// 获取底层合成注解
            /// </summary>
            ISynthesizedAnnotation GetSynthesizedAnnotation();
        }
    }
}