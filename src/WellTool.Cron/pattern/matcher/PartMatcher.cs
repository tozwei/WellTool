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

namespace WellTool.Cron.Pattern.Matcher
{
    /// <summary>
    /// 定时任务表达式匹配器接口
    /// </summary>
    public interface PartMatcher
    {
        /// <summary>
        /// 匹配指定值
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>是否匹配</returns>
        bool Match(int value);
    }
}