namespace WellTool.Core.text.csv;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

/// <summary>
/// CSV工具类
/// </summary>
public class CsvUtil
{
	/// <summary>
	/// 读取CSV
	/// </summary>
	/// <param name="reader">读取器</param>
	/// <returns>行列表</returns>
	public static IList<string[]> Read(ICsvRowHandler reader)
	{
		var rows = new List<string[]>();
		string? line;
		while ((line = reader.ReadLine()) != null)
		{
			rows.Add(ParseLine(line));
		}
		return rows;
	}

	/// <summary>
	/// 读取CSV文件
	/// </summary>
	/// <param name="filePath">文件路径</param>
	/// <param name="encoding">编码</param>
	/// <returns>行列表</returns>
	public static IList<string[]> Read(string filePath, Encoding encoding = null)
	{
		encoding ??= Encoding.UTF8;
		var rows = new List<string[]>();
		using var reader = new StreamReader(filePath, encoding);
		string? line;
		while ((line = reader.ReadLine()) != null)
		{
			rows.Add(ParseLine(line));
		}
		return rows;
	}

	/// <summary>
	/// 解析CSV行
	/// </summary>
	/// <param name="line">行</param>
	/// <param name="separator">分隔符</param>
	/// <returns>字段列表</returns>
	public static string[] ParseLine(string line, char separator = ',')
	{
		if (string.IsNullOrEmpty(line))
			return Array.Empty<string>();

		var fields = new List<string>();
		var sb = new StringBuilder();
		var inQuotes = false;

		for (int i = 0; i < line.Length; i++)
		{
			var c = line[i];
			if (c == '"')
			{
				inQuotes = !inQuotes;
			}
			else if (c == separator && !inQuotes)
			{
				fields.Add(sb.ToString().Trim());
				sb.Clear();
			}
			else
			{
				sb.Append(c);
			}
		}

		fields.Add(sb.ToString().Trim());
		return fields.ToArray();
	}

	/// <summary>
	/// 写入CSV文件
	/// </summary>
	/// <param name="filePath">文件路径</param>
	/// <param name="rows">行数据</param>
	/// <param name="encoding">编码</param>
	public static void Write(string filePath, IEnumerable<string[]> rows, Encoding encoding = null)
	{
		encoding ??= Encoding.UTF8;
		using var writer = new StreamWriter(filePath, false, encoding);
		foreach (var row in rows)
		{
			writer.WriteLine(ToLine(row));
		}
	}

	/// <summary>
	/// 转换为CSV行
	/// </summary>
	/// <param name="fields">字段</param>
	/// <param name="separator">分隔符</param>
	/// <returns>CSV行</returns>
	public static string ToLine(string[] fields, char separator = ',')
	{
		return string.Join(separator.ToString(), fields.Select(f => EscapeField(f)));
	}

	/// <summary>
	/// 转义字段
	/// </summary>
	/// <param name="field">字段</param>
	/// <returns>转义后的字段</returns>
	public static string EscapeField(string field)
	{
		if (field.Contains(',') || field.Contains('"') || field.Contains('\n'))
		{
			return $"\"{field.Replace("\"", "\"\"")}\"";
		}
		return field;
	}
}

/// <summary>
/// CSV行处理器接口
/// </summary>
public interface ICsvRowHandler
{
	/// <summary>
	/// 读取一行
	/// </summary>
	/// <returns>行内容</returns>
	string? ReadLine();
}

/// <summary>
/// CSV解析器
/// </summary>
public class CsvParser
{
	/// <summary>
	/// 解析CSV文本
	/// </summary>
	/// <param name="csvText">CSV文本</param>
	/// <param name="separator">分隔符</param>
	/// <returns>行列表</returns>
	public static IList<string[]> Parse(string csvText, char separator = ',')
	{
		var lines = csvText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
		return lines.Select(line => CsvUtil.ParseLine(line, separator)).ToList();
	}
}

/// <summary>
/// CSV基础读取器
/// </summary>
public class CsvBaseReader : ICsvRowHandler
{
	private readonly StreamReader _reader;

	public CsvBaseReader(StreamReader reader)
	{
		_reader = reader;
	}

	public string? ReadLine()
	{
		return _reader.ReadLine();
	}
}

/// <summary>
/// CSV词元解析器
/// </summary>
public class CsvTokener
{
	private readonly string _csv;
	private int _position;

	public CsvTokener(string csv)
	{
		_csv = csv ?? throw new ArgumentNullException(nameof(csv));
		_position = 0;
	}

	/// <summary>
	/// 获取下一个词元
	/// </summary>
	/// <returns>词元</returns>
	public string? NextToken()
	{
		SkipWhitespace();
		if (_position >= _csv.Length) return null;

		var sb = new StringBuilder();
		var inQuotes = false;

		while (_position < _csv.Length)
		{
			var c = _csv[_position++];
			if (c == '"')
			{
				inQuotes = !inQuotes;
			}
			else if (c == ',' && !inQuotes)
			{
				break;
			}
			else
			{
				sb.Append(c);
			}
		}

		return sb.ToString().Trim();
	}

	private void SkipWhitespace()
	{
		while (_position < _csv.Length && char.IsWhiteSpace(_csv[_position]))
			_position++;
	}
}
