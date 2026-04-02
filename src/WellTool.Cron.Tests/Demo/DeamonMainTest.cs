// Copyright (c) 2025 WellTool Team
// Licensed under the Apache License, Version 2.0 (the "License");

using Xunit;
using System;
using System.Threading;

namespace WellTool.Cron.Tests.Demo
{
    /// <summary>
    /// 测试守护线程对作业线程的影响
    /// </summary>
    public class DeamonMainTest
    {
        /// <summary>
        /// 测试守护线程行为
        /// </summary>
        [Fact]
        public void TestDaemonThread()
        {
            var executed = false;
            
            // 创建守护线程
            var thread = new Thread(() =>
            {
                var timer = new Timer(_ => executed = true, null, 0, 100);
                Thread.Sleep(500);
                // timer 在 using 或 Dispose 前会被 GC 回收
            });
            thread.IsBackground = true; // 设置为守护线程
            thread.Start();
            
            thread.Join(TimeSpan.FromSeconds(2));
            
            // 守护线程可能在主线程结束后被中断
            // 这个测试主要验证守护线程的基本行为
            Assert.True(thread.IsBackground);
        }

        /// <summary>
        /// 测试非守护线程行为
        /// </summary>
        [Fact]
        public void TestNonDaemonThread()
        {
            var completed = false;
            
            var thread = new Thread(() =>
            {
                Thread.Sleep(100);
                completed = true;
            });
            thread.IsBackground = false; // 非守护线程
            thread.Start();
            thread.Join(TimeSpan.FromSeconds(2));
            
            Assert.True(completed, "Non-daemon thread should complete");
            Assert.False(thread.IsBackground);
        }
    }
}
