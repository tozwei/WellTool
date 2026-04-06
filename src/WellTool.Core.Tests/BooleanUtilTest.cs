using Xunit;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Boolean工具单元测试
    /// </summary>
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
            Assert.True(WellTool.Core.Util.BooleanUtil.ToBoolean("correct"));
            Assert.True(WellTool.Core.Util.BooleanUtil.ToBoolean("success"));
            Assert.True(WellTool.Core.Util.BooleanUtil.ToBoolean("1"));
            Assert.True(WellTool.Core.Util.BooleanUtil.ToBoolean("On"));
            Assert.True(WellTool.Core.Util.BooleanUtil.ToBoolean("是"));
            Assert.True(WellTool.Core.Util.BooleanUtil.ToBoolean("对"));
            Assert.True(WellTool.Core.Util.BooleanUtil.ToBoolean("真"));

            Assert.False(WellTool.Core.Util.BooleanUtil.ToBoolean("false"));
            Assert.False(WellTool.Core.Util.BooleanUtil.ToBoolean("no"));
            Assert.False(WellTool.Core.Util.BooleanUtil.ToBoolean("n"));
            Assert.False(WellTool.Core.Util.BooleanUtil.ToBoolean("f"));
            Assert.False(WellTool.Core.Util.BooleanUtil.ToBoolean("off"));
            Assert.False(WellTool.Core.Util.BooleanUtil.ToBoolean("wrong"));
            Assert.False(WellTool.Core.Util.BooleanUtil.ToBoolean("fail"));
            Assert.False(WellTool.Core.Util.BooleanUtil.ToBoolean("0"));
            Assert.False(WellTool.Core.Util.BooleanUtil.ToBoolean("Off"));
            Assert.False(WellTool.Core.Util.BooleanUtil.ToBoolean("否"));
            Assert.False(WellTool.Core.Util.BooleanUtil.ToBoolean(""));
            Assert.False(WellTool.Core.Util.BooleanUtil.ToBoolean("6455434"));
        }

        [Fact]
        public void AndTest()
        {
            Assert.False(WellTool.Core.Util.BooleanUtil.And(true, false));
        }

        [Fact]
        public void OrTest()
        {
            Assert.True(WellTool.Core.Util.BooleanUtil.Or(true, false));
        }

        [Fact]
        public void XorTest()
        {
            Assert.True(WellTool.Core.Util.BooleanUtil.Xor(true, false));
        }

        [Fact]
        public void IsTrueIsFalseTest()
        {
            Assert.False(WellTool.Core.Util.BooleanUtil.IsTrue(null));
            Assert.False(WellTool.Core.Util.BooleanUtil.IsFalse(null));
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
            Assert.Equal("true", WellTool.Core.Util.BooleanUtil.ToStringTrueFalse(true));
            Assert.Equal("false", WellTool.Core.Util.BooleanUtil.ToStringTrueFalse(false));

            Assert.Equal("yes", WellTool.Core.Util.BooleanUtil.ToStringYesNo(true));
            Assert.Equal("no", WellTool.Core.Util.BooleanUtil.ToStringYesNo(false));

            Assert.Equal("on", WellTool.Core.Util.BooleanUtil.ToStringOnOff(true));
            Assert.Equal("off", WellTool.Core.Util.BooleanUtil.ToStringOnOff(false));
        }

        [Fact]
        public void ExactlyOneTrueTest()
        {
            Assert.True(WellTool.Core.Util.BooleanUtil.ExactlyOneTrue(true, false, false));
            Assert.False(WellTool.Core.Util.BooleanUtil.ExactlyOneTrue(true, true, false));
            Assert.False(WellTool.Core.Util.BooleanUtil.ExactlyOneTrue(false, false, false));
        }
    }
}
