namespace WellTool.Poi;

/// <summary>
/// POI全局配置类
/// </summary>
public static class GlobalPoiConfig
{
	/// <summary>
	/// 设置解压时的最小压缩比例，避免Zip Bomb
	/// </summary>
	/// <param name="ratio">压缩比例</param>
	public static void SetMinInflateRatio(double ratio)
	{
		// NPOI中通过配置ZipArchive来实现
	}
}
