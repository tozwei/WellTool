// Copyright (c) 2025 WellTool Team
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace WellTool.Core.Text;

/// <summary>
/// 字符串匹配器<br>
/// 用于匹配字符串中的特定内容
/// </summary>
public abstract class StrMatcher
{
    /// <summary>
    /// 匹配字符串
    /// </summary>
    /// <param name="text">要匹配的文本</param>
    /// <param name="startIndex">开始索引</param>
    /// <returns>匹配结果，-1表示未匹配到</returns>
    public abstract int Match(string text, int startIndex);

    /// <summary>
    /// 获取匹配器的长度
    /// </summary>
    /// <returns>匹配器的长度</returns>
    public abstract int Length { get; }

    /// <summary>
    /// 获取匹配的字符串
    /// </summary>
    /// <returns>匹配的字符串</returns>
    public abstract string Matcher { get; }

    /// <summary>
    /// 创建字符串匹配器
    /// </summary>
    /// <param name="matcher">匹配的字符串</param>
    /// <returns>字符串匹配器</returns>
    public static StrMatcher Of(string matcher)
    {
        return new StringStrMatcher(matcher);
    }

    /// <summary>
    /// 创建字符匹配器
    /// </summary>
    /// <param name="matcher">匹配的字符</param>
    /// <returns>字符匹配器</returns>
    public static StrMatcher Of(char matcher)
    {
        return new CharStrMatcher(matcher);
    }

    /// <summary>
    /// 字符串匹配器
    /// </summary>
    private class StringStrMatcher : StrMatcher
    {
        private readonly string _matcher;

        public StringStrMatcher(string matcher)
        {
            _matcher = matcher;
        }

        public override int Match(string text, int startIndex)
        {
            if (string.IsNullOrEmpty(text) || startIndex < 0 || startIndex > text.Length - Length)
            {
                return -1;
            }

            return text.IndexOf(_matcher, startIndex, StringComparison.Ordinal);
        }

        public override int Length => _matcher.Length;

        public override string Matcher => _matcher;
    }

    /// <summary>
    /// 字符匹配器
    /// </summary>
    private class CharStrMatcher : StrMatcher
    {
        private readonly char _matcher;

        public CharStrMatcher(char matcher)
        {
            _matcher = matcher;
        }

        public override int Match(string text, int startIndex)
        {
            if (string.IsNullOrEmpty(text) || startIndex < 0 || startIndex >= text.Length)
            {
                return -1;
            }

            return text.IndexOf(_matcher, startIndex);
        }

        public override int Length => 1;

        public override string Matcher => _matcher.ToString();
    }
}
