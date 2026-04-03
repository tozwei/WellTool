using System;

namespace WellTool.Core.Util
{
    /// <summary>
    /// 坐标工具类
    /// </summary>
    public static class CoordinateUtil
    {
        private const double EarthRadius = 6371000; // 地球半径（米）

        /// <summary>
        /// 计算两点之间的距离（米）
        /// </summary>
        public static double Distance(double lat1, double lon1, double lat2, double lon2)
        {
            double dLat = ToRadian(lat2 - lat1);
            double dLon = ToRadian(lon2 - lon1);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(ToRadian(lat1)) * Math.Cos(ToRadian(lat2)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return EarthRadius * c;
        }

        /// <summary>
        /// 计算两点之间的距离（公里）
        /// </summary>
        public static double DistanceKm(double lat1, double lon1, double lat2, double lon2)
        {
            return Distance(lat1, lon1, lat2, lon2) / 1000;
        }

        /// <summary>
        /// 计算两点之间的距离（英里）
        /// </summary>
        public static double DistanceMile(double lat1, double lon1, double lat2, double lon2)
        {
            return Distance(lat1, lon1, lat2, lon2) / 1609.344;
        }

        /// <summary>
        /// 角度转弧度
        /// </summary>
        public static double ToRadian(double angle)
        {
            return angle * Math.PI / 180;
        }

        /// <summary>
        /// 弧度转角度
        /// </summary>
        public static double ToAngle(double radian)
        {
            return radian * 180 / Math.PI;
        }

        /// <summary>
        /// 判断点是否在多边形内
        /// </summary>
        public static bool IsPointInPolygon(double lat, double lon, double[] polygon)
        {
            // polygon为交替的经度和纬度数组，如 [lon1,lat1,lon2,lat2,...]
            int n = polygon.Length / 2;
            bool inside = false;

            for (int i = 0, j = n - 1; i < n; j = i++)
            {
                double xi = polygon[i * 2], yi = polygon[i * 2 + 1];
                double xj = polygon[j * 2], yj = polygon[j * 2 + 1];

                if (((yi > lat) != (yj > lat)) &&
                    (lon < (xj - xi) * (lat - yi) / (yj - yi) + xi))
                {
                    inside = !inside;
                }
            }

            return inside;
        }

        /// <summary>
        /// 判断点是否在圆内
        /// </summary>
        public static bool IsPointInCircle(double lat, double lon, double centerLat, double centerLon, double radius)
        {
            return Distance(lat, lon, centerLat, centerLon) <= radius;
        }

        /// <summary>
        /// 根据起点、方向和距离计算终点坐标
        /// </summary>
        public static (double lat, double lon) CalculateEndPoint(double lat, double lon, double bearing, double distance)
        {
            double angularDistance = distance / EarthRadius;
            double lat1 = ToRadian(lat);
            double lon1 = ToRadian(lon);
            double bearingRad = ToRadian(bearing);

            double lat2 = Math.Asin(
                Math.Sin(lat1) * Math.Cos(angularDistance) +
                Math.Cos(lat1) * Math.Sin(angularDistance) * Math.Cos(bearingRad));

            double lon2 = lon1 + Math.Atan2(
                Math.Sin(bearingRad) * Math.Sin(angularDistance) * Math.Cos(lat1),
                Math.Cos(angularDistance) - Math.Sin(lat1) * Math.Sin(lat2));

            return (ToAngle(lat2), ToAngle(lon2));
        }

        /// <summary>
        /// 计算方位角（从北顺时针方向）
        /// </summary>
        public static double CalculateBearing(double lat1, double lon1, double lat2, double lon2)
        {
            double lat1Rad = ToRadian(lat1);
            double lat2Rad = ToRadian(lat2);
            double dLon = ToRadian(lon2 - lon1);

            double y = Math.Sin(dLon) * Math.Cos(lat2Rad);
            double x = Math.Cos(lat1Rad) * Math.Sin(lat2Rad) -
                       Math.Sin(lat1Rad) * Math.Cos(lat2Rad) * Math.Cos(dLon);

            double bearing = Math.Atan2(y, x);
            bearing = ToAngle(bearing);
            bearing = (bearing + 360) % 360;

            return bearing;
        }
    }
}
