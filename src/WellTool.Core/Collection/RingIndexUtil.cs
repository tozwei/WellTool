using System;

namespace WellTool.Core.Collection
{
    /// <summary>
    /// 环形索引工具类，用于在固定大小的数组中循环访问
    /// </summary>
    public static class RingIndexUtil
    {
        /// <summary>
        /// 计算环形索引
        /// </summary>
        /// <param name="index">当前索引</param>
        /// <param name="capacity">容量</param>
        /// <returns>调整后的索引</returns>
        public static int RingIndex(int index, int capacity)
        {
            if (capacity <= 0)
            {
                throw new ArgumentException("Capacity must be greater than 0", nameof(capacity));
            }

            int result = index % capacity;
            if (result < 0)
            {
                result += capacity;
            }
            return result;
        }

        /// <summary>
        /// 移动到下一个索引
        /// </summary>
        public static int Next(int index, int capacity)
        {
            return RingIndex(index + 1, capacity);
        }

        /// <summary>
        /// 移动到上一个索引
        /// </summary>
        public static int Previous(int index, int capacity)
        {
            return RingIndex(index - 1, capacity);
        }

        /// <summary>
        /// 向前移动指定步数
        /// </summary>
        public static int Forward(int index, int steps, int capacity)
        {
            return RingIndex(index + steps, capacity);
        }

        /// <summary>
        /// 向后移动指定步数
        /// </summary>
        public static int Backward(int index, int steps, int capacity)
        {
            return RingIndex(index - steps, capacity);
        }

        /// <summary>
        /// 计算两个索引之间的距离（顺时针）
        /// </summary>
        public static int DistanceClockwise(int from, int to, int capacity)
        {
            from = RingIndex(from, capacity);
            to = RingIndex(to, capacity);

            if (to >= from)
            {
                return to - from;
            }
            return capacity - from + to;
        }

        /// <summary>
        /// 计算两个索引之间的距离（逆时针）
        /// </summary>
        public static int DistanceCounterClockwise(int from, int to, int capacity)
        {
            return DistanceClockwise(to, from, capacity);
        }

        /// <summary>
        /// 判断索引是否有效
        /// </summary>
        public static bool IsValidIndex(int index, int capacity)
        {
            return capacity > 0 && index >= 0 && index < capacity;
        }

        /// <summary>
        /// 环形迭代器
        /// </summary>
        public static IEnumerable<int> Range(int start, int count, int capacity)
        {
            int index = RingIndex(start, capacity);
            for (int i = 0; i < count; i++)
            {
                yield return index;
                index = Next(index, capacity);
            }
        }
    }
}
