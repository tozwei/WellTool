using System;
using System.Collections.Generic;
using System.Text;

namespace WellTool.Core.Text.Replacer
{
    /// <summary>
    /// 查找替换器接口
    /// </summary>
    public interface IReplacer
    {
        /// <summary>
        /// 替换
        /// </summary>
        string Replace(string text);
    }

    /// <summary>
    /// 查找替换器
    /// </summary>
    public class LookupReplacer : IReplacer
    {
        private readonly Dictionary<string, string> _replacements;

        public LookupReplacer()
        {
            _replacements = new Dictionary<string, string>();
        }

        public LookupReplacer(Dictionary<string, string> replacements)
        {
            _replacements = new Dictionary<string, string>(replacements);
        }

        public void Add(string oldValue, string newValue)
        {
            _replacements[oldValue] = newValue;
        }

        public void Remove(string oldValue)
        {
            _replacements.Remove(oldValue);
        }

        public string Replace(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;

            var sb = new StringBuilder(text);
            foreach (var kvp in _replacements)
            {
                sb.Replace(kvp.Key, kvp.Value);
            }
            return sb.ToString();
        }
    }

    /// <summary>
    /// 替换器链
    /// </summary>
    public class ReplacerChain : IReplacer
    {
        private readonly List<IReplacer> _replacers = new List<IReplacer>();

        public void Add(IReplacer replacer)
        {
            _replacers.Add(replacer);
        }

        public void Add(string oldValue, string newValue)
        {
            _replacers.Add(new LookupReplacer(new Dictionary<string, string> { { oldValue, newValue } }));
        }

        public string Replace(string text)
        {
            foreach (var replacer in _replacers)
            {
                text = replacer.Replace(text);
            }
            return text;
        }

        public int Count => _replacers.Count;
    }
}
