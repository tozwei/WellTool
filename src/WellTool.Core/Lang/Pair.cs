using System;

namespace WellTool.Core.Lang;

/// <summary>
/// 键值对封装
/// </summary>
/// <typeparam name="K">键类型</typeparam>
/// <typeparam name="V">值类型</typeparam>
public class Pair<K, V>
{
	/// <summary>
	/// 键
	/// </summary>
	public K Key { get; set; }

	/// <summary>
	/// 值
	/// </summary>
	public V Value { get; set; }

	/// <summary>
	/// 构造
	/// </summary>
	public Pair()
	{
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="key">键</param>
	/// <param name="value">值</param>
	public Pair(K key, V value)
	{
		Key = key;
		Value = value;
	}

	/// <summary>
	/// 创建键值对
	/// </summary>
	/// <param name="key">键</param>
	/// <param name="value">值</param>
	/// <returns>键值对</returns>
	public static Pair<K, V> Of(K key, V value) => new Pair<K, V>(key, value);

	/// <summary>
	/// 转换为元组
	/// </summary>
	public (K, V) ToTuple() => (Key, Value);

	/// <summary>
	/// 获取键，如果值为空返回默认值
	/// </summary>
	public K GetKeyOrDefault() => Key;

	/// <summary>
	/// 获取值，如果值为空返回默认值
	/// </summary>
	public V GetValueOrDefault(V defaultValue = default) => Value ?? defaultValue;

	public override bool Equals(object obj)
	{
		if (obj is Pair<K, V> other)
		{
			return Equals(Key, other.Key) && Equals(Value, other.Value);
		}
		return false;
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(Key, Value);
	}

	public override string ToString()
	{
		return $"{Key}={Value}";
	}
}
