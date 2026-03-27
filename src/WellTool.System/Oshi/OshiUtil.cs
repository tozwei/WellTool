// Copyright (c) 2025 WellTool Team
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Diagnostics;
using System.Management;

namespace WellTool.System.Oshi;

/// <summary>
/// Oshi工具类，用于获取系统硬件信息
/// </summary>
public static class OshiUtil
{
    /// <summary>
    /// 获取CPU信息
    /// </summary>
    /// <returns>CPU信息</returns>
    public static CpuInfo GetCpuInfo()
    {
        var cpuInfo = new CpuInfo();

        try
        {
            // 使用WMI获取CPU信息
            using var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
            foreach (var obj in searcher.Get())
            {
                cpuInfo.Model = obj["Name"]?.ToString() ?? "Unknown";
                cpuInfo.CoreCount = Convert.ToInt32(obj["NumberOfCores"]);
                cpuInfo.ThreadCount = Convert.ToInt32(obj["NumberOfLogicalProcessors"]);
                cpuInfo.Frequency = Convert.ToDouble(obj["MaxClockSpeed"]);
                break;
            }

            // 计算CPU使用率
            cpuInfo.Usage = GetCpuUsage();
        }
        catch
        {
            // 如果WMI不可用，使用默认值
            cpuInfo.Model = "Unknown";
            cpuInfo.CoreCount = Environment.ProcessorCount;
            cpuInfo.ThreadCount = Environment.ProcessorCount;
            cpuInfo.Frequency = 0;
            cpuInfo.Usage = 0;
        }

        return cpuInfo;
    }

    /// <summary>
    /// 获取CPU使用率
    /// </summary>
    /// <returns>CPU使用率（%）</returns>
    public static double GetCpuUsage()
    {
        try
        {
            var counter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            // 第一次获取的值可能不准确，需要先获取一次
            counter.NextValue();
            Thread.Sleep(100);
            return counter.NextValue();
        }
        catch
        {
            return 0;
        }
    }

    /// <summary>
    /// 获取CPU ticks信息
    /// </summary>
    /// <returns>CPU ticks信息</returns>
    public static CpuTicks GetCpuTicks()
    {
        var ticks = new CpuTicks();

        try
        {
            // 使用PerformanceCounter获取CPU使用情况
            var userCounter = new PerformanceCounter("Processor", "% User Time", "_Total");
            var systemCounter = new PerformanceCounter("Processor", "% Privileged Time", "_Total");
            var idleCounter = new PerformanceCounter("Processor", "% Idle Time", "_Total");

            // 第一次获取的值可能不准确，需要先获取一次
            userCounter.NextValue();
            systemCounter.NextValue();
            idleCounter.NextValue();
            Thread.Sleep(100);

            // 转换为ticks（这里使用相对值，实际ticks需要更复杂的计算）
            ticks.User = (long)(userCounter.NextValue() * 10000);
            ticks.System = (long)(systemCounter.NextValue() * 10000);
            ticks.Idle = (long)(idleCounter.NextValue() * 10000);
        }
        catch
        {
            // 如果PerformanceCounter不可用，使用默认值
            ticks.User = 0;
            ticks.System = 0;
            ticks.Idle = 0;
            ticks.IoWait = 0;
            ticks.Irq = 0;
            ticks.SoftIrq = 0;
            ticks.Steal = 0;
        }

        return ticks;
    }

    /// <summary>
    /// 获取内存信息
    /// </summary>
    /// <returns>内存信息（总内存和可用内存，单位：字节）</returns>
    public static (long TotalMemory, long AvailableMemory) GetMemoryInfo()
    {
        try
        {
            using var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
            foreach (var obj in searcher.Get())
            {
                long totalMemory = Convert.ToInt64(obj["TotalVisibleMemorySize"]) * 1024;
                long freeMemory = Convert.ToInt64(obj["FreePhysicalMemory"]) * 1024;
                return (totalMemory, freeMemory);
            }
        }
        catch
        {
            // 如果WMI不可用，使用GC.GetTotalMemory
            long totalMemory = GC.GetTotalMemory(false);
            return (totalMemory, totalMemory);
        }

        return (0, 0);
    }

    /// <summary>
    /// 获取磁盘信息
    /// </summary>
    /// <returns>磁盘信息（总空间和可用空间，单位：字节）</returns>
    public static (long TotalSpace, long AvailableSpace) GetDiskInfo()
    {
        try
        {
            foreach (var drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady && drive.DriveType == DriveType.Fixed)
                {
                    return (drive.TotalSize, drive.AvailableFreeSpace);
                }
            }
        }
        catch
        {
            // 忽略异常
        }

        return (0, 0);
    }
}