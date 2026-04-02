// Copyright (c) 2025 WellTool Team
// Licensed under the Apache License, Version 2.0 (the "License");

using WellTool.Cron.Pattern;
using Xunit;

namespace WellTool.Cron.Tests
{
    /// <summary>
    /// IssueI9FQUA 测试 - 秒匹配问题
    /// </summary>
    public class IssueI9FQUATest
    {
        /// <summary>
        /// 测试秒匹配
        /// </summary>
        [Fact]
        public void NextDateAfterTest()
        {
            var cron = "0/5 * * * * ?";
            var pattern = new CronPattern(cron);
            var begin = DateTime.Parse("2024-01-01 00:00:00");
            var nextDate = CronPatternUtil.NextDateAfter(pattern, begin);

            Assert.Equal(DateTime.Parse("2024-01-01 00:00:05"), nextDate);
        }
    }
}
