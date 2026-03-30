using System.Text;
using System.Runtime.InteropServices;

namespace WellTool.System;

/// <summary>
/// .NET Runtime Specification 信息
/// </summary>
public class NetSpecInfo
{
    private static readonly Lazy<NetSpecInfo> _instance = new(() => new NetSpecInfo());
    public static NetSpecInfo Instance => _instance.Value;

    private readonly string _specificationName;
    private readonly string _specificationVersion;
    private readonly string _specificationVendor;

    private NetSpecInfo()
    {
        _specificationName = RuntimeInformation.FrameworkDescription;
        _specificationVersion = Environment.Version.ToString();
        _specificationVendor = "Microsoft Corporation";
    }

    /// <summary>
    /// 取得当前.NET Spec.的名称
    /// </summary>
    /// <returns>规范名称</returns>
    public string Name => _specificationName;

    /// <summary>
    /// 取得当前.NET Spec.的版本
    /// </summary>
    /// <returns>规范版本</returns>
    public string Version => _specificationVersion;

    /// <summary>
    /// 取得当前.NET Spec.的厂商
    /// </summary>
    /// <returns>规范厂商</returns>
    public string Vendor => _specificationVendor;

    /// <summary>
    /// 将.NET Specification的信息转换成字符串
    /// </summary>
    /// <returns>字符串表示</returns>
    public override string ToString()
    {
        var builder = new StringBuilder();

        SystemUtil.Append(builder, ".NET Spec. Name:    ", Name);
        SystemUtil.Append(builder, ".NET Spec. Version: ", Version);
        SystemUtil.Append(builder, ".NET Spec. Vendor:  ", Vendor);

        return builder.ToString();
    }
}