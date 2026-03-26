using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WellTool.Core.Codec
{
	/// <summary>
	/// 莫尔斯电码的编码和解码实现<br>
	/// 参考：https://github.com/TakWolf-Deprecated/Java-MorseCoder
	/// </summary>
	public class Morse
	{
		private static readonly Dictionary<int, string> Alphabets = new Dictionary<int, string>(); // code point -> morse
		private static readonly Dictionary<string, int> Dictionaries = new Dictionary<string, int>(); // morse -> code point

		/// <summary>
		/// 注册莫尔斯电码表
		/// </summary>
		/// <param name="abc">字母和字符</param>
		/// <param name="dict">二进制</param>
		private static void RegisterMorse(char abc, string dict)
		{
			Alphabets[(int)abc] = dict;
			Dictionaries[dict] = (int)abc;
		}

		static Morse()
		{
			// Letters
			RegisterMorse('A', "01");
			RegisterMorse('B', "1000");
			RegisterMorse('C', "1010");
			RegisterMorse('D', "100");
			RegisterMorse('E', "0");
			RegisterMorse('F', "0010");
			RegisterMorse('G', "110");
			RegisterMorse('H', "0000");
			RegisterMorse('I', "00");
			RegisterMorse('J', "0111");
			RegisterMorse('K', "101");
			RegisterMorse('L', "0100");
			RegisterMorse('M', "11");
			RegisterMorse('N', "10");
			RegisterMorse('O', "111");
			RegisterMorse('P', "0110");
			RegisterMorse('Q', "1101");
			RegisterMorse('R', "010");
			RegisterMorse('S', "000");
			RegisterMorse('T', "1");
			RegisterMorse('U', "001");
			RegisterMorse('V', "0001");
			RegisterMorse('W', "011");
			RegisterMorse('X', "1001");
			RegisterMorse('Y', "1011");
			RegisterMorse('Z', "1100");
			// Numbers
			RegisterMorse('0', "11111");
			RegisterMorse('1', "01111");
			RegisterMorse('2', "00111");
			RegisterMorse('3', "00011");
			RegisterMorse('4', "00001");
			RegisterMorse('5', "00000");
			RegisterMorse('6', "10000");
			RegisterMorse('7', "11000");
			RegisterMorse('8', "11100");
			RegisterMorse('9', "11110");
			// Punctuation
			RegisterMorse('.', "010101");
			RegisterMorse(',', "110011");
			RegisterMorse('?', "001100");
			RegisterMorse('\'', "011110");
			RegisterMorse('!', "101011");
			RegisterMorse('/', "10010");
			RegisterMorse('(', "10110");
			RegisterMorse(')', "101101");
			RegisterMorse('&', "01000");
			RegisterMorse(':', "111000");
			RegisterMorse(';', "101010");
			RegisterMorse('=', "10001");
			RegisterMorse('+', "01010");
			RegisterMorse('-', "100001");
			RegisterMorse('_', "001101");
			RegisterMorse('"', "010010");
			RegisterMorse('$', "0001001");
			RegisterMorse('@', "011010");
		}

		private readonly char dit; // short mark or dot
		private readonly char dah; // longer mark or dash
		private readonly char split;

		/// <summary>
		/// 构造
		/// </summary>
		public Morse() : this('.', '-', '/')
		{
		}

		/// <summary>
		/// 构造
		/// </summary>
		/// <param name="dit">点表示的字符</param>
		/// <param name="dah">横线表示的字符</param>
		/// <param name="split">分隔符</param>
		public Morse(char dit, char dah, char split)
		{
			this.dit = dit;
			this.dah = dah;
			this.split = split;
		}

		/// <summary>
		/// 编码
		/// </summary>
		/// <param name="text">文本</param>
		/// <returns>密文</returns>
		public string Encode(string text)
		{
			if (text == null)
			{
				throw new ArgumentNullException(nameof(text), "Text should not be null.");
			}

			text = text.ToUpper();
			var morseBuilder = new StringBuilder();
			foreach (var c in text)
			{
				int codePoint = c;
				string word;
				if (!Alphabets.TryGetValue(codePoint, out word))
				{
					word = System.Convert.ToString(codePoint, 2);
				}
				morseBuilder.Append(word.Replace('0', dit).Replace('1', dah)).Append(split);
			}
			return morseBuilder.ToString();
		}

		/// <summary>
		/// 解码
		/// </summary>
		/// <param name="morse">莫尔斯电码</param>
		/// <returns>明文</returns>
		public string Decode(string morse)
		{
			if (morse == null)
			{
				throw new ArgumentNullException(nameof(morse), "Morse should not be null.");
			}

			var allowedChars = new[] { dit, dah, split };
			if (!morse.All(c => allowedChars.Contains(c)))
			{
				throw new ArgumentException("Incorrect morse.");
			}
			var words = morse.Split(split);
			var textBuilder = new StringBuilder();
			int codePoint;
			foreach (var word in words)
			{
				if (string.IsNullOrEmpty(word))
				{
					continue;
				}
				var binaryWord = word.Replace(dit, '0').Replace(dah, '1');
				if (!Dictionaries.TryGetValue(binaryWord, out codePoint))
				{
					codePoint = System.Convert.ToInt32(binaryWord, 2);
				}
				textBuilder.Append((char)codePoint);
			}
			return textBuilder.ToString();
		}
	}
}