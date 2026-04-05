using System.Collections;

namespace WellTool.Core.Collection;

/// <summary>
/// 列表分区或分段（可随机访问列表）
/// 通过传入分区长度，将指定列表分区为不同的块，每块区域的长度相同（最后一块可能小于长度）
/// 分区是在原List的基础上进行的，返回的分区是不可变的抽象列表，原列表元素变更，分区中元素也会变更。
/// </summary>
/// <typeparam name="T">元素类型</typeparam>
public class RandomAccessPartition<T> : Partition<T>, IList
{
    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="list">被分区的列表，必须实现随机访问</param>
    /// <param name="size">每个分区的长度</param>
    public RandomAccessPartition(IList<T> list, int size) : base(list, size)
    {
    }

    // IList 实现
    public bool IsFixedSize => true;
    public bool IsReadOnly => true;
    public object this[int index] { get => ((IList)_list)[index]; set => throw new NotSupportedException(); }
    public void CopyTo(Array array, int index) { ((IList)_list).CopyTo(array, index); }
    public IEnumerator GetEnumerator() => ((IEnumerable<T>)this).GetEnumerator();
    public int Add(object value) => throw new NotSupportedException();
    public void Clear() => throw new NotSupportedException();
    public bool Contains(object value) => ((IList)_list).Contains(value);
    public int IndexOf(object value) => ((IList)_list).IndexOf(value);
    public void Insert(int index, object value) => throw new NotSupportedException();
    public void Remove(object value) => throw new NotSupportedException();
    public void RemoveAt(int index) => throw new NotSupportedException();

    // ICollection 实现
    public bool IsSynchronized => false;
    public object SyncRoot => this;
}
