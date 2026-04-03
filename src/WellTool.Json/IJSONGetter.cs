using System;

namespace WellTool.Json
{
    /// <summary>
    /// 用于JSON的Getter类，提供各种类型的Getter方法
    /// </summary>
    /// <typeparam name="K">Key类型</typeparam>
    public interface IJSONGetter<K>
    {
        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        object GetObj(K key);
    }
}
