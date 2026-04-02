// Copyright (c) 2025 WellTool Team
// Licensed under the Apache License, Version 2.0 (the "License");

using WellTool.Cron.Pattern;
using Xunit;

namespace WellTool.Cron.Tests
{
    /// <summary>
    /// Issue3685 测试 - 周匹配问题
    /// </summary>
    public class Issue3685Test
    {
        /// <summary>
        /// 测试每周一匹配
        /// </summary>
        [Fact]
        public void NextDateAfterTest()
        {
            var pattern = new CronPattern("0 0 * * MON");
            var begin = DateTime.Parse("2024-08-01");
            var nextDate = CronPatternUtil.NextDateAfter(pattern, begin);
            Assert.Equal(DateTime.Parse("2024-08-05 00:00:00"), nextDate);

            begin = DateTime.Parse("2024-08-02");
            nextDate = CronPatternUtil.NextDateAfter(pattern, begin);
            Assert.Equal(DateTime.Parse("2024-08-05 00:00:00"), nextDate);

            begin = DateTime.Parse("2024-08-03");
            nextDate = CronPatternUtil.NextDateAfter(pattern, begin);
            Assert.Equal(DateTime.Parse("2024-08-05 00:00:00"), nextDate);

            begin = DateTime.Parse("2024-08-04");
            nextDate = CronPatternUtil.NextDateAfter(pattern, begin);
            Assert.Equal(DateTime.Parse("2024-08-05 00:00:00"), nextDate);

            begin = DateTime.Parse("2024-08-05");
            nextDate = CronPatternUtil.NextDateAfter(pattern, begin);
            Assert.Equal(DateTime.Parse("2024-08-12 00:00:00"), nextDate);
        }
    }
}
