using System;
using System.Reflection;

namespace WellTool.Core.Util
{
    /// <summary>
    /// JDK工具类，用于检测JDK版本和特性
    /// </summary>
    public static class JdkUtil
    {
        /// <summary>
        /// 获取当前运行时版本
        /// </summary>
        public static string Version => typeof(object).Assembly.ImageRuntimeVersion;

        /// <summary>
        /// 获取主版本号
        /// </summary>
        public static int MajorVersion
        {
            get
            {
                var version = Environment.Version;
                // .NET Framework 返回 4.x
                // .NET Core/.NET 5+ 返回实际版本
                if (version.Major < 5)
                {
                    return version.Major;
                }
                return version.Major;
            }
        }

        /// <summary>
        /// 获取次版本号
        /// </summary>
        public static int MinorVersion => Environment.Version.Minor;

        /// <summary>
        /// 获取构建号
        /// </summary>
        public static int BuildNumber => Environment.Version.Build;

        /// <summary>
        /// 判断是否为.NET Framework
        /// </summary>
        public static bool IsNetFramework => Type.GetType("Mono.Runtime") == null && 
                                              !IsNetCore;

        /// <summary>
        /// 判断是否为.NET Core
        /// </summary>
        public static bool IsNetCore => Environment.Version.Major >= 5;

        /// <summary>
        /// 判断是否为Mono
        /// </summary>
        public static bool IsMono => Type.GetType("Mono.Runtime") != null;

        /// <summary>
        /// 获取运行时名称
        /// </summary>
        public static string RuntimeName
        {
            get
            {
                if (IsMono)
                {
                    return "Mono";
                }
                if (IsNetCore)
                {
                    return ".NET Core";
                }
                if (IsNetFramework)
                {
                    return ".NET Framework";
                }
                return "Unknown";
            }
        }

        /// <summary>
        /// 获取运行时完整版本字符串
        /// </summary>
        public static string FullVersion => $"{RuntimeName} {Version}";

        /// <summary>
        /// 检查是否支持指定版本或更高版本
        /// </summary>
        public static bool IsSupport(int majorVersion, int minorVersion = 0)
        {
            if (MajorVersion > majorVersion)
            {
                return true;
            }
            if (MajorVersion == majorVersion)
            {
                return MinorVersion >= minorVersion;
            }
            return false;
        }

        /// <summary>
        /// 获取系统架构
        /// </summary>
        public static string Architecture => Environment.Is64BitOperatingSystem ? "x64" : "x86";

        /// <summary>
        /// 判断是否支持值类型元组
        /// </summary>
        public static bool SupportValueTuple => IsSupport(4, 7);

        /// <summary>
        /// 判断是否支持默认接口方法（需要C# 8.0）
        /// </summary>
        public static bool SupportDefaultInterfaceMethod => IsSupport(5, 0);
    }
}
