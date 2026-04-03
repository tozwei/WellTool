using System.Collections;

namespace WellTool.Core.Collection;

/// <summary>
/// 列表分区或分段（可随机访问列表）
/// 通过传入分区个数，将指定列表分区为不同的块，每块区域的长度均匀分布（个数差不超过1）
/// 分区是在原List的基础上进行的，返回的分区是不可变的抽象列表，原列表元素变更，分区中元素也会变更。
/// </summary>
/// <typeparam name="T">元素类型</typeparam>
public class RandomAccessAvgPartition<T> : AvgPartition<T>, IList
{
    /// <summary>
    /// 列表分区
    /// </summary>
    /// <param name="list">被分区的列表</param>
    /// <param name="limit">分区个数</param>
    public RandomAccessAvgPartition(IList<T> list, int limit) : base(list, limit)
    {
    }

    // IList 实现
    public bool IsFixedSize => true;
    public bool IsReadOnly => true;
    public object this[int index] { get => ((IList)GetSourceList())[index]; set => throw new NotSupportedException(); }
    public void CopyTo(Array array, int index) { ((IList)GetSourceList()).CopyTo(array, index); }
    public IEnumerator GetEnumerator() => ((IEnumerable<T>)this).GetEnumerator();
    public int Add(object value) => throw new NotSupportedException();
    public void Clear() => throw new NotSupportedException();
    public bool Contains(object value) => ((IList)GetSourceList()).Contains(value);
    public int IndexOf(object value) => ((IList)GetSourceList()).IndexOf(value);
    public void Insert(int index, object value) => throw new NotSupportedException();
    public void Remove(object value) => throw new NotSupportedException();
    public void RemoveAt(int index) => throw new NotSupportedException();
}
