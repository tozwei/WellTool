using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WellTool.Core.Text
{
    /// <summary>
    /// 字符串格式化工具类
    /// </summary>
    public static class StrFormatter
    {
        private static readonly Regex _parameterPattern = new Regex(@"\{(\d+)\}", RegexOptions.Compiled);
        private static readonly Regex _namedParameterPattern = new Regex(@"\{(\w+)\}", RegexOptions.Compiled);

        /// <summary>
        /// 格式化字符串，类似 string.Format
        /// </summary>
        /// <param name="template">模板字符串</param>
        /// <param name="args">参数</param>
        /// <returns>格式化后的字符串</returns>
        public static string Format(string template, params object[] args)
        {
            if (string.IsNullOrEmpty(template))
            {
                return template;
            }

            if (args == null || args.Length == 0)
            {
                return template;
            }

            // 直接使用 string.Format 来处理参数替换和双大括号转义
            return string.Format(template, args);
        }

        /// <summary>
        /// 格式化字符串，使用命名参数
        /// </summary>
        /// <param name="template">模板字符串</param>
        /// <param name="args">命名参数</param>
        /// <returns>格式化后的字符串</returns>
        public static string Format(string template, Dictionary<string, object> args)
        {
            if (string.IsNullOrEmpty(template))
            {
                return template;
            }

            if (args == null || args.Count == 0)
            {
                return template;
            }

            return _namedParameterPattern.Replace(template, match =>
            {
                var key = match.Groups[1].Value;
                if (args.TryGetValue(key, out var value))
                {
                    return value?.ToString() ?? "";
                }
                return match.Value;
            });
        }

        /// <summary>
        /// 格式化字符串，使用对象属性
        /// </summary>
        /// <param name="template">模板字符串</param>
        /// <param name="args">参数对象</param>
        /// <returns>格式化后的字符串</returns>
        public static string FormatWithObject(string template, object args)
        {
            if (string.IsNullOrEmpty(template))
            {
                return template;
            }

            if (args == null)
            {
                return template;
            }

            var type = args.GetType();
            return _namedParameterPattern.Replace(template, match =>
            {
                var key = match.Groups[1].Value;
                var prop = type.GetProperty(key);
                if (prop != null)
                {
                    var value = prop.GetValue(args);
                    return value?.ToString() ?? "";
                }
                return match.Value;
            });
        }

        /// <summary>
        /// 带默认值的格式化
        /// </summary>
        /// <param name="template">模板字符串，格式：{key:defaultValue}</param>
        /// <param name="args">参数</param>
        /// <returns>格式化后的字符串</returns>
        public static string FormatWithDefault(string template, Dictionary<string, object> args)
        {
            if (string.IsNullOrEmpty(template))
            {
                return template;
            }

            var pattern = new Regex(@"\{(\w+)(?::([^}]*))?\}", RegexOptions.Compiled);
            return pattern.Replace(template, match =>
            {
                var key = match.Groups[1].Value;
                var defaultValue = match.Groups[2].Success ? match.Groups[2].Value : "";

                if (args != null && args.TryGetValue(key, out var value) && value != null)
                {
                    return value.ToString();
                }
                return defaultValue;
            });
        }

        /// <summary>
        /// 安全格式化，避免参数不匹配时抛出异常
        /// </summary>
        public static string FormatSafe(string template, params object[] args)
        {
            try
            {
                return Format(template, args);
            }
            catch
            {
                return template;
            }
        }
    }
}
