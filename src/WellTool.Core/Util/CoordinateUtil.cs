using System;

namespace WellTool.Core.Util;

/// <summary>
/// 坐标工具类
/// </summary>
public static class CoordinateUtil
{
	/// <summary>
	/// 计算两点之间的距离（单位：米）
	/// </summary>
	/// <param name="lat1">纬度1</param>
	/// <param name="lon1">经度1</param>
	/// <param name="lat2">纬度2</param>
	/// <param name="lon2">经度2</param>
	/// <returns>距离（米）</returns>
	public static double Distance(double lat1, double lon1, double lat2, double lon2)
	{
		const double R = 6371000; // 地球半径（米）
		double dLat = ToRadians(lat2 - lat1);
		double dLon = ToRadians(lon2 - lon1);
		double a = System.Math.Sin(dLat / 2) * System.Math.Sin(dLat / 2) +
				   System.Math.Cos(ToRadians(lat1)) * System.Math.Cos(ToRadians(lat2)) *
				   System.Math.Sin(dLon / 2) * System.Math.Sin(dLon / 2);
		double c = 2 * System.Math.Atan2(System.Math.Sqrt(a), System.Math.Sqrt(1 - a));
		return R * c;
	}

	/// <summary>
	/// 计算两点之间的距离（单位：公里）
	/// </summary>
	/// <param name="lat1">纬度1</param>
	/// <param name="lon1">经度1</param>
	/// <param name="lat2">纬度2</param>
	/// <param name="lon2">经度2</param>
	/// <returns>距离（公里）</returns>
	public static double DistanceKm(double lat1, double lon1, double lat2, double lon2)
	{
		return Distance(lat1, lon1, lat2, lon2) / 1000;
	}

	/// <summary>
	/// 度转弧度
	/// </summary>
	/// <param name="degree">度</param>
	/// <returns>弧度</returns>
	public static double ToRadians(double degree)
	{
		return degree * System.Math.PI / 180;
	}

	/// <summary>
	/// 弧度转度
	/// </summary>
	/// <param name="radian">弧度</param>
	/// <returns>度</returns>
	public static double ToDegree(double radian)
	{
		return radian * 180 / System.Math.PI;
	}

	/// <summary>
	/// 判断经纬度是否有效
	/// </summary>
	/// <param name="lat">纬度</param>
	/// <param name="lon">经度</param>
	/// <returns>是否有效</returns>
	public static bool IsValidCoord(double lat, double lon)
	{
		return lat >= -90 && lat <= 90 && lon >= -180 && lon <= 180;
	}

	/// <summary>
	/// 根据距离和方向计算目标点坐标
	/// </summary>
	/// <param name="lat">起点纬度</param>
	/// <param name="lon">起点经度</param>
	/// <param name="distance">距离（米）</param>
	/// <param name="bearing">方向（度）</param>
	/// <returns>目标点坐标 (lat, lon)</returns>
	public static (double lat, double lon) Destination(double lat, double lon, double distance, double bearing)
	{
		const double R = 6371000; // 地球半径（米）
		double brng = ToRadians(bearing);
		double d = distance / R;

		double lat1 = ToRadians(lat);
		double lon1 = ToRadians(lon);

		double lat2 = System.Math.Asin(System.Math.Sin(lat1) * System.Math.Cos(d) +
						   System.Math.Cos(lat1) * System.Math.Sin(d) * System.Math.Cos(brng));
		double lon2 = lon1 + System.Math.Atan2(System.Math.Sin(brng) * System.Math.Sin(d) * System.Math.Cos(lat1),
								System.Math.Cos(d) - System.Math.Sin(lat1) * System.Math.Sin(lat2));

		return (ToDegree(lat2), ToDegree(lon2));
	}
}
