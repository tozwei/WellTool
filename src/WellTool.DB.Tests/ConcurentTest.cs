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

using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace WellTool.DB.Tests;

/// <summary>
/// 并发测试
/// </summary>
public class ConcurentTest
{
    /// <summary>
    /// 测试并发操作
    /// </summary>
    [Fact]
    public void TestConcurent()
    {
        // 测试并发操作
        var taskCount = 10;
        var tasks = new List<Task>();
        var counter = 0;
        var mutex = new object();

        // 创建多个任务并发执行
        for (int i = 0; i < taskCount; i++)
        {
            tasks.Add(Task.Run(() =>
            {
                // 模拟数据库操作
                for (int j = 0; j < 100; j++)
                {
                    lock (mutex)
                    {
                        counter++;
                    }
                    // 模拟操作延迟
                    Thread.Sleep(1);
                }
            }));
        }

        // 等待所有任务完成
        Task.WaitAll(tasks.ToArray());

        // 验证计数器值是否正确
        Assert.Equal(taskCount * 100, counter);
    }
} 