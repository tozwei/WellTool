using System;
using System.Collections.Generic;
using System.Reflection;

namespace WellTool.Core.Bean.Copier
{
	/// <summary>
	/// 属性拷贝选项<br>
	/// 包括：<br>
	/// 1、限制的类或接口，必须为目标对象的实现接口或父类，用于限制拷贝的属性，例如一个类我只想复制其父类的一些属性，就可以将editable设置为父类<br>
	/// 2、是否忽略空值，当源对象的值为null时，true: 忽略而不注入此值，false: 注入null<br>
	/// 3、忽略的属性列表，设置一个属性列表，不拷贝这些属性值<br>
	/// </summary>
	/// <author>Looly</author>
	public class CopyOptions
	{
		/// <summary>
		/// 限制的类或接口，必须为目标对象的实现接口或父类，用于限制拷贝的属性，例如一个类我只想复制其父类的一些属性，就可以将editable设置为父类<br>
		/// 如果目标对象是Map，源对象是Bean，则作用于源对象上
		/// </summary>
		public Type Editable { get; set; }

		/// <summary>
		/// 是否忽略空值，当源对象的值为null时，true: 忽略而不注入此值，false: 注入null
		/// </summary>
		public bool IgnoreNullValue { get; set; }

		/// <summary>
		/// 属性过滤器，断言通过的属性才会被复制<br>
		/// 断言参数中Field为源对象的字段对象,如果源对象为Map，使用目标对象，Object为源对象的对应值
		/// </summary>
		public Func<FieldInfo, object, bool> PropertiesFilter { get; set; }

		/// <summary>
		/// 是否忽略字段注入错误
		/// </summary>
		public bool IgnoreError { get; set; }

		/// <summary>
		/// 是否忽略字段大小写
		/// </summary>
		public bool IgnoreCase { get; set; }

		/// <summary>
		/// 字段属性编辑器，用于自定义属性转换规则，例如驼峰转下划线等<br>
		/// 规则为，{@link Editor#edit(Object)}属性为源对象的字段名称或key，返回值为目标对象的字段名称或key
		/// </summary>
		public Func<string, string> FieldNameEditor { get; set; }

		/// <summary>
		/// 字段属性值编辑器，用于自定义属性值转换规则，例如null转""等
		/// </summary>
		public Func<string, object, object> FieldValueEditor { get; set; }

		/// <summary>
		/// 是否支持transient关键字修饰和@Transient注解，如果支持，被修饰的字段或方法对应的字段将被忽略。
		/// </summary>
		public bool TransientSupport { get; set; } = true;

		/// <summary>
		/// 是否覆盖目标值，如果不覆盖，会先读取目标对象的值，非{@code null}则写，否则忽略。如果覆盖，则不判断直接写
		/// </summary>
		public bool Override { get; set; } = true;

		/// <summary>
		/// 是否自动转换为驼峰方式
		/// </summary>
		public bool AutoTransCamelCase { get; set; } = true;

		/// <summary>
		/// 源对象和目标对象都是 {@code Map} 时, 需要忽略的源对象 {@code Map} key
		/// </summary>
		public HashSet<string> IgnoreKeySet { get; set; }

		/// <summary>
		/// 自定义类型转换器，默认使用全局万能转换器转换
		/// </summary>
		public Func<Type, object, object> Converter { get; set; }

		/// <summary>
		/// 在Bean转换时，如果源是String，目标对象是Date或LocalDateTime，则可自定义转换格式
		/// </summary>
		public string FormatIfDate { get; set; }

		//region create

		/// <summary>
		/// 创建拷贝选项
		/// </summary>
		/// <returns>拷贝选项</returns>
		public static CopyOptions Create()
		{
			return new CopyOptions();
		}

		/// <summary>
		/// 创建拷贝选项
		/// </summary>
		/// <param name="editable">限制的类或接口，必须为目标对象的实现接口或父类，用于限制拷贝的属性</param>
		/// <param name="ignoreNullValue">是否忽略空值，当源对象的值为null时，true: 忽略而不注入此值，false: 注入null</param>
		/// <param name="ignoreProperties">忽略的属性列表，设置一个属性列表，不拷贝这些属性值</param>
		/// <returns>拷贝选项</returns>
		public static CopyOptions Create(Type editable, bool ignoreNullValue, params string[] ignoreProperties)
		{
			return new CopyOptions(editable, ignoreNullValue, ignoreProperties);
		}
		//endregion

		/// <summary>
		/// 构造拷贝选项
		/// </summary>
		public CopyOptions()
		{
			// 默认转换器
			Converter = (type, value) => {
				if (value == null)
				{
					return null;
				}

				// 处理空字符串到可空类型的转换
				if (value is string strValue && string.IsNullOrEmpty(strValue))
				{
					if (type is Type targetType11)
					{
						// 如果目标类型是可空类型，返回null
						if (targetType1.IsGenericType && targetType1.GetGenericTypeDefinition() == typeof(Nullable<>))
						{
							return null;
						}
					}
				}

				// 快速处理简单值类型的转换
				if (type is Type targetType11)
				{
					if (IsSimpleValueType(targetType1) && targetType1.IsAssignableFrom(value.GetType()))
					{
						return value;
					}
				}

				// 这里可以添加更多的转换逻辑
				return value;
			};
		}

		/// <summary>
		/// 构造拷贝选项
		/// </summary>
		/// <param name="editable">限制的类或接口，必须为目标对象的实现接口或父类，用于限制拷贝的属性</param>
		/// <param name="ignoreNullValue">是否忽略空值，当源对象的值为null时，true: 忽略而不注入此值，false: 注入null</param>
		/// <param name="ignoreProperties">忽略的目标对象中属性列表，设置一个属性列表，不拷贝这些属性值</param>
		public CopyOptions(Type editable, bool ignoreNullValue, params string[] ignoreProperties)
		{
			PropertiesFilter = (f, v) => true;
			Editable = editable;
			IgnoreNullValue = ignoreNullValue;
			SetIgnoreProperties(ignoreProperties);

			// 默认转换器
			Converter = (type, value) => {
				if (value == null)
				{
					return null;
				}

				// 处理空字符串到可空类型的转换
				if (value is string strValue && string.IsNullOrEmpty(strValue))
				{
					if (type is Type targetType11)
					{
						// 如果目标类型是可空类型，返回null
						if (targetType1.IsGenericType && targetType1.GetGenericTypeDefinition() == typeof(Nullable<>))
						{
							return null;
						}
					}
				}

				// 快速处理简单值类型的转换
				if (type is Type targetType11)
				{
					if (IsSimpleValueType(targetType1) && targetType1.IsAssignableFrom(value.GetType()))
					{
						return value;
					}
				}

				// 这里可以添加更多的转换逻辑
				return value;
			};
		}

		/// <summary>
		/// 设置限制的类或接口，必须为目标对象的实现接口或父类，用于限制拷贝的属性
		/// </summary>
		/// <param name="editable">限制的类或接口</param>
		/// <returns>CopyOptions</returns>
		public CopyOptions SetEditable(Type editable)
		{
			Editable = editable;
			return this;
		}

		/// <summary>
		/// 设置是否忽略空值，当源对象的值为null时，true: 忽略而不注入此值，false: 注入null
		/// </summary>
		/// <param name="ignoreNullValue">是否忽略空值，当源对象的值为null时，true: 忽略而不注入此值，false: 注入null</param>
		/// <returns>CopyOptions</returns>
		public CopyOptions SetIgnoreNullValue(bool ignoreNullValue)
		{
			IgnoreNullValue = ignoreNullValue;
			return this;
		}

		/// <summary>
		/// 设置忽略空值，当源对象的值为null时，忽略而不注入此值
		/// </summary>
		/// <returns>CopyOptions</returns>
		public CopyOptions IgnoreNullValueOption()
		{
			return SetIgnoreNullValue(true);
		}

		/// <summary>
		/// 属性过滤器，断言通过的属性才会被复制<br>
		/// 返回{@code true}则属性通过，{@code false}不通过，抛弃之
		/// </summary>
		/// <param name="propertiesFilter">属性过滤器</param>
		/// <returns>CopyOptions</returns>
		public CopyOptions SetPropertiesFilter(Func<FieldInfo, object, bool> propertiesFilter)
		{
			PropertiesFilter = propertiesFilter;
			return this;
		}

		/// <summary>
		/// 设置忽略的目标对象中属性列表，设置一个属性列表，不拷贝这些属性值
		/// </summary>
		/// <param name="ignoreProperties">忽略的目标对象中属性列表，设置一个属性列表，不拷贝这些属性值</param>
		/// <returns>CopyOptions</returns>
		public CopyOptions SetIgnoreProperties(params string[] ignoreProperties)
		{
			IgnoreKeySet = new HashSet<string>(ignoreProperties);
			return this;
		}

		/// <summary>
		/// 设置是否忽略字段的注入错误
		/// </summary>
		/// <param name="ignoreError">是否忽略注入错误</param>
		/// <returns>CopyOptions</returns>
		public CopyOptions SetIgnoreError(bool ignoreError)
		{
			IgnoreError = ignoreError;
			return this;
		}

		/// <summary>
		/// 设置忽略字段的注入错误
		/// </summary>
		/// <returns>CopyOptions</returns>
		public CopyOptions IgnoreErrorOption()
		{
			return SetIgnoreError(true);
		}

		/// <summary>
		/// 设置是否忽略字段的大小写
		/// </summary>
		/// <param name="ignoreCase">是否忽略大小写</param>
		/// <returns>CopyOptions</returns>
		public CopyOptions SetIgnoreCase(bool ignoreCase)
		{
			IgnoreCase = ignoreCase;
			return this;
		}

		/// <summary>
		/// 设置忽略字段的大小写
		/// </summary>
		/// <returns>CopyOptions</returns>
		public CopyOptions IgnoreCaseOption()
		{
			return SetIgnoreCase(true);
		}

		/// <summary>
		/// 设置拷贝属性的字段映射，用于不同的属性之前拷贝做对应表用<br>
		/// 需要注意的是，当使用ValueProvider作为数据提供者时，这个映射是相反的，即fieldMapping中key为目标Bean的名称，而value是提供者中的key
		/// </summary>
		/// <param name="fieldMapping">拷贝属性的字段映射，用于不同的属性之前拷贝做对应表用</param>
		/// <returns>CopyOptions</returns>
		public CopyOptions SetFieldMapping(Dictionary<string, string> fieldMapping)
		{
			return SetFieldNameEditor(key => fieldMapping.ContainsKey(key) ? fieldMapping[key] : key);
		}

		/// <summary>
		/// 设置字段属性编辑器，用于自定义属性转换规则，例如驼峰转下划线等<br>
		/// 此转换器只针对源端的字段做转换，请确认转换后与目标端字段一致<br>
		/// 当转换后的字段名为null时忽略这个字段<br>
		/// 需要注意的是，当使用ValueProvider作为数据提供者时，这个映射是相反的，即fieldMapping中key为目标Bean的名称，而value是提供者中的key
		/// </summary>
		/// <param name="fieldNameEditor">字段属性编辑器，用于自定义属性转换规则，例如驼峰转下划线等</param>
		/// <returns>CopyOptions</returns>
		public CopyOptions SetFieldNameEditor(Func<string, string> fieldNameEditor)
		{
			FieldNameEditor = fieldNameEditor;
			return this;
		}

		/// <summary>
		/// 设置字段属性值编辑器，用于自定义属性值转换规则，例如null转""等<br>
		/// </summary>
		/// <param name="fieldValueEditor">字段属性值编辑器，用于自定义属性值转换规则，例如null转""等</param>
		/// <returns>CopyOptions</returns>
		public CopyOptions SetFieldValueEditor(Func<string, object, object> fieldValueEditor)
		{
			FieldValueEditor = fieldValueEditor;
			return this;
		}

		/// <summary>
		/// 编辑字段值
		/// </summary>
		/// <param name="fieldName">字段名</param>
		/// <param name="fieldValue">字段值</param>
		/// <returns>编辑后的字段值</returns>
		public object EditFieldValue(string fieldName, object fieldValue)
		{
			return FieldValueEditor != null ? FieldValueEditor(fieldName, fieldValue) : fieldValue;
		}

		/// <summary>
		/// 设置是否支持transient关键字修饰和@Transient注解，如果支持，被修饰的字段或方法对应的字段将被忽略。
		/// </summary>
		/// <param name="transientSupport">是否支持</param>
		/// <returns>this</returns>
		public CopyOptions SetTransientSupport(bool transientSupport)
		{
			TransientSupport = transientSupport;
			return this;
		}

		/// <summary>
		/// 设置是否覆盖目标值，如果不覆盖，会先读取目标对象的值，为{@code null}则写，否则忽略。如果覆盖，则不判断直接写
		/// </summary>
		/// <param name="override">是否覆盖目标值</param>
		/// <returns>this</returns>
		public CopyOptions SetOverride(bool @override)
		{
			Override = @override;
			return this;
		}

		/// <summary>
		/// 设置是否自动转换为驼峰方式<br>
		/// 一般用于map转bean和bean转bean出现非驼峰格式时，在尝试转换失败的情况下，是否二次检查转为驼峰匹配<br>
		/// 此设置用于解决Bean和Map转换中的匹配问题而设置，并不是一个强制参数。
		/// <ol>
		///     <li>当map转bean时，如果map中是下划线等非驼峰模式，自动匹配对应的驼峰字段，避免出现字段不拷贝问题。</li>
		///     <li>当bean转bean时，由于字段命名不规范，使用了非驼峰方式，增加兼容性。</li>
		/// </ol>
		/// <p>
		/// 但是bean转Map和map转map时，没有使用这个参数，是因为没有匹配的必要，转map不存在无法匹配到的问题，因此此参数无效。
		/// </summary>
		/// <param name="autoTransCamelCase">是否自动转换为驼峰方式</param>
		/// <returns>this</returns>
		public CopyOptions SetAutoTransCamelCase(bool autoTransCamelCase)
		{
			AutoTransCamelCase = autoTransCamelCase;
			return this;
		}

		/// <summary>
		/// 设置自定义类型转换器，默认使用全局万能转换器转换。
		/// </summary>
		/// <param name="converter">转换器</param>
		/// <returns>this</returns>
		public CopyOptions SetConverter(Func<Type, object, object> converter)
		{
			Converter = converter;
			return this;
		}

		/// <summary>
		/// 设置日期格式，用于日期转字符串，默认为{@code null}
		/// </summary>
		/// <param name="formatIfDate">日期格式</param>
		/// <returns>this</returns>
		public CopyOptions SetFormatIfDate(string formatIfDate)
		{
			FormatIfDate = formatIfDate;
			return this;
		}

		/// <summary>
		/// 使用自定义转换器转换字段值<br>
		/// 如果自定义转换器为{@code null}，则返回原值。
		/// </summary>
		/// <param name="targetType1">目标类型</param>
		/// <param name="fieldValue">字段值</param>
		/// <returns>编辑后的字段值</returns>
		public object ConvertField(Type targetType1, object fieldValue)
		{
			// 这里可以添加日期转换逻辑

			return Converter != null ? Converter(targetType1, fieldValue) : fieldValue;
		}

		/// <summary>
		/// 转换字段名为编辑后的字段名
		/// </summary>
		/// <param name="fieldName">字段名</param>
		/// <returns>编辑后的字段名</returns>
		public string EditFieldName(string fieldName)
		{
			return FieldNameEditor != null ? FieldNameEditor(fieldName) : fieldName;
		}

		/// <summary>
		/// 测试是否保留字段，{@code true}保留，{@code false}不保留
		/// </summary>
		/// <param name="field">字段</param>
		/// <param name="value">值</param>
		/// <returns>是否保留</returns>
		public bool TestPropertyFilter(FieldInfo field, object value)
		{
			return PropertiesFilter == null || PropertiesFilter(field, value);
		}

		/// <summary>
		/// 测试是否保留key, {@code true} 保留， {@code false} 不保留
		/// </summary>
		/// <param name="key">{@link Map} key</param>
		/// <returns>是否保留</returns>
		public bool TestKeyFilter(object key)
		{
			if (IgnoreKeySet == null || IgnoreKeySet.Count == 0)
			{
				return true;
			}

			string keyStr = key.ToString();
			if (IgnoreCase)
			{
				// 忽略大小写时要遍历检查
				foreach (string ignoreKey in IgnoreKeySet)
				{
					if (string.Equals(keyStr, ignoreKey, StringComparison.OrdinalIgnoreCase))
					{
						return false;
					}
				}
			}

			return !IgnoreKeySet.Contains(keyStr);
		}

		/// <summary>
		/// 查找Map对应Bean的名称<br>
		/// 尝试原名称、转驼峰名称、首字母大写名称
		/// </summary>
		/// <param name="targetPropDescMap">目标bean的属性描述Map</param>
		/// <param name="sKeyStr">键或字段名</param>
		/// <returns>{@link PropDesc}</returns>
		public PropDesc FindPropDesc(Dictionary<string, PropDesc> targetPropDescMap, string sKeyStr)
		{
			if (targetPropDescMap.TryGetValue(sKeyStr, out PropDesc propDesc))
			{
				return propDesc;
			}

			// 尝试首字母大写
			if (!string.IsNullOrEmpty(sKeyStr))
			{
				string upperFirstKey = char.ToUpper(sKeyStr[0]) + sKeyStr.Substring(1);
				if (upperFirstKey != sKeyStr && targetPropDescMap.TryGetValue(upperFirstKey, out propDesc))
				{
					return propDesc;
				}
			}

			// 转驼峰尝试查找
			if (AutoTransCamelCase)
			{
				string camelCaseKey = ToCamelCase(sKeyStr);
				if (camelCaseKey != sKeyStr && targetPropDescMap.TryGetValue(camelCaseKey, out propDesc))
				{
					// 只有转换为驼峰后与原key不同才重复查询，相同说明本身就是驼峰，不需要二次查询
					return propDesc;
				}
			}

			return null;
		}

		/// <summary>
		/// 判断是否为简单值类型
		/// </summary>
		/// <param name="type">类型</param>
		/// <returns>是否为简单值类型</returns>
		private bool IsSimpleValueType(Type type)
		{
			return type.IsPrimitive || 
				type == typeof(string) || 
				type == typeof(decimal) || 
				type == typeof(DateTime) || 
				type == typeof(Guid) || 
				type.IsEnum;
		}

		/// <summary>
		/// 转换为驼峰命名
		/// </summary>
		/// <param name="str">字符串</param>
		/// <returns>驼峰命名的字符串</returns>
		private string ToCamelCase(string str)
		{
			if (string.IsNullOrEmpty(str))
			{
				return str;
			}

			// 处理下划线分隔的情况
			if (str.Contains("_"))
			{
				string[] parts = str.Split('_');
				string result = parts[0].ToLower();
				for (int i = 1; i < parts.Length; i++)
				{
					if (!string.IsNullOrEmpty(parts[i]))
					{
						result += char.ToUpper(parts[i][0]) + parts[i].Substring(1).ToLower();
					}
				}
				return result;
			}

			// 处理其他情况
			return char.ToLower(str[0]) + str.Substring(1);
		}
	}
}