// Copyright (c) 2025 WellTool Team
// Licensed under the Apache License, Version 2.0 (the "License");

using WellTool.Cron.Pattern;
using Xunit;

namespace WellTool.Cron.Tests
{
    /// <summary>
    /// CronPatternUtil 测试
    /// </summary>
    public class CronPatternUtilTest
    {
        /// <summary>
        /// 测试每30秒执行的匹配日期
        /// </summary>
        [Fact]
        public void MatchedDatesTest()
        {
            var pattern = new CronPattern("0/30 * 8-18 * * ?");
            var begin = DateTime.Parse("2018-10-15 14:33:22");
            var matchedDates = CronPatternUtil.MatchedDates(pattern, begin, 5);

            Assert.Equal(5, matchedDates.Count);
            Assert.Equal(DateTime.Parse("2018-10-15 14:33:30"), matchedDates[0]);
            Assert.Equal(DateTime.Parse("2018-10-15 14:34:00"), matchedDates[1]);
            Assert.Equal(DateTime.Parse("2018-10-15 14:34:30"), matchedDates[2]);
            Assert.Equal(DateTime.Parse("2018-10-15 14:35:00"), matchedDates[3]);
            Assert.Equal(DateTime.Parse("2018-10-15 14:35:30"), matchedDates[4]);
        }

        /// <summary>
        /// 测试每小时执行的匹配日期
        /// </summary>
        [Fact]
        public void MatchedDatesTest2()
        {
            var pattern = new CronPattern("0 0 */1 * * *");
            var begin = DateTime.Parse("2018-10-15 14:33:22");
            var matchedDates = CronPatternUtil.MatchedDates(pattern, begin, 5);

            Assert.Equal(5, matchedDates.Count);
            Assert.Equal(DateTime.Parse("2018-10-15 15:00:00"), matchedDates[0]);
            Assert.Equal(DateTime.Parse("2018-10-15 16:00:00"), matchedDates[1]);
            Assert.Equal(DateTime.Parse("2018-10-15 17:00:00"), matchedDates[2]);
            Assert.Equal(DateTime.Parse("2018-10-15 18:00:00"), matchedDates[3]);
            Assert.Equal(DateTime.Parse("2018-10-15 19:00:00"), matchedDates[4]);
        }

        /// <summary>
        /// 测试最后一天
        /// </summary>
        [Fact]
        public void MatchedDatesTest3()
        {
            var pattern = new CronPattern("0 0 */1 L * *");
            var begin = DateTime.Parse("2018-10-30 23:33:22");
            var matchedDates = CronPatternUtil.MatchedDates(pattern, begin, 5);

            Assert.Equal(5, matchedDates.Count);
            Assert.Equal(DateTime.Parse("2018-10-31 00:00:00"), matchedDates[0]);
            Assert.Equal(DateTime.Parse("2018-10-31 01:00:00"), matchedDates[1]);
            Assert.Equal(DateTime.Parse("2018-10-31 02:00:00"), matchedDates[2]);
            Assert.Equal(DateTime.Parse("2018-10-31 03:00:00"), matchedDates[3]);
            Assert.Equal(DateTime.Parse("2018-10-31 04:00:00"), matchedDates[4]);
        }

        /// <summary>
        /// Issue4056 测试 - */5 和 1/5 意义相同
        /// </summary>
        [Fact]
        public void Issue4056Test()
        {
            var cron = "0 0 0 */5 * ? *";
            var pattern = new CronPattern(cron);

            // 2025-02-28不应该在匹配之列
            var dt1 = DateTime.Parse("2025-02-28 00:00:00");
            Assert.False(pattern.Match(dt1));

            var dt2 = DateTime.Parse("2025-03-01 00:00:00");
            Assert.True(pattern.Match(dt2));

            var dt3 = DateTime.Parse("2025-03-31 00:00:00");
            Assert.True(pattern.Match(dt3));
        }

        /// <summary>
        /// Issue4056 测试 - 计算下一个匹配日期
        /// </summary>
        [Fact]
        public void Issue4056Test2()
        {
            var cron = "0 0 0 */5 * ? *";
            var pattern = new CronPattern(cron);
            var judgeTime = DateTime.Parse("2025-02-27 23:59:59");
            var nextDate = CronPatternUtil.NextDateAfter(pattern, judgeTime);

            Assert.Equal(DateTime.Parse("2025-03-01 00:00:00"), nextDate);
        }
    }
}
