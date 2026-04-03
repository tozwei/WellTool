using System.Text.RegularExpressions;

namespace WellTool.Core.Lang;

/// <summary>
/// 版本号比较工具类
/// </summary>
public class Version : IComparable<Version>, IComparable
{
	private readonly int[] _versionParts;

	/// <summary>
	/// 获取主版本号
	/// </summary>
	public int Major => _versionParts.Length > 0 ? _versionParts[0] : 0;

	/// <summary>
	/// 获取次版本号
	/// </summary>
	public int Minor => _versionParts.Length > 1 ? _versionParts[1] : 0;

	/// <summary>
	/// 获取修订版本号
	/// </summary>
	public int Patch => _versionParts.Length > 2 ? _versionParts[2] : 0;

	/// <summary>
	/// 完整版本字符串
	/// </summary>
	public string VersionString { get; }

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="versionString">版本字符串，如 "1.2.3"</param>
	public Version(string versionString)
	{
		VersionString = versionString;
		_versionParts = ParseVersionParts(versionString);
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="major">主版本号</param>
	/// <param name="minor">次版本号</param>
	/// <param name="patch">修订版本号</param>
	public Version(int major, int minor, int patch)
	{
		_versionParts = new[] { major, minor, patch };
		VersionString = $"{major}.{minor}.{patch}";
	}

	private static int[] ParseVersionParts(string versionString)
	{
		if (string.IsNullOrWhiteSpace(versionString))
			return Array.Empty<int>();

		var parts = Regex.Split(versionString.Trim(), @"[.\-_]");
		var result = new List<int>();
		foreach (var part in parts)
		{
			if (int.TryParse(part, out var num))
				result.Add(num);
		}
		return result.ToArray();
	}

	/// <summary>
	/// 比较两个版本
	/// </summary>
	/// <param name="other">另一个版本</param>
	/// <returns>比较结果</returns>
	public int CompareTo(Version? other)
	{
		if (other == null) return 1;

		var maxLen = Math.Max(_versionParts.Length, other._versionParts.Length);
		for (var i = 0; i < maxLen; i++)
		{
			var thisPart = i < _versionParts.Length ? _versionParts[i] : 0;
			var otherPart = i < other._versionParts.Length ? other._versionParts[i] : 0;

			if (thisPart != otherPart)
				return thisPart.CompareTo(otherPart);
		}
		return 0;
	}

	/// <summary>
	/// 比较两个版本（显式实现IComparable）
	/// </summary>
	int IComparable.CompareTo(object? obj)
	{
		if (obj is Version other)
			return CompareTo(other);
		if (obj == null)
			return 1;
		throw new ArgumentException("Object must be of type Version", nameof(obj));
	}

	/// <summary>
	/// 是否大于
	/// </summary>
	public static bool operator >(Version left, Version right) => left.CompareTo(right) > 0;

	/// <summary>
	/// 是否小于
	/// </summary>
	public static bool operator <(Version left, Version right) => left.CompareTo(right) < 0;

	/// <summary>
	/// 是否大于等于
	/// </summary>
	public static bool operator >=(Version left, Version right) => left.CompareTo(right) >= 0;

	/// <summary>
	/// 是否小于等于
	/// </summary>
	public static bool operator <=(Version left, Version right) => left.CompareTo(right) <= 0;

	/// <summary>
	/// 是否等于
	/// </summary>
	public static bool operator ==(Version? left, Version? right)
	{
		if (left is null) return right is null;
		return left.Equals(right);
	}

	/// <summary>
	/// 是否不等于
	/// </summary>
	public static bool operator !=(Version? left, Version? right) => !(left == right);

	/// <inheritdoc />
	public override bool Equals(object? obj)
	{
		if (obj is Version other)
			return CompareTo(other) == 0;
		return false;
	}

	/// <inheritdoc />
	public override int GetHashCode() => VersionString.GetHashCode();

	/// <inheritdoc />
	public override string ToString() => VersionString;

	/// <summary>
	/// 检查当前版本是否在指定范围内
	/// </summary>
	/// <param name="minVersion">最小版本（包含）</param>
	/// <param name="maxVersion">最大版本（包含）</param>
	/// <returns>是否在范围内</returns>
	public bool IsInRange(Version minVersion, Version maxVersion)
	{
		return this >= minVersion && this <= maxVersion;
	}
}
