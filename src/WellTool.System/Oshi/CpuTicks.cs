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

namespace WellTool.System.Oshi;

/// <summary>
/// CPU ticks信息
/// </summary>
public class CpuTicks
{
    /// <summary>
    /// 用户态ticks
    /// </summary>
    public long User { get; set; }

    /// <summary>
    /// 系统态ticks
    /// </summary>
    public long System { get; set; }

    /// <summary>
    /// 空闲态ticks
    /// </summary>
    public long Idle { get; set; }

    /// <summary>
    /// 等待I/O的ticks
    /// </summary>
    public long IoWait { get; set; }

    /// <summary>
    /// 中断的ticks
    /// </summary>
    public long Irq { get; set; }

    /// <summary>
    /// 软中断的ticks
    /// </summary>
    public long SoftIrq { get; set; }

    /// <summary>
    /// 窃取的ticks
    /// </summary>
    public long Steal { get; set; }

    /// <summary>
    /// 总ticks
    /// </summary>
    public long Total => User + System + Idle + IoWait + Irq + SoftIrq + Steal;

    /// <summary>
    /// 计算CPU使用率
    /// </summary>
    /// <param name="prevTicks">之前的ticks</param>
    /// <returns>CPU使用率（%）</returns>
    public double CalculateUsage(CpuTicks prevTicks)
    {
        long totalDiff = Total - prevTicks.Total;
        long idleDiff = Idle - prevTicks.Idle;

        if (totalDiff <= 0) return 0;

        return 100.0 * (totalDiff - idleDiff) / totalDiff;
    }

    /// <summary>
    /// 将CPU ticks信息转换为字符串
    /// </summary>
    /// <returns>字符串表示</returns>
    public override string ToString()
    {
        return $"User: {User}, System: {System}, Idle: {Idle}, IoWait: {IoWait}, Irq: {Irq}, SoftIrq: {SoftIrq}, Steal: {Steal}";
    }
}