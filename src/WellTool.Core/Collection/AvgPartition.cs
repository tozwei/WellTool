using System;
using System.Collections.Generic;

namespace WellTool.Core.Collection
{
    /// <summary>
    /// 平均分割器，将集合分割成多个大小相近的分组
    /// </summary>
    public static class AvgPartition
    {
        /// <summary>
        /// 将列表分割成指定数量的组
        /// </summary>
        public static List<List<T>> PartitionByCount<T>(IList<T> list, int partitionCount)
        {
            if (list == null || list.Count == 0)
            {
                return new List<List<T>>();
            }

            if (partitionCount <= 0)
            {
                throw new ArgumentException("Partition count must be greater than 0", nameof(partitionCount));
            }

            partitionCount = Math.Min(partitionCount, list.Count);
            var result = new List<List<T>>(partitionCount);

            int avgSize = list.Count / partitionCount;
            int remainder = list.Count % partitionCount;

            int index = 0;
            for (int i = 0; i < partitionCount; i++)
            {
                int size = avgSize + (i < remainder ? 1 : 0);
                var group = new List<T>(size);
                for (int j = 0; j < size; j++)
                {
                    group.Add(list[index++]);
                }
                result.Add(group);
            }

            return result;
        }

        /// <summary>
        /// 将列表分割成指定大小的组
        /// </summary>
        public static List<List<T>> PartitionBySize<T>(IList<T> list, int size)
        {
            if (list == null || list.Count == 0)
            {
                return new List<List<T>>();
            }

            if (size <= 0)
            {
                throw new ArgumentException("Size must be greater than 0", nameof(size));
            }

            int partitionCount = (list.Count + size - 1) / size;
            var result = new List<List<T>>(partitionCount);

            for (int i = 0; i < partitionCount; i++)
            {
                int start = i * size;
                int end = Math.Min(start + size, list.Count);
                var group = new List<T>(end - start);
                for (int j = start; j < end; j++)
                {
                    group.Add(list[j]);
                }
                result.Add(group);
            }

            return result;
        }

        /// <summary>
        /// 将数组分割成指定数量的组
        /// </summary>
        public static T[][] PartitionByCount<T>(T[] array, int partitionCount)
        {
            if (array == null || array.Length == 0)
            {
                return Array.Empty<T[]>();
            }

            var list = new List<T>(array);
            var result = PartitionByCount(list, partitionCount);
            var arrayResult = new T[result.Count][];
            for (int i = 0; i < result.Count; i++)
            {
                arrayResult[i] = result[i].ToArray();
            }
            return arrayResult;
        }

        /// <summary>
        /// 将数组分割成指定大小的组
        /// </summary>
        public static T[][] PartitionBySize<T>(T[] array, int size)
        {
            if (array == null || array.Length == 0)
            {
                return Array.Empty<T[]>();
            }

            var list = new List<T>(array);
            var result = PartitionBySize(list, size);
            var arrayResult = new T[result.Count][];
            for (int i = 0; i < result.Count; i++)
            {
                arrayResult[i] = result[i].ToArray();
            }
            return arrayResult;
        }
    }
}
