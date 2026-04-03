using System.Text;

namespace WellTool.Core.Lang.Id;

/// <summary>
/// NanoId算法实现，生成URL友好的唯一ID
/// </summary>
public static class NanoId
{
	private static readonly Random Random = new Random();
	private const string DefaultAlphabet = IdConstants.DefaultAlphabet;

	/// <summary>
	/// 生成随机NanoId
	/// </summary>
	/// <returns>NanoId字符串</returns>
	public static string Next()
	{
		return Next(DefaultAlphabet, IdConstants.DefaultSize);
	}

	/// <summary>
	/// 生成随机NanoId
	/// </summary>
	/// <param name="size">ID大小</param>
	/// <returns>NanoId字符串</returns>
	public static string Next(int size)
	{
		return Next(DefaultAlphabet, size);
	}

	/// <summary>
	/// 生成随机NanoId
	/// </summary>
	/// <param name="alphabet">字符集</param>
	/// <returns>NanoId字符串</returns>
	public static string Next(string alphabet)
	{
		return Next(alphabet, IdConstants.DefaultSize);
	}

	/// <summary>
	/// 生成随机NanoId
	/// </summary>
	/// <param name="alphabet">字符集</param>
	/// <param name="size">ID大小</param>
	/// <returns>NanoId字符串</returns>
	public static string Next(string alphabet, int size)
	{
		if (string.IsNullOrEmpty(alphabet))
			throw new ArgumentException("Alphabet cannot be empty", nameof(alphabet));
		if (size <= 0)
			throw new ArgumentException("Size must be positive", nameof(size));

		var alphabetLen = alphabet.Length;
		var log2 = Math.Floor(Math.Log(alphabetLen - 1) / Math.Log(2));
		var mask = (int)((2 << (int)log2) - 1);
		var step = (int)Math.Ceiling(1.6 * mask * size / alphabetLen);

		var idBuilder = new StringBuilder(size);
		var bytes = new byte[step];

		while (idBuilder.Length < size)
		{
			Random.NextBytes(bytes);
			for (var i = 0; i < step && idBuilder.Length < size; i++)
			{
				var index = mask & bytes[i];
				if (index < alphabetLen)
				{
					idBuilder.Append(alphabet[index]);
				}
			}
		}

		return idBuilder.ToString();
	}

	/// <summary>
	/// 生成随机NanoId（使用指定随机数生成器）
	/// </summary>
	/// <param name="random">随机数生成器</param>
	/// <param name="alphabet">字符集</param>
	/// <param name="size">ID大小</param>
	/// <returns>NanoId字符串</returns>
	public static string Next(Random random, string alphabet, int size)
	{
		if (random == null)
			throw new ArgumentNullException(nameof(random));
		if (string.IsNullOrEmpty(alphabet))
			throw new ArgumentException("Alphabet cannot be empty", nameof(alphabet));
		if (size <= 0)
			throw new ArgumentException("Size must be positive", nameof(size));

		var alphabetLen = alphabet.Length;
		var log2 = Math.Floor(Math.Log(alphabetLen - 1) / Math.Log(2));
		var mask = (int)((2 << (int)log2) - 1);
		var step = (int)Math.Ceiling(1.6 * mask * size / alphabetLen);

		var idBuilder = new StringBuilder(size);
		var bytes = new byte[step];

		while (idBuilder.Length < size)
		{
			random.NextBytes(bytes);
			for (var i = 0; i < step && idBuilder.Length < size; i++)
			{
				var index = mask & bytes[i];
				if (index < alphabetLen)
				{
					idBuilder.Append(alphabet[index]);
				}
			}
		}

		return idBuilder.ToString();
	}
}
