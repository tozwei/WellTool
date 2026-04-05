using System;
using System.IO;

namespace WellTool.Core.Codec
{
	/// <summary>
	/// Base62编码解码实现，常用于短URL<br>
	/// From https://github.com/seruco/base62
	/// </summary>
	public class Base62Codec : Encoder<byte[], byte[]>, Decoder<byte[], byte[]>
	{
		private const int STANDARD_BASE = 256;
		private const int TARGET_BASE = 62;

		public static Base62Codec Instance = new Base62Codec();

		/// <summary>
		/// 编码指定消息bytes为Base62格式的bytes
		/// </summary>
		/// <param name="data">被编码的消息</param>
		/// <returns>Base62内容</returns>
		public byte[] Encode(byte[] data)
		{
			return Encode(data, false);
		}

		/// <summary>
		/// 编码指定消息bytes为Base62格式的bytes
		/// </summary>
		/// <param name="data">被编码的消息</param>
		/// <param name="useInverted">是否使用反转风格，即将GMP风格中的大小写做转换</param>
		/// <returns>Base62内容</returns>
		public byte[] Encode(byte[] data, bool useInverted)
		{
			var encoder = useInverted ? Base62Encoder.InvertedEncoder : Base62Encoder.GmpEncoder;
			return encoder.Encode(data);
		}

		/// <summary>
		/// 解码Base62消息
		/// </summary>
		/// <param name="encoded">Base62内容</param>
		/// <returns>消息</returns>
		public byte[] Decode(byte[] encoded)
		{
			return Decode(encoded, false);
		}

		/// <summary>
		/// 解码Base62消息
		/// </summary>
		/// <param name="encoded">Base62内容</param>
		/// <param name="useInverted">是否使用反转风格，即将GMP风格中的大小写做转换</param>
		/// <returns>消息</returns>
		public byte[] Decode(byte[] encoded, bool useInverted)
		{
			var decoder = useInverted ? Base62Decoder.InvertedDecoder : Base62Decoder.GmpDecoder;
			return decoder.Decode(encoded);
		}

		/// <summary>
		/// Base62编码器
		/// </summary>
		public class Base62Encoder : Encoder<byte[], byte[]>
		{
			/// <summary>
			/// GMP风格
			/// </summary>
			public static readonly byte[] Gmp = {
				(byte)'0', (byte)'1', (byte)'2', (byte)'3', (byte)'4', (byte)'5', (byte)'6', (byte)'7',
				(byte)'8', (byte)'9', (byte)'A', (byte)'B', (byte)'C', (byte)'D', (byte)'E', (byte)'F',
				(byte)'G', (byte)'H', (byte)'I', (byte)'J', (byte)'K', (byte)'L', (byte)'M', (byte)'N',
				(byte)'O', (byte)'P', (byte)'Q', (byte)'R', (byte)'S', (byte)'T', (byte)'U', (byte)'V',
				(byte)'W', (byte)'X', (byte)'Y', (byte)'Z', (byte)'a', (byte)'b', (byte)'c', (byte)'d',
				(byte)'e', (byte)'f', (byte)'g', (byte)'h', (byte)'i', (byte)'j', (byte)'k', (byte)'l',
				(byte)'m', (byte)'n', (byte)'o', (byte)'p', (byte)'q', (byte)'r', (byte)'s', (byte)'t',
				(byte)'u', (byte)'v', (byte)'w', (byte)'x', (byte)'y', (byte)'z'
			};

			/// <summary>
			/// 反转风格，即将GMP风格中的大小写做转换
			/// </summary>
			public static readonly byte[] Inverted = {
				(byte)'0', (byte)'1', (byte)'2', (byte)'3', (byte)'4', (byte)'5', (byte)'6', (byte)'7',
				(byte)'8', (byte)'9', (byte)'a', (byte)'b', (byte)'c', (byte)'d', (byte)'e', (byte)'f',
				(byte)'g', (byte)'h', (byte)'i', (byte)'j', (byte)'k', (byte)'l', (byte)'m', (byte)'n',
				(byte)'o', (byte)'p', (byte)'q', (byte)'r', (byte)'s', (byte)'t', (byte)'u', (byte)'v',
				(byte)'w', (byte)'x', (byte)'y', (byte)'z', (byte)'A', (byte)'B', (byte)'C', (byte)'D',
				(byte)'E', (byte)'F', (byte)'G', (byte)'H', (byte)'I', (byte)'J', (byte)'K', (byte)'L',
				(byte)'M', (byte)'N', (byte)'O', (byte)'P', (byte)'Q', (byte)'R', (byte)'S', (byte)'T',
				(byte)'U', (byte)'V', (byte)'W', (byte)'X', (byte)'Y', (byte)'Z'
			};

			public static Base62Encoder GmpEncoder = new Base62Encoder(Gmp);
			public static Base62Encoder InvertedEncoder = new Base62Encoder(Inverted);

			private readonly byte[] alphabet;

			/// <summary>
			/// 构造
			/// </summary>
			/// <param name="alphabet">字符表</param>
			public Base62Encoder(byte[] alphabet)
			{
				this.alphabet = alphabet;
			}

			public byte[] Encode(byte[] data)
			{
				var indices = Convert(data, STANDARD_BASE, TARGET_BASE);
				return Translate(indices, alphabet);
			}
		}

		/// <summary>
		/// Base62解码器
		/// </summary>
		public class Base62Decoder : Decoder<byte[], byte[]>
		{
			public static Base62Decoder GmpDecoder = new Base62Decoder(Base62Encoder.Gmp);
			public static Base62Decoder InvertedDecoder = new Base62Decoder(Base62Encoder.Inverted);

			private readonly byte[] lookupTable;

			/// <summary>
			/// 构造
			/// </summary>
			/// <param name="alphabet">字母表</param>
			public Base62Decoder(byte[] alphabet)
			{
				lookupTable = new byte['z' + 1];
				for (int i = 0; i < alphabet.Length; i++)
				{
					lookupTable[alphabet[i]] = (byte)i;
				}
			}

			public byte[] Decode(byte[] encoded)
			{
				var prepared = Translate(encoded, lookupTable);
				return Convert(prepared, TARGET_BASE, STANDARD_BASE);
			}
		}

		// region Private Methods

		/// <summary>
		/// 按照字典转换bytes
		/// </summary>
		/// <param name="indices">内容</param>
		/// <param name="dictionary">字典</param>
		/// <returns>转换值</returns>
		private static byte[] Translate(byte[] indices, byte[] dictionary)
		{
			var translation = new byte[indices.Length];

			for (int i = 0; i < indices.Length; i++)
			{
				translation[i] = dictionary[indices[i]];
			}

			return translation;
		}

		/// <summary>
		/// 使用定义的字母表从源基准到目标基准
		/// </summary>
		/// <param name="message">消息bytes</param>
		/// <param name="sourceBase">源基准长度</param>
		/// <param name="targetBase">目标基准长度</param>
		/// <returns>计算结果</returns>
		private static byte[] Convert(byte[] message, int sourceBase, int targetBase)
		{
			// 计算结果长度，算法来自：http://codegolf.stackexchange.com/a/21672
			var estimatedLength = EstimateOutputLength(message.Length, sourceBase, targetBase);

			using (var outputStream = new MemoryStream(estimatedLength))
			{
				byte[] source = message;

				while (source.Length > 0)
				{
					using (var quotientStream = new MemoryStream(source.Length))
					{
						int remainder = 0;

						foreach (byte b in source)
						{
							var accumulator = (b & 0xFF) + remainder * sourceBase;
							var digit = (accumulator - (accumulator % targetBase)) / targetBase;

							remainder = accumulator % targetBase;

							if (quotientStream.Length > 0 || digit > 0)
							{
								quotientStream.WriteByte((byte)digit);
							}
						}

						outputStream.WriteByte((byte)remainder);

						source = quotientStream.ToArray();
					}
				}

				// pad output with zeroes corresponding to the number of leading zeroes in the message
				for (int i = 0; i < message.Length - 1 && message[i] == 0; i++)
				{
					outputStream.WriteByte(0);
				}

				var result = outputStream.ToArray();
				Array.Reverse(result);
				return result;
			}
		}

		/// <summary>
		/// 估算结果长度
		/// </summary>
		/// <param name="inputLength">输入长度</param>
		/// <param name="sourceBase">源基准长度</param>
		/// <param name="targetBase">目标基准长度</param>
		/// <returns>估算长度</returns>
		private static int EstimateOutputLength(int inputLength, int sourceBase, int targetBase)
		{
			return (int)System.Math.Ceiling((System.Math.Log(sourceBase) / System.Math.Log(targetBase)) * inputLength);
		}
		// endregion
	}
}