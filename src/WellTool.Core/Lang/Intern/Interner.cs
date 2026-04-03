using System;
using System.Collections.Concurrent;

namespace WellTool.Core.Lang.Intern
{
    /// <summary>
    /// 字符串驻留器，用于减少重复字符串的内存占用
    /// </summary>
    public class StringInterner
    {
        private readonly ConcurrentDictionary<string, string> _interned = new ConcurrentDictionary<string, string>();

        /// <summary>
        /// 获取驻留的字符串
        /// </summary>
        public string Intern(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            return _interned.GetOrAdd(str, s => s);
        }

        /// <summary>
        /// 清除驻留缓存
        /// </summary>
        public void Clear()
        {
            _interned.Clear();
        }

        /// <summary>
        /// 获取驻留字符串数量
        /// </summary>
        public int Count => _interned.Count;
    }

    /// <summary>
    /// 全局字符串驻留器
    /// </summary>
    public static class GlobalStringInterner
    {
        private static readonly StringInterner _interner = new StringInterner();

        /// <summary>
        /// 获取驻留的字符串
        /// </summary>
        public static string Intern(string str)
        {
            return _interner.Intern(str);
        }

        /// <summary>
        /// 清除驻留缓存
        /// </summary>
        public static void Clear()
        {
            _interner.Clear();
        }
    }
}
