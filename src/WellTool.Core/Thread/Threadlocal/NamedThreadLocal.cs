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
    /// 带有Name标识的 ThreadLocal，调用ToString返回name
    /// </summary>
    /// <typeparam name="T">值类型</typeparam>
    public class NamedThreadLocal<T> : ThreadLocal<T>
    {
        private readonly string _name;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="name">名字</param>
        public NamedThreadLocal(string name)
        {
            _name = name;
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="name">名字</param>
        /// <param name="valueFactory">值工厂</param>
        public NamedThreadLocal(string name, System.Func<T> valueFactory) : base(valueFactory)
        {
            _name = name;
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="name">名字</param>
        /// <param name="valueFactory">值工厂</param>
        /// <param name="trackAllValues">是否跟踪所有值</param>
        public NamedThreadLocal(string name, System.Func<T> valueFactory, bool trackAllValues) : base(valueFactory, trackAllValues)
        {
            _name = name;
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