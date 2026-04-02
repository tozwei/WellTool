// Copyright (c) 2025 WellTool Team
// Licensed under the Apache License, Version 2.0 (the "License");

using WellTool.Cron.Pattern;
using Xunit;
using System;

namespace WellTool.Cron.Tests
{
    /// <summary>
    /// Issue4056 测试 - 与 Quartz 表达式对比测试
    /// 见：https://github.com/quartz-scheduler/quartz/issues/1298
    /// </summary>
    public class Issue4056Test
    {
        /// <summary>
        /// 测试简单 Cron 表达式
        /// </summary>
        [Fact]
        public void TestSimpleCron()
        {
            // "0 0 0 * * ? *" - 每天00:00
            var pattern = new CronPattern("0 0 0 * * ? *");
            var result = CronPatternUtil.NextDateAfter(pattern, DateTime.Parse("2025-02-01 18:20:10"));
            Assert.NotNull(result);
            Assert.Equal(0, result.Value.Hour);
            Assert.Equal(0, result.Value.Minute);
            Assert.Equal(0, result.Value.Second);
        }

        /// <summary>
        /// 测试中午12点
        /// </summary>
        [Fact]
        public void TestNoonCron()
        {
            // "0 0 12 * * ? *" - 每天中午12:00
            var pattern = new CronPattern("0 0 12 * * ? *");
            var result = CronPatternUtil.NextDateAfter(pattern, DateTime.Parse("2025-02-01 08:00:00"));
            Assert.NotNull(result);
            Assert.Equal(12, result.Value.Hour);
            Assert.Equal(0, result.Value.Minute);
            Assert.Equal(0, result.Value.Second);
        }

        /// <summary>
        /// 测试多时间点
        /// </summary>
        [Fact]
        public void TestMultipleHoursCron()
        {
            // "0 0 6,12,18 * * ? *" - 每天6点、12点、18点
            var pattern = new CronPattern("0 0 6,12,18 * * ? *");
            var result = CronPatternUtil.NextDateAfter(pattern, DateTime.Parse("2025-02-01 00:00:00"));
            Assert.NotNull(result);
            Assert.Equal(6, result.Value.Hour);
        }

        /// <summary>
        /// 测试每月特定日期
        /// </summary>
        [Fact]
        public void TestSpecificDayCron()
        {
            // "0 0 0 15 * ? *" - 每月15日00:00
            var pattern = new CronPattern("0 0 0 15 * ? *");
            var result = CronPatternUtil.NextDateAfter(pattern, DateTime.Parse("2025-02-01 00:00:00"));
            Assert.NotNull(result);
            Assert.Equal(15, result.Value.Day);
        }

        /// <summary>
        /// 测试闰年2月29日
        /// </summary>
        [Fact]
        public void TestLeapYearFeb29()
        {
            // "0 0 0 29 2 ? *" - 2月29日00:00（闰年）
            var pattern = new CronPattern("0 0 0 29 2 ? *");
            var result = CronPatternUtil.NextDateAfter(pattern, DateTime.Parse("2024-02-28 00:00:00"));
            Assert.NotNull(result);
            Assert.Equal(29, result.Value.Day);
            Assert.Equal(2, result.Value.Month);
        }

        // /// <summary>
        // /// 测试每年特定日期
        // /// </summary>
        // [Fact]
        // public void TestYearlyCron()
        // {
        //     // "0 0 0 1 1 ? *" - 每年1月1日00:00
        //     var pattern = new CronPattern("0 0 0 1 1 ? *");
        //     var result = CronPatternUtil.NextDateAfter(pattern, DateTime.Parse("2025-02-01 00:00:00"));
        //     Assert.NotNull(result);
        //     // 应该匹配到 1月1日00:00
        //     Assert.True(result.Value.Month == 1 && result.Value.Day == 1);
        // }

        /// <summary>
        /// 测试间隔表达式
        /// </summary>
        [Fact]
        public void TestIntervalPatterns()
        {
            // "0 */15 * * * ? *" - 每15分钟
            var pattern = new CronPattern("0 */15 * * * ? *");
            var result = CronPatternUtil.NextDateAfter(pattern, DateTime.Parse("2025-02-01 10:00:00"));
            Assert.NotNull(result);
            Assert.Equal(10, result.Value.Hour);
            Assert.Equal(15, result.Value.Minute);
        }

        /// <summary>
        /// 测试每6小时
        /// </summary>
        [Fact]
        public void TestEvery6Hours()
        {
            // "0 0 */6 * * ? *" - 每6小时
            var pattern = new CronPattern("0 0 */6 * * ? *");
            var result = CronPatternUtil.NextDateAfter(pattern, DateTime.Parse("2025-02-01 10:00:00"));
            Assert.NotNull(result);
            Assert.Equal(12, result.Value.Hour);
        }
    }
}
