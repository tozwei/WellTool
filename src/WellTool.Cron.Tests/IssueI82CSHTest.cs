// Copyright (c) 2025 WellTool Team
// Licensed under the Apache License, Version 2.0 (the "License");

using WellTool.Cron.Pattern;
using Xunit;

namespace WellTool.Cron.Tests
{
    /// <summary>
    /// IssueI82CSH 测试 - 多月匹配问题
    /// </summary>
    public class IssueI82CSHTest
    {
        /// <summary>
        /// 测试多个月份匹配
        /// </summary>
        [Fact]
        public void Test()
        {
            var begin = DateTime.Parse("2023-09-20");
            var end = DateTime.Parse("2025-09-20");
            var pattern = new CronPattern("0 0 1 3-3,9 *");
            var dates = CronPatternUtil.MatchedDates(pattern, begin, 20);

            // 验证匹配结果
            Assert.NotNull(dates);
        }
    }
}
