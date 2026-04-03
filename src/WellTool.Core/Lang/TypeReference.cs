namespace WellTool.Core.Lang;

/// <summary>
/// 类型引用，用于在运行时获取泛型类型信息
/// </summary>
/// <typeparam name="T">泛型类型</typeparam>
public class TypeReference<T> : ITypeReference
{
	/// <summary>
	/// 获取类型
	/// </summary>
	public Type Type => typeof(T);

	/// <inheritdoc />
	Type ITypeReference.RawType => typeof(T);

	/// <inheritdoc />
	Type? ITypeReference.GetParentType()
	{
		var type = typeof(T);
		if (type.IsGenericType)
			return type.GetGenericArguments().FirstOrDefault();
		var baseType = type.BaseType;
		if (baseType != null && baseType != typeof(object))
			return baseType;
		return null;
	}
}

/// <summary>
/// 类型引用接口
/// </summary>
public interface ITypeReference
{
	/// <summary>
	/// 获取原始类型
	/// </summary>
	Type RawType { get; }

	/// <summary>
	/// 获取父类型
	/// </summary>
	Type? GetParentType();
}
