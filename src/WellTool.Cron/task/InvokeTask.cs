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

using System;
using System.Reflection;

namespace WellTool.Cron.Task
{
    /// <summary>
    /// 反射调用任务，通过反射执行指定类的方法
    /// </summary>
    public class InvokeTask : Task
    {
        /// <summary>
        /// 类名
        /// </summary>
        private readonly string className;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="className">类名，格式为：namespace.ClassName.MethodName</param>
        public InvokeTask(string className)
        {
            this.className = className ?? throw new ArgumentNullException(nameof(className));
        }

        /// <summary>
        /// 执行任务
        /// </summary>
        public void Execute()
        {
            try
            {
                // 解析类名和方法名
                string[] parts = className.Split('.');
                if (parts.Length < 2)
                {
                    throw new CronException("Invalid class name format: {0}, expected format: namespace.ClassName.MethodName", className);
                }

                string methodName = parts[parts.Length - 1];
                string classFullName = string.Join(".", parts, 0, parts.Length - 1);

                // 加载类型
                Type type = Type.GetType(classFullName);
                if (type == null)
                {
                    // 尝试从所有已加载的程序集中查找
                    foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                    {
                        type = assembly.GetType(classFullName);
                        if (type != null)
                        {
                            break;
                        }
                    }

                    if (type == null)
                    {
                        throw new CronException("Class not found: {0}", classFullName);
                    }
                }

                // 获取方法
                MethodInfo method = type.GetMethod(methodName, BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
                if (method == null)
                {
                    throw new CronException("Method not found: {0} in class {1}", methodName, classFullName);
                }

                // 创建实例（如果方法不是静态的）
                object instance = null;
                if (!method.IsStatic)
                {
                    instance = Activator.CreateInstance(type);
                }

                // 执行方法
                method.Invoke(instance, null);
            }
            catch (System.Exception ex)
            {
                throw new CronException(ex, "Invoke task failed: {0}", className);
            }
        }
    }
}