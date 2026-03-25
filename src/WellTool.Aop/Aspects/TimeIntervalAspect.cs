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

using System.Diagnostics;
using System.Reflection;

namespace WellTool.Aop.Aspects
{
    /// <summary>
    /// 通过日志打印方法的执行时间的切面
    /// </summary>
    public class TimeIntervalAspect : SimpleAspect
    {
        private readonly Stopwatch stopwatch = new Stopwatch();

        /// <summary>
        /// 目标方法执行前的操作
        /// </summary>
        /// <param name="target">目标对象</param>
        /// <param name="method">目标方法</param>
        /// <param name="args">参数</param>
        /// <returns>是否继续执行接下来的操作</returns>
        public override bool Before(object target, MethodInfo method, object[] args)
        {
            stopwatch.Start();
            return true;
        }

        /// <summary>
        /// 目标方法执行后的操作
        /// </summary>
        /// <param name="target">目标对象</param>
        /// <param name="method">目标方法</param>
        /// <param name="args">参数</param>
        /// <param name="returnVal">目标方法执行返回值</param>
        /// <returns>是否允许返回值（接下来的操作）</returns>
        public override bool After(object target, MethodInfo method, object[] args, object returnVal)
        {
            stopwatch.Stop();
            Console.WriteLine($"Method [{target.GetType().Name}.{method.Name}] execute spend [{stopwatch.ElapsedMilliseconds}]ms return value [{returnVal}]");
            stopwatch.Reset();
            return true;
        }
    }
}