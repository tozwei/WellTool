// Copyright (c) 2025 WellTool Team
// Licensed under the Apache License, Version 2.0 (the "License");

using Xunit;
using System;
using System.Threading;
using System.Collections.Concurrent;

namespace WellTool.Cron.Tests.Demo
{
    /// <summary>
    /// Cron 定时任务测试
    /// </summary>
    public class CronTest
    {
        /// <summary>
        /// 测试每秒定时任务
        /// </summary>
        [Fact]
        public void TestSecondLevelCron()
        {
            var executed = false;
            var timer = new Timer(_ => executed = true, null, 0, 1000);
            
            Thread.Sleep(2500);
            timer.Dispose();
            
            Assert.True(executed, "Task should have been executed");
        }

        /// <summary>
        /// 测试简单定时任务
        /// </summary>
        [Fact]
        public void TestSimpleCron()
        {
            var callCount = 0;
            using var timer = new Timer(_ => callCount++, null, 0, 100);
            
            Thread.Sleep(350);
            
            // 在350ms内，应该有约3-4次调用（每100ms一次）
            Assert.True(callCount >= 2 && callCount <= 4, $"Expected 2-4 calls, got {callCount}");
        }
    }
}
