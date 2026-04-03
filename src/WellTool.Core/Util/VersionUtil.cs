using System;
using System.Text.RegularExpressions;

namespace WellTool.Core.Util
{
    /// <summary>
    /// 版本比较工具类
    /// </summary>
    public static class VersionUtil
    {
        /// <summary>
        /// 比较两个版本号
        /// </summary>
        /// <returns>
        /// 小于0：v1 &lt; v2
        /// 等于0：v1 == v2
        /// 大于0：v1 &gt; v2
        /// </returns>
        public static int Compare(string version1, string version2)
        {
            var v1 = Parse(version1);
            var v2 = Parse(version2);
            return Compare(v1, v2);
        }

        /// <summary>
        /// 比较两个版本号
        /// </summary>
        public static int Compare(Version v1, Version v2)
        {
            if (v1.Major != v2.Major)
            {
                return v1.Major - v2.Major;
            }

            if (v1.Minor != v2.Minor)
            {
                return v1.Minor - v2.Minor;
            }

            if (v1.Build != v2.Build)
            {
                return v1.Build - v2.Build;
            }

            return v1.Revision - v2.Revision;
        }

        /// <summary>
        /// 检查v1是否大于v2
        /// </summary>
        public static bool IsGreater(string version1, string version2)
        {
            return Compare(version1, version2) > 0;
        }

        /// <summary>
        /// 检查v1是否小于v2
        /// </summary>
        public static bool IsLess(string version1, string version2)
        {
            return Compare(version1, version2) < 0;
        }

        /// <summary>
        /// 检查v1是否大于等于v2
        /// </summary>
        public static bool IsGreaterOrEqual(string version1, string version2)
        {
            return Compare(version1, version2) >= 0;
        }

        /// <summary>
        /// 检查v1是否小于等于v2
        /// </summary>
        public static bool IsLessOrEqual(string version1, string version2)
        {
            return Compare(version1, version2) <= 0;
        }

        /// <summary>
        /// 解析版本字符串
        /// </summary>
        public static Version Parse(string version)
        {
            if (string.IsNullOrEmpty(version))
            {
                return new Version(0, 0, 0, 0);
            }

            // 移除前缀如"v", "V"
            version = version.TrimStart('v', 'V');

            // 提取主版本号.次版本号.构建号.修订号
            var match = Regex.Match(version, @"^(\d+)(?:\.(\d+))?(?:\.(\d+))?(?:\.(\d+))?");
            if (!match.Success)
            {
                return new Version(0, 0, 0, 0);
            }

            int major = int.Parse(match.Groups[1].Value);
            int minor = match.Groups[2].Success ? int.Parse(match.Groups[2].Value) : 0;
            int build = match.Groups[3].Success ? int.Parse(match.Groups[3].Value) : 0;
            int revision = match.Groups[4].Success ? int.Parse(match.Groups[4].Value) : 0;

            return new Version(major, minor, build, revision);
        }

        /// <summary>
        /// 验证版本号格式是否正确
        /// </summary>
        public static bool IsValid(string version)
        {
            if (string.IsNullOrEmpty(version))
            {
                return false;
            }

            version = version.TrimStart('v', 'V');
            return Regex.IsMatch(version, @"^\d+(?:\.\d+){0,3}$");
        }

        /// <summary>
        /// 增加主版本号
        /// </summary>
        public static string IncreaseMajor(string version)
        {
            var v = Parse(version);
            return $"{v.Major + 1}.0.0.0";
        }

        /// <summary>
        /// 增加次版本号
        /// </summary>
        public static string IncreaseMinor(string version)
        {
            var v = Parse(version);
            return $"{v.Major}.{v.Minor + 1}.0.0";
        }

        /// <summary>
        /// 增加构建号
        /// </summary>
        public static string IncreaseBuild(string version)
        {
            var v = Parse(version);
            return $"{v.Major}.{v.Minor}.{v.Build + 1}.0";
        }

        /// <summary>
        /// 增加修订号
        /// </summary>
        public static string IncreaseRevision(string version)
        {
            var v = Parse(version);
            return $"{v.Major}.{v.Minor}.{v.Build}.{v.Revision + 1}";
        }
    }

    /// <summary>
    /// 版本号
    /// </summary>
    public class Version
    {
        public int Major { get; set; }
        public int Minor { get; set; }
        public int Build { get; set; }
        public int Revision { get; set; }

        public Version(int major, int minor = 0, int build = 0, int revision = 0)
        {
            Major = major;
            Minor = minor;
            Build = build;
            Revision = revision;
        }

        public override string ToString()
        {
            return $"{Major}.{Minor}.{Build}.{Revision}";
        }
    }
}
