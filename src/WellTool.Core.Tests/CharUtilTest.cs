using Xunit;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Char工具单元测试
    /// </summary>
    public class CharUtilTest
    {
        [Fact]
        public void IsCharTest()
        {
            Assert.True(WellTool.Core.CharUtil.IsChar('a'));
            Assert.True(WellTool.Core.CharUtil.IsChar('Z'));
            Assert.False(WellTool.Core.CharUtil.IsChar('1'));
            Assert.False(WellTool.Core.CharUtil.IsChar(' '));
        }

        [Fact]
        public void IsBlankCharTest()
        {
            Assert.True(WellTool.Core.CharUtil.IsBlankChar('\u00A0')); // 不间断空格
            Assert.True(WellTool.Core.CharUtil.IsBlankChar('\u0020')); // 普通空格
            Assert.True(WellTool.Core.CharUtil.IsBlankChar('\u3000')); // 全角空格
            Assert.True(WellTool.Core.CharUtil.IsBlankChar('\u0000')); // null字符
            Assert.False(WellTool.Core.CharUtil.IsBlankChar('a'));
            Assert.False(WellTool.Core.CharUtil.IsBlankChar('1'));
        }

        [Fact]
        public void IsEmojiTest()
        {
            // 莉🌹 - 第0个字符不是emoji，第1个是
            var a = "莉🌹";
            Assert.False(WellTool.Core.CharUtil.IsEmoji(a[0]));
            Assert.True(WellTool.Core.CharUtil.IsEmoji(a[1]));
        }

        [Fact]
        public void IsLetterTest()
        {
            Assert.True(WellTool.Core.CharUtil.IsLetter('a'));
            Assert.True(WellTool.Core.CharUtil.IsLetter('Z'));
            Assert.False(WellTool.Core.CharUtil.IsLetter('1'));
            Assert.False(WellTool.Core.CharUtil.IsLetter(' '));
        }

        [Fact]
        public void IsLowerCaseTest()
        {
            Assert.True(WellTool.Core.CharUtil.IsLowerCase('a'));
            Assert.False(WellTool.Core.CharUtil.IsLowerCase('A'));
            Assert.False(WellTool.Core.CharUtil.IsLowerCase('1'));
        }

        [Fact]
        public void IsUpperCaseTest()
        {
            Assert.True(WellTool.Core.CharUtil.IsUpperCase('A'));
            Assert.False(WellTool.Core.CharUtil.IsUpperCase('a'));
            Assert.False(WellTool.Core.CharUtil.IsUpperCase('1'));
        }

        [Fact]
        public void IsDigitTest()
        {
            Assert.True(WellTool.Core.CharUtil.IsDigit('0'));
            Assert.True(WellTool.Core.CharUtil.IsDigit('9'));
            Assert.False(WellTool.Core.CharUtil.IsDigit('a'));
            Assert.False(WellTool.Core.CharUtil.IsDigit(' '));
        }

        [Fact]
        public void ToIntTest()
        {
            Assert.Equal(1, WellTool.Core.CharUtil.ToInt('1'));
            Assert.Equal(9, WellTool.Core.CharUtil.ToInt('9'));
        }

        [Fact]
        public void ToStringTest()
        {
            Assert.Equal("a", WellTool.Core.CharUtil.ToString('a'));
        }
    }
}
