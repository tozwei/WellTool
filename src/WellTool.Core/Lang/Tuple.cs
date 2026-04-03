using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WellTool.Core.Clone;

namespace WellTool.Core.Lang;

/// <summary>
/// 不可变数组类型（元组），用于多值返回
/// </summary>
[Serializable]
public class Tuple : CloneSupport<Tuple>, IEnumerable<object>
{
	private readonly object[] _members;
	private int _hashCode;
	private bool _cacheHash;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="members">成员数组</param>
	public Tuple(params object[] members)
	{
		_members = (object[])members.Clone();
	}

	/// <summary>
	/// 获取指定位置元素
	/// </summary>
	/// <typeparam name="T">返回对象类型</typeparam>
	/// <param name="index">位置</param>
	/// <returns>元素</returns>
	public T Get<T>(int index)
	{
		return (T)_members[index];
	}

	/// <summary>
	/// 获取指定位置元素
	/// </summary>
	/// <param name="index">位置</param>
	/// <returns>元素</returns>
	public object Get(int index)
	{
		return _members[index];
	}

	/// <summary>
	/// 获得所有元素
	/// </summary>
	/// <returns>所有元素的副本</returns>
	public object[] GetMembers()
	{
		return (object[])_members.Clone();
	}

	/// <summary>
	/// 将元组转换成列表
	/// </summary>
	/// <returns>转换得到的列表</returns>
	public List<object> ToList()
	{
		return _members.ToList();
	}

	/// <summary>
	/// 缓存Hash值
	/// </summary>
	/// <param name="cacheHash">是否缓存hash值</param>
	/// <returns>this</returns>
	public Tuple SetCacheHash(bool cacheHash)
	{
		_cacheHash = cacheHash;
		return this;
	}

	/// <summary>
	/// 得到元组的大小
	/// </summary>
	public int Size => _members.Length;

	/// <summary>
	/// 判断元组中是否包含某元素
	/// </summary>
	/// <param name="value">需要判定的元素</param>
	/// <returns>是否包含</returns>
	public bool Contains(object value)
	{
		return _members.Contains(value);
	}

	/// <summary>
	/// 获取第0个元素
	/// </summary>
	public object Item0 => _members.Length > 0 ? _members[0] : null;

	/// <summary>
	/// 获取第1个元素
	/// </summary>
	public object Item1 => _members.Length > 1 ? _members[1] : null;

	/// <summary>
	/// 获取第2个元素
	/// </summary>
	public object Item2 => _members.Length > 2 ? _members[2] : null;

	/// <summary>
	/// 获取第3个元素
	/// </summary>
	public object Item3 => _members.Length > 3 ? _members[3] : null;

	public override int GetHashCode()
	{
		if (_cacheHash && _hashCode != 0)
		{
			return _hashCode;
		}
		int result = 1;
		foreach (var item in _members)
		{
			result = 31 * result + (item?.GetHashCode() ?? 0);
		}
		if (_cacheHash)
		{
			_hashCode = result;
		}
		return result;
	}

	public override bool Equals(object obj)
	{
		if (this == obj) return true;
		if (obj == null || GetType() != obj.GetType()) return false;
		var other = (Tuple)obj;
		if (_members.Length != other._members.Length) return false;
		for (int i = 0; i < _members.Length; i++)
		{
			if (!Equals(_members[i], other._members[i])) return false;
		}
		return true;
	}

	public override string ToString()
	{
		return "[" + string.Join(", ", _members.Select(m => m?.ToString() ?? "null")) + "]";
	}

	public IEnumerator<object> GetEnumerator()
	{
		return ((IEnumerable<object>)_members).GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}
