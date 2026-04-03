using Xunit;
using WellTool.Core;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Page工具单元测试
    /// </summary>
    public class PageUtilTest
    {
        [Fact]
        public void TotalPageTest()
        {
            Assert.Equal(10, PageUtil.TotalPage(100, 10));
            Assert.Equal(11, PageUtil.TotalPage(101, 10));
            Assert.Equal(1, PageUtil.TotalPage(0, 10));
        }

        [Fact]
        public void TotalPageByStartEndTest()
        {
            Assert.Equal(10, PageUtil.TotalPage(1, 100, 10));
            Assert.Equal(11, PageUtil.TotalPage(1, 101, 10));
        }

        [Fact]
        public void ToPageFirstResultTest()
        {
            Assert.Equal(0, PageUtil.ToPageFirstResult(1, 10));
            Assert.Equal(10, PageUtil.ToPageFirstResult(2, 10));
            Assert.Equal(20, PageUtil.ToPageFirstResult(3, 10));
        }

        [Fact]
        public void ToPageFirstResultZeroStartTest()
        {
            Assert.Equal(0, PageUtil.ToPageFirstResult(1, 10, true));
            Assert.Equal(1, PageUtil.ToPageFirstResult(1, 10, false));
        }

        [Fact]
        public void ToPageInfoTest()
        {
            var pageInfo = PageUtil.ToPageInfo(0, 10);
            Assert.Equal(0, pageInfo.PageNum);
            Assert.Equal(10, pageInfo.PageSize);
            Assert.Equal(0, pageInfo.FirstResult);
        }

        [Fact]
        public void ToRainbowSatrTest()
        {
            var rainbow = PageUtil.ToRainbowSatr(1, 10, 5);
            Assert.Equal(5, rainbow.Length);
        }

        [Fact]
        public void ToSqlPageTest()
        {
            var sql = PageUtil.ToSqlPage("SELECT * FROM table", 10, 20);
            Assert.Contains("LIMIT", sql.ToUpper());
            Assert.Contains("OFFSET", sql.ToUpper());
        }

        [Fact]
        public void TransToStartEndTest()
        {
            var result = PageUtil.TransToStartEnd(2, 10);
            Assert.Equal(10, result[0]);
            Assert.Equal(20, result[1]);
        }

        [Fact]
        public void RainbowTest()
        {
            var rainbow = PageUtil.Rainbow(1, 10, 5);
            Assert.Equal(5, rainbow.Length);
        }

        [Fact]
        public voidrainbowStartEndTest()
        {
            var rainbow = PageUtil.Rainbow(1, 100, 3, 7, 5);
            Assert.Equal(5, rainbow.Length);
        }

        [Fact]
        public void FixPageTest()
        {
            Assert.Equal(1, PageUtil.FixPage(0, 10));
            Assert.Equal(1, PageUtil.FixPage(-1, 10));
            Assert.Equal(5, PageUtil.FixPage(5, 10));
        }
    }
}
