using System;
using System.IO;
using System.Text;

namespace WellTool.Core.Codec
{
	/// <summary>
	/// Base62工具类，提供Base62的编码和解码方案
	/// </summary>
	public static class Base62
	{
		private static readonly Encoding DefaultEncoding = Encoding.UTF8;

		// -------------------------------------------------------------------- encode
		/// <summary>
		/// Base62编码
		/// </summary>
		/// <param name="source">被编码的Base62字符串</param>
		/// <returns>被加密后的字符串</returns>
		public static string Encode(string source)
		{
			return Encode(source, DefaultEncoding);
		}

		/// <summary>
		/// Base62编码
		/// </summary>
		/// <param name="source">被编码的Base62字符串</param>
		/// <param name="encoding">字符集</param>
		/// <returns>被加密后的字符串</returns>
		public static string Encode(string source, Encoding encoding)
		{
			return Encode(encoding.GetBytes(source));
		}

		/// <summary>
		/// Base62编码
		/// </summary>
		/// <param name="source">被编码的Base62字符串</param>
		/// <returns>被加密后的字符串</returns>
		public static string Encode(byte[] source)
		{
			return Encoding.ASCII.GetString(Base62Codec.Instance.Encode(source));
		}

		/// <summary>
		/// Base62编码
		/// </summary>
		/// <param name="input">被编码Base62的流（一般为图片流或者文件流）</param>
		/// <returns>被加密后的字符串</returns>
		public static string Encode(Stream input)
		{
			using (var memoryStream = new MemoryStream())
			{
				input.CopyTo(memoryStream);
				return Encode(memoryStream.ToArray());
			}
		}

		/// <summary>
		/// Base62编码
		/// </summary>
		/// <param name="file">被编码Base62的文件</param>
		/// <returns>被加密后的字符串</returns>
		public static string Encode(FileInfo file)
		{
			return Encode(File.ReadAllBytes(file.FullName));
		}

		/// <summary>
		/// Base62编码（反转字母表模式）
		/// </summary>
		/// <param name="source">被编码的Base62字符串</param>
		/// <returns>被加密后的字符串</returns>
		public static string EncodeInverted(string source)
		{
			return EncodeInverted(source, DefaultEncoding);
		}

		/// <summary>
		/// Base62编码（反转字母表模式）
		/// </summary>
		/// <param name="source">被编码的Base62字符串</param>
		/// <param name="encoding">字符集</param>
		/// <returns>被加密后的字符串</returns>
		public static string EncodeInverted(string source, Encoding encoding)
		{
			return EncodeInverted(encoding.GetBytes(source));
		}

		/// <summary>
		/// Base62编码（反转字母表模式）
		/// </summary>
		/// <param name="source">被编码的Base62字符串</param>
		/// <returns>被加密后的字符串</returns>
		public static string EncodeInverted(byte[] source)
		{
			return Encoding.ASCII.GetString(Base62Codec.Instance.Encode(source, true));
		}

		/// <summary>
		/// Base62编码
		/// </summary>
		/// <param name="input">被编码Base62的流（一般为图片流或者文件流）</param>
		/// <returns>被加密后的字符串</returns>
		public static string EncodeInverted(Stream input)
		{
			using (var memoryStream = new MemoryStream())
			{
				input.CopyTo(memoryStream);
				return EncodeInverted(memoryStream.ToArray());
			}
		}

		/// <summary>
		/// Base62编码（反转字母表模式）
		/// </summary>
		/// <param name="file">被编码Base62的文件</param>
		/// <returns>被加密后的字符串</returns>
		public static string EncodeInverted(FileInfo file)
		{
			return EncodeInverted(File.ReadAllBytes(file.FullName));
		}

		// -------------------------------------------------------------------- decode
		/// <summary>
		/// Base62解码
		/// </summary>
		/// <param name="source">被解码的Base62字符串</param>
		/// <returns>被加密后的字符串</returns>
		public static string DecodeStrGbk(string source)
		{
			return DecodeStr(source, Encoding.GetEncoding("GBK"));
		}

		/// <summary>
		/// Base62解码
		/// </summary>
		/// <param name="source">被解码的Base62字符串</param>
		/// <returns>被加密后的字符串</returns>
		public static string DecodeStr(string source)
		{
			return DecodeStr(source, DefaultEncoding);
		}

		/// <summary>
		/// Base62解码
		/// </summary>
		/// <param name="source">被解码的Base62字符串</param>
		/// <param name="encoding">字符集</param>
		/// <returns>被加密后的字符串</returns>
		public static string DecodeStr(string source, Encoding encoding)
		{
			return encoding.GetString(Decode(source));
		}

		/// <summary>
		/// Base62解码
		/// </summary>
		/// <param name="base62">被解码的Base62字符串</param>
		/// <param name="destFile">目标文件</param>
		/// <returns>目标文件</returns>
		public static FileInfo DecodeToFile(string base62, FileInfo destFile)
		{
			File.WriteAllBytes(destFile.FullName, Decode(base62));
			return destFile;
		}

		/// <summary>
		/// Base62解码
		/// </summary>
		/// <param name="base62Str">被解码的Base62字符串</param>
		/// <param name="output">写出到的流</param>
		/// <param name="isCloseOutput">是否关闭输出流</param>
		public static void DecodeToStream(string base62Str, Stream output, bool isCloseOutput)
		{
			try
			{
				var bytes = Decode(base62Str);
				output.Write(bytes, 0, bytes.Length);
			}
			finally
			{
				if (isCloseOutput)
				{
					output.Dispose();
				}
			}
		}

		/// <summary>
		/// Base62解码
		/// </summary>
		/// <param name="base62Str">被解码的Base62字符串</param>
		/// <returns>被加密后的字符串</returns>
		public static byte[] Decode(string base62Str)
		{
			return Decode(Encoding.ASCII.GetBytes(base62Str));
		}

		/// <summary>
		/// 解码Base62
		/// </summary>
		/// <param name="base62Bytes">Base62输入</param>
		/// <returns>解码后的bytes</returns>
		public static byte[] Decode(byte[] base62Bytes)
		{
			return Base62Codec.Instance.Decode(base62Bytes);
		}

		/// <summary>
		/// Base62解码（反转字母表模式）
		/// </summary>
		/// <param name="source">被解码的Base62字符串</param>
		/// <returns>被加密后的字符串</returns>
		public static string DecodeStrInverted(string source)
		{
			return DecodeStrInverted(source, DefaultEncoding);
		}

		/// <summary>
		/// Base62解码（反转字母表模式）
		/// </summary>
		/// <param name="source">被解码的Base62字符串</param>
		/// <param name="encoding">字符集</param>
		/// <returns>被加密后的字符串</returns>
		public static string DecodeStrInverted(string source, Encoding encoding)
		{
			return encoding.GetString(DecodeInverted(source));
		}

		/// <summary>
		/// Base62解码（反转字母表模式）
		/// </summary>
		/// <param name="base62">被解码的Base62字符串</param>
		/// <param name="destFile">目标文件</param>
		/// <returns>目标文件</returns>
		public static FileInfo DecodeToFileInverted(string base62, FileInfo destFile)
		{
			File.WriteAllBytes(destFile.FullName, DecodeInverted(base62));
			return destFile;
		}

		/// <summary>
		/// Base62解码（反转字母表模式）
		/// </summary>
		/// <param name="base62Str">被解码的Base62字符串</param>
		/// <param name="output">写出到的流</param>
		/// <param name="isCloseOutput">是否关闭输出流</param>
		public static void DecodeToStreamInverted(string base62Str, Stream output, bool isCloseOutput)
		{
			try
			{
				var bytes = DecodeInverted(base62Str);
				output.Write(bytes, 0, bytes.Length);
			}
			finally
			{
				if (isCloseOutput)
				{
					output.Dispose();
				}
			}
		}

		/// <summary>
		/// Base62解码（反转字母表模式）
		/// </summary>
		/// <param name="base62Str">被解码的Base62字符串</param>
		/// <returns>被加密后的字符串</returns>
		public static byte[] DecodeInverted(string base62Str)
		{
			return DecodeInverted(Encoding.ASCII.GetBytes(base62Str));
		}

		/// <summary>
		/// 解码Base62（反转字母表模式）
		/// </summary>
		/// <param name="base62Bytes">Base62输入</param>
		/// <returns>解码后的bytes</returns>
		public static byte[] DecodeInverted(byte[] base62Bytes)
		{
			return Base62Codec.Instance.Decode(base62Bytes, true);
		}
	}
}