using Xunit;
using WellTool.Core;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Coordinate工具单元测试
    /// </summary>
    public class CoordinateUtilTest
    {
        [Fact]
        public void IsValidTest()
        {
            // 北京天安门坐标
            Assert.True(CoordinateUtil.IsValid(39.908823, 116.397470));

            // 无效坐标
            Assert.False(CoordinateUtil.IsValid(91.0, 116.397470));
            Assert.False(CoordinateUtil.IsValid(-91.0, 116.397470));
            Assert.False(CoordinateUtil.IsValid(39.908823, 181.0));
            Assert.False(CoordinateUtil.IsValid(39.908823, -181.0));
        }

        [Fact]
        public void IsValidLngTest()
        {
            Assert.True(CoordinateUtil.IsValidLng(116.397470));
            Assert.False(CoordinateUtil.IsValidLng(181.0));
            Assert.False(CoordinateUtil.IsValidLng(-181.0));
        }

        [Fact]
        public void IsValidLatTest()
        {
            Assert.True(CoordinateUtil.IsValidLat(39.908823));
            Assert.False(CoordinateUtil.IsValidLat(91.0));
            Assert.False(CoordinateUtil.IsValidLat(-91.0));
        }

        [Fact]
        public void GetDistanceTest()
        {
            // 北京天安门到上海东方明珠的距离约1068公里
            var distance = CoordinateUtil.GetDistance(39.908823, 116.397470, 31.245104, 121.498886);
            Assert.True(distance > 1000000); // 大于1000公里
            Assert.True(distance < 1200000); // 小于1200公里
        }

        [Fact]
        public void GetDistanceSamePointTest()
        {
            var distance = CoordinateUtil.GetDistance(39.908823, 116.397470, 39.908823, 116.397470);
            Assert.Equal(0, distance, 1);
        }

        [Fact]
        public void TranslateTest()
        {
            // 偏移100米
            var result = CoordinateUtil.Translate(39.908823, 116.397470, 100, 100);
            Assert.NotNull(result);
            Assert.Equal(2, result.Length);
            // 坐标应该有小幅度变化
            Assert.NotEqual(39.908823, result[0], 4);
            Assert.NotEqual(116.397470, result[1], 4);
        }
    }
}
