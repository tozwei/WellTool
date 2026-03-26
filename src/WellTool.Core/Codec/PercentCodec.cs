using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WellTool.Core.Codec
{
	/// <summary>
	/// 百分号编码(Percent-encoding), 也称作URL编码(URL encoding)。<br>
	/// 百分号编码可用于URI的编码，也可以用于"application/x-www-form-urlencoded"的MIME准备数据。
	/// 
	/// <p>
	/// 百分号编码会对 URI 中不允许出现的字符或者其他特殊情况的允许的字符进行编码，对于被编码的字符，最终会转为以百分号"%"开头，后面跟着两位16进制数值的形式。
	/// 举个例子，空格符（SP）是不允许的字符，在 ASCII 码对应的二进制值是"00100000"，最终转为"%20"。
	/// </p>
	/// <p>
	/// 对于不同场景应遵循不同规范：
	/// 
	/// <ul>
	///     <li>URI：遵循RFC 3986保留字规范</li>
	///     <li>application/x-www-form-urlencoded，遵循W3C HTML Form content types规范，如空格须转+</li>
	/// </ul>
	/// </p>
	/// </summary>
	public class PercentCodec
	{
		/// <summary>
		/// 从已知PercentCodec创建PercentCodec，会复制给定PercentCodec的安全字符
		/// </summary>
		/// <param name="codec">PercentCodec</param>
		/// <returns>PercentCodec</returns>
		public static PercentCodec Of(PercentCodec codec)
		{
			return new PercentCodec(new HashSet<char>(codec.safeCharacters));
		}

		/// <summary>
		/// 创建PercentCodec，使用指定字符串中的字符作为安全字符
		/// </summary>
		/// <param name="chars">安全字符合集</param>
		/// <returns>PercentCodec</returns>
		public static PercentCodec Of(string chars)
		{
			if (chars == null)
			{
				throw new ArgumentNullException(nameof(chars), "chars must not be null");
			}
			var codec = new PercentCodec();
			foreach (var c in chars)
			{
				codec.AddSafe(c);
			}
			return codec;
		}

		/// <summary>
		/// 存放安全编码
		/// </summary>
		private readonly HashSet<char> safeCharacters;

		/// <summary>
		/// 是否编码空格为+<br>
		/// 如果为{@code true}，则将空格编码为"+"，此项只在"application/x-www-form-urlencoded"中使用<br>
		/// 如果为{@code false}，则空格编码为"%20",此项一般用于URL的Query部分（RFC3986规范）
		/// </summary>
		private bool encodeSpaceAsPlus = false;

		/// <summary>
		/// 构造<br>
		/// [a-zA-Z0-9]默认不被编码
		/// </summary>
		public PercentCodec() : this(new HashSet<char>())
		{
			// 添加默认安全字符 [a-zA-Z0-9]
			for (char c = 'a'; c <= 'z'; c++)
			{
				AddSafe(c);
			}
			for (char c = 'A'; c <= 'Z'; c++)
			{
				AddSafe(c);
			}
			for (char c = '0'; c <= '9'; c++)
			{
				AddSafe(c);
			}
		}

		/// <summary>
		/// 构造
		/// </summary>
		/// <param name="safeCharacters">安全字符，安全字符不被编码</param>
		public PercentCodec(HashSet<char> safeCharacters)
		{
			this.safeCharacters = safeCharacters;
		}

		/// <summary>
		/// 增加安全字符<br>
		/// 安全字符不被编码
		/// </summary>
		/// <param name="c">字符</param>
		/// <returns>this</returns>
		public PercentCodec AddSafe(char c)
		{
			safeCharacters.Add(c);
			return this;
		}

		/// <summary>
		/// 移除安全字符<br>
		/// 安全字符不被编码
		/// </summary>
		/// <param name="c">字符</param>
		/// <returns>this</returns>
		public PercentCodec RemoveSafe(char c)
		{
			safeCharacters.Remove(c);
			return this;
		}

		/// <summary>
		/// 增加安全字符到当前的PercentCodec
		/// </summary>
		/// <param name="codec">PercentCodec</param>
		/// <returns>this</returns>
		public PercentCodec Or(PercentCodec codec)
		{
			foreach (var c in codec.safeCharacters)
			{
				safeCharacters.Add(c);
			}
			return this;
		}

		/// <summary>
		/// 组合当前PercentCodec和指定PercentCodec为一个新的PercentCodec，安全字符为并集
		/// </summary>
		/// <param name="codec">PercentCodec</param>
		/// <returns>新的PercentCodec</returns>
		public PercentCodec OrNew(PercentCodec codec)
		{
			return Of(this).Or(codec);
		}

		/// <summary>
		/// 是否将空格编码为+<br>
		/// 如果为{@code true}，则将空格编码为"+"，此项只在"application/x-www-form-urlencoded"中使用<br>
		/// 如果为{@code false}，则空格编码为"%20",此项一般用于URL的Query部分（RFC3986规范）
		/// </summary>
		/// <param name="encodeSpaceAsPlus">是否将空格编码为+</param>
		/// <returns>this</returns>
		public PercentCodec SetEncodeSpaceAsPlus(bool encodeSpaceAsPlus)
		{
			this.encodeSpaceAsPlus = encodeSpaceAsPlus;
			return this;
		}

		/// <summary>
		/// 将URL中的字符串编码为%形式
		/// </summary>
		/// <param name="path">需要编码的字符串</param>
		/// <param name="encoding">编码, {@code null}返回原字符串，表示不编码</param>
		/// <param name="customSafeChar">自定义安全字符</param>
		/// <returns>编码后的字符串</returns>
		public string Encode(string path, Encoding encoding, params char[] customSafeChar)
		{
			if (encoding == null || string.IsNullOrEmpty(path))
			{
				return path;
			}

			var rewrittenPath = new StringBuilder(path.Length);

			foreach (var c in path)
			{
				if (safeCharacters.Contains(c) || (customSafeChar != null && Array.IndexOf(customSafeChar, c) >= 0))
				{
					rewrittenPath.Append(c);
				}
				else if (encodeSpaceAsPlus && c == ' ')
				{
					// 对于空格单独处理
					rewrittenPath.Append('+');
				}
				else
				{
					// convert to external encoding before hex conversion
					byte[] ba = encoding.GetBytes(new[] { c });
					foreach (byte toEncode in ba)
					{
						// Converting each byte in the buffer
						rewrittenPath.Append('%');
						rewrittenPath.Append(toEncode.ToString("X2"));
					}
				}
			}
			return rewrittenPath.ToString();
		}
	}
}