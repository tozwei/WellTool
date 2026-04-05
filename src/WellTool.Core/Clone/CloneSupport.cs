using System;

namespace WellTool.Core.Clone
{
    /// <summary>
    /// 克隆支持类
    /// </summary>
    /// <typeparam name="T">克隆类型</typeparam>
    public abstract class CloneSupport<T> : Cloneable<T>
    {
        /// <summary>
        /// 克隆对象
        /// </summary>
        /// <returns>克隆后的对象</returns>
        public virtual T Clone()
        {
            try
            {
                return (T)MemberwiseClone();
            }
            catch (System.Exception e)
            {
                throw new CloneRuntimeException(e);
            }
        }

        /// <summary>
        /// 创建当前对象的浅拷贝
        /// </summary>
        /// <returns>浅拷贝对象</returns>
        protected object MemberwiseClone()
        {
            return base.MemberwiseClone();
        }
    }
}