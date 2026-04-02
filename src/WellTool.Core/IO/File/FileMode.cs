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
    /// 文件读写模式，常用于RandomAccessFile
    /// </summary>
    public enum FileMode
    {
        /// <summary>以只读方式打开。调用结果对象的任何 write 方法都将导致抛出 IOException。</summary>
        r,
        /// <summary>打开以便读取和写入。</summary>
        rw,
        /// <summary>打开以便读取和写入。相对于 "rw"，"rws" 还要求对"文件的内容"或"元数据"的每个更新都同步写入到基础存储设备。</summary>
        rws,
        /// <summary>打开以便读取和写入，相对于 "rw"，"rwd" 还要求对"文件的内容"的每个更新都同步写入到基础存储设备。</summary>
        rwd
    }
}