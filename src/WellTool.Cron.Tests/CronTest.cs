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
using System.Threading;
using Xunit;
using WellTool.Cron;
using WellTool.Cron.Pattern;

namespace WellTool.Cron.Tests
{
    /// <summary>
    /// Cron测试类
    /// </summary>
    public class CronTest
    {
        /// <summary>
        /// 测试Cron表达式解析
        /// </summary>
        [Fact]
        public void TestCronPatternParsing()
        {
            // 测试标准Cron表达式（5个字段）
            var pattern1 = new CronPattern("0 0 12 * *");
            Assert.NotNull(pattern1);

            // 测试带秒的Cron表达式（6个字段）
            var pattern2 = new CronPattern("0 0 0 12 * *", true);
            Assert.NotNull(pattern2);
        }

        /// <summary>
        /// 测试Cron表达式匹配
        /// </summary>
        [Fact]
        public void TestCronPatternMatching()
        {
            // 测试每天12:00:00匹配
            var pattern = new CronPattern("0 0 12 * *");
            var now = DateTime.Now;
            var testTime = new DateTime(now.Year, now.Month, now.Day, 12, 0, 0);
            Assert.True(pattern.Match(testTime));

            // 测试非匹配时间
            var nonMatchTime = new DateTime(now.Year, now.Month, now.Day, 12, 0, 1);
            Assert.False(pattern.Match(nonMatchTime));
        }

        /// <summary>
        /// 测试定时任务调度
        /// </summary>
        [Fact]
        public void TestTaskScheduling()
        {
            // 重置默认调度器
            CronUtil.SetDefaultScheduler(new Scheduler());

            // 标记任务是否执行
            bool taskExecuted = false;

            // 添加一个1秒后执行的任务
            string taskId = CronUtil.Schedule("* * * * * *", () =>
            {
                taskExecuted = true;
            });

            // 启动调度器
            CronUtil.SetMatchSecond(true);
            CronUtil.Start();

            // 等待2秒
            Thread.Sleep(2000);

            // 停止调度器
            CronUtil.Stop(true);

            // 验证任务是否执行
            Assert.True(taskExecuted);
        }

        /// <summary>
        /// 测试任务管理
        /// </summary>
        [Fact]
        public void TestTaskManagement()
        {
            // 重置默认调度器
            CronUtil.SetDefaultScheduler(new Scheduler());

            // 添加任务
            string taskId = CronUtil.Schedule("0 0 12 * *", () => { });
            Assert.False(CronUtil.IsEmpty());
            Assert.Equal(1, CronUtil.Size());

            // 移除任务
            CronUtil.Deschedule(taskId);
            Assert.True(CronUtil.IsEmpty());
            Assert.Equal(0, CronUtil.Size());
        }

        /// <summary>
        /// 测试CronUtil静态方法
        /// </summary>
        [Fact]
        public void TestCronUtilStaticMethods()
        {
            // 重置默认调度器
            CronUtil.SetDefaultScheduler(new Scheduler());

            // 测试设置时区
            var timeZone = TimeZoneInfo.Local;
            CronUtil.SetTimeZone(timeZone);
            Assert.Equal(timeZone, CronUtil.GetTimeZone());

            // 测试设置守护线程
            CronUtil.SetDaemon(true);
            Assert.True(CronUtil.IsDaemon());

            // 测试设置秒匹配
            CronUtil.SetMatchSecond(true);
            Assert.True(CronUtil.IsMatchSecond());
        }
    }
}
