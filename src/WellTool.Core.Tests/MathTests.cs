using System;
using System.Globalization;
using Xunit;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// 数学工具单元测试
    /// </summary>
    public class MathTests
    {
        #region 基础数学测试

        [Fact]
        public void BasicMathTest()
        {
            // 测试基本数学运算
            Assert.Equal(4, 2 + 2);
            Assert.Equal(2, 4 - 2);
            Assert.Equal(6, 2 * 3);
            Assert.Equal(2, 6 / 3);
            Assert.Equal(1, 5 % 2);
        }

        [Fact]
        public void MathUtilTest()
        {
            // 测试MathUtil类的基本方法
            // 测试Min方法
            Assert.Equal(2, WellTool.Core.MathUtil.Min(2, 3));
            Assert.Equal("a", WellTool.Core.MathUtil.Min("a", "b"));
            Assert.Equal(1.5, WellTool.Core.MathUtil.Min(1.5, 2.5));

            // 测试Max方法
            Assert.Equal(3, WellTool.Core.MathUtil.Max(2, 3));
            Assert.Equal("b", WellTool.Core.MathUtil.Max("a", "b"));
            Assert.Equal(2.5, WellTool.Core.MathUtil.Max(1.5, 2.5));

            // 测试Abs方法
            Assert.Equal(5, WellTool.Core.MathUtil.Abs(-5));
            Assert.Equal(9, WellTool.Core.MathUtil.Abs(9));
            Assert.Equal(10L, WellTool.Core.MathUtil.Abs(-10L));
            Assert.Equal(3.14, WellTool.Core.MathUtil.Abs(-3.14));

            // 测试Pow方法
            Assert.Equal(8, WellTool.Core.MathUtil.Pow(2, 3));
            Assert.Equal(9, WellTool.Core.MathUtil.Pow(3, 2));
            Assert.Equal(1, WellTool.Core.MathUtil.Pow(5, 0));
        }

        #endregion
    }
}