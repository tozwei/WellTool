//using System.Text;
//using System.Runtime.InteropServices;

//namespace WellTool.System;

///// <summary>
///// JVM信息（对应Java的JvmInfo）
///// </summary>
//public class JvmInfo
//{
//    private static readonly Lazy<JvmInfo> _instance = new(() => new JvmInfo());
//    public static JvmInfo Instance => _instance.Value;

//    private readonly string _name;
//    private readonly string _version;
//    private readonly string _vendor;
//    private readonly string _info;

//    private JvmInfo()
//    {
//        _name = RuntimeInformation.FrameworkDescription;
//        _version = Environment.Version.ToString();
//        _vendor = "Microsoft Corporation";
//        _info = RuntimeInformation.OSDescription;
//    }

//    /// <summary>
//    /// 取得当前JVM impl.的名称
//    /// </summary>
//    /// <returns>JVM名称</returns>
//    public string Name => _name;

//    /// <summary>
//    /// 取得当前JVM impl.的版本
//    /// </summary>
//    /// <returns>JVM版本</returns>
//    public string Version => _version;

//    /// <summary>
//    /// 取得当前JVM impl.的厂商
//    /// </summary>
//    /// <returns>JVM厂商</returns>
//    public string Vendor => _vendor;

//    /// <summary>
//    /// 取得当前JVM impl.的信息
//    /// </summary>
//    /// <returns>JVM信息</returns>
//    public string Info => _info;

//    /// <summary>
//    /// 将Java Virtual Machine Implementation的信息转换成字符串
//    /// </summary>
//    /// <returns>字符串表示</returns>
//    public override string ToString()
//    {
//        var builder = new StringBuilder();

//        SystemUtil.Append(builder, "JavaVM Name:    ", Name);
//        SystemUtil.Append(builder, "JavaVM Version: ", Version);
//        SystemUtil.Append(builder, "JavaVM Vendor:  ", Vendor);
//        SystemUtil.Append(builder, "JavaVM Info:    ", Info);

//        return builder.ToString();
//    }
//}