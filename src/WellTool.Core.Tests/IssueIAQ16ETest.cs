using Xunit;
using WellTool.Core;
using WellTool.Core.Util;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Issue #IAQ16E 测试
    /// </summary>
    public class IssueIAQ16ETest
    {
        [Fact]
        public void LastIndexOfSubTest()
        {
            int[] bigBytes = new int[] { 1, 2, 2, 2, 3, 2, 2, 2, 3 };
            int[] subBytes = new int[] { 2, 2 };
            var i = ArrayUtil.LastIndexOfSub(bigBytes, subBytes);
            Assert.Equal(6, i);
        }

        [Fact]
        public void LastIndexOfSubTest2()
        {
            int[] bigBytes = new int[] { 1, 2, 2, 2, 3, 2, 2, 2, 3, 4, 5 };
            int[] subBytes = new int[] { 2, 2, 2, 3 };
            var i = ArrayUtil.LastIndexOfSub(bigBytes, subBytes);
            Assert.Equal(5, i);
        }

        [Fact]
        public void LastIndexOfSubTest3()
        {
            int[] a = { 0x12, 0x34, 0x56, 0x78, 0x9A };
            int[] b = { 0x56, 0x78 };

            var i = ArrayUtil.LastIndexOfSub(a, b);
            Assert.Equal(2, i);
        }
    }
}
