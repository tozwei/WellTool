using Xunit;
using WellTool.Core;
using WellTool.Core.Util;
using System.Collections.Generic;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Issue #3660 测试
    /// </summary>
    public class Issue3660Test
    {
        [Fact]
        public void SplitTest()
        {
            var split = StrUtil.Split("", ',');
            Assert.Equal(1, split.Length);

            split = StrUtil.SplitTrim("", ',');
            Assert.Equal(0, split.Length);
        }
    }
}
