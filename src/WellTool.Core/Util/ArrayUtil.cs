// Copyright (c) 2025 WellTool Team
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;

namespace WellTool.Core.Util;

/// <summary>
/// 数组工具类
/// </summary>
public static class ArrayUtil
{
    /// <summary>
    /// 合并两个数组
    /// </summary>
    /// <typeparam name="T">数组类型</typeparam>
    /// <param name="array1">第一个数组</param>
    /// <param name="array2">第二个数组</param>
    /// <returns>合并后的数组</returns>
    public static T[] AddAll<T>(T[] array1, T[] array2)
    {
        if (array1 == null || array1.Length == 0)
        {
            return array2;
        }
        if (array2 == null || array2.Length == 0)
        {
            return array1;
        }

        T[] result = new T[array1.Length + array2.Length];
        Array.Copy(array1, 0, result, 0, array1.Length);
        Array.Copy(array2, 0, result, array1.Length, array2.Length);
        return result;
    }

    /// <summary>
    /// 检查数组是否为空
    /// </summary>
    /// <typeparam name="T">数组类型</typeparam>
    /// <param name="array">数组</param>
    /// <returns>是否为空</returns>
    public static bool IsEmpty<T>(T[] array)
    {
        return array == null || array.Length == 0;
    }

    /// <summary>
    /// 检查数组是否不为空
    /// </summary>
    /// <typeparam name="T">数组类型</typeparam>
    /// <param name="array">数组</param>
    /// <returns>是否不为空</returns>
    public static bool IsNotEmpty<T>(T[] array)
    {
        return !IsEmpty(array);
    }

    /// <summary>
    /// 获取数组长度
    /// </summary>
    /// <typeparam name="T">数组类型</typeparam>
    /// <param name="array">数组</param>
    /// <returns>数组长度</returns>
    public static int Length<T>(T[] array)
    {
        return array?.Length ?? 0;
    }

    /// <summary>
    /// 将数组转换为列表
    /// </summary>
    /// <typeparam name="T">数组类型</typeparam>
    /// <param name="array">数组</param>
    /// <returns>列表</returns>
    public static List<T> ToList<T>(T[] array)
    {
        return array == null ? new List<T>() : new List<T>(array);
    }

    /// <summary>
    /// 反转数组
    /// </summary>
    /// <typeparam name="T">数组类型</typeparam>
    /// <param name="array">数组</param>
    /// <returns>反转后的数组</returns>
    public static T[] Reverse<T>(T[] array)
    {
        if (array == null)
        {
            return null;
        }

        // 反转原数组
        for (int i = 0; i < array.Length / 2; i++)
        {
            T temp = array[i];
            array[i] = array[array.Length - 1 - i];
            array[array.Length - 1 - i] = temp;
        }
        return array;
    }

    /// <summary>
    /// 复制数组
    /// </summary>
    /// <typeparam name="T">数组类型</typeparam>
    /// <param name="array">数组</param>
    /// <returns>复制后的数组</returns>
    public static T[] CopyOf<T>(T[] array)
    {
        if (array == null)
        {
            return null;
        }

        T[] result = new T[array.Length];
        Array.Copy(array, result, array.Length);
        return result;
    }

    /// <summary>
    /// 复制数组的一部分
    /// </summary>
    /// <typeparam name="T">数组类型</typeparam>
    /// <param name="array">数组</param>
    /// <param name="startIndex">起始索引</param>
    /// <param name="length">长度</param>
    /// <returns>复制后的数组</returns>
    public static T[] CopyOfRange<T>(T[] array, int startIndex, int length)
    {
        if (array == null)
        {
            return null;
        }

        if (startIndex < 0 || startIndex >= array.Length)
        {
            throw new IndexOutOfRangeException("startIndex is out of range");
        }

        int actualLength = System.Math.Min(length, array.Length - startIndex);
        T[] result = new T[actualLength];
        Array.Copy(array, startIndex, result, 0, actualLength);
        return result;
    }

    /// <summary>
    /// 填充数组
    /// </summary>
    /// <typeparam name="T">数组类型</typeparam>
    /// <param name="array">数组</param>
    /// <param name="value">填充值</param>
    /// <returns>填充后的数组</returns>
    public static T[] Fill<T>(T[] array, T value)
    {
        if (array == null)
        {
            return null;
        }

        for (int i = 0; i < array.Length; i++)
        {
            array[i] = value;
        }
        return array;
    }

    /// <summary>
    /// 查找元素在数组中的索引
    /// </summary>
    /// <typeparam name="T">数组类型</typeparam>
    /// <param name="array">数组</param>
    /// <param name="value">要查找的元素</param>
    /// <returns>元素在数组中的索引，-1表示未找到</returns>
    public static int IndexOf<T>(T[] array, T value)
    {
        if (array == null)
        {
            return -1;
        }

        for (int i = 0; i < array.Length; i++)
        {
            if (Equals(array[i], value))
            {
                return i;
            }
        }
        return -1;
    }

    /// <summary>
    /// 检查数组是否包含指定元素
    /// </summary>
    /// <typeparam name="T">数组类型</typeparam>
    /// <param name="array">数组</param>
    /// <param name="value">要检查的元素</param>
    /// <returns>是否包含指定元素</returns>
    public static bool Contains<T>(T[] array, T value)
    {
        return IndexOf(array, value) != -1;
    }

    /// <summary>
    /// 移除数组中的指定元素
    /// </summary>
    /// <typeparam name="T">数组类型</typeparam>
    /// <param name="array">数组</param>
    /// <param name="value">要移除的元素</param>
    /// <returns>移除元素后的数组</returns>
    public static T[] Remove<T>(T[] array, T value)
    {
        if (array == null)
        {
            return null;
        }

        int index = IndexOf(array, value);
        if (index == -1)
        {
            return array;
        }

        T[] result = new T[array.Length - 1];
        Array.Copy(array, 0, result, 0, index);
        Array.Copy(array, index + 1, result, index, array.Length - index - 1);
        return result;
    }

    /// <summary>
    /// 移除数组中指定索引的元素
    /// </summary>
    /// <typeparam name="T">数组类型</typeparam>
    /// <param name="array">数组</param>
    /// <param name="index">要移除的元素的索引</param>
    /// <returns>移除元素后的数组</returns>
    public static T[] RemoveAt<T>(T[] array, int index)
    {
        if (array == null)
        {
            return null;
        }

        if (index < 0 || index >= array.Length)
        {
            throw new IndexOutOfRangeException("index is out of range");
        }

        T[] result = new T[array.Length - 1];
        Array.Copy(array, 0, result, 0, index);
        Array.Copy(array, index + 1, result, index, array.Length - index - 1);
        return result;
    }

    /// <summary>
    /// 在数组的指定位置插入元素
    /// </summary>
    /// <typeparam name="T">数组类型</typeparam>
    /// <param name="array">数组</param>
    /// <param name="index">插入位置</param>
    /// <param name="value">要插入的元素</param>
    /// <returns>插入元素后的数组</returns>
    public static T[] Insert<T>(T[] array, int index, T value)
    {
        if (array == null)
        {
            return new T[] { value };
        }

        if (index < 0 || index > array.Length)
        {
            throw new IndexOutOfRangeException("index is out of range");
        }

        T[] result = new T[array.Length + 1];
        Array.Copy(array, 0, result, 0, index);
        result[index] = value;
        Array.Copy(array, index, result, index + 1, array.Length - index);
        return result;
    }

    /// <summary>
    /// 截取数组的一部分
    /// </summary>
    /// <typeparam name="T">数组类型</typeparam>
    /// <param name="array">数组</param>
    /// <param name="start">起始索引</param>
    /// <param name="end">结束索引</param>
    /// <param name="step">步长</param>
    /// <returns>截取后的数组</returns>
    public static T[] Sub<T>(T[] array, int start, int end, int step)
    {
        if (array == null)
        {
            return null;
        }

        int length = array.Length;
        if (start < 0)
        {
            start = length + start;
        }
        if (end < 0)
        {
            end = length + end;
        }

        start = System.Math.Max(0, start);
        end = System.Math.Min(length, end);

        if (start >= end || step <= 0)
        {
            return Array.Empty<T>();
        }

        List<T> result = new List<T>();
        for (int i = start; i < end; i += step)
        {
            result.Add(array[i]);
        }

        return result.ToArray();
    }

    /// <summary>
    /// 截取数组的一部分
    /// </summary>
    /// <typeparam name="T">数组类型</typeparam>
    /// <param name="array">数组</param>
    /// <param name="start">起始索引</param>
    /// <param name="end">结束索引</param>
    /// <returns>截取后的数组</returns>
    public static T[] Sub<T>(T[] array, int start, int end)
    {
        return Sub(array, start, end, 1);
    }
}