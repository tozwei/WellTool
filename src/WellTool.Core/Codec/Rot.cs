using System;

namespace WellTool.Core.Codec
{
	/// <summary>
	/// RotN（rotate by N places），回转N位密码，是一种简易的替换式密码，也是过去在古罗马开发的凯撒加密的一种变体。<br>
	/// 代码来自：https://github.com/orclight/jencrypt
	/// </summary>
	public static class Rot
	{
		private const char aCHAR = 'a';
		private const char zCHAR = 'z';
		private const char ACHAR = 'A';
		private const char ZCHAR = 'Z';
		private const char CHAR0 = '0';
		private const char CHAR9 = '9';

		/// <summary>
		/// Rot-13编码，同时编码数字
		/// </summary>
		/// <param name="message">被编码的消息</param>
		/// <returns>编码后的字符串</returns>
		public static string Encode13(string message)
		{
			return Encode13(message, true);
		}

		/// <summary>
		/// Rot-13编码
		/// </summary>
		/// <param name="message">被编码的消息</param>
		/// <param name="isEncodeNumber">是否编码数字</param>
		/// <returns>编码后的字符串</returns>
		public static string Encode13(string message, bool isEncodeNumber)
		{
			return Encode(message, 13, isEncodeNumber);
		}

		/// <summary>
		/// RotN编码
		/// </summary>
		/// <param name="message">被编码的消息</param>
		/// <param name="offset">位移，常用位移13</param>
		/// <param name="isEncodeNumber">是否编码数字</param>
		/// <returns>编码后的字符串</returns>
		public static string Encode(string message, int offset, bool isEncodeNumber)
		{
			if (message == null)
			{
				throw new ArgumentNullException(nameof(message), "message must not be null");
			}
			var len = message.Length;
			var chars = new char[len];

			for (int i = 0; i < len; i++)
			{
				chars[i] = EncodeChar(message[i], offset, isEncodeNumber);
			}
			return new string(chars);
		}

		/// <summary>
		/// Rot-13解码，同时解码数字
		/// </summary>
		/// <param name="rot">被解码的消息密文</param>
		/// <returns>解码后的字符串</returns>
		public static string Decode13(string rot)
		{
			return Decode13(rot, true);
		}

		/// <summary>
		/// Rot-13解码
		/// </summary>
		/// <param name="rot">被解码的消息密文</param>
		/// <param name="isDecodeNumber">是否解码数字</param>
		/// <returns>解码后的字符串</returns>
		public static string Decode13(string rot, bool isDecodeNumber)
		{
			return Decode(rot, 13, isDecodeNumber);
		}

		/// <summary>
		/// RotN解码
		/// </summary>
		/// <param name="rot">被解码的消息密文</param>
		/// <param name="offset">位移，常用位移13</param>
		/// <param name="isDecodeNumber">是否解码数字</param>
		/// <returns>解码后的字符串</returns>
		public static string Decode(string rot, int offset, bool isDecodeNumber)
		{
			if (rot == null)
			{
				throw new ArgumentNullException(nameof(rot), "rot must not be null");
			}
			var len = rot.Length;
			var chars = new char[len];

			for (int i = 0; i < len; i++)
			{
				chars[i] = DecodeChar(rot[i], offset, isDecodeNumber);
			}
			return new string(chars);
		}

		// ------------------------------------------------------------------------------------------ Private method start
		/// <summary>
		/// 解码字符
		/// </summary>
		/// <param name="c">字符</param>
		/// <param name="offset">位移</param>
		/// <param name="isEncodeNumber">是否解码数字</param>
		/// <returns>解码后的字符串</returns>
		private static char EncodeChar(char c, int offset, bool isEncodeNumber)
		{
			if (isEncodeNumber)
			{
				if (c >= CHAR0 && c <= CHAR9)
				{
					c -= CHAR0;
					c = (char)((c + offset) % 10);
					c += CHAR0;
				}
			}

			// A == 65, Z == 90
			if (c >= ACHAR && c <= ZCHAR)
			{
				c -= ACHAR;
				c = (char)((c + offset) % 26);
				c += ACHAR;
			}
			// a == 97, z == 122.
			else if (c >= aCHAR && c <= zCHAR)
			{
				c -= aCHAR;
				c = (char)((c + offset) % 26);
				c += aCHAR;
			}
			return c;
		}

		/// <summary>
		/// 编码字符
		/// </summary>
		/// <param name="c">字符</param>
		/// <param name="offset">位移</param>
		/// <param name="isDecodeNumber">是否编码数字</param>
		/// <returns>编码后的字符串</returns>
		private static char DecodeChar(char c, int offset, bool isDecodeNumber)
		{
			int temp = c;
			// if converting numbers is enabled
			if (isDecodeNumber)
			{
				if (temp >= CHAR0 && temp <= CHAR9)
				{
					temp -= CHAR0;
					temp = temp - offset;
					while (temp < 0)
					{
						temp += 10;
					}
					temp += CHAR0;
				}
			}

			// A == 65, Z == 90
			if (temp >= ACHAR && temp <= ZCHAR)
			{
				temp -= ACHAR;

				temp = temp - offset;
				while (temp < 0)
				{
					temp = 26 + temp;
				}
				temp += ACHAR;
			}
			else if (temp >= aCHAR && temp <= zCHAR)
			{
				temp -= aCHAR;

				temp = temp - offset;
				if (temp < 0)
					temp = 26 + temp;

				temp += aCHAR;
			}
			return (char)temp;
		}
		// ------------------------------------------------------------------------------------------ Private method end
	}
}