using System;
using System.Collections.Generic;
using System.Linq;

namespace WellTool.Core.Lang;

/// <summary>
/// 字符串版本表示，用于解析版本号的不同部分并比较大小
/// </summary>
[Serializable]
public class Version : IComparable<Version>
{
	/// <summary>
	/// 解析版本字符串为Version对象
	/// </summary>
	/// <param name="v">版本字符串</param>
	/// <returns>Version</returns>
	public static Version Of(string v)
	{
		return new Version(v);
	}

	private readonly string _version;
	private readonly List<object> _sequence = new();
	private readonly List<object> _pre = new();
	private readonly List<object> _build = new();

	/// <summary>
	/// 版本对象，格式：主版本[-次版本][+构建版本]
	/// </summary>
	/// <param name="v">版本字符串</param>
	public Version(string v)
	{
		if (string.IsNullOrEmpty(v))
		{
			throw new ArgumentNullException(nameof(v), "Null version string");
		}

		_version = v;

		if (v.Length == 0)
		{
			return;
		}

		int i = 0;
		char c = v[i];

		// 解析主版本
		i = TakeNumber(v, i, _sequence);

		while (i < v.Length)
		{
			c = v[i];
			if (c == '.')
			{
				i++;
				continue;
			}
			if (c == '-' || c == '+')
			{
				i++;
				break;
			}
			if (char.IsDigit(c))
			{
				i = TakeNumber(v, i, _sequence);
			}
			else
			{
				i = TakeString(v, i, _sequence);
			}
		}

		if (c == '-' && i >= v.Length)
		{
			return;
		}

		// 解析次版本
		while (i < v.Length)
		{
			c = v[i];
			if (char.IsDigit(c))
			{
				i = TakeNumber(v, i, _pre);
			}
			else
			{
				i = TakeString(v, i, _pre);
			}
			if (i >= v.Length)
			{
				break;
			}
			c = v[i];
			if (c == '.' || c == '-')
			{
				i++;
				continue;
			}
			if (c == '+')
			{
				i++;
				break;
			}
		}

		if (c == '+' && i >= v.Length)
		{
			return;
		}

		// 解析build版本
		while (i < v.Length)
		{
			c = v[i];
			if (char.IsDigit(c))
			{
				i = TakeNumber(v, i, _build);
			}
			else
			{
				i = TakeString(v, i, _build);
			}
			if (i >= v.Length)
			{
				break;
			}
			c = v[i];
			if (c == '.' || c == '-' || c == '+')
			{
				i++;
			}
		}
	}

	/// <summary>
	/// 比较版本
	/// </summary>
	public int CompareTo(Version that)
	{
		if (that == null) return 1;

		int c = CompareTokens(_sequence, that._sequence);
		if (c != 0) return c;

		if (_pre.Count == 0)
		{
			if (that._pre.Count > 0)
			{
				return +1;
			}
		}
		else
		{
			if (that._pre.Count == 0)
			{
				return -1;
			}
		}
		c = CompareTokens(_pre, that._pre);
		if (c != 0) return c;

		return CompareTokens(_build, that._build);
	}

	public override bool Equals(object ob)
	{
		if (!(ob is Version))
		{
			return false;
		}
		return CompareTo((Version)ob) == 0;
	}

	public override int GetHashCode()
	{
		return _version.GetHashCode();
	}

	public override string ToString()
	{
		return _version;
	}

	/// <summary>
	/// 获取字符串中从位置i开始的数字
	/// </summary>
	private static int TakeNumber(string s, int i, List<object> acc)
	{
		char c = s[i];
		int d = (c - '0');
		while (++i < s.Length)
		{
			c = s[i];
			if (char.IsDigit(c))
			{
				d = d * 10 + (c - '0');
				continue;
			}
			break;
		}
		acc.Add(d);
		return i;
	}

	/// <summary>
	/// 获取字符串中从位置i开始的字符串
	/// </summary>
	private static int TakeString(string s, int i, List<object> acc)
	{
		int b = i;
		int n = s.Length;
		while (++i < n)
		{
			char c = s[i];
			if (c != '.' && c != '-' && c != '+' && !char.IsDigit(c))
			{
				continue;
			}
			break;
		}
		acc.Add(s.Substring(b, i - b));
		return i;
	}

	/// <summary>
	/// 比较节点
	/// </summary>
	private static int CompareTokens(List<object> ts1, List<object> ts2)
	{
		int n = System.Math.Min(ts1.Count, ts2.Count);
		for (int i = 0; i < n; i++)
		{
			object o1 = ts1[i];
			object o2 = ts2[i];
			if (o1.GetType() == o2.GetType())
			{
				int c = Comparer<object>.Default.Compare(o1, o2);
				if (c == 0) continue;
				return c;
			}
			int compareResult = o1.ToString().CompareTo(o2.ToString());
			if (compareResult == 0) continue;
			return compareResult;
		}
		List<object> rest = ts1.Count > ts2.Count ? ts1 : ts2;
		int e = rest.Count;
		for (int i = n; i < e; i++)
		{
			object o = rest[i];
			if (o is int intVal && intVal == 0)
			{
				continue;
			}
			return ts1.Count - ts2.Count;
		}
		return 0;
	}
}
