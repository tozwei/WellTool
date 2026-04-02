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
/// ASCII字符转字符串的缓存<br>
/// 用于减少String对象的创建，提高性能
/// </summary>
public static class ASCIIStrCache
{
    private static readonly string[] Cache = new string[128];

    static ASCIIStrCache()
    {
        for (var i = 0; i < Cache.Length; i++)
        {
            Cache[i] = ((char)i).ToString();
        }
    }

    /// <summary>
    /// 获取ASCII字符对应的字符串
    /// </summary>
    /// <param name="c">ASCII字符（0~127）</param>
    /// <returns>字符对应的字符串</returns>
    public static string Get(char c)
    {
        return c < 128 ? Cache[c] : c.ToString();
    }
}
