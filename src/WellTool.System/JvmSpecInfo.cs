//using System.Text;

//namespace WellTool.System;

///// <summary>
///// JVM Specification信息（对应Java的JvmSpecInfo）
///// </summary>
//public class JvmSpecInfo
//{
//    private static readonly Lazy<JvmSpecInfo> _instance = new(() => new JvmSpecInfo());
//    public static JvmSpecInfo Instance => _instance.Value;

//    private readonly string _specificationName;
//    private readonly string _specificationVersion;
//    private readonly string _specificationVendor;

//    private JvmSpecInfo()
//    {
//        _specificationName = ".NET Virtual Machine Specification";
//        _specificationVersion = Environment.Version.ToString();
//        _specificationVendor = "Microsoft Corporation";
//    }

//    /// <summary>
//    /// 取得当前JVM spec.的名称
//    /// </summary>
//    /// <returns>规范名称</returns>
//    public string Name => _specificationName;

//    /// <summary>
//    /// 取得当前JVM spec.的版本
//    /// </summary>
//    /// <returns>规范版本</returns>
//    public string Version => _specificationVersion;

//    /// <summary>
//    /// 取得当前JVM spec.的厂商
//    /// </summary>
//    /// <returns>规范厂商</returns>
//    public string Vendor => _specificationVendor;

//    /// <summary>
//    /// 将Java Virtual Machine Specification的信息转换成字符串
//    /// </summary>
//    /// <returns>字符串表示</returns>
//    public override string ToString()
//    {
//        var builder = new StringBuilder();

//        SystemUtil.Append(builder, "JavaVM Spec. Name:    ", Name);
//        SystemUtil.Append(builder, "JavaVM Spec. Version: ", Version);
//        SystemUtil.Append(builder, "JavaVM Spec. Vendor:  ", Vendor);

//        return builder.ToString();
//    }
//}