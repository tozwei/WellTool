using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using WellTool.Core.Lang;

namespace WellTool.Core.Text
{
    /// <summary>
    /// Ant 风格的路径匹配器
    /// </summary>
    public class AntPathMatcher
    {
        /// <summary>
        /// 默认路径分隔符："/"
        /// </summary>
        public static readonly string DefaultPathSeparator = "/";

        private static readonly int CACHE_TURNOFF_THRESHOLD = 65536;
        private static readonly Regex VariablePattern = new Regex(@"\{[^/]+?\}", RegexOptions.Compiled);
        private static readonly char[] WildcardChars = { '*', '?', '{' };

        private string _pathSeparator;
        private bool _caseSensitive = true;
        private bool _trimTokens = false;
        private bool? _cachePatterns;

        private readonly ConcurrentDictionary<string, AntPathStringMatcher> _stringMatcherCache = new ConcurrentDictionary<string, AntPathStringMatcher>();
        private int _patternCacheSize;

        /// <summary>
        /// 是否缓存模式
        /// </summary>
        public bool CachePatterns
        {
            get => _cachePatterns ?? (_patternCacheSize < CACHE_TURNOFF_THRESHOLD);
            set => _cachePatterns = value;
        }

        /// <summary>
        /// 使用默认分隔符构造
        /// </summary>
        public AntPathMatcher() : this(DefaultPathSeparator)
        {
        }

        /// <summary>
        /// 使用自定义分隔符构造
        /// </summary>
        /// <param name="pathSeparator">分隔符</param>
        public AntPathMatcher(string pathSeparator)
        {
            SetPathSeparator(pathSeparator ?? DefaultPathSeparator);
        }

        /// <summary>
        /// 设置路径分隔符
        /// </summary>
        /// <param name="pathSeparator">分隔符</param>
        /// <returns>this</returns>
        public AntPathMatcher SetPathSeparator(string pathSeparator)
        {
            _pathSeparator = pathSeparator ?? DefaultPathSeparator;
            return this;
        }

        /// <summary>
        /// 是否区分大小写
        /// </summary>
        public bool CaseSensitive
        {
            get => _caseSensitive;
            set => _caseSensitive = value;
        }

        /// <summary>
        /// 是否修剪 token
        /// </summary>
        public bool TrimTokens
        {
            get => _trimTokens;
            set => _trimTokens = value;
        }

        /// <summary>
        /// 匹配路径
        /// </summary>
        /// <param name="pattern">模式</param>
        /// <param name="path">路径</param>
        /// <returns>是否匹配</returns>
        public bool Match(string pattern, string path)
        {
            return DoMatch(pattern, path);
        }

        /// <summary>
        /// 匹配并开始提取变量
        /// </summary>
        /// <param name="pattern">模式</param>
        /// <param name="path">路径</param>
        /// <returns>匹配的变量 Map，如果不匹配返回 null</returns>
        public IDictionary<string, string> MatchAndExtract(string pattern, string path)
        {
            var matcher = GetMatcher(pattern);
            return matcher.MatchAndExtract(path);
        }

        private bool DoMatch(string pattern, string path)
        {
            if (string.IsNullOrEmpty(pattern) || string.IsNullOrEmpty(path))
            {
                return false;
            }

            // 快速检查：如果 pattern 不包含通配符，直接比较
            if (!ContainsWildcard(pattern))
            {
                return Equals(pattern, path);
            }

            var matcher = GetMatcher(pattern);
            return matcher.Match(path);
        }

        private AntPathStringMatcher GetMatcher(string pattern)
        {
            if (CachePatterns)
            {
                return _stringMatcherCache.GetOrAdd(pattern, p => new AntPathStringMatcher(p, _caseSensitive, _pathSeparator));
            }
            return new AntPathStringMatcher(pattern, _caseSensitive, _pathSeparator);
        }

        private bool ContainsWildcard(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }
            return value.IndexOfAny(WildcardChars) >= 0;
        }

        private bool Equals(string str1, string str2)
        {
            return _caseSensitive ? str1 == str2 : string.Equals(str1, str2, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 内部字符串匹配器
        /// </summary>
        private class AntPathStringMatcher
        {
            private readonly string _pattern;
            private readonly bool _caseSensitive;
            private readonly string _pathSeparator;
            private readonly List<string> _patternVariables;

            public AntPathStringMatcher(string pattern, bool caseSensitive, string pathSeparator)
            {
                _pattern = pattern;
                _caseSensitive = caseSensitive;
                _pathSeparator = pathSeparator;
                _patternVariables = ExtractVariables(pattern);
            }

            private static List<string> ExtractVariables(string pattern)
            {
                var variables = new List<string>();
                var matches = VariablePattern.Matches(pattern);
                foreach (Match match in matches)
                {
                    var variable = match.Value;
                    // 提取变量名 {name:regex} -> name
                    var colonIndex = variable.IndexOf(':');
                    if (colonIndex > 0)
                    {
                        variable = variable.Substring(1, colonIndex - 1);
                    }
                    else
                    {
                        variable = variable.Trim('{', '}');
                    }
                    variables.Add(variable);
                }
                return variables;
            }

            public bool Match(string str)
            {
                if (string.IsNullOrEmpty(str))
                {
                    return false;
                }

                var regexPattern = BuildRegex(_pattern);
                var options = _caseSensitive ? RegexOptions.None : RegexOptions.IgnoreCase;
                var fullPattern = "^" + regexPattern + "$";
                try
                {
                    // debug logging to help test failures
                    System.Console.WriteLine($"AntPathMatcher DEBUG pattern='{_pattern}' regex='{fullPattern}' path='{str}'");
                    var isMatch = Regex.IsMatch(str, fullPattern, options);
                    System.Console.WriteLine($"AntPathMatcher DEBUG result={isMatch}");
                    return isMatch;
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine($"AntPathMatcher DEBUG error building regex: {ex}");
                    return false;
                }
            }

            public IDictionary<string, string> MatchAndExtract(string str)
            {
                if (string.IsNullOrEmpty(str))
                {
                    return null;
                }

                var regexPattern = BuildRegex(_pattern);
                var options = _caseSensitive ? RegexOptions.None : RegexOptions.IgnoreCase;
                var match = Regex.Match(str, "^" + regexPattern + "$", options);

                if (!match.Success)
                {
                    return null;
                }

                var result = new Dictionary<string, string>();
                for (int i = 0; i < _patternVariables.Count && i < match.Groups.Count - 1; i++)
                {
                    result[_patternVariables[i]] = match.Groups[i + 1].Value;
                }

                return result;
            }

            private string BuildRegex(string pattern)
            {
                var separator = Regex.Escape(_pathSeparator);
                var regexBuilder = new System.Text.StringBuilder();
                var length = pattern.Length;
                var i = 0;

                // 特殊处理以 /** 开头的模式
                if (pattern.StartsWith("/**"))
                {
                    // 对于 /** 开头的模式，直接匹配任意路径
                    regexBuilder.Append(".*");
                    i += 3; // 跳过 /**
                }

                while (i < length)
                {
                    var currentChar = pattern[i];
                    if (currentChar == '*')
                    {
                        if (i + 1 < length && pattern[i + 1] == '*')
                        {
                            // ** 通配符
                            if (i + 2 == length)
                            {
                                // 以 ** 结尾
                                regexBuilder.Append($"(?:{separator}.*)*");
                                i += 2;
                            }
                            else
                            {
                                // 中间的 **
                                regexBuilder.Append($"(?:{separator}.*)*");
                                i += 2;
                                // 跳过后面的分隔符
                                if (i < length && pattern[i] == _pathSeparator[0])
                                {
                                    i++;
                                }
                            }
                        }
                        else
                        {
                            // * 通配符
                            regexBuilder.Append($"[^{separator}]*");
                            i++;
                        }
                    }
                    else if (currentChar == '?')
                    {
                        // ? 通配符
                        regexBuilder.Append(".");
                        i++;
                    }
                    else if (currentChar == '{')
                    {
                        // 变量
                        var endIndex = pattern.IndexOf('}', i);
                        if (endIndex > i)
                        {
                            var variable = pattern.Substring(i, endIndex - i + 1);
                            var colonIndex = variable.IndexOf(':');
                            if (colonIndex > 0)
                            {
                                var regexPart = variable.Substring(colonIndex + 1, variable.Length - colonIndex - 2);
                                regexBuilder.Append($"({regexPart})");
                            }
                            else
                            {
                                regexBuilder.Append($"([^{separator}]+)");
                            }
                            i = endIndex + 1;
                        }
                        else
                        {
                            regexBuilder.Append(Regex.Escape(currentChar.ToString()));
                            i++;
                        }
                    }
                    else
                    {
                        // 普通字符
                        regexBuilder.Append(Regex.Escape(currentChar.ToString()));
                        i++;
                    }
                }

                return regexBuilder.ToString();
            }
        }
    }
}