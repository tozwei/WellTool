using WellTool.Core.Util;
using Xunit;

namespace WellTool.Core.Tests
{
    public class BooleanUtilTest
    {
        [Fact]
        public void ToBooleanTest()
        {
            Assert.True(WellTool.Core.Util.BooleanUtil.ToBoolean("true"));
            Assert.True(WellTool.Core.Util.BooleanUtil.ToBoolean("yes"));
            Assert.True(WellTool.Core.Util.BooleanUtil.ToBoolean("y"));
            Assert.True(WellTool.Core.Util.BooleanUtil.ToBoolean("t"));
            Assert.True(WellTool.Core.Util.BooleanUtil.ToBoolean("OK"));
            Assert.True(WellTool.Core.Util.BooleanUtil.ToBoolean("1"));
            Assert.True(WellTool.Core.Util.BooleanUtil.ToBoolean("是"));
            Assert.True(WellTool.Core.Util.BooleanUtil.ToBoolean("对"));

            Assert.False(WellTool.Core.Util.BooleanUtil.ToBoolean("false"));
            Assert.False(WellTool.Core.Util.BooleanUtil.ToBoolean("no"));
            Assert.False(WellTool.Core.Util.BooleanUtil.ToBoolean("n"));
            Assert.False(WellTool.Core.Util.BooleanUtil.ToBoolean("f"));
            Assert.False(WellTool.Core.Util.BooleanUtil.ToBoolean("0"));
            Assert.False(WellTool.Core.Util.BooleanUtil.ToBoolean(""));
        }

        [Fact]
        public void NegateTest()
        {
            Assert.False(WellTool.Core.Util.BooleanUtil.Negate(true));
            Assert.True(WellTool.Core.Util.BooleanUtil.Negate(false));
        }

        [Fact]
        public void ToStringTest()
        {
            Assert.Equal("True", WellTool.Core.Util.BooleanUtil.ToString(true));
            Assert.Equal("False", WellTool.Core.Util.BooleanUtil.ToString(false));
        }

        [Fact]
        public void ToIntTest()
        {
            Assert.Equal(1, WellTool.Core.Util.BooleanUtil.ToInt(true));
            Assert.Equal(0, WellTool.Core.Util.BooleanUtil.ToInt(false));
        }

        [Fact]
        public void ToBooleanIntTest()
        {
            Assert.True(WellTool.Core.Util.BooleanUtil.ToBoolean(1));
            Assert.False(WellTool.Core.Util.BooleanUtil.ToBoolean(0));
        }

        [Fact]
        public void IsTrueIsFalseTest()
        {
            Assert.True(WellTool.Core.Util.BooleanUtil.IsTrue("true"));
            Assert.True(WellTool.Core.Util.BooleanUtil.IsTrue("1"));
            Assert.False(WellTool.Core.Util.BooleanUtil.IsTrue("false"));
            Assert.False(WellTool.Core.Util.BooleanUtil.IsTrue("0"));
            Assert.False(WellTool.Core.Util.BooleanUtil.IsTrue(null));
        }
    }
}
