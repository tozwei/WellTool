using Xunit;
using WellTool.Core;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Runtime工具单元测试
    /// </summary>
    public class RuntimeUtilTest
    {
        [Fact]
        public void GetRuntimeTest()
        {
            var runtime = RuntimeUtil.GetRuntime();
            Assert.NotNull(runtime);
        }

        [Fact]
        public void GetAvailableProcessorsTest()
        {
            var processors = RuntimeUtil.GetAvailableProcessors();
            Assert.True(processors > 0);
        }

        [Fact]
        public void GetFreeMemoryTest()
        {
            var memory = RuntimeUtil.GetFreeMemory();
            Assert.True(memory >= 0);
        }

        [Fact]
        public void GetTotalMemoryTest()
        {
            var memory = RuntimeUtil.GetTotalMemory();
            Assert.True(memory >= 0);
        }

        [Fact]
        public void GetMaxMemoryTest()
        {
            var memory = RuntimeUtil.GetMaxMemory();
            Assert.True(memory >= 0);
        }

        [Fact]
        public void GetUsedMemoryTest()
        {
            var memory = RuntimeUtil.GetUsedMemory();
            Assert.True(memory >= 0);
        }

        [Fact]
        public void GetJvmInfoTest()
        {
            var info = RuntimeUtil.GetJvmInfo();
            Assert.NotNull(info);
        }

        [Fact]
        public void GetJavaInfoTest()
        {
            var info = RuntimeUtil.GetJavaInfo();
            Assert.NotNull(info);
        }

        [Fact]
        public void GetOsInfoTest()
        {
            var info = RuntimeUtil.GetOsInfo();
            Assert.NotNull(info);
        }

        [Fact]
        public void GetRuntimeInfoTest()
        {
            var info = RuntimeUtil.GetRuntimeInfo();
            Assert.NotNull(info);
        }

        [Fact]
        public void GcTest()
        {
            RuntimeUtil.Gc();
            RuntimeUtil.Gc(RuntimeUtil.GcType.Foreground);
            RuntimeUtil.Gc(RuntimeUtil.GcType.Background);
        }

        [Fact]
        public void HaltTest()
        {
            // 测试不会抛出异常
            RuntimeUtil.Halt(0);
        }

        [Fact]
        public void ShutdownTest()
        {
            // 测试不会抛出异常
            RuntimeUtil.Shutdown();
        }

        [Fact]
        public void AddShutdownHookTest()
        {
            var called = false;
            RuntimeUtil.AddShutdownHook(() => { called = true; });
            Assert.True(true); // 确保不抛异常
        }

        [Fact]
        public void GetProcessTest()
        {
            var process = RuntimeUtil.GetProcess();
            Assert.NotNull(process);
        }
    }
}
