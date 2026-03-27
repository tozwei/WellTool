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
/// CPU信息
/// </summary>
public class CpuInfo
{
    /// <summary>
    /// CPU型号
    /// </summary>
    public string Model { get; set; }

    /// <summary>
    /// CPU核心数
    /// </summary>
    public int CoreCount { get; set; }

    /// <summary>
    /// CPU线程数
    /// </summary>
    public int ThreadCount { get; set; }

    /// <summary>
    /// CPU频率（MHz）
    /// </summary>
    public double Frequency { get; set; }

    /// <summary>
    /// CPU使用率（%）
    /// </summary>
    public double Usage { get; set; }

    /// <summary>
    /// 将CPU信息转换为字符串
    /// </summary>
    /// <returns>字符串表示</returns>
    public override string ToString()
    {
        return $"Model: {Model}, CoreCount: {CoreCount}, ThreadCount: {ThreadCount}, Frequency: {Frequency}MHz, Usage: {Usage}%";
    }
}