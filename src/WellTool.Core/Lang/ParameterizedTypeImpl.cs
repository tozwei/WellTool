using System.Text;

namespace WellTool.Core.Lang;

/// <summary>
/// 参数化类型实现，用于重新定义泛型类型
/// </summary>
public class ParameterizedTypeImpl : IParameterizedType
{
	private readonly Type[] _actualTypeArguments;
	private readonly Type? _ownerType;
	private readonly Type _rawType;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="actualTypeArguments">实际的泛型参数类型</param>
	/// <param name="ownerType">拥有者类型</param>
	/// <param name="rawType">原始类型</param>
	public ParameterizedTypeImpl(Type[] actualTypeArguments, Type? ownerType, Type rawType)
	{
		_actualTypeArguments = actualTypeArguments;
		_ownerType = ownerType;
		_rawType = rawType;
	}

	/// <inheritdoc />
	public Type[] ActualTypeArguments => _actualTypeArguments;

	/// <inheritdoc />
	public Type? OwnerType => _ownerType;

	/// <inheritdoc />
	public Type RawType => _rawType;

	/// <inheritdoc />
	public override string ToString()
	{
		var buf = new StringBuilder();
		var useOwner = _ownerType;
		var raw = _rawType;

		if (useOwner == null)
		{
			buf.Append(raw.Name);
		}
		else
		{
			if (useOwner.IsGenericType)
			{
				buf.Append(useOwner.GetGenericTypeDefinition().Name);
			}
			else
			{
				buf.Append(useOwner.Name);
			}
			buf.Append('.').Append(raw.Name);
		}

		if (_actualTypeArguments.Length > 0)
		{
			buf.Append('<');
			for (var i = 0; i < _actualTypeArguments.Length; i++)
			{
				if (i > 0)
					buf.Append(", ");
				var type = _actualTypeArguments[i];
				buf.Append(type.IsGenericType ? type.GetGenericTypeDefinition().Name : type.Name);
			}
			buf.Append('>');
		}

		return buf.ToString();
	}
}

/// <summary>
/// 参数化类型接口
/// </summary>
public interface IParameterizedType
{
	/// <summary>
	/// 获取实际的泛型参数类型
	/// </summary>
	Type[] ActualTypeArguments { get; }

	/// <summary>
	/// 获取拥有者类型
	/// </summary>
	Type? OwnerType { get; }

	/// <summary>
	/// 获取原始类型
	/// </summary>
	Type RawType { get; }
}
