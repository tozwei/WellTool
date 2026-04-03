namespace WellTool.Core.Lang;

using System;

namespace WellTool.Core.Lang
{
    /// <summary>
    /// 片段默认实现
    /// </summary>
    /// <typeparam name="T">数字类型，用于表示位置index</typeparam>
    public class DefaultSegment<T> : Segment where T : struct
    {
        protected T _startIndex;
        protected T _endIndex;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="startIndex">起始位置</param>
        /// <param name="endIndex">结束位置</param>
        public DefaultSegment(T startIndex, T endIndex)
        {
            _startIndex = startIndex;
            _endIndex = endIndex;
        }

        /// <summary>
        /// 获取起始位置
        /// </summary>
        public T GetStartIndex() => _startIndex;

        /// <summary>
        /// 获取结束位置
        /// </summary>
        public T GetEndIndex() => _endIndex;

        /// <summary>
        /// 获取起始位置（作为 object 返回）
        /// </summary>
        object? Segment.StartIndex => _startIndex;

        /// <summary>
        /// 获取结束位置（作为 object 返回）
        /// </summary>
        object? Segment.EndIndex => _endIndex;
    }
}
