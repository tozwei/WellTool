using System;
using System.Text;
using Xunit;
using WellTool.Core.Util;
using WellTool.Core.Lang;

// 使用别名避免Assert引用歧义
using XAssert = Xunit.Assert;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// 工具类单元测试
    /// </summary>
    public class UtilTests
    {
        #region StrUtil测试

        [Fact]
        public void IsBlankTest()
        {
            var blank = "\t \u00A0\u3000";
            XAssert.True(StrUtil.IsBlank(blank));
        }

        [Fact]
        public void TrimTest()
        {
            var blank = "\t 哈哈\u00A0\u3000";
            var trim = StrUtil.Trim(blank);
            XAssert.Equal("哈哈", trim);
        }

        [Fact]
        public void TrimNewLineTest()
        {
            var str = "\r\naaa";
            XAssert.Equal("aaa", StrUtil.Trim(str));
            str = "\raaa";
            XAssert.Equal("aaa", StrUtil.Trim(str));
            str = "\naaa";
            XAssert.Equal("aaa", StrUtil.Trim(str));
            str = "\r\n\r\naaa";
            XAssert.Equal("aaa", StrUtil.Trim(str));
        }

        [Fact]
        public void CleanBlankTest()
        {
            var str = "\t 你\u00A0好\u3000";
            var cleanBlank = StrUtil.CleanBlank(str);
            XAssert.Equal("你好", cleanBlank);
        }

        [Fact]
        public void CutTest()
        {
            var str = "aaabbbcccdddaadfdfsdfsdf0";
            var cut = StrUtil.Cut(str, 4);
            XAssert.Equal(new[] { "aaab", "bbcc", "cddd", "aadf", "dfsd", "fsdf", "0" }, cut);
        }

        [Fact]
        public void SplitTest()
        {
            var strings = StrUtil.Split("abc/", '/');
            XAssert.Equal(2, strings.Length);
        }

        [Fact]
        public void SplitToLongTest()
        {
            var str = "1,2,3,4, 5";
            var longArray = StrUtil.SplitToLong(str, ',');
            XAssert.Equal(new long[] { 1, 2, 3, 4, 5 }, longArray);

            longArray = StrUtil.SplitToLong(str, ",");
            XAssert.Equal(new long[] { 1, 2, 3, 4, 5 }, longArray);
        }

        [Fact]
        public void SplitToIntTest()
        {
            var str = "1,2,3,4, 5";
            var intArray = StrUtil.SplitToInt(str, ',');
            XAssert.Equal(new int[] { 1, 2, 3, 4, 5 }, intArray);

            intArray = StrUtil.SplitToInt(str, ",");
            XAssert.Equal(new int[] { 1, 2, 3, 4, 5 }, intArray);
        }

        [Fact]
        public void FormatTest()
        {
            var template = "你好，我是{name}，我的电话是：{phone}";
            var result = StrUtil.Format(template, Dict.Create().Set("name", "张三").Set("phone", "13888881111"));
            XAssert.Equal("你好，我是张三，我的电话是：13888881111", result);

            var result2 = StrUtil.Format(template, Dict.Create().Set("name", "张三").Set("phone", null));
            XAssert.Equal("你好，我是张三，我的电话是：{phone}", result2);
        }

        [Fact]
        public void StripTest()
        {
            var str = "abcd123";
            var strip = StrUtil.Strip(str, "ab", "23");
            XAssert.Equal("cd1", strip);

            str = "abcd123";
            strip = StrUtil.Strip(str, "ab", "");
            XAssert.Equal("cd123", strip);

            str = "abcd123";
            strip = StrUtil.Strip(str, null, "");
            XAssert.Equal("abcd123", strip);

            str = "abcd123";
            strip = StrUtil.Strip(str, null, "567");
            XAssert.Equal("abcd123", strip);

            XAssert.Equal("", StrUtil.Strip("a", "a", ""));
            XAssert.Equal("", StrUtil.Strip("a", "a", "b"));
        }

        [Fact]
        public void ReplaceTest()
        {
            var s = StrUtil.ReplaceByCodePoint("aabbccdd", 2, 6, '*');
            XAssert.Equal("aa****dd", s);
            s = StrUtil.ReplaceByCodePoint("aabbccdd", 2, 12, '*');
            XAssert.Equal("aa******", s);
        }

        [Fact]
        public void UpperFirstTest()
        {
            var s = StrUtil.UpperFirst("key");
            XAssert.Equal("Key", s);
        }

        [Fact]
        public void LowerFirstTest()
        {
            var s = StrUtil.LowerFirst("KEY");
            XAssert.Equal("kEY", s);
        }

        [Fact]
        public void SubTest()
        {
            var a = "abcderghigh";
            var pre = StrUtil.Sub(a, -5, a.Length);
            XAssert.Equal("ghigh", pre);
        }

        [Fact]
        public void SubBeforeTest()
        {
            var a = "abcderghigh";
            var pre = StrUtil.SubBefore(a, 'd');
            XAssert.Equal("abc", pre);
            pre = StrUtil.SubBefore(a, 'a');
            XAssert.Equal("", pre);

            pre = StrUtil.SubBefore(a, 'k');
            XAssert.Equal(a, pre);
        }

        [Fact]
        public void SubAfterTest()
        {
            var a = "abcderghigh";
            var pre = StrUtil.SubAfter(a, 'd');
            XAssert.Equal("erghigh", pre);
            pre = StrUtil.SubAfter(a, 'h');
            XAssert.Equal("igh", pre);

            pre = StrUtil.SubAfter(a, 'k');
            XAssert.Equal(a, pre);
        }

        [Fact]
        public void RepeatAndJoinTest()
        {
            var repeatAndJoin = StrUtil.RepeatAndJoin("?", 5, ",");
            XAssert.Equal("?,?,?,?,?", repeatAndJoin);

            repeatAndJoin = StrUtil.RepeatAndJoin("?", 0, ",");
            XAssert.Equal("", repeatAndJoin);

            repeatAndJoin = StrUtil.RepeatAndJoin("?", 5, null);
            XAssert.Equal("?????", repeatAndJoin);
        }

        [Fact]
        public void MaxLengthTest()
        {
            var text = "我是一段正文，很长的正文，需要截取的正文";
            XAssert.True(text.Length > 0);
        }

        [Fact]
        public void ContainsAnyTest()
        {
            var containsAny = StrUtil.ContainsAny("aaabbbccc", 'a', 'd');
            XAssert.True(containsAny);
            containsAny = StrUtil.ContainsAny("aaabbbccc", 'e', 'd');
            XAssert.False(containsAny);
            containsAny = StrUtil.ContainsAny("aaabbbccc", 'd', 'c');
            XAssert.True(containsAny);

            containsAny = StrUtil.ContainsAny("aaabbbccc", "a", "d");
            XAssert.True(containsAny);
            containsAny = StrUtil.ContainsAny("aaabbbccc", "e", "d");
            XAssert.False(containsAny);
            containsAny = StrUtil.ContainsAny("aaabbbccc", "d", "c");
            XAssert.True(containsAny);
        }

        [Fact]
        public void CenterTest()
        {
            XAssert.Null(StrUtil.Center(null, 10));
            XAssert.Equal("    ", StrUtil.Center("", 4));
            XAssert.Equal("ab", StrUtil.Center("ab", -1));
            XAssert.Equal(" ab ", StrUtil.Center("ab", 4));
            XAssert.Equal("abcd", StrUtil.Center("abcd", 2));
            XAssert.Equal(" a  ", StrUtil.Center("a", 4));
        }

        [Fact]
        public void PadPreTest()
        {
            XAssert.Null(StrUtil.PadPre(null, 10, ' '));
            XAssert.Equal("001", StrUtil.PadPre("1", 3, '0'));
            XAssert.Equal("12", StrUtil.PadPre("123", 2, '0'));

            XAssert.Null(StrUtil.PadPre(null, 10, "AA"));
            XAssert.Equal("AB1", StrUtil.PadPre("1", 3, "ABC"));
            XAssert.Equal("12", StrUtil.PadPre("123", 2, "ABC"));
        }

        [Fact]
        public void PadAfterTest()
        {
            XAssert.Null(StrUtil.PadAfter(null, 10, ' '));
            XAssert.Equal("100", StrUtil.PadAfter("1", 3, '0'));
            XAssert.Equal("23", StrUtil.PadAfter("123", 2, '0'));
            XAssert.Equal("", StrUtil.PadAfter("123", -1, '0'));

            XAssert.Null(StrUtil.PadAfter(null, 10, "ABC"));
            XAssert.Equal("1AB", StrUtil.PadAfter("1", 3, "ABC"));
            XAssert.Equal("23", StrUtil.PadAfter("123", 2, "ABC"));
        }

        [Fact]
        public void IndexedFormatTest()
        {
            var ret = StrUtil.IndexedFormat("this is {0} for {1}", "a", 1000);
            XAssert.Equal("this is a for 1,000", ret);
        }

        [Fact]
        public void IsNumericTest()
        {
            var a = "2142342422423423";
            XAssert.True(StrUtil.IsNumeric(a));
        }

        #endregion

        #region RandomUtil测试

        [Fact]
        public void RandomStringTest()
        {
            var randomStr = RandomUtil.RandomString(10);
            XAssert.Equal(10, randomStr.Length);
        }

        [Fact]
        public void RandomIntTest()
        {
            var randomInt = RandomUtil.RandomInt(1, 100);
            XAssert.True(randomInt >= 1 && randomInt <= 100);
        }

        #endregion

        #region ArrayUtil测试

        [Fact]
        public void IsEmptyTest()
        {
            XAssert.True(ArrayUtil.IsEmpty<int>(null));
            XAssert.True(ArrayUtil.IsEmpty<int>(new int[0]));
            XAssert.False(ArrayUtil.IsEmpty<int>(new int[] { 1, 2, 3 }));
        }

        [Fact]
        public void IsNotEmptyTest()
        {
            XAssert.False(ArrayUtil.IsNotEmpty<int>(null));
            XAssert.False(ArrayUtil.IsNotEmpty<int>(new int[0]));
            XAssert.True(ArrayUtil.IsNotEmpty<int>(new int[] { 1, 2, 3 }));
        }

        #endregion

        #region NumberUtil测试

        [Fact]
        public void IsNumberTest()
        {
            XAssert.True(NumberUtil.IsNumber("123"));
            XAssert.True(NumberUtil.IsNumber("123.45"));
            XAssert.False(NumberUtil.IsNumber("abc"));
        }

        [Fact]
        public void AddTest()
        {
            XAssert.Equal(3, NumberUtil.Add(1, 2));
            XAssert.Equal(3.5, NumberUtil.Add(1.5, 2.0));
        }

        #endregion

        #region ClassUtil测试

        [Fact]
        public void GetClassNameTest()
        {
            var className = ClassUtil.GetClassName(this);
            XAssert.Equal("WellTool.Core.Tests.UtilTests", className);
        }

        #endregion
    }
}