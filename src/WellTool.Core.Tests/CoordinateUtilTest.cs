using WellTool.Core.Util;
using Xunit;

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
            // 北京坐标
            Assert.True(CoordinateUtil.IsValidCoord(39.9042, 116.4074));
            // 无效坐标
            Assert.False(CoordinateUtil.IsValidCoord(100, 200));
            Assert.False(CoordinateUtil.IsValidCoord(-91, 0));
            Assert.False(CoordinateUtil.IsValidCoord(0, 181));
        }

        [Fact]
        public void IsValidLngTest()
        {
            Assert.True(CoordinateUtil.IsValidCoord(0, 0));
            Assert.False(CoordinateUtil.IsValidCoord(0, 200));
            Assert.False(CoordinateUtil.IsValidCoord(0, -181));
        }

        [Fact]
        public void IsValidLatTest()
        {
            Assert.True(CoordinateUtil.IsValidCoord(0, 0));
            Assert.False(CoordinateUtil.IsValidCoord(100, 0));
            Assert.False(CoordinateUtil.IsValidCoord(-91, 0));
        }

        [Fact]
        public void GetDistanceTest()
        {
            // 北京到上海约1060公里
            double distance = CoordinateUtil.DistanceKm(39.9042, 116.4074, 31.2304, 121.4737);
            Assert.True(distance > 1000 && distance < 1100);
        }

        [Fact]
        public void GetDistanceSamePointTest()
        {
            double distance = CoordinateUtil.DistanceKm(39.9042, 116.4074, 39.9042, 116.4074);
            Assert.Equal(0, distance, 5);
        }

        [Fact]
        public void ToRadiansTest()
        {
            double radians = CoordinateUtil.ToRadians(180);
            Assert.Equal(Math.PI, radians, 5);
        }

        [Fact]
        public void ToDegreeTest()
        {
            double degrees = CoordinateUtil.ToDegree(Math.PI);
            Assert.Equal(180, degrees, 5);
        }
    }
}
