using System;
using System.Linq;

namespace WellTool.Core.Lang;

/// <summary>
/// 枚举元素通用接口
/// </summary>
/// <typeparam name="E">Enum类型</typeparam>
public interface IEnumItem<E> where E : IEnumItem<E>
{
	/// <summary>
	/// 获取名称
	/// </summary>
	string Name();

	/// <summary>
	/// 获取文本
	/// </summary>
	string Text();

	/// <summary>
	/// 获取整数值
	/// </summary>
	int IntVal();

	/// <summary>
	/// 获取所有枚举对象
	/// </summary>
	/// <returns>枚举对象数组</returns>
	static E[] Items()
	{
		throw new NotImplementedException();
	}

	/// <summary>
	/// 通过int类型值查找枚举
	/// </summary>
	/// <param name="intVal">int值</param>
	/// <returns>Enum</returns>
	static E FromInt(int? intVal)
	{
		if (intVal == null) return default;
		var items = Items();
		foreach (var item in items)
		{
			if (item.IntVal() == intVal)
			{
				return item;
			}
		}
		return default;
	}

	/// <summary>
	/// 通过String类型的值转换
	/// </summary>
	/// <param name="strVal">String值</param>
	/// <returns>Enum</returns>
	static E FromStr(string strVal)
	{
		if (strVal == null) return default;
		var items = Items();
		foreach (var item in items)
		{
			if (strVal.Equals(item.Name(), StringComparison.OrdinalIgnoreCase))
			{
				return item;
			}
		}
		return default;
	}
}

/// <summary>
/// 枚举项接口的默认实现
/// </summary>
/// <typeparam name="E">枚举类型</typeparam>
public abstract class EnumItem<E> : IEnumItem<E> where E : EnumItem<E>
{
	public abstract string Name();
	public virtual string Text() => Name();
	public abstract int IntVal();

	/// <summary>
	/// 获取所有枚举值
	/// </summary>
	public static E[] Items => (E[])typeof(E).GetEnumValues();
}
