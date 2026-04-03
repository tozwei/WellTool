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
            Assert.True(WellTool.Core.BooleanUtil.ToBoolean("true"));
            Assert.True(WellTool.Core.BooleanUtil.ToBoolean("yes"));
            Assert.True(WellTool.Core.BooleanUtil.ToBoolean("y"));
            Assert.True(WellTool.Core.BooleanUtil.ToBoolean("t"));
            Assert.True(WellTool.Core.BooleanUtil.ToBoolean("OK"));
            Assert.True(WellTool.Core.BooleanUtil.ToBoolean("correct"));
            Assert.True(WellTool.Core.BooleanUtil.ToBoolean("success"));
            Assert.True(WellTool.Core.BooleanUtil.ToBoolean("1"));
            Assert.True(WellTool.Core.BooleanUtil.ToBoolean("On"));
            Assert.True(WellTool.Core.BooleanUtil.ToBoolean("是"));
            Assert.True(WellTool.Core.BooleanUtil.ToBoolean("对"));
            Assert.True(WellTool.Core.BooleanUtil.ToBoolean("真"));

            Assert.False(WellTool.Core.BooleanUtil.ToBoolean("false"));
            Assert.False(WellTool.Core.BooleanUtil.ToBoolean("no"));
            Assert.False(WellTool.Core.BooleanUtil.ToBoolean("n"));
            Assert.False(WellTool.Core.BooleanUtil.ToBoolean("f"));
            Assert.False(WellTool.Core.BooleanUtil.ToBoolean("off"));
            Assert.False(WellTool.Core.BooleanUtil.ToBoolean("wrong"));
            Assert.False(WellTool.Core.BooleanUtil.ToBoolean("fail"));
            Assert.False(WellTool.Core.BooleanUtil.ToBoolean("0"));
            Assert.False(WellTool.Core.BooleanUtil.ToBoolean("Off"));
            Assert.False(WellTool.Core.BooleanUtil.ToBoolean("否"));
            Assert.False(WellTool.Core.BooleanUtil.ToBoolean(""));
            Assert.False(WellTool.Core.BooleanUtil.ToBoolean("6455434"));
        }

        [Fact]
        public void AndTest()
        {
            Assert.False(WellTool.Core.BooleanUtil.And(true, false));
        }

        [Fact]
        public void OrTest()
        {
            Assert.True(WellTool.Core.BooleanUtil.Or(true, false));
        }

        [Fact]
        public void XorTest()
        {
            Assert.True(WellTool.Core.BooleanUtil.Xor(true, false));
        }

        [Fact]
        public void IsTrueIsFalseTest()
        {
            Assert.False(WellTool.Core.BooleanUtil.IsTrue(null));
            Assert.False(WellTool.Core.BooleanUtil.IsFalse(null));
        }

        [Fact]
        public void NegateTest()
        {
            Assert.False(WellTool.Core.BooleanUtil.Negate(true));
            Assert.True(WellTool.Core.BooleanUtil.Negate(false));
        }

        [Fact]
        public void ToStringTest()
        {
            Assert.Equal("true", WellTool.Core.BooleanUtil.ToStringTrueFalse(true));
            Assert.Equal("false", WellTool.Core.BooleanUtil.ToStringTrueFalse(false));

            Assert.Equal("yes", WellTool.Core.BooleanUtil.ToStringYesNo(true));
            Assert.Equal("no", WellTool.Core.BooleanUtil.ToStringYesNo(false));

            Assert.Equal("on", WellTool.Core.BooleanUtil.ToStringOnOff(true));
            Assert.Equal("off", WellTool.Core.BooleanUtil.ToStringOnOff(false));
        }

        [Fact]
        public void ExactlyOneTrueTest()
        {
            Assert.True(WellTool.Core.BooleanUtil.ExactlyOneTrue(true, false, false));
            Assert.False(WellTool.Core.BooleanUtil.ExactlyOneTrue(true, true, false));
            Assert.False(WellTool.Core.BooleanUtil.ExactlyOneTrue(false, false, false));
        }
    }
}
