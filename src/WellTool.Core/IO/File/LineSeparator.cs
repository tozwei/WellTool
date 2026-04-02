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

namespace WellTool.Core.IO.File
{
    /// <summary>
    /// 换行符枚举<br>
    /// 换行符包括：
    /// <pre>
    /// Mac系统换行符："\r"
    /// Linux系统换行符："\n"
    /// Windows系统换行符："\r\n"
    /// </pre>
    /// </summary>
    public class LineSeparator
    {
        /// <summary>Mac系统换行符："\r"</summary>
        public static readonly LineSeparator MAC = new LineSeparator("\r");
        /// <summary>Linux系统换行符："\n"</summary>
        public static readonly LineSeparator LINUX = new LineSeparator("\n");
        /// <summary>Windows系统换行符："\r\n"</summary>
        public static readonly LineSeparator WINDOWS = new LineSeparator("\r\n");

        /// <summary>所有换行符</summary>
        public static readonly LineSeparator[] Values = new LineSeparator[] { MAC, LINUX, WINDOWS };

        private readonly string _value;

        private LineSeparator(string lineSeparator)
        {
            _value = lineSeparator;
        }

        public string GetValue()
        {
            return _value;
        }
    }
}