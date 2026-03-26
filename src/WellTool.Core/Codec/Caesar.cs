using System;

namespace WellTool.Core.Codec
{
	/// <summary>
	/// 凯撒密码实现<br>
	/// 算法来自：https://github.com/zhaorenjie110/SymmetricEncryptionAndDecryption
	/// </summary>
	public static class Caesar
	{
		// 26个字母表
		public const string Table = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz";

		/// <summary>
		/// 传入明文，加密得到密文
		/// </summary>
		/// <param name="message">加密的消息</param>
		/// <param name="offset">偏移量</param>
		/// <returns>加密后的内容</returns>
		public static string Encode(string message, int offset)
		{
			if (message == null)
			{
				throw new ArgumentNullException(nameof(message), "message must be not null!");
			}
			var len = message.Length;
			var plain = message.ToCharArray();
			char c;
			for (int i = 0; i < len; i++)
			{
				c = message[i];
				if (!char.IsLetter(c))
				{
					continue;
				}
				plain[i] = EncodeChar(c, offset);
			}
			return new string(plain);
		}

		/// <summary>
		/// 传入明文解密到密文
		/// </summary>
		/// <param name="cipherText">密文</param>
		/// <param name="offset">偏移量</param>
		/// <returns>解密后的内容</returns>
		public static string Decode(string cipherText, int offset)
		{
			if (cipherText == null)
			{
				throw new ArgumentNullException(nameof(cipherText), "cipherText must be not null!");
			}
			var len = cipherText.Length;
			var plain = cipherText.ToCharArray();
			char c;
			for (int i = 0; i < len; i++)
			{
				c = cipherText[i];
				if (!char.IsLetter(c))
				{
					continue;
				}
				plain[i] = DecodeChar(c, offset);
			}
			return new string(plain);
		}

		// ----------------------------------------------------------------------------------------- Private method start

		/// <summary>
		/// 加密轮盘
		/// </summary>
		/// <param name="c">被加密字符</param>
		/// <param name="offset">偏移量</param>
		/// <returns>加密后的字符</returns>
		private static char EncodeChar(char c, int offset)
		{
			int position = (Table.IndexOf(c) + offset) % 52;
			return Table[position];
		}

		/// <summary>
		/// 解密轮盘
		/// </summary>
		/// <param name="c">字符</param>
		/// <param name="offset">偏移量</param>
		/// <returns>解密后的字符</returns>
		private static char DecodeChar(char c, int offset)
		{
			int position = (Table.IndexOf(c) - offset) % 52;
			if (position < 0)
			{
				position += 52;
			}
			return Table[position];
		}
		// ----------------------------------------------------------------------------------------- Private method end
	}
}