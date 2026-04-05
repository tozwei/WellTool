using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using WellTool.Core.Math;

namespace WellTool.Core.Codec
{
	/// <summary>
	/// <a href="http://hashids.org/">Hashids</a> 协议实现，以实现：
	/// <ul>
	/// <li>生成简短、唯一、大小写敏感并无序的hash值</li>
	/// <li>自然数字的Hash值</li>
	/// <li>可以设置不同的盐，具有保密性</li>
	/// <li>可配置的hash长度</li>
	/// <li>递增的输入产生的输出无法预测</li>
	/// </ul>
	/// 
	/// <p>
	/// 来自：<a href="https://github.com/davidafsilva/java-hashids">https://github.com/davidafsilva/java-hashids</a>
	/// </p>
	/// 
	/// <p>
	/// {@code Hashids}可以将数字或者16进制字符串转为短且唯一不连续的字符串，采用双向编码实现，比如，它可以将347之类的数字转换为yr8之类的字符串，也可以将yr8之类的字符串重新解码为347之类的数字。<br>
	/// 此编码算法主要是解决爬虫类应用对连续ID爬取问题，将有序的ID转换为无序的Hashids，而且一一对应。
	/// </p>
	/// </summary>
	public class Hashids : Encoder<long[], string>, Decoder<string, long[]>
	{
		private const int LotteryMod = 100;
		private const double GuardThreshold = 12;
		private const double SeparatorThreshold = 3.5;
		// 最小编解码字符串
		private const int MinAlphabetLength = 16;
		private static readonly Regex HexValuesPattern = new Regex("[\\w\\W]{1,12}");

		// 默认编解码字符串
		public static readonly char[] DefaultAlphabet = {
			'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
			'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
			'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
			'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
			'1', '2', '3', '4', '5', '6', '7', '8', '9', '0'
		};
		// 默认分隔符
		private static readonly char[] DefaultSeparators = {
			'c', 'f', 'h', 'i', 's', 't', 'u', 'C', 'F', 'H', 'I', 'S', 'T', 'U'
		};

		// algorithm properties
		private readonly char[] alphabet;
		// 多个数字编解码的分界符
		private readonly char[] separators;
		private readonly HashSet<char> separatorsSet;
		private readonly char[] salt;
		// 补齐至 minLength 长度添加的字符列表
		private readonly char[] guards;
		// 编码后最小的字符长度
		private readonly int minLength;

		// region create

		/// <summary>
		/// 根据参数值，创建{@code Hashids}，使用默认{@link #DefaultAlphabet}作为字母表，不限制最小长度
		/// </summary>
		/// <param name="salt">加盐值</param>
		/// <returns>{@code Hashids}</returns>
		public static Hashids Create(char[] salt)
		{
			return Create(salt, DefaultAlphabet, -1);
		}

		/// <summary>
		/// 根据参数值，创建{@code Hashids}，使用默认{@link #DefaultAlphabet}作为字母表
		/// </summary>
		/// <param name="salt">加盐值</param>
		/// <param name="minLength">限制最小长度，-1表示不限制</param>
		/// <returns>{@code Hashids}</returns>
		public static Hashids Create(char[] salt, int minLength)
		{
			return Create(salt, DefaultAlphabet, minLength);
		}

		/// <summary>
		/// 根据参数值，创建{@code Hashids}
		/// </summary>
		/// <param name="salt">加盐值</param>
		/// <param name="alphabet">hash字母表</param>
		/// <param name="minLength">限制最小长度，-1表示不限制</param>
		/// <returns>{@code Hashids}</returns>
		public static Hashids Create(char[] salt, char[] alphabet, int minLength)
		{
			return new Hashids(salt, alphabet, minLength);
		}
		// endregion

		/// <summary>
		/// 构造
		/// </summary>
		/// <param name="salt">加盐值</param>
		/// <param name="alphabet">hash字母表</param>
		/// <param name="minLength">限制最小长度，-1表示不限制</param>
		public Hashids(char[] salt, char[] alphabet, int minLength)
		{
			this.minLength = minLength;
			this.salt = (char[])salt.Clone();

			// filter and shuffle separators
			var tmpSeparators = Shuffle(FilterSeparators(DefaultSeparators, alphabet), this.salt);

			// validate and filter the alphabet
			var tmpAlphabet = ValidateAndFilterAlphabet(alphabet, tmpSeparators);

			// check separator threshold
			if (tmpSeparators.Length == 0 ||
				((double)(tmpAlphabet.Length / tmpSeparators.Length)) > SeparatorThreshold)
			{
				var minSeparatorsSize = (int)System.Math.Ceiling(tmpAlphabet.Length / SeparatorThreshold);
				// check minimum size of separators
				if (minSeparatorsSize > tmpSeparators.Length)
				{
					// fill separators from alphabet
					var missingSeparators = minSeparatorsSize - tmpSeparators.Length;
					tmpSeparators = tmpSeparators.Concat(tmpAlphabet.Take(missingSeparators)).ToArray();
					tmpAlphabet = tmpAlphabet.Skip(missingSeparators).ToArray();
				}
			}

			// shuffle the current alphabet
			Shuffle(tmpAlphabet, this.salt);

			// check guards
			this.guards = new char[(int)System.Math.Ceiling(tmpAlphabet.Length / GuardThreshold)];
			if (alphabet.Length < 3)
			{
				Array.Copy(tmpSeparators, 0, guards, 0, guards.Length);
				this.separators = tmpSeparators.Skip(guards.Length).ToArray();
				this.alphabet = tmpAlphabet;
			}
			else
			{
				Array.Copy(tmpAlphabet, 0, guards, 0, guards.Length);
				this.separators = tmpSeparators;
				this.alphabet = tmpAlphabet.Skip(guards.Length).ToArray();
			}

			// create the separators set
			separatorsSet = new HashSet<char>(separators);
		}

		/// <summary>
		/// 编码给定的16进制数字
		/// </summary>
		/// <param name="hexNumbers">16进制数字</param>
		/// <returns>编码后的值, {@code null} if {@code numbers} 是 {@code null}.</returns>
		/// <exception cref="ArgumentException">数字不支持抛出此异常</exception>
		public string EncodeFromHex(string hexNumbers)
		{
			if (hexNumbers == null)
			{
				return null;
			}

			// remove the prefix, if present
			var hex = hexNumbers.StartsWith("0x", StringComparison.OrdinalIgnoreCase) ?
				hexNumbers.Substring(2) : hexNumbers;

			// get the associated long value and encode it
			var values = new List<long>();
			var matcher = HexValuesPattern.Matches(hex);
			foreach (Match match in matcher)
			{
				var value = System.Convert.ToInt64("1" + match.Value, 16);
				values.Add(value);
			}

			return Encode(values.ToArray());
		}

		/// <summary>
		/// 编码给定的数字数组
		/// </summary>
		/// <param name="numbers">数字数组</param>
		/// <returns>编码后的值, {@code null} if {@code numbers} 是 {@code null}.</returns>
		/// <exception cref="ArgumentException">数字不支持抛出此异常</exception>
		public string Encode(params long[] numbers)
		{
			if (numbers == null)
			{
				return null;
			}

			// copy alphabet
			var currentAlphabet = (char[])alphabet.Clone();

			// determine the lottery number
			long lotteryId = 0;
			for (int i = 0; i < numbers.Length; i++)
			{
				var number = numbers[i];
				if (number < 0)
				{
					throw new ArgumentException("invalid number: " + number);
				}
				lotteryId += number % (i + LotteryMod);
			}
			var lottery = currentAlphabet[(int)(lotteryId % currentAlphabet.Length)];

			// encode each number
			var global = new StringBuilder();
			for (int idx = 0; idx < numbers.Length; idx++)
			{
				// derive alphabet
				DeriveNewAlphabet(currentAlphabet, salt, lottery);

				// encode
				var initialLength = global.Length;
				Translate(numbers[idx], currentAlphabet, global, initialLength);

				// prepend the lottery
				if (idx == 0)
				{
					global.Insert(0, lottery);
				}

				// append the separator, if more numbers are pending encoding
				if (idx + 1 < numbers.Length)
				{
					long n = numbers[idx] % (global[initialLength] + 1);
					global.Append(separators[(int)(n % separators.Length)]);
				}
			}

			// add the guards, if there's any space left
			if (minLength > global.Length)
			{
				int guardIdx = (int)((lotteryId + lottery) % guards.Length);
				global.Insert(0, guards[guardIdx]);
				if (minLength > global.Length)
				{
					guardIdx = (int)((lotteryId + global[2]) % guards.Length);
					global.Append(guards[guardIdx]);
				}
			}

			// add the necessary padding
			int paddingLeft = minLength - global.Length;
			while (paddingLeft > 0)
			{
				Shuffle(currentAlphabet, (char[])currentAlphabet.Clone());

				var alphabetHalfSize = currentAlphabet.Length / 2;
				var initialSize = global.Length;
				if (paddingLeft > currentAlphabet.Length)
				{
					// entire alphabet with the current encoding in the middle of it
					int offset = alphabetHalfSize + (currentAlphabet.Length % 2 == 0 ? 0 : 1);

					global.Insert(0, currentAlphabet, alphabetHalfSize, offset);
					global.Insert(offset + initialSize, currentAlphabet, 0, alphabetHalfSize);
					// decrease the padding left
					paddingLeft -= currentAlphabet.Length;
				}
				else
				{
					// calculate the excess
					var excess = currentAlphabet.Length + global.Length - minLength;
					var secondHalfStartOffset = alphabetHalfSize + excess / 2;
					var secondHalfLength = currentAlphabet.Length - secondHalfStartOffset;
					var firstHalfLength = paddingLeft - secondHalfLength;

					global.Insert(0, currentAlphabet, secondHalfStartOffset, secondHalfLength);
					global.Insert(secondHalfLength + initialSize, currentAlphabet, 0, firstHalfLength);

					paddingLeft = 0;
				}
			}

			return global.ToString();
		}

		//-------------------------
		// Decode
		//-------------------------

		/// <summary>
		/// 解码Hash值为16进制数字
		/// </summary>
		/// <param name="hash">hash值</param>
		/// <returns>解码后的16进制值, {@code null} if {@code numbers} 是 {@code null}.</returns>
		/// <exception cref="ArgumentException">if the hash is invalid.</exception>
		public string DecodeToHex(string hash)
		{
			if (hash == null)
			{
				return null;
			}

			var sb = new StringBuilder();
			foreach (var value in Decode(hash))
			{
				sb.Append(System.Convert.ToString(value, 16).Substring(1));
			}
			return sb.ToString();
		}

		/// <summary>
		/// 解码Hash值为数字数组
		/// </summary>
		/// <param name="hash">hash值</param>
		/// <returns>解码后的16进制值, {@code null} if {@code numbers} 是 {@code null}.</returns>
		/// <exception cref="ArgumentException">if the hash is invalid.</exception>
		public long[] Decode(string hash)
		{
			if (hash == null)
			{
				return null;
			}

			// create a set of the guards
			var guardsSet = new HashSet<char>(guards);
			// count the total guards used
			var guardsIdx = new List<int>();
			for (int i = 0; i < hash.Length; i++)
			{
				if (guardsSet.Contains(hash[i]))
				{
					guardsIdx.Add(i);
				}
			}
			// get the start/end index base on the guards count
			int startIdx, endIdx;
			if (guardsIdx.Count > 0)
			{
				startIdx = guardsIdx[0] + 1;
				endIdx = guardsIdx.Count > 1 ? guardsIdx[1] : hash.Length;
			}
			else
			{
				startIdx = 0;
				endIdx = hash.Length;
			}

			var decoded = new List<long>();
			// parse the hash
			if (hash.Length > 0)
			{
				var lottery = hash[startIdx];

				// create the initial accumulation string
				var length = hash.Length - guardsIdx.Count - 1;
				var block = new StringBuilder(length);

				// create the base salt
				var decodeSalt = new char[alphabet.Length];
				decodeSalt[0] = lottery;
				var saltLength = salt.Length >= alphabet.Length ? alphabet.Length - 1 : salt.Length;
				Array.Copy(salt, 0, decodeSalt, 1, saltLength);
				var saltLeft = alphabet.Length - saltLength - 1;

				// copy alphabet
				var currentAlphabet = (char[])alphabet.Clone();

				for (int i = startIdx + 1; i < endIdx; i++)
				{
					if (!separatorsSet.Contains(hash[i]))
					{
						block.Append(hash[i]);
						// continue if we have not reached the end, yet
						if (i < endIdx - 1)
						{
							continue;
						}
					}

					if (block.Length > 0)
					{
						// create the salt
						if (saltLeft > 0)
						{
							Array.Copy(currentAlphabet, 0, decodeSalt,
								alphabet.Length - saltLeft, saltLeft);
						}

						// shuffle the alphabet
						Shuffle(currentAlphabet, decodeSalt);

						// prepend the decoded value
						var n = Translate(block.ToString().ToCharArray(), currentAlphabet);
						decoded.Add(n);

						// create a new block
						block = new StringBuilder(length);
					}
				}
			}

			// validate the hash
			var decodedValue = decoded.ToArray();
			if (!hash.Equals(Encode(decodedValue)))
			{
				throw new ArgumentException("invalid hash: " + hash);
			}

			return decodedValue;
		}

		private StringBuilder Translate(long n, char[] alphabet, StringBuilder sb, int start)
		{
			long input = n;
			do
			{
				// prepend the chosen char
				sb.Insert(start, alphabet[(int)(input % alphabet.Length)]);

				// trim the input
				input = input / alphabet.Length;
			} while (input > 0);

			return sb;
		}

		private long Translate(char[] hash, char[] alphabet)
		{
			long number = 0;

			var alphabetMapping = new Dictionary<char, int>();
			for (int i = 0; i < alphabet.Length; i++)
			{
				alphabetMapping[alphabet[i]] = i;
			}

			for (int i = 0; i < hash.Length; ++i)
			{
				if (!alphabetMapping.TryGetValue(hash[i], out int value))
				{
					throw new ArgumentException("Invalid alphabet for hash");
				}
				number += value * (long)System.Math.Pow(alphabet.Length, hash.Length - i - 1);
			}

			return number;
		}

		private char[] DeriveNewAlphabet(char[] alphabet, char[] salt, char lottery)
		{
			// create the new salt
			var newSalt = new char[alphabet.Length];

			// 1. lottery
			newSalt[0] = lottery;
			int spaceLeft = newSalt.Length - 1;
			int offset = 1;
			// 2. salt
			if (salt.Length > 0 && spaceLeft > 0)
			{
				int length = System.Math.Min(salt.Length, spaceLeft);
				Array.Copy(salt, 0, newSalt, offset, length);
				spaceLeft -= length;
				offset += length;
			}
			// 3. alphabet
			if (spaceLeft > 0)
			{
				Array.Copy(alphabet, 0, newSalt, offset, spaceLeft);
			}

			// shuffle
			return Shuffle(alphabet, newSalt);
		}

		private char[] ValidateAndFilterAlphabet(char[] alphabet, char[] separators)
		{
			// validate size
			if (alphabet.Length < MinAlphabetLength)
			{
				throw new ArgumentException(string.Format("alphabet must contain at least {0} unique " +
					"characters: {1}", MinAlphabetLength, alphabet.Length));
			}

			var seen = new HashSet<char>();
			var invalid = new HashSet<char>(separators);

			// add to seen set (without duplicates)
			for (int i = 0; i < alphabet.Length; i++)
			{
				if (alphabet[i] == ' ')
				{
					throw new ArgumentException(string.Format("alphabet must not contain spaces: " +
						"index {0}", i));
				}
				var c = alphabet[i];
				if (!invalid.Contains(c))
				{
					seen.Add(c);
				}
			}

			// create a new alphabet without the duplicates
			return seen.ToArray();
		}

		private char[] FilterSeparators(char[] separators, char[] alphabet)
		{
			var valid = new HashSet<char>(alphabet);

			return separators.Where(valid.Contains).ToArray();
		}

		private char[] Shuffle(char[] alphabet, char[] salt)
		{
			for (int i = alphabet.Length - 1, v = 0, p = 0, j, z; salt.Length > 0 && i > 0; i--, v++)
			{
				v %= salt.Length;
				p += z = salt[v];
				j = (z + v + p) % i;
				var tmp = alphabet[j];
				alphabet[j] = alphabet[i];
				alphabet[i] = tmp;
			}
			return alphabet;
		}
	}
}