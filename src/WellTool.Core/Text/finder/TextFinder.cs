using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using WellTool.Core.Lang;

namespace WellTool.Core.Text.Finder
{
    /// <summary>
    /// 字符串查找器接口
    /// </summary>
    public interface ITextFinder
    {
        /// <summary>
        /// 查找所有匹配的位置
        /// </summary>
        List<int> FindAll(string text);

        /// <summary>
        /// 查找第一个匹配的位置
        /// </summary>
        int FindFirst(string text);

        /// <summary>
        /// 是否包含匹配
        /// </summary>
        bool Contains(string text);
    }

    /// <summary>
    /// 字符查找器
    /// </summary>
    public class CharFinder : ITextFinder
    {
        private readonly char _target;

        public CharFinder(char target)
        {
            _target = target;
        }

        public List<int> FindAll(string text)
        {
            var result = new List<int>();
            if (string.IsNullOrEmpty(text)) return result;

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == _target)
                {
                    result.Add(i);
                }
            }
            return result;
        }

        public int FindFirst(string text)
        {
            if (string.IsNullOrEmpty(text)) return -1;
            return text.IndexOf(_target);
        }

        public bool Contains(string text)
        {
            return FindFirst(text) >= 0;
        }
    }

    /// <summary>
    /// 字符串查找器
    /// </summary>
    public class StrFinder : ITextFinder
    {
        private readonly string _target;

        public StrFinder(string target)
        {
            _target = target ?? throw new ArgumentNullException(nameof(target));
        }

        public List<int> FindAll(string text)
        {
            var result = new List<int>();
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(_target)) return result;

            int index = 0;
            while ((index = text.IndexOf(_target, index, StringComparison.Ordinal)) >= 0)
            {
                result.Add(index);
                index += _target.Length;
            }
            return result;
        }

        public int FindFirst(string text)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(_target)) return -1;
            return text.IndexOf(_target, StringComparison.Ordinal);
        }

        public bool Contains(string text)
        {
            return FindFirst(text) >= 0;
        }
    }

    /// <summary>
    /// 正则查找器
    /// </summary>
    public class PatternFinder : ITextFinder
    {
        private readonly Regex _pattern;

        public PatternFinder(string pattern)
        {
            _pattern = new Regex(pattern);
        }

        public PatternFinder(Regex pattern)
        {
            _pattern = pattern;
        }

        public List<int> FindAll(string text)
        {
            var result = new List<int>();
            if (string.IsNullOrEmpty(text)) return result;

            var matches = _pattern.Matches(text);
            foreach (Match match in matches)
            {
                result.Add(match.Index);
            }
            return result;
        }

        public int FindFirst(string text)
        {
            if (string.IsNullOrEmpty(text)) return -1;

            var match = _pattern.Match(text);
            return match.Success ? match.Index : -1;
        }

        public bool Contains(string text)
        {
            return FindFirst(text) >= 0;
        }
    }

    /// <summary>
    /// 长度查找器，查找指定长度的位置
    /// </summary>
    public class LengthFinder : ITextFinder
    {
        private readonly int _length;

        public LengthFinder(int length)
        {
            if (length <= 0) throw new ArgumentException("Length must be greater than 0");
            _length = length;
        }

        public List<int> FindAll(string text)
        {
            var result = new List<int>();
            if (string.IsNullOrEmpty(text)) return result;

            for (int i = 0; i <= text.Length - _length; i++)
            {
                result.Add(i);
            }
            return result;
        }

        public int FindFirst(string text)
        {
            return !string.IsNullOrEmpty(text) && text.Length >= _length ? 0 : -1;
        }

        public bool Contains(string text)
        {
            return FindFirst(text) >= 0;
        }
    }

    /// <summary>
    /// 字符匹配查找器，查找满足指定匹配器匹配的字符所在位置
    /// </summary>
    public class CharMatcherFinder : ITextFinder
    {
        private readonly Matcher<char> _matcher;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="matcher">被查找的字符匹配器</param>
        public CharMatcherFinder(Matcher<char> matcher)
        {
            _matcher = matcher ?? throw new ArgumentNullException(nameof(matcher));
        }

        public List<int> FindAll(string text)
        {
            var result = new List<int>();
            if (string.IsNullOrEmpty(text)) return result;

            for (int i = 0; i < text.Length; i++)
            {
                if (_matcher.Match(text[i]))
                {
                    result.Add(i);
                }
            }
            return result;
        }

        public int FindFirst(string text)
        {
            if (string.IsNullOrEmpty(text)) return -1;

            for (int i = 0; i < text.Length; i++)
            {
                if (_matcher.Match(text[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        public bool Contains(string text)
        {
            return FindFirst(text) >= 0;
        }
    }
}
