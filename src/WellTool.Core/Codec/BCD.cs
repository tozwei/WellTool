using System;

namespace WellTool.Core.Codec
{
	/// <summary>
	/// BCD码（Binary-Coded Decimal）亦称二进码十进数或二-十进制代码<br>
	/// BCD码这种编码形式利用了四个位元来储存一个十进制的数码，使二进制和十进制之间的转换得以快捷的进行<br>
	/// see http://cuisuqiang.iteye.com/blog/1429956
	/// </summary>
	[Obsolete("由于对于ASCII的编码解码有缺陷，且这种BCD实现并不规范，因此会在后续版本中移除")]
	public static class BCD
	{
		/// <summary>
		/// 字符串转BCD码
		/// </summary>
		/// <param name="asc">ASCII字符串</param>
		/// <returns>BCD</returns>
		public static byte[] StrToBcd(string asc)
		{
			if (asc == null)
			{
				throw new ArgumentNullException(nameof(asc), "ASCII must not be null!");
			}
			int len = asc.Length;
			int mod = len % 2;
			if (mod != 0)
			{
				asc = "0" + asc;
				len = asc.Length;
			}
			byte[] abt;
			if (len >= 2)
			{
				len >>= 1;
			}
			byte[] bbt = new byte[len];
			abt = System.Text.Encoding.ASCII.GetBytes(asc);
			int j;
			int k;
			for (int p = 0; p < asc.Length / 2; p++)
			{
				if ((abt[2 * p] >= '0') && (abt[2 * p] <= '9'))
				{
					j = abt[2 * p] - '0';
				}
				else if ((abt[2 * p] >= 'a') && (abt[2 * p] <= 'z'))
				{
					j = abt[2 * p] - 'a' + 0x0a;
				}
				else
				{
					j = abt[2 * p] - 'A' + 0x0a;
				}
				if ((abt[2 * p + 1] >= '0') && (abt[2 * p + 1] <= '9'))
				{
					k = abt[2 * p + 1] - '0';
				}
				else if ((abt[2 * p + 1] >= 'a') && (abt[2 * p + 1] <= 'z'))
				{
					k = abt[2 * p + 1] - 'a' + 0x0a;
				}
				else
				{
					k = abt[2 * p + 1] - 'A' + 0x0a;
				}
				int a = (j << 4) + k;
				byte b = (byte)a;
				bbt[p] = b;
			}
			return bbt;
		}

		/// <summary>
		/// ASCII转BCD
		/// </summary>
		/// <param name="ascii">ASCII byte数组</param>
		/// <returns>BCD</returns>
		public static byte[] AscToBcd(byte[] ascii)
		{
			if (ascii == null)
			{
				throw new ArgumentNullException(nameof(ascii), "Ascii must be not null!");
			}
			return AscToBcd(ascii, ascii.Length);
		}

		/// <summary>
		/// ASCII转BCD
		/// </summary>
		/// <param name="ascii">ASCII byte数组</param>
		/// <param name="ascLength">长度</param>
		/// <returns>BCD</returns>
		public static byte[] AscToBcd(byte[] ascii, int ascLength)
		{
			if (ascii == null)
			{
				throw new ArgumentNullException(nameof(ascii), "Ascii must be not null!");
			}
			byte[] bcd = new byte[ascLength / 2];
			int j = 0;
			for (int i = 0; i < (ascLength + 1) / 2; i++)
			{
				bcd[i] = AscToBcd(ascii[j++]);
				bcd[i] = (byte)(((j >= ascLength) ? 0x00 : AscToBcd(ascii[j++])) + (bcd[i] << 4));
			}
			return bcd;
		}

		/// <summary>
		/// BCD转ASCII字符串
		/// </summary>
		/// <param name="bytes">BCD byte数组</param>
		/// <returns>ASCII字符串</returns>
		public static string BcdToStr(byte[] bytes)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException(nameof(bytes), "Bcd bytes must be not null!");
			}
			char[] temp = new char[bytes.Length * 2];
			char val;

			for (int i = 0; i < bytes.Length; i++)
			{
				val = (char)(((bytes[i] & 0xf0) >> 4) & 0x0f);
				temp[i * 2] = (char)(val > 9 ? val + 'A' - 10 : val + '0');

				val = (char)(bytes[i] & 0x0f);
				temp[i * 2 + 1] = (char)(val > 9 ? val + 'A' - 10 : val + '0');
			}
			return new string(temp);
		}

		//----------------------------------------------------------------- Private method start
		/// <summary>
		/// 转换单个byte为BCD
		/// </summary>
		/// <param name="asc">ACSII</param>
		/// <returns>BCD</returns>
		private static byte AscToBcd(byte asc)
		{
			byte bcd;

			if ((asc >= '0') && (asc <= '9'))
			{
				bcd = (byte)(asc - '0');
			}
			else if ((asc >= 'A') && (asc <= 'F'))
			{
				bcd = (byte)(asc - 'A' + 10);
			}
			else if ((asc >= 'a') && (asc <= 'f'))
			{
				bcd = (byte)(asc - 'a' + 10);
			}
			else
			{
				bcd = (byte)(asc - 48);
			}
			return bcd;
		}
		//----------------------------------------------------------------- Private method end
	}
}