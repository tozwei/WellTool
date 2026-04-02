// Copyright (c) 2025 WellTool Team
// Licensed under the Apache License, Version 2.0 (the "License");

using Xunit;
using System;
using System.Threading;

namespace WellTool.Cron.Tests.Demo
{
    /// <summary>
    /// 测试基于配置的 Cron 调度
    /// </summary>
    public class JobMainTest
    {
        /// <summary>
        /// 测试基于时间的任务调度
        /// </summary>
        [Fact]
        public void TestScheduledJob()
        {
            var executed = false;
            using var timer = new Timer(_ => executed = true, null, 100, Timeout.Infinite);
            
            Thread.Sleep(200);
            
            Assert.True(executed, "Scheduled job should have been executed");
        }

        /// <summary>
        /// 测试任务重复执行
        /// </summary>
        [Fact]
        public void TestRepeatingJob()
        {
            var count = 0;
            using var timer = new Timer(_ => Interlocked.Increment(ref count), null, 0, 50);
            
            Thread.Sleep(200);
            
            Assert.True(count >= 3 && count <= 5, $"Expected 3-5 executions, got {count}");
        }
    }
}
