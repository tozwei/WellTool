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

using System.Threading;

namespace WellTool.Core.Thread.ThreadLocal
{
    /// <summary>
    /// 带有Name标识的 AsyncLocal，调用ToString返回name
    /// <para>在 C# 中，AsyncLocal 类似于 Java 中的 InheritableThreadLocal，用于在异步操作中传递值</para>
    /// </summary>
    /// <typeparam name="T">值类型</typeparam>
    public class NamedInheritableThreadLocal<T>
    {
        private readonly AsyncLocal<T> _asyncLocal;
        private readonly string _name;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="name">名字</param>
        public NamedInheritableThreadLocal(string name)
        {
            _name = name;
            _asyncLocal = new AsyncLocal<T>();
        }

        /// <summary>
        /// 获取当前线程的值
        /// </summary>
        /// <returns>当前线程的值</returns>
        public T Value
        {
            get => _asyncLocal.Value;
            set => _asyncLocal.Value = value;
        }

        /// <summary>
        /// 返回线程本地存储的名称
        /// </summary>
        /// <returns>名称</returns>
        public override string ToString()
        {
            return _name;
        }
    }
}