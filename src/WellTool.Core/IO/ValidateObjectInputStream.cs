using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

#pragma warning disable SYSLIB0011 // BinaryFormatter is obsolete

namespace WellTool.Core.IO;

/// <summary>
/// 验证对象输入流
/// </summary>
public class ValidateObjectInputStream
{
	private readonly BinaryFormatter _formatter;
	private readonly Stream _inputStream;
	private readonly Type[] _acceptedTypes;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="inputStream">输入流</param>
	/// <param name="acceptedTypes">接受的类型</param>
	public ValidateObjectInputStream(Stream inputStream, params Type[] acceptedTypes)
	{
		_formatter = new BinaryFormatter();
		_inputStream = inputStream;
		_acceptedTypes = acceptedTypes;
		
		// 设置类型解析器
		_formatter.AssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple;
		_formatter.Binder = new TypeValidationBinder(_acceptedTypes);
	}

	/// <summary>
	/// 读取对象
	/// </summary>
	/// <returns>读取的对象</returns>
	public object ReadObject()
	{
		return _formatter.Deserialize(_inputStream);
	}

	/// <summary>
	/// 类型验证绑定器
	/// </summary>
	private class TypeValidationBinder : SerializationBinder
	{
		private readonly Type[] _acceptedTypes;

		/// <summary>
		/// 构造
		/// </summary>
		/// <param name="acceptedTypes">接受的类型</param>
		public TypeValidationBinder(Type[] acceptedTypes)
		{
			_acceptedTypes = acceptedTypes;
		}

		/// <summary>
		/// 绑定类型
		/// </summary>
		/// <param name="assemblyName">程序集名称</param>
		/// <param name="typeName">类型名称</param>
		/// <returns>解析后的类型</returns>
		public override Type BindToType(string assemblyName, string typeName)
		{
			var type = Type.GetType($"{typeName}, {assemblyName}");
			
			if (_acceptedTypes != null && _acceptedTypes.Length > 0)
			{
				var isValid = false;
				foreach (var acceptedType in _acceptedTypes)
				{
					if (type == acceptedType || type.IsSubclassOf(acceptedType))
					{
						isValid = true;
						break;
					}
				}
				
				if (!isValid)
				{
					throw new SerializationException($"Type {type} is not accepted");
				}
			}
			
			return type;
		}
	}
}
