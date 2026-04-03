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

namespace WellTool.Core.Lang.Ansi
{
    /// <summary>
    /// 生成ANSI格式的编码输出
    /// </summary>
    public static class AnsiEncoder
    {
        private const string EncodeJoin = ";";
        private const string EncodeStart = "\u001B[";
        private const string EncodeEnd = "m";
        private static readonly string Reset = "0;" + AnsiColor.Default.GetCode();

        /// <summary>
        /// 创建ANSI字符串，参数中的{@link IAnsiElement}会被转换为编码形式。
        /// </summary>
        /// <param name="elements">节点数组</param>
        /// <returns>ANSI字符串</returns>
        public static string Encode(params object[] elements)
        {
            var sb = new System.Text.StringBuilder();
            BuildEnabled(sb, elements);
            return sb.ToString();
        }

        /// <summary>
        /// 追加需要需转义的节点
        /// </summary>
        /// <param name="sb">StringBuilder</param>
        /// <param name="elements">节点列表</param>
        private static void BuildEnabled(System.Text.StringBuilder sb, object[] elements)
        {
            bool writingAnsi = false;
            bool containsEncoding = false;
            foreach (var element in elements)
            {
                if (element == null)
                {
                    continue;
                }
                if (element is IAnsiElement)
                {
                    containsEncoding = true;
                    if (writingAnsi)
                    {
                        sb.Append(EncodeJoin);
                    }
                    else
                    {
                        sb.Append(EncodeStart);
                        writingAnsi = true;
                    }
                }
                else
                {
                    if (writingAnsi)
                    {
                        sb.Append(EncodeEnd);
                        writingAnsi = false;
                    }
                }
                sb.Append(element);
            }

            // 恢复默认
            if (containsEncoding)
            {
                sb.Append(writingAnsi ? EncodeJoin : EncodeStart);
                sb.Append(Reset);
                sb.Append(EncodeEnd);
            }
        }
    }
}