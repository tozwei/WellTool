using WellTool.Cron.Pattern;
using Xunit;
using System;

namespace WellTool.Cron.Tests.Pattern
{
    /// <summary>
    /// Cron 模式下次匹配时间测试（对应 Hutool CronPatternNextMatchTest）
    /// </summary>
    public class CronPatternNextMatchTests
    {
        [Fact]
        public void NextMatchAllAfterTest()
        {
            // 匹配所有，返回下一秒的时间
            var pattern = new CronPattern("* * * * * *");
            var date = DateTime.Now.Date.AddHours(DateTime.Now.Hour).AddMinutes(DateTime.Now.Minute).AddSeconds(DateTime.Now.Second);
            var next = pattern.NextMatch(date);

            Assert.NotNull(next);
            Assert.True(next.Value > date);

            // 匹配所有分，返回下一分钟
            pattern = new CronPattern("0 * * * * *");
            date = new DateTime(2022, 4, 8, 7, 44, 16);
            next = pattern.NextMatch(date);

            Assert.NotNull(next);
            Assert.Equal(new DateTime(2022, 4, 8, 7, 45, 0), next.Value);

            // 匹配所有时，返回下一小时
            pattern = new CronPattern("0 0 * * * *");
            date = new DateTime(2022, 4, 8, 7, 44, 16);
            next = pattern.NextMatch(date);

            Assert.NotNull(next);
            Assert.Equal(new DateTime(2022, 4, 8, 8, 0, 0), next.Value);

            // 匹配所有天，返回明日
            pattern = new CronPattern("0 0 0 * * *");
            date = new DateTime(2022, 4, 8, 7, 44, 16);
            next = pattern.NextMatch(date);

            Assert.NotNull(next);
            Assert.Equal(new DateTime(2022, 4, 9, 0, 0, 0), next.Value);

            // 匹配所有月，返回下一月
            pattern = new CronPattern("0 0 0 1 * *");
            date = new DateTime(2022, 4, 8, 7, 44, 16);
            next = pattern.NextMatch(date);

            Assert.NotNull(next);
            Assert.Equal(new DateTime(2022, 5, 1, 0, 0, 0), next.Value);
        }

        [Fact]
        public void NextMatchAfterTest()
        {
            var pattern = new CronPattern("23 12 * 12 * *");

            // 时间正常递增
            var calendar = pattern.NextMatch(new DateTime(2022, 4, 12, 9, 12, 12));
            Assert.NotNull(calendar);
            Assert.True(pattern.Match(calendar.Value));
            Assert.Equal(new DateTime(2022, 4, 12, 9, 12, 23), calendar.Value);

            // 秒超出规定值的最大值，分 +1，秒取最小值
            calendar = pattern.NextMatch(new DateTime(2022, 4, 12, 9, 9, 24));
            Assert.NotNull(calendar);
            Assert.True(pattern.Match(calendar.Value));
            Assert.Equal(new DateTime(2022, 4, 12, 9, 12, 23), calendar.Value);

            // 秒超出规定值的最大值，分不变，小时 +1，秒和分使用最小值
            calendar = pattern.NextMatch(new DateTime(2022, 4, 12, 9, 12, 24));
            Assert.NotNull(calendar);
            Assert.True(pattern.Match(calendar.Value));
            Assert.Equal(new DateTime(2022, 4, 12, 10, 12, 23), calendar.Value);

            // 天超出规定值的最大值，月 +1，天、时、分、秒取最小值
            calendar = pattern.NextMatch(new DateTime(2022, 4, 13, 9, 12, 24));
            Assert.NotNull(calendar);
            Assert.True(pattern.Match(calendar.Value));
            Assert.Equal(new DateTime(2022, 5, 12, 0, 12, 23), calendar.Value);

            // 跨年
            calendar = pattern.NextMatch(new DateTime(2021, 12, 22, 0, 0, 0));
            Assert.NotNull(calendar);
            Assert.True(pattern.Match(calendar.Value));
            Assert.Equal(new DateTime(2022, 1, 12, 0, 12, 23), calendar.Value);
        }

        [Fact]
        public void NextMatchAfterByWeekTest()
        {
            // 每周六 01:01:01 执行
            var pattern = new CronPattern("1 1 1 * * Sat");

            // 周日，下个周六在 4 月 9 日
            var time = new DateTime(2022, 4, 3); // 周日
            var calendar = pattern.NextMatch(time);

            Assert.NotNull(calendar);
            Assert.Equal(new DateTime(2022, 4, 9, 1, 1, 1), calendar.Value);
            Assert.Equal(DayOfWeek.Saturday, calendar.Value.DayOfWeek);
        }

        [Fact]
        public void LastDayOfMonthForEveryMonthTest()
        {
            // 匹配所有月，生成每个月的最后一天
            var pattern = new CronPattern("1 2 3 L * ?");
            var date = new DateTime(2023, 1, 8, 7, 44, 16);

            // 测试第一个月
            var calendar = pattern.NextMatch(date);

            Assert.NotNull(calendar);
            Assert.Equal(31, calendar.Value.Day); // 1 月最后一天是 31 日
            Assert.Equal(3, calendar.Value.Hour);
            Assert.Equal(2, calendar.Value.Minute);
            Assert.Equal(1, calendar.Value.Second);
        }

        [Fact]
        public void LastDayOfMonthForFebruaryTest()
        {
            // 匹配每年 2 月的最后一天
            var pattern = new CronPattern("1 2 3 L 2 ?");

            // 2023 年 2 月最后一天是 28 日
            var date = new DateTime(2023, 1, 8, 7, 44, 16);
            var calendar = pattern.NextMatch(date);

            Assert.NotNull(calendar);
            Assert.Equal(2023, calendar.Value.Year);
            Assert.Equal(2, calendar.Value.Month);
            Assert.Equal(28, calendar.Value.Day); // 平年 2 月 28 天
            Assert.Equal(3, calendar.Value.Hour);
            Assert.Equal(2, calendar.Value.Minute);
            Assert.Equal(1, calendar.Value.Second);

            // 2024 年是闰年，2 月有 29 天
            date = new DateTime(2023, 3, 8, 7, 44, 16);
            calendar = pattern.NextMatch(date);

            Assert.NotNull(calendar);
            Assert.Equal(2024, calendar.Value.Year);
            Assert.Equal(2, calendar.Value.Month);
            Assert.Equal(29, calendar.Value.Day); // 闰年 2 月 29 天
        }

        [Fact]
        public void EveryHourTest()
        {
            // 每小时执行
            var pattern = new CronPattern("1 2 */1 * * ?");
            var date = new DateTime(2022, 2, 28, 7, 44, 16);

            var calendar = pattern.NextMatch(date);

            Assert.NotNull(calendar);
            Assert.Equal(8, calendar.Value.Hour); // 下一个小时
            Assert.Equal(2, calendar.Value.Minute);
            Assert.Equal(1, calendar.Value.Second);
        }

        [Fact]
        public void SpecificTimeMatchTest()
        {
            // 特定时间匹配测试
            var pattern = new CronPattern("30 15 10 * * ?");

            // 当天 10:15:30 之前，应该匹配当天
            var before = new DateTime(2022, 4, 8, 9, 0, 0);
            var next = pattern.NextMatch(before);

            Assert.NotNull(next);
            Assert.Equal(new DateTime(2022, 4, 8, 10, 15, 30), next.Value);

            // 当天 10:15:30 之后，应该匹配第二天
            var after = new DateTime(2022, 4, 8, 11, 0, 0);
            next = pattern.NextMatch(after);

            Assert.NotNull(next);
            Assert.Equal(new DateTime(2022, 4, 9, 10, 15, 30), next.Value);
        }

        [Fact]
        public void MonthBoundaryTest()
        {
            // 月份边界测试
            var pattern = new CronPattern("0 0 0 1 * ?");

            // 1 月 31 日，下个月应该是 2 月 1 日
            var date = new DateTime(2022, 1, 31, 12, 0, 0);
            var next = pattern.NextMatch(date);

            Assert.NotNull(next);
            Assert.Equal(new DateTime(2022, 2, 1, 0, 0, 0), next.Value);

            // 12 月 31 日，下个月应该是次年 1 月 1 日
            date = new DateTime(2022, 12, 31, 12, 0, 0);
            next = pattern.NextMatch(date);

            Assert.NotNull(next);
            Assert.Equal(new DateTime(2023, 1, 1, 0, 0, 0), next.Value);
        }

        [Fact]
        public void WeekdayMatchTest()
        {
            // 工作日（周一到周五）测试
            var pattern = new CronPattern("0 0 9 ? * MON-FRI");

            // 周六，下个工作日是周一
            var saturday = new DateTime(2022, 4, 9); // 周六
            var next = pattern.NextMatch(saturday);

            Assert.NotNull(next);
            Assert.Equal(DayOfWeek.Monday, next.Value.DayOfWeek);
            Assert.Equal(9, next.Value.Hour);
        }

        [Fact]
        public void ComplexPatternMatchTest()
        {
            // 复杂模式匹配测试
            var pattern = new CronPattern("0 30 8 ? * MON,WED,FRI");

            // 周二，下次执行应该是周三
            var tuesday = new DateTime(2022, 4, 5, 10, 0, 0); // 周二
            var next = pattern.NextMatch(tuesday);

            Assert.NotNull(next);
            Assert.Equal(DayOfWeek.Wednesday, next.Value.DayOfWeek);
            Assert.Equal(8, next.Value.Hour);
            Assert.Equal(30, next.Value.Minute);
        }
    }
}