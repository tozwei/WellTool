using Xunit;
using WellTool.Core;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Id工具单元测试
    /// </summary>
    public class IdUtilTest
    {
        [Fact]
        public void FastSimpleUUIDTest()
        {
            var id = IdUtil.FastSimpleUUID();
            Assert.NotNull(id);
            Assert.Equal(32, id.Length);
        }

        [Fact]
        public void SimpleUUIDTest()
        {
            var id = IdUtil.SimpleUUID();
            Assert.NotNull(id);
            Assert.Equal(32, id.Length);
        }

        [Fact]
        public void FastUUIDTest()
        {
            var id = IdUtil.FastUUID();
            Assert.NotNull(id);
            var uuid = Guid.Parse(id);
            Assert.NotEqual(Guid.Empty, uuid);
        }

        [Fact]
        public void UUIDTest()
        {
            var id = IdUtil.UUID();
            Assert.NotNull(id);
            var uuid = Guid.Parse(id);
            Assert.NotEqual(Guid.Empty, uuid);
        }

        [Fact]
        public void ObjectIdTest()
        {
            var id = IdUtil.ObjectId();
            Assert.NotNull(id);
            Assert.Equal(24, id.Length);
        }

        [Fact]
        public void NanoIdTest()
        {
            var id = IdUtil.NanoId();
            Assert.NotNull(id);
            Assert.True(id.Length > 0);
        }

        [Fact]
        public void SnowflakeTest()
        {
            var id1 = IdUtil.SnowflakeNextId();
            var id2 = IdUtil.SnowflakeNextId();
            Assert.NotEqual(id1, id2);
            Assert.True(id1 > 0);
            Assert.True(id2 > 0);
        }

        [Fact]
        public void SnowflakeNextIdStrTest()
        {
            var id = IdUtil.SnowflakeNextIdStr();
            Assert.NotNull(id);
            Assert.True(id.Length > 0);
        }

        [Fact]
        public void IncrementTest()
        {
            var id1 = IdUtil.Increment();
            var id2 = IdUtil.Increment();
            Assert.True(id2 > id1);
        }

        [Fact]
        public void IncrementWithPrefixTest()
        {
            var id = IdUtil.Increment("prefix");
            Assert.StartsWith("prefix", id);
        }

        [Fact]
        public void IsValidTest()
        {
            Assert.True(IdUtil.IsValid(UUID.Parse("550e8400-e29b-41d4-a716-446655440000")));
            Assert.False(IdUtil.IsValid(Guid.Empty));
        }
    }
}
