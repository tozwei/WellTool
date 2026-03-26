using System;
using System.Collections.Generic;
using System.Text;

namespace WellTool.Core.Codec
{
	/// <summary>
	/// Punycode是一个根据RFC 3492标准而制定的编码系统，主要用于把域名从地方语言所采用的Unicode编码转换成为可用于DNS系统的编码
	/// <p>
	/// 参考：https://blog.csdn.net/a19881029/article/details/18262671
	/// </p>
	/// </summary>
	public static class PunyCode
	{
		private const int TMIN = 1;
		private const int TMAX = 26;
		private const int BASE = 36;
		private const int INITIAL_N = 128;
		private const int INITIAL_BIAS = 72;
		private const int DAMP = 700;
		private const int SKEW = 38;
		private const char DELIMITER = '-';

		public const string PunyCodePrefix = "xn--";

		/// <summary>
		/// punycode转码域名
		/// </summary>
		/// <param name="domain">域名</param>
		/// <returns>编码后的域名</returns>
		/// <exception cref="Exception">计算异常</exception>
		public static string EncodeDomain(string domain)
		{
			if (domain == null)
			{
				throw new ArgumentNullException(nameof(domain), "domain must not be null!");
			}
			var parts = domain.Split('.');
			var result = new StringBuilder(domain.Length * 4);
			foreach (var part in parts)
			{
				if (result.Length != 0)
				{
					result.Append('.');
				}
				result.Append(Encode(part, true));
			}

			return result.ToString();
		}

		/// <summary>
		/// 将内容编码为PunyCode
		/// </summary>
		/// <param name="input">字符串</param>
		/// <returns>PunyCode字符串</returns>
		/// <exception cref="Exception">计算异常</exception>
		public static string Encode(string input)
		{
			return Encode(input, false);
		}

		/// <summary>
		/// 将内容编码为PunyCode
		/// </summary>
		/// <param name="input">字符串</param>
		/// <param name="withPrefix">是否包含 "xn--"前缀</param>
		/// <returns>PunyCode字符串</returns>
		/// <exception cref="Exception">计算异常</exception>
		public static string Encode(string input, bool withPrefix)
		{
			if (input == null)
			{
				throw new ArgumentNullException(nameof(input), "input must not be null!");
			}
			int n = INITIAL_N;
			int delta = 0;
			int bias = INITIAL_BIAS;
			var output = new StringBuilder();
			// Copy all basic code points to the output
			int b = 0;
			foreach (var c in input)
			{
				if (IsBasic(c))
				{
					output.Append(c);
					b++;
				}
			}
			// Append delimiter
			if (b > 0)
			{
				if (b == input.Length)
				{
					// 无需要编码的字符
					return output.ToString();
				}
				output.Append(DELIMITER);
			}
			int h = b;
			while (h < input.Length)
			{
				int m = int.MaxValue;
				// Find the minimum code point >= n
				foreach (var c in input)
				{
					if (c >= n && c < m)
					{
						m = c;
					}
				}
				if (m - n > (int.MaxValue - delta) / (h + 1))
				{
					throw new Exception("OVERFLOW");
				}
				delta = delta + (m - n) * (h + 1);
				n = m;
				foreach (var c in input)
				{
					if (c < n)
					{
						delta++;
						if (delta == 0)
						{
							throw new Exception("OVERFLOW");
						}
					}
					if (c == n)
					{
						int q = delta;
						for (int k = BASE; ; k += BASE)
						{
							int t;
							if (k <= bias)
							{
								t = TMIN;
							}
							else if (k >= bias + TMAX)
							{
								t = TMAX;
							}
							else
							{
								t = k - bias;
							}
							if (q < t)
							{
								break;
							}
							output.Append((char)DigitToCodepoint(t + (q - t) % (BASE - t)));
							q = (q - t) / (BASE - t);
						}
						output.Append((char)DigitToCodepoint(q));
						bias = Adapt(delta, h + 1, h == b);
						delta = 0;
						h++;
					}
				}
				delta++;
				n++;
			}

			if (withPrefix)
			{
				output.Insert(0, PunyCodePrefix);
			}
			return output.ToString();
		}

		/// <summary>
		/// 解码punycode域名
		/// </summary>
		/// <param name="domain">PunyCode域名</param>
		/// <returns>解码后的域名</returns>
		/// <exception cref="Exception">计算异常</exception>
		public static string DecodeDomain(string domain)
		{
			if (domain == null)
			{
				throw new ArgumentNullException(nameof(domain), "domain must not be null!");
			}
			var parts = domain.Split('.');
			var result = new StringBuilder(domain.Length / 4 + 1);
			foreach (var part in parts)
			{
				if (result.Length != 0)
				{
					result.Append('.');
				}
				result.Append(part.StartsWith(PunyCodePrefix, StringComparison.OrdinalIgnoreCase) ? Decode(part) : part);
			}

			return result.ToString();
		}

		/// <summary>
		/// 解码 PunyCode为字符串
		/// </summary>
		/// <param name="input">PunyCode</param>
		/// <returns>字符串</returns>
		/// <exception cref="Exception">计算异常</exception>
		public static string Decode(string input)
		{
			if (input == null)
			{
				throw new ArgumentNullException(nameof(input), "input must not be null!");
			}
			if (input.StartsWith(PunyCodePrefix, StringComparison.OrdinalIgnoreCase))
			{
				input = input.Substring(PunyCodePrefix.Length);
			}

			int n = INITIAL_N;
			int i = 0;
			int bias = INITIAL_BIAS;
			var output = new StringBuilder();
			int d = input.LastIndexOf(DELIMITER);
			if (d > 0)
			{
				for (int j = 0; j < d; j++)
				{
					var c = input[j];
					if (IsBasic(c))
					{
						output.Append(c);
					}
				}
				d++;
			}
			else
			{
				d = 0;
			}
			while (d < input.Length)
			{
				int oldi = i;
				int w = 1;
				for (int k = BASE; ; k += BASE)
				{
					if (d == input.Length)
					{
						throw new Exception("BAD_INPUT");
					}
					int c = input[d++];
					int digit = CodepointToDigit(c);
					if (digit > (int.MaxValue - i) / w)
					{
						throw new Exception("OVERFLOW");
					}
					i = i + digit * w;
					int t;
					if (k <= bias)
					{
						t = TMIN;
					}
					else if (k >= bias + TMAX)
					{
						t = TMAX;
					}
					else
					{
						t = k - bias;
					}
					if (digit < t)
					{
						break;
					}
					w = w * (BASE - t);
				}
				bias = Adapt(i - oldi, output.Length + 1, oldi == 0);
				if (i / (output.Length + 1) > int.MaxValue - n)
				{
					throw new Exception("OVERFLOW");
				}
				n = n + i / (output.Length + 1);
				i = i % (output.Length + 1);
				output.Insert(i, (char)n);
				i++;
			}

			return output.ToString();
		}

		private static int Adapt(int delta, int numpoints, bool first)
		{
			if (first)
			{
				delta = delta / DAMP;
			}
			else
			{
				delta = delta / 2;
			}
			delta = delta + (delta / numpoints);
			int k = 0;
			while (delta > ((BASE - TMIN) * TMAX) / 2)
			{
				delta = delta / (BASE - TMIN);
				k = k + BASE;
			}
			return k + ((BASE - TMIN + 1) * delta) / (delta + SKEW);
		}

		private static bool IsBasic(char c)
		{
			return c < 0x80;
		}

		/// <summary>
		/// 将数字转为字符，对应关系为：
		/// <pre>
		///     0 -&gt; a
		///     1 -&gt; b
		///     ...
		///     25 -&gt; z
		///     26 -&gt; '0'
		///     ...
		///     35 -&gt; '9'
		/// </pre>
		/// </summary>
		/// <param name="d">输入字符</param>
		/// <returns>转换后的字符</returns>
		/// <exception cref="Exception">无效字符</exception>
		private static int DigitToCodepoint(int d)
		{
			if (d < 0 || d > 35)
			{
				throw new ArgumentOutOfRangeException(nameof(d), "d must be between 0 and 35");
			}
			if (d < 26)
			{
				// 0..25 : 'a'..'z'
				return d + 'a';
			}
			else if (d < 36)
			{
				// 26..35 : '0'..'9';
				return d - 26 + '0';
			}
			else
			{
				throw new Exception("BAD_INPUT");
			}
		}

		/// <summary>
		/// 将字符转为数字，对应关系为：
		/// <pre>
		///     a -&gt; 0
		///     b -&gt; 1
		///     ...
		///     z -&gt; 25
		///     '0' -&gt; 26
		///     ...
		///     '9' -&gt; 35
		/// </pre>
		/// </summary>
		/// <param name="c">输入字符</param>
		/// <returns>转换后的字符</returns>
		/// <exception cref="Exception">无效字符</exception>
		private static int CodepointToDigit(int c)
		{
			if (c - '0' < 10)
			{
				// '0'..'9' : 26..35
				return c - '0' + 26;
			}
			else if (c - 'a' < 26)
			{
				// 'a'..'z' : 0..25
				return c - 'a';
			}
			else
			{
				throw new Exception("BAD_INPUT");
			}
		}
	}
}