using System;
using System.Collections.Generic;

namespace WellTool.Core.Annotation
{
    /// <summary>
    /// 包装注解属性基类
    /// </summary>
    public abstract class AbstractWrappedAnnotationAttribute : IAnnotationAttribute
    {
        protected readonly IAnnotationAttribute _original;
        protected readonly IAnnotationAttribute _linked;

        protected AbstractWrappedAnnotationAttribute(IAnnotationAttribute original, IAnnotationAttribute linked)
        {
            if (original == null)
                throw new ArgumentNullException(nameof(original));
            if (linked == null)
                throw new ArgumentNullException(nameof(linked));
            
            _original = original;
            _linked = linked;
        }

        /// <summary>
        /// 获取被包装的注解属性对象
        /// </summary>
        public virtual IAnnotationAttribute GetOriginal() => _original;

        /// <summary>
        /// 获取包装对象的注解属性
        /// </summary>
        public virtual IAnnotationAttribute GetLinked() => _linked;

        /// <summary>
        /// 获取最初的被包装的注解属性
        /// </summary>
        public virtual IAnnotationAttribute GetNonWrappedOriginal()
        {
            IAnnotationAttribute curr = null;
            IAnnotationAttribute next = _original;
            while (next != null)
            {
                curr = next;
                next = curr.IsWrapped() ? ((AbstractWrappedAnnotationAttribute)curr).GetOriginal() : null;
            }
            return curr;
        }

        /// <summary>
        /// 遍历以当前实例为根节点的树结构，获取所有未被包装的属性
        /// </summary>
        public virtual IEnumerable<IAnnotationAttribute> GetAllLinkedNonWrappedAttributes()
        {
            var leafAttributes = new List<IAnnotationAttribute>();
            CollectLeafAttribute(this, leafAttributes);
            return leafAttributes;
        }

        private void CollectLeafAttribute(IAnnotationAttribute curr, List<IAnnotationAttribute> leafAttributes)
        {
            if (curr == null)
            {
                return;
            }
            if (!curr.IsWrapped())
            {
                leafAttributes.Add(curr);
                return;
            }
            var wrappedAttribute = (AbstractWrappedAnnotationAttribute)curr;
            CollectLeafAttribute(wrappedAttribute.GetOriginal(), leafAttributes);
            CollectLeafAttribute(wrappedAttribute.GetLinked(), leafAttributes);
        }

        public virtual Annotation GetAnnotation() => GetOriginal().GetAnnotation();

        public virtual System.Reflection.MethodInfo GetAttribute() => GetOriginal().GetAttribute();

        public virtual Type GetAnnotationType() => GetAttribute().DeclaringType;

        public virtual string GetAttributeName() => GetAttribute().Name;

        public virtual Type GetAttributeType() => GetOriginal().GetAttributeType();

        public bool IsWrapped() => true;
    }
}