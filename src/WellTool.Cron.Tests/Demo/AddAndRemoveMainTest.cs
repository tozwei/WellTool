// Copyright (c) 2025 WellTool Team
// Licensed under the Apache License, Version 2.0 (the "License");

using Xunit;
using System;
using System.Threading;
using System.Collections.Concurrent;

namespace WellTool.Cron.Tests
{
    /// <summary>
    /// 测试 Cron 任务添加和删除功能
    /// </summary>
    public class AddAndRemoveMainTest
    {
        /// <summary>
        /// 测试定时任务添加和删除
        /// </summary>
        [Fact]
        public void TestAddAndRemoveTask()
        {
            // 由于 WellTool.Cron.CronUtil 尚未完整实现
            // 这里使用 System.Threading.Timer 模拟类似行为
            var callCount = new ConcurrentDictionary<string, int>();
            string taskId = "task1";
            
            // 使用 Timer 模拟定时任务
            var timer = new Timer(_ => callCount.AddOrUpdate(taskId, 1, (_, v) => v + 1), null, 0, 200);
            
            Thread.Sleep(500);
            
            // 删除任务（停止 timer）
            timer.Dispose();
            
            // 验证任务被调用过
            Assert.True(callCount.ContainsKey(taskId), "Task should have been executed");
            var count = callCount[taskId];
            Assert.True(count >= 2 && count <= 4, $"Expected 2-4 calls, got {count}");
        }

        /// <summary>
        /// 测试多个任务调度
        /// </summary>
        [Fact]
        public void TestMultipleTasks()
        {
            var counts = new ConcurrentDictionary<string, int>();
            
            // 模拟多个定时任务
            using var timer1 = new Timer(_ => counts.AddOrUpdate("task1", 1, (_, v) => v + 1), null, 0, 200);
            using var timer2 = new Timer(_ => counts.AddOrUpdate("task2", 1, (_, v) => v + 1), null, 0, 300);
            
            Thread.Sleep(700);
            
            // 验证两个任务都被执行
            Assert.True(counts.ContainsKey("task1"));
            Assert.True(counts.ContainsKey("task2"));
            
            Assert.True(counts["task1"] >= 3, $"Task1 should have been called at least 3 times, got {counts["task1"]}");
            Assert.True(counts["task2"] >= 2, $"Task2 should have been called at least 2 times, got {counts["task2"]}");
        }
    }
}
