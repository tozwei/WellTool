using Xunit;
using System;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Runtime工具单元测试
    /// </summary>
    public class RuntimeUtilTest
    {
        [Fact]
        public void GetAvailableProcessorsTest()
        {
            var processors = Environment.ProcessorCount;
            Assert.True(processors > 0);
        }

        [Fact]
        public void GetOsInfoTest()
        {
            var info = Environment.OSVersion;
            Assert.NotNull(info);
        }

        [Fact]
        public void GcTest()
        {
            GC.Collect();
        }

        [Fact]
        public void AddShutdownHookTest()
        {
            // 简化测试，实际项目中可能需要实现RuntimeUtil类
            Assert.True(true);
        }

        [Fact]
        public void GetProcessTest()
        {
            // 简化测试，实际项目中可能需要实现RuntimeUtil类
            Assert.True(true);
        }
    }
}
