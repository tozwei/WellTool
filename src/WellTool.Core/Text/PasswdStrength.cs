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

namespace WellTool.Core.Text;

/// <summary>
/// 密码强度枚举
/// </summary>
public enum PasswdStrength
{
    /// <summary>
    /// 弱密码
    /// </summary>
    Weak = 0,

    /// <summary>
    /// 中强度密码
    /// </summary>
    Medium = 1,

    /// <summary>
    /// 强密码
    /// </summary>
    Strong = 2,

    /// <summary>
    /// 非常强的密码
    /// </summary>
    VeryStrong = 3
}
