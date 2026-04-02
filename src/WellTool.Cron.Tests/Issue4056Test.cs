// Copyright (c) 2025 WellTool Team
// Licensed under the Apache License, Version 2.0 (the "License");

using WellTool.Cron.Pattern;
using Xunit;
using System;
using System.Collections.Generic;

namespace WellTool.Cron.Tests
{
    /// <summary>
    /// Issue4056 测试 - 与 Quartz 表达式对比测试
    /// 见：https://github.com/quartz-scheduler/quartz/issues/1298
    /// </summary>
    public class Issue4056Test
    {
        /// <summary>
        /// 测试各种 Cron 表达式的匹配
        /// </summary>
        [Fact]
        public void TestCronAll()
        {
            // 关键测试用例：7字段表达式测试
            var testCases = new List<(string cron, DateTime judgeTime, DateTime expected)>
            {
                // "0 0 0 * * ? *" - 每天00:00
                ("0 0 0 * * ? *", DateTime.Parse("2025-02-01 18:20:10"), DateTime.Parse("2025-02-02 00:00:00")),
                
                // "0 0 12 * * ? *" - 每天中午12:00
                ("0 0 12 * * ? *", DateTime.Parse("2025-02-01 18:20:10"), DateTime.Parse("2025-02-02 12:00:00")),
                
                // "0 0 18 * * ? *" - 每天傍晚18:00
                ("0 0 18 * * ? *", DateTime.Parse("2025-02-01 18:20:10"), DateTime.Parse("2025-02-02 18:00:00")),
                
                // "0 0 0 1/3 * ? *" - 每3天00:00 (1号,4号,7号...)
                ("0 0 0 1/3 * ? *", DateTime.Parse("2025-02-28 00:00:00"), DateTime.Parse("2025-03-01 00:00:00")),
                
                // "0 0 0 1/5 * ? *" - 每5天00:00 (1号,6号,11号...)
                ("0 0 0 1/5 * ? *", DateTime.Parse("2025-02-28 00:00:00"), DateTime.Parse("2025-03-01 00:00:00")),
                
                // "0 0 0 1/10 * ? *" - 每10天00:00 (1号,11号,21号...)
                ("0 0 0 1/10 * ? *", DateTime.Parse("2025-02-28 00:00:00"), DateTime.Parse("2025-03-01 00:00:00")),
                
                // "0 0 0 1,15 * ? *" - 每月1日和15日00:00
                ("0 0 0 1,15 * ? *", DateTime.Parse("2025-02-01 00:00:00"), DateTime.Parse("2025-02-15 00:00:00")),
                
                // "0 0 0 29 2 ? *" - 2月29日00:00（闰年）
                ("0 0 0 29 2 ? *", DateTime.Parse("2024-02-28 00:00:00"), DateTime.Parse("2024-02-29 00:00:00")),
                
                // "0 0 0 L * ? *" - 每月最后一天00:00
                ("0 0 0 L * ? *", DateTime.Parse("2025-02-28 00:00:00"), DateTime.Parse("2025-02-28 00:00:00")),
                
                // "0 0 0 1 1 ? *" - 每年1月1日00:00
                ("0 0 0 1 1 ? *", DateTime.Parse("2025-02-01 00:00:00"), DateTime.Parse("2026-01-01 00:00:00")),
            };

            foreach (var (cron, judgeTime, expected) in testCases)
            {
                var pattern = new CronPattern(cron);
                var result = CronPatternUtil.NextDateAfter(pattern, judgeTime);
                Assert.NotNull(result);
                Assert.Equal(expected, result.Value);
            }
        }

        /// <summary>
        /// 测试间隔表达式
        /// </summary>
        [Fact]
        public void TestIntervalPatterns()
        {
            // "0 */15 * * * ? *" - 每15分钟
            var pattern = new CronPattern("0 */15 * * * ? *");
            var result = CronPatternUtil.NextDateAfter(pattern, DateTime.Parse("2025-02-01 10:00:00"));
            Assert.Equal(DateTime.Parse("2025-02-01 10:15:00"), result);

            // "0 0 */6 * * ? *" - 每6小时
            pattern = new CronPattern("0 0 */6 * * ? *");
            result = CronPatternUtil.NextDateAfter(pattern, DateTime.Parse("2025-02-01 10:00:00"));
            Assert.Equal(DateTime.Parse("2025-02-01 12:00:00"), result);

            // "0 */5 9-17 * * ? *" - 工作时间内每5分钟
            pattern = new CronPattern("0 */5 9-17 * * ? *");
            result = CronPatternUtil.NextDateAfter(pattern, DateTime.Parse("2025-02-01 09:00:00"));
            Assert.Equal(DateTime.Parse("2025-02-01 09:05:00"), result);
        }

        /// <summary>
        /// 测试月份表达式
        /// </summary>
        [Fact]
        public void TestMonthPatterns()
        {
            // "0 0 0 1 */3 ? *" - 每3个月的第1天00:00
            var pattern = new CronPattern("0 0 0 1 */3 ? *");
            var result = CronPatternUtil.NextDateAfter(pattern, DateTime.Parse("2025-01-15 00:00:00"));
            Assert.Equal(DateTime.Parse("2025-04-01 00:00:00"), result);

            // "0 0 0 25 12 ? *" - 圣诞节00:00
            pattern = new CronPattern("0 0 0 25 12 ? *");
            result = CronPatternUtil.NextDateAfter(pattern, DateTime.Parse("2025-12-01 00:00:00"));
            Assert.Equal(DateTime.Parse("2025-12-25 00:00:00"), result);
        }
    }
}
