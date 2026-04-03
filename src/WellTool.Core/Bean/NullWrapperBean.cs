using System;

namespace WellTool.Core.Bean
{
    /// <summary>
    /// Null值包装类，用于在反射过程中传递null参数但保留类型信息
    /// </summary>
    /// <typeparam name="T">Null值对应的类型</typeparam>
    public class NullWrapperBean<T>
    {
        private readonly Type _wrappedClass;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="clazz">null的类型</param>
        public NullWrapperBean(Type clazz)
        {
            _wrappedClass = clazz;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public NullWrapperBean()
        {
            _wrappedClass = typeof(T);
        }

        /// <summary>
        /// 获取null值对应的类型
        /// </summary>
        public Type WrappedClass
        {
            get { return _wrappedClass; }
        }

        /// <summary>
        /// 获取null值对应的类型
        /// </summary>
        public Type GetWrappedClass()
        {
            return _wrappedClass;
        }
    }
}
