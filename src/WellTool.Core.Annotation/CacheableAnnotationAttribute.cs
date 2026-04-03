using System;
using System.Reflection;

namespace WellTool.Core.Annotation
{
    /// <summary>
    /// 缓存的注解属性实现
    /// </summary>
    public class CacheableAnnotationAttribute : IAnnotationAttribute
    {
        private volatile bool _valueInvoked;
        private volatile object _value;
        private bool _defaultValueInvoked;
        private object _defaultValue;
        private readonly Annotation _annotation;
        private readonly MethodInfo _attribute;

        public CacheableAnnotationAttribute(Annotation annotation, MethodInfo attribute)
        {
            if (annotation == null)
                throw new ArgumentNullException(nameof(annotation));
            if (attribute == null)
                throw new ArgumentNullException(nameof(attribute));
            
            _annotation = annotation;
            _attribute = attribute;
            _valueInvoked = false;
            _defaultValueInvoked = false;
        }

        public Annotation GetAnnotation() => _annotation;

        public MethodInfo GetAttribute() => _attribute;

        public Type GetAnnotationType() => GetAttribute().DeclaringType;

        public string GetAttributeName() => GetAttribute().Name;

        public object GetValue()
        {
            if (!_valueInvoked)
            {
                lock (this)
                {
                    if (!_valueInvoked)
                    {
                        _valueInvoked = true;
                        _value = GetAttribute().Invoke(_annotation, null);
                    }
                }
            }
            return _value;
        }

        public bool IsValueEquivalentToDefaultValue()
        {
            if (!_defaultValueInvoked)
            {
                _defaultValue = GetAttribute().GetDefaultValue();
                _defaultValueInvoked = true;
            }
            return Equals(GetValue(), _defaultValue);
        }

        public Type GetAttributeType() => GetAttribute().ReturnType;

        public bool IsWrapped() => false;
    }
}