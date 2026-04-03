using System;
using System.Text;

namespace WellTool.Core.Lang;

/// <summary>
/// 参数化类型实现
/// </summary>
[Serializable]
public class ParameterizedTypeImpl : System.Reflection.ParameterizedType
{
	private readonly Type[] _actualTypeArguments;
	private readonly Type _ownerType;
	private readonly Type _rawType;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="actualTypeArguments">实际的泛型参数类型</param>
	/// <param name="ownerType">拥有者类型</param>
	/// <param name="rawType">原始类型</param>
	public ParameterizedTypeImpl(Type[] actualTypeArguments, Type ownerType, Type rawType)
	{
		_actualTypeArguments = actualTypeArguments;
		_ownerType = ownerType;
		_rawType = rawType;
	}

	/// <summary>
	/// 获取实际的泛型参数类型
	/// </summary>
	public override Type[] GetGenericArguments()
	{
		return _actualTypeArguments;
	}

	/// <summary>
	/// 获取拥有者类型
	/// </summary>
	public Type OwnerType => _ownerType;

	/// <summary>
	/// 获取原始类型
	/// </summary>
	public Type RawType => _rawType;

	/// <summary>
	/// 获取基础类型
	/// </summary>
	public override Type BaseType => _rawType;

	public override string Name => GetName();

	private string GetName()
	{
		var buf = new StringBuilder();
		var raw = _rawType;
		if (_ownerType == null)
		{
			buf.Append(raw.FullName);
		}
		else
		{
			buf.Append(_ownerType.FullName);
			buf.Append('.');
			buf.Append(raw.Name);
		}
		buf.Append('<');
		for (int i = 0; i < _actualTypeArguments.Length; i++)
		{
			if (i > 0) buf.Append(", ");
			buf.Append(_actualTypeArguments[i].Name);
		}
		buf.Append('>');
		return buf.ToString();
	}
}
