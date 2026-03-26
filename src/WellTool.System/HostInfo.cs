using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace WellTool.System;

/// <summary>
/// 主机信息（对应Java的HostInfo）
/// </summary>
public class HostInfo
{
    private static readonly Lazy<HostInfo> _instance = new(() => new HostInfo());
    public static HostInfo Instance => _instance.Value;

    private readonly string _hostName;
    private readonly string _hostAddress;

    private HostInfo()
    {
        try
        {
            var hostName = Dns.GetHostName();
            _hostName = hostName;

            var addresses = Dns.GetHostAddresses(hostName);
            var ipv4 = addresses.FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork);
            _hostAddress = ipv4?.ToString() ?? "127.0.0.1";
        }
        catch
        {
            _hostName = null;
            _hostAddress = null;
        }
    }

    /// <summary>
    /// 取得当前主机的名称
    /// </summary>
    public string Name => _hostName;

    /// <summary>
    /// 取得当前主机的地址
    /// </summary>
    public string Address => _hostAddress;

    /// <summary>
    /// 将当前主机的信息转换成字符串
    /// </summary>
    /// <returns>字符串表示</returns>
    public override string ToString()
    {
        var builder = new StringBuilder();

        SystemUtil.Append(builder, "Host Name:    ", Name);
        SystemUtil.Append(builder, "Host Address: ", Address);

        return builder.ToString();
    }
}