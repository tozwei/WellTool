using Xunit;
using WellTool.Cron.Pattern;
using System;

namespace WellTool.Cron.Tests.Pattern
{
    /// <summary>
    /// Cron 模式下一次匹配时间测试
    /// </summary>
    public class CronPatternNextMatchTests
    {
        [Fact]
        public void NextMatchAllAfterTest()
        {
            // 测试每小时的第 45 分钟执行
            var pattern = new CronPattern("0 45 * * * *");
            var baseTime = new DateTime(2022, 4, 8, 7, 44, 17);
            var nextMatch = pattern.NextMatchingAfter(baseTime);

            Assert.NotNull(nextMatch);
            Assert.Equal(new DateTime(2022, 4, 8, 7, 45, 0), nextMatch.Value);
        }

        [Fact]
        public void NextMatchAfterTest()
        {
            // 测试每秒执行
            var pattern = new CronPattern("* * * * * *");
            var baseTime = new DateTime(2022, 4, 12, 9, 12, 13);
            var nextMatch = pattern.NextMatchingAfter(baseTime);

            Assert.NotNull(nextMatch);
            Assert.Equal(new DateTime(2022, 4, 12, 9, 12, 14), nextMatch.Value);
        }

        [Fact]
        public void NextMatchAfterByWeekTest()
        {
            // 测试每周六执行
            var pattern = new CronPattern("0 0 0 ? * SAT");
            var baseTime = new DateTime(2022, 4, 3, 0, 0, 0);
            var nextMatch = pattern.NextMatchingAfter(baseTime);

            Assert.NotNull(nextMatch);
            Assert.Equal(new DateTime(2022, 4, 9, 0, 0, 0), nextMatch.Value);
        }

        [Fact]
        public void LastDayOfMonthForEveryMonthTest()
        {
            // 测试每月最后一天执行
            var pattern = new CronPattern("0 0 0 L * ?");
            var baseTime = new DateTime(2022, 4, 8, 0, 0, 0);
            var nextMatch = pattern.NextMatchingAfter(baseTime);

            Assert.NotNull(nextMatch);
            Assert.Equal(30, nextMatch.Value.Day); // 4月的最后一天是 30 日
        }

        [Fact]
        public void LastDayOfMonthForFebruaryTest()
        {
            // 测试 2 月最后一天执行
            var pattern = new CronPattern("0 0 0 L * ?");
            var baseTime = new DateTime(2022, 2, 1, 0, 0, 0);
            var nextMatch = pattern.NextMatchingAfter(baseTime);

            Assert.NotNull(nextMatch);
            Assert.Equal(28, nextMatch.Value.Day); // 2022 年 2 月的最后一天是 28 日
        }

        [Fact]
        public void EveryHourTest()
        {
            // 测试每小时执行
            var pattern = new CronPattern("0 0 * * * *");
            var baseTime = new DateTime(2022, 4, 8, 7, 30, 0);
            var nextMatch = pattern.NextMatchingAfter(baseTime);

            Assert.NotNull(nextMatch);
            Assert.Equal(8, nextMatch.Value.Hour);
        }

        [Fact]
        public void SpecificTimeMatchTest()
        {
            // 测试特定时间执行
            var pattern = new CronPattern("30 15 10 * * ?");
            var baseTime = new DateTime(2022, 4, 8, 9, 0, 0);
            var nextMatch = pattern.NextMatchingAfter(baseTime);

            Assert.NotNull(nextMatch);
            Assert.Equal(new DateTime(2022, 4, 8, 10, 15, 30), nextMatch.Value);
        }

        [Fact]
        public void MonthBoundaryTest()
        {
            // 测试月份边界
            var pattern = new CronPattern("0 0 0 1 * ?");
            var baseTime = new DateTime(2022, 1, 31, 12, 0, 0);
            var nextMatch = pattern.NextMatchingAfter(baseTime);

            Assert.NotNull(nextMatch);
            Assert.Equal(new DateTime(2022, 2, 1, 0, 0, 0), nextMatch.Value);
        }

        [Fact]
        public void WeekdayMatchTest()
        {
            // 测试工作日执行
            var pattern = new CronPattern("0 0 0 ? * MON-FRI");
            var baseTime = new DateTime(2022, 4, 9, 0, 0, 0); // 周六
            var nextMatch = pattern.NextMatchingAfter(baseTime);

            Assert.NotNull(nextMatch);
            Assert.Equal(DayOfWeek.Monday, nextMatch.Value.DayOfWeek);
        }

        [Fact]
        public void ComplexPatternMatchTest()
        {
            // 测试复杂模式
            var pattern = new CronPattern("0 30 9 ? * WED");
            var baseTime = new DateTime(2022, 4, 8, 0, 0, 0);
            var nextMatch = pattern.NextMatchingAfter(baseTime);

            Assert.NotNull(nextMatch);
            Assert.Equal(DayOfWeek.Wednesday, nextMatch.Value.DayOfWeek);
        }
    }
}