using System.Text;
using System.Runtime.InteropServices;

namespace WellTool.System;

/// <summary>
/// .NET Virtual Machine 信息
/// </summary>
public class NetVmInfo
{
    private static readonly Lazy<NetVmInfo> _instance = new(() => new NetVmInfo());
    public static NetVmInfo Instance => _instance.Value;

    private readonly string _name;
    private readonly string _version;
    private readonly string _vendor;
    private readonly string _info;

    private NetVmInfo()
    {
        _name = RuntimeInformation.FrameworkDescription;
        _version = Environment.Version.ToString();
        _vendor = "Microsoft Corporation";
        _info = RuntimeInformation.OSDescription;
    }

    /// <summary>
    /// 取得当前.NET VM impl.的名称
    /// </summary>
    /// <returns>VM名称</returns>
    public string Name => _name;

    /// <summary>
    /// 取得当前.NET VM impl.的版本
    /// </summary>
    /// <returns>VM版本</returns>
    public string Version => _version;

    /// <summary>
    /// 取得当前.NET VM impl.的厂商
    /// </summary>
    /// <returns>VM厂商</returns>
    public string Vendor => _vendor;

    /// <summary>
    /// 取得当前.NET VM impl.的信息
    /// </summary>
    /// <returns>VM信息</returns>
    public string Info => _info;

    /// <summary>
    /// 将.NET Virtual Machine Implementation的信息转换成字符串
    /// </summary>
    /// <returns>字符串表示</returns>
    public override string ToString()
    {
        var builder = new StringBuilder();

        SystemUtil.Append(builder, ".NET VM Name:    ", Name);
        SystemUtil.Append(builder, ".NET VM Version: ", Version);
        SystemUtil.Append(builder, ".NET VM Vendor:  ", Vendor);
        SystemUtil.Append(builder, ".NET VM Info:    ", Info);

        return builder.ToString();
    }
}