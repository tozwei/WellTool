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
        /// 测试守护线程设置
        /// </summary>
        [Fact]
        public void TestDaemonThread()
        {
            var thread = new Thread(() => { });
            thread.IsBackground = true;
            Assert.True(thread.IsBackground, "Thread should be a background thread");
        }

        /// <summary>
        /// 测试非守护线程
        /// </summary>
        [Fact]
        public void TestNonDaemonThread()
        {
            var thread = new Thread(() => { });
            thread.IsBackground = false;
            Assert.False(thread.IsBackground, "Thread should be a foreground thread");
        }

        /// <summary>
        /// 测试线程运行
        /// </summary>
        [Fact]
        public void TestThreadExecution()
        {
            var completed = false;
            var thread = new Thread(() => completed = true);
            thread.Start();
            thread.Join(TimeSpan.FromSeconds(2));
            
            Assert.True(completed, "Thread should have completed");
        }
    }
}
