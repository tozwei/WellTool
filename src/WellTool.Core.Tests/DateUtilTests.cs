using System;
using Xunit;

namespace WellTool.Core.Tests
{
    public class DateUtilTests
    {
        [Fact]
        public void TestNow()
        {
            // 当前时间
            var date = WellTool.Core.Date.DateUtil.Now();
            Assert.True(date != default(DateTime));
            
            // 当前日期字符串，格式：yyyy-MM-dd HH:mm:ss
            var now = WellTool.Core.Date.DateUtil.NowString();
            Assert.NotNull(now);
            
            // 当前日期字符串，格式：yyyy-MM-dd
            var today = WellTool.Core.Date.DateUtil.Today();
            Assert.NotNull(today);
        }

        [Fact]
        public void TestFormatAndParse()
        {
            var dateStr = "2017-03-01";
            var date = WellTool.Core.Date.DateUtil.Parse(dateStr);
            
            var format = WellTool.Core.Date.DateUtil.Format(date, "yyyy/MM/dd");
            Assert.Equal("2017/03/01", format);
            
            // 常用格式的格式化
            var formatDate = WellTool.Core.Date.DateUtil.FormatDate(date);
            Assert.Equal("2017-03-01", formatDate);
            
            var formatDateTime = WellTool.Core.Date.DateUtil.FormatDateTime(date);
            Assert.Equal("2017-03-01 00:00:00", formatDateTime);
            
            var formatTime = WellTool.Core.Date.DateUtil.FormatTime(date);
            Assert.Equal("00:00:00", formatTime);
        }

        [Fact]
        public void TestBeginAndEndOfDay()
        {
            var dateStr = "2017-03-01 00:33:23";
            var date = WellTool.Core.Date.DateUtil.Parse(dateStr);
            
            // 一天的开始
            var beginOfDay = WellTool.Core.Date.DateUtil.BeginOfDay(date);
            Assert.Equal("2017-03-01 00:00:00", beginOfDay.ToString("yyyy-MM-dd HH:mm:ss"));
            
            // 一天的结束
            var endOfDay = WellTool.Core.Date.DateUtil.EndOfDay(date);
            Assert.Equal("2017-03-01 23:59:59", endOfDay.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        [Fact]
        public void TestOffsetDate()
        {
            var dateStr = "2017-03-01 22:33:23";
            var date = WellTool.Core.Date.DateUtil.Parse(dateStr);
            
            // 偏移天
            var newDate2 = WellTool.Core.Date.DateUtil.OffsetDay(date, 3);
            Assert.NotNull(newDate2);
            Assert.Equal("2017-03-04 22:33:23", newDate2.ToString("yyyy-MM-dd HH:mm:ss"));
            
            // 偏移小时
            var newDate3 = WellTool.Core.Date.DateUtil.OffsetHour(date, -3);
            Assert.NotNull(newDate3);
            Assert.Equal("2017-03-01 19:33:23", newDate3.ToString("yyyy-MM-dd HH:mm:ss"));
            
            // 偏移月
            var offsetMonth = WellTool.Core.Date.DateUtil.OffsetMonth(date, -1);
            Assert.NotNull(offsetMonth);
            Assert.Equal("2017-02-01 22:33:23", offsetMonth.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        [Fact]
        public void TestBetween()
        {
            var dateStr1 = "2017-03-01 22:34:23";
            var date1 = WellTool.Core.Date.DateUtil.Parse(dateStr1);
            
            var dateStr2 = "2017-04-01 23:56:14";
            var date2 = WellTool.Core.Date.DateUtil.Parse(dateStr2);
            
            // 相差天
            var betweenDay = WellTool.Core.Date.DateUtil.Between(date1, date2, WellTool.Core.Date.DateUnit.Day);
            Assert.Equal(31, betweenDay);
            
            // 相差小时
            var betweenHour = WellTool.Core.Date.DateUtil.Between(date1, date2, WellTool.Core.Date.DateUnit.Hour);
            Assert.Equal(745, betweenHour);
            
            // 相差分
            var betweenMinute = WellTool.Core.Date.DateUtil.Between(date1, date2, WellTool.Core.Date.DateUnit.Minute);
            Assert.Equal(44721, betweenMinute);
            
            // 相差秒
            var betweenSecond = WellTool.Core.Date.DateUtil.Between(date1, date2, WellTool.Core.Date.DateUnit.Second);
            Assert.Equal(2683311, betweenSecond);
        }

        [Fact]
        public void TestTimeToSecond()
        {
            int second = WellTool.Core.Date.DateUtil.TimeToSecond("00:01:40");
            Assert.Equal(100, second);
            
            second = WellTool.Core.Date.DateUtil.TimeToSecond("00:00:40");
            Assert.Equal(40, second);
            
            second = WellTool.Core.Date.DateUtil.TimeToSecond("01:00:00");
            Assert.Equal(3600, second);
            
            second = WellTool.Core.Date.DateUtil.TimeToSecond("00:00:00");
            Assert.Equal(0, second);
        }

        [Fact]
        public void TestSecondToTime()
        {
            string time = WellTool.Core.Date.DateUtil.SecondToTime(3600);
            Assert.Equal("01:00:00", time);
            
            time = WellTool.Core.Date.DateUtil.SecondToTime(3800);
            Assert.Equal("01:03:20", time);
            
            time = WellTool.Core.Date.DateUtil.SecondToTime(0);
            Assert.Equal("00:00:00", time);
            
            time = WellTool.Core.Date.DateUtil.SecondToTime(30);
            Assert.Equal("00:00:30", time);
        }

        [Fact]
        public void TestParseDate()
        {
            var dateStr = "2018-4-10";
            var date = WellTool.Core.Date.DateUtil.ParseDate(dateStr);
            var format = WellTool.Core.Date.DateUtil.Format(date, "yyyy-MM-dd");
            Assert.Equal("2018-04-10", format);
        }

        [Fact]
        public void TestWeekOfYear()
        {
            // 第一周周日
            var weekOfYear1 = WellTool.Core.Date.DateUtil.WeekOfYear(WellTool.Core.Date.DateUtil.Parse("2016-01-03"));
            Assert.Equal(1, weekOfYear1);
            
            // 第二周周四
            var weekOfYear2 = WellTool.Core.Date.DateUtil.WeekOfYear(WellTool.Core.Date.DateUtil.Parse("2016-01-07"));
            Assert.Equal(2, weekOfYear2);
        }

        [Fact]
        public void TestIsWeekend()
        {
            var parse = WellTool.Core.Date.DateUtil.Parse("2021-07-28");
            Assert.False(WellTool.Core.Date.DateUtil.IsWeekend(parse));
            
            parse = WellTool.Core.Date.DateUtil.Parse("2021-07-25");
            Assert.True(WellTool.Core.Date.DateUtil.IsWeekend(parse));
            
            parse = WellTool.Core.Date.DateUtil.Parse("2021-07-24");
            Assert.True(WellTool.Core.Date.DateUtil.IsWeekend(parse));
        }

        [Fact]
        public void TestDayOfYear()
        {
            var dayOfYear = WellTool.Core.Date.DateUtil.DayOfYear(WellTool.Core.Date.DateUtil.Parse("2020-01-01"));
            Assert.Equal(1, dayOfYear);
            
            var lengthOfYear = WellTool.Core.Date.DateUtil.LengthOfYear(2020);
            Assert.Equal(366, lengthOfYear);
        }

        [Fact]
        public void TestCompare()
        {
            var date1 = WellTool.Core.Date.DateUtil.Parse("2021-04-13 23:59:59.999");
            var date2 = WellTool.Core.Date.DateUtil.Parse("2021-04-13 23:59:10");
            
            Assert.Equal(1, WellTool.Core.Date.DateUtil.Compare(date1, date2));
        }

        [Fact]
        public void TestYearAndQuarter()
        {
            var yearAndQuarter = WellTool.Core.Date.DateUtil.YearAndQuarter(WellTool.Core.Date.DateUtil.Parse("2018-12-01"));
            Assert.Equal("20184", yearAndQuarter);
        }

        [Fact]
        public void TestIsLastDayOfMonth()
        {
            var dateTime = WellTool.Core.Date.DateUtil.Parse("2022-09-30");
            Assert.NotNull(dateTime);
            
            var dayOfMonth = WellTool.Core.Date.DateUtil.GetLastDayOfMonth(dateTime);
            Assert.Equal(30, dayOfMonth);
            
            Assert.True(WellTool.Core.Date.DateUtil.IsLastDayOfMonth(dateTime));
        }
    }
}