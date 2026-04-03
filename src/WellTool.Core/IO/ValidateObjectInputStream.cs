using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace WellTool.Core.IO;

/// <summary>
/// 验证对象输入流
/// </summary>
public class ValidateObjectInputStream : ObjectInputStream
{
	private readonly Type[] _acceptedTypes;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="inputStream">输入流</param>
	/// <param name="acceptedTypes">接受的类型</param>
	public ValidateObjectInputStream(Stream inputStream, params Type[] acceptedTypes)
		: base(inputStream)
	{
		_acceptedTypes = acceptedTypes;
	}

	/// <summary>
	/// 验证类型
	/// </summary>
	protected override Type ResolveType(Type type)
	{
		if (_acceptedTypes != null && _acceptedTypes.Length > 0)
		{
			foreach (var acceptedType in _acceptedTypes)
			{
				if (type == acceptedType || type.IsSubclassOf(acceptedType))
				{
					return base.ResolveType(type);
				}
			}
			throw new SerializationException($"Type {type} is not accepted");
		}
		return base.ResolveType(type);
	}
}
