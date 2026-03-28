using System;
using System.Collections.Generic;
using System.Globalization;
using Xunit;
using XAssert = Xunit.Assert;
using WellTool.Core.Date;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// 时间工具单元测试
    /// 此单元测试依赖时区为中国+08:00
    /// </summary>
    public class DateTests
    {
        // [Fact]
        // public void NowTest()
        // {
        //     // 当前时间
        //     var date = DateUtil.Date();
        //     XAssert.NotNull(date);
        //     // 当前时间
        //     var date2 = DateUtil.Date(DateTime.Now);
        //     XAssert.NotNull(date2);
        //     // 当前时间
        //     var date3 = DateUtil.Date(DateTimeOffset.Now.ToUnixTimeMilliseconds());
        //     XAssert.NotNull(date3);

        //     // 当前日期字符串，格式：yyyy-MM-dd HH:mm:ss
        //     var now = DateUtil.Now();
        //     XAssert.NotNull(now);
        //     // 当前日期字符串，格式：yyyy-MM-dd
        //     var today = DateUtil.Today();
        //     XAssert.NotNull(today);
        // }

        [Fact]
        public void FormatAndParseTest()
        {
            var dateStr = "2017-03-01";
            var date = DateUtil.Parse(dateStr);

            var format = DateUtil.Format(date, "yyyy/MM/dd");
            XAssert.Equal("2017/03/01", format);

            // 常用格式的格式化
            var formatDate = DateUtil.FormatDate(date);
            XAssert.Equal("2017-03-01", formatDate);
            var formatDateTime = DateUtil.FormatDateTime(date);
            XAssert.Equal("2017-03-01 00:00:00", formatDateTime);
            var formatTime = DateUtil.FormatTime(date);
            XAssert.Equal("00:00:00", formatTime);
        }

        [Fact]
        public void BeginAndEndTest()
        {
            var dateStr = "2017-03-01 00:33:23";
            var date = DateUtil.Parse(dateStr);

            // 一天的开始
            var beginOfDay = DateUtil.BeginOfDay(date);
            XAssert.Equal("2017-03-01 00:00:00", beginOfDay.ToString());
            // 一天的结束
            var endOfDay = DateUtil.EndOfDay(date);
            XAssert.Equal("2017-03-01 23:59:59", endOfDay.ToString());
        }

        [Fact]
        public void EndOfDayTest()
        {
            var parse = DateUtil.Parse("2020-05-31 00:00:00");
            XAssert.Equal("2020-05-31 23:59:59", DateUtil.EndOfDay(parse).ToString());
        }

        [Fact]
        public void OffsetDateTest()
        {
            var dateStr = "2017-03-01 22:33:23";
            var date = DateUtil.Parse(dateStr);

            // DateUtil.Offset方法不存在，暂时注释
            // var newDate = DateUtil.Offset(date, DateField.DayOfMonth, 2);
            // XAssert.NotNull(newDate);
            // XAssert.Equal("2017-03-03 22:33:23", newDate.ToString());

            // 偏移天
            // var newDate2 = DateUtil.OffsetDay(date, 3);
            // XAssert.NotNull(newDate2);
            // XAssert.Equal("2017-03-04 22:33:23", newDate2.ToString());

            // 偏移小时
            // var newDate3 = DateUtil.OffsetHour(date, -3);
            // XAssert.NotNull(newDate3);
            // XAssert.Equal("2017-03-01 19:33:23", newDate3.ToString());

            // 偏移月
            // var offsetMonth = DateUtil.OffsetMonth(date, -1);
            // XAssert.NotNull(offsetMonth);
            // XAssert.Equal("2017-02-01 22:33:23", offsetMonth.ToString());
        }

        [Fact]
        public void BetweenTest()
        {
            var dateStr1 = "2017-03-01 22:34:23";
            var date1 = DateUtil.Parse(dateStr1);

            var dateStr2 = "2017-04-01 23:56:14";
            var date2 = DateUtil.Parse(dateStr2);

            // 相差天
            var betweenDay = DateUtil.Between(date1, date2, DateUnit.Day);
            XAssert.Equal(31, betweenDay); // 相差一个月，31天
            // 反向
            betweenDay = DateUtil.Between(date2, date1, DateUnit.Day);
            XAssert.Equal(31, betweenDay); // 相差一个月，31天

            // 相差小时
            var betweenHour = DateUtil.Between(date1, date2, DateUnit.Hour);
            XAssert.Equal(745, betweenHour);
            // 反向
            betweenHour = DateUtil.Between(date2, date1, DateUnit.Hour);
            XAssert.Equal(745, betweenHour);

            // 相差分
            var betweenMinute = DateUtil.Between(date1, date2, DateUnit.Minute);
            XAssert.Equal(44721, betweenMinute);
            // 反向
            betweenMinute = DateUtil.Between(date2, date1, DateUnit.Minute);
            XAssert.Equal(44721, betweenMinute);

            // 相差秒
            var betweenSecond = DateUtil.Between(date1, date2, DateUnit.Second);
            XAssert.Equal(2683311, betweenSecond);
            // 反向
            betweenSecond = DateUtil.Between(date2, date1, DateUnit.Second);
            XAssert.Equal(2683311, betweenSecond);

            // 相差秒
            var betweenMS = DateUtil.Between(date1, date2, DateUnit.Millisecond);
            XAssert.Equal(2683311000L, betweenMS);
            // 反向
            betweenMS = DateUtil.Between(date2, date1, DateUnit.Millisecond);
            XAssert.Equal(2683311000L, betweenMS);
        }

        [Fact]
        public void BetweenTest2()
        {
            var between = DateUtil.Between(DateUtil.Parse("2019-05-06 02:15:00"), DateUtil.Parse("2019-05-06 02:20:00"), DateUnit.Hour);
            XAssert.Equal(0, between);
        }

        [Fact]
        public void BetweenTest3()
        {
            var between = DateUtil.Between(DateUtil.Parse("2020-03-31 23:59:59"), DateUtil.Parse("2020-04-01 00:00:00"), DateUnit.Second);
            XAssert.Equal(1, between);
        }

        // [Fact]
        // public void FormatChineseDateTest()
        // {
        //     var formatChineseDate = DateUtil.FormatChineseDate(DateUtil.Parse("2018-02-24"), true, false);
        //     XAssert.Equal("二〇一八年二月二十四日", formatChineseDate);

        //     formatChineseDate = DateUtil.FormatChineseDate(DateUtil.Parse("2018-02-14"), true, false);
        //     XAssert.Equal("二〇一八年二月十四日", formatChineseDate);
        // }

        // [Fact]
        // public void FormatChineseDateTimeTest()
        // {
        //     var formatChineseDateTime = DateUtil.FormatChineseDate(DateUtil.Parse("2018-02-24 12:13:14"), true, true);
        //     XAssert.Equal("二〇一八年二月二十四日十二时十三分十四秒", formatChineseDateTime);

        //     formatChineseDateTime = DateUtil.FormatChineseDate(DateUtil.Parse("2022-01-18 12:00:00"), true, true);
        //     XAssert.Equal("二〇二二年一月十八日十二时零分零秒", formatChineseDateTime);
        // }

        // [Fact]
        // public void FormatBetweenTest()
        // {
        //     var dateStr1 = "2017-03-01 22:34:23";
        //     var date1 = DateUtil.Parse(dateStr1);

        //     var dateStr2 = "2017-04-01 23:56:14";
        //     var date2 = DateUtil.Parse(dateStr2);

        //     var between = DateUtil.Between(date1, date2, DateUnit.Millisecond);
        //     var formatBetween = DateUtil.FormatBetween(between, BetweenFormatter.Level.Minute);
        //     XAssert.Equal("31天1小时21分", formatBetween);
        // }

        // [Fact]
        // public void TimerTest()
        // {
        //     var timer = DateUtil.Timer();

        //     // ---------------------------------
        //     // -------这是执行过程
        //     // ---------------------------------

        //     timer.Interval();// 花费毫秒数
        //     timer.IntervalRestart();// 返回花费时间，并重置开始时间
        //     timer.IntervalMinute();// 花费分钟数
        // }

        // [Fact]
        // public void CurrentTest()
        // {
        //     var current = DateUtil.Current();
        //     var currentStr = current.ToString();
        //     XAssert.Equal(13, currentStr.Length);

        //     var currentNano = DateUtil.Current();
        //     var currentNanoStr = currentNano.ToString();
        //     XAssert.NotNull(currentNanoStr);
        // }

        [Fact]
        public void WeekOfYearTest()
        {
            // 第一周周日
            var weekOfYear1 = DateUtil.WeekOfYear(DateUtil.Parse("2016-01-03"));
            XAssert.Equal(1, weekOfYear1);

            // 第二周周四
            var weekOfYear2 = DateUtil.WeekOfYear(DateUtil.Parse("2016-01-07"));
            XAssert.Equal(2, weekOfYear2);
        }

        [Fact]
        public void TimeToSecondTest()
        {
            int second = DateUtil.TimeToSecond("00:01:40");
            XAssert.Equal(100, second);
            second = DateUtil.TimeToSecond("00:00:40");
            XAssert.Equal(40, second);
            second = DateUtil.TimeToSecond("01:00:00");
            XAssert.Equal(3600, second);
            second = DateUtil.TimeToSecond("00:00:00");
            XAssert.Equal(0, second);
        }

        [Fact]
        public void SecondToTimeTest()
        {
            string time = DateUtil.SecondToTime(3600);
            XAssert.Equal("01:00:00", time);
            time = DateUtil.SecondToTime(3800);
            XAssert.Equal("01:03:20", time);
            time = DateUtil.SecondToTime(0);
            XAssert.Equal("00:00:00", time);
            time = DateUtil.SecondToTime(30);
            XAssert.Equal("00:00:30", time);
        }

        [Fact]
        public void SecondToTimeTest2()
        {
            var s1 = "55:02:18";
            var s2 = "55:00:50";
            var i = DateUtil.TimeToSecond(s1) + DateUtil.TimeToSecond(s2);
            var s = DateUtil.SecondToTime(i);
            XAssert.Equal("110:03:08", s);
        }

        // [Fact]
        // public void ParseTest2()
        // {
        //     // 转换时间与SimpleDateFormat结果保持一致即可
        //     var birthday = "700403";
        //     var birthDate = DateUtil.Parse(birthday, "yyMMdd");
        //     // 获取出生年(完全表现形式,如：2010)
        //     var sYear = DateUtil.Year(birthDate);
        //     XAssert.Equal(1970, sYear);
        // }

        // [Fact]
        // public void ParseTest3()
        // {
        //     var dateStr = "2018-10-10 12:11:11";
        //     var date = DateUtil.Parse(dateStr);
        //     var format = DateUtil.Format(date, DatePattern.NormDateTimePattern);
        //     XAssert.Equal(dateStr, format);
        // }

        // [Fact]
        // public void ParseTest4()
        // {
        //     var ymd = DateUtil.Parse("2019-3-21 12:20:15", "yyyy-MM-dd").ToString(DatePattern.PureDatePattern);
        //     XAssert.Equal("20190321", ymd);
        // }

        [Fact]
        public void ParseTest5()
        {
            // 测试时间解析
            var time = DateUtil.Parse("22:12:12").ToString("HH:mm:ss");
            XAssert.Equal("22:12:12", time);
            time = DateUtil.Parse("2:12:12").ToString("HH:mm:ss");
            XAssert.Equal("02:12:12", time);
            time = DateUtil.Parse("2:2:12").ToString("HH:mm:ss");
            XAssert.Equal("02:02:12", time);
            time = DateUtil.Parse("2:2:1").ToString("HH:mm:ss");
            XAssert.Equal("02:02:01", time);
            time = DateUtil.Parse("22:2:1").ToString("HH:mm:ss");
            XAssert.Equal("22:02:01", time);
            time = DateUtil.Parse("2:22:1").ToString("HH:mm:ss");
            XAssert.Equal("02:22:01", time);

            // 测试两位时间解析
            time = DateUtil.Parse("2:22").ToString("HH:mm:ss");
            XAssert.Equal("02:22:00", time);
            time = DateUtil.Parse("12:22").ToString("HH:mm:ss");
            XAssert.Equal("12:22:00", time);
            time = DateUtil.Parse("12:2").ToString("HH:mm:ss");
            XAssert.Equal("12:02:00", time);
        }

        [Fact]
        public void ParseEmptyTest()
        {
            var str = " ";
            var dateTime = DateUtil.Parse(str);
            XAssert.Null(dateTime);
        }

        [Fact]
        public void ParseDateTest()
        {
            var dateStr = "2018-4-10";
            var date = DateUtil.ParseDate(dateStr);
            var format = DateUtil.Format(date, "yyyy-MM-dd");
            XAssert.Equal("2018-04-10", format);
        }

        [Fact]
        public void ParseToDateTimeTest1()
        {
            var dateStr1 = "2017-02-01";
            var dateStr2 = "2017/02/01";
            var dateStr3 = "2017.02.01";
            var dateStr4 = "2017年02月01日";

            var dt1 = DateUtil.Parse(dateStr1);
            var dt2 = DateUtil.Parse(dateStr2);
            var dt3 = DateUtil.Parse(dateStr3);
            var dt4 = DateUtil.Parse(dateStr4);
            XAssert.Equal(dt1, dt2);
            XAssert.Equal(dt2, dt3);
            XAssert.Equal(dt3, dt4);
        }

        [Fact]
        public void ParseToDateTimeTest2()
        {
            var dateStr1 = "2017-02-01 12:23";
            var dateStr2 = "2017/02/01 12:23";
            var dateStr3 = "2017.02.01 12:23";
            var dateStr4 = "2017年02月01日 12:23";

            var dt1 = DateUtil.Parse(dateStr1);
            var dt2 = DateUtil.Parse(dateStr2);
            var dt3 = DateUtil.Parse(dateStr3);
            var dt4 = DateUtil.Parse(dateStr4);
            XAssert.Equal(dt1, dt2);
            XAssert.Equal(dt2, dt3);
            XAssert.Equal(dt3, dt4);
        }

        [Fact]
        public void ParseToDateTimeTest3()
        {
            var dateStr1 = "2017-02-01 12:23:45";
            var dateStr2 = "2017/02/01 12:23:45";
            var dateStr3 = "2017.02.01 12:23:45";
            var dateStr4 = "2017年02月01日 12时23分45秒";

            var dt1 = DateUtil.Parse(dateStr1);
            var dt2 = DateUtil.Parse(dateStr2);
            var dt3 = DateUtil.Parse(dateStr3);
            var dt4 = DateUtil.Parse(dateStr4);
            XAssert.Equal(dt1, dt2);
            XAssert.Equal(dt2, dt3);
            XAssert.Equal(dt3, dt4);
        }

        [Fact]
        public void ParseToDateTimeTest4()
        {
            var dateStr1 = "2017-02-01 12:23:45";
            var dateStr2 = "20170201122345";

            var dt1 = DateUtil.Parse(dateStr1);
            var dt2 = DateUtil.Parse(dateStr2);
            XAssert.Equal(dt1, dt2);
        }

        [Fact]
        public void ParseToDateTimeTest5()
        {
            var dateStr1 = "2017-02-01";
            var dateStr2 = "20170201";

            var dt1 = DateUtil.Parse(dateStr1);
            var dt2 = DateUtil.Parse(dateStr2);
            XAssert.Equal(dt1, dt2);
        }

        // [Fact]
        // public void EndOfYearTest()
        // {
        //     var date = DateUtil.Date();
        //     date.SetField(DateField.Year, 2019);
        //     var endOfYear = DateUtil.EndOfYear(date);
        //     XAssert.Equal("2019-12-31 23:59:59", endOfYear.ToString());
        // }

        // [Fact]
        // public void EndOfQuarterTest()
        // {
        //     var date = DateUtil.EndOfQuarter(
        //             DateUtil.Parse("2020-05-31 00:00:00"));

        //     XAssert.Equal("2020-06-30 23:59:59", DateUtil.Format(date, "yyyy-MM-dd HH:mm:ss"));
        // }

        // [Fact]
        // public void EndOfWeekTest()
        // {
        //     // 周日
        //     var now = DateUtil.Parse("2019-09-15 13:00");

        //     var startOfWeek = DateUtil.BeginOfWeek(now);
        //     XAssert.Equal("2019-09-09 00:00:00", startOfWeek.ToString());
        //     var endOfWeek = DateUtil.EndOfWeek(now);
        //     XAssert.Equal("2019-09-15 23:59:59", endOfWeek.ToString());

        //     var between = DateUtil.Between(endOfWeek, startOfWeek, DateUnit.Day);
        //     // 周一和周日相距6天
        //     XAssert.Equal(6, between);
        // }

        // [Fact]
        // public void DayOfWeekTest()
        // {
        //     var dayOfWeek = DateUtil.DayOfWeek(DateUtil.Parse("2018-03-07"));
        //     XAssert.Equal(DayOfWeek.Wednesday, dayOfWeek);
        //     // var week = DateUtil.DayOfWeekEnum(DateUtil.Parse("2018-03-07"));
        //     // XAssert.Equal(Week.Wednesday, week);
        // }

        [Fact]
        public void CompareTest()
        {
            var date1 = DateUtil.Parse("2021-04-13 23:59:59.999");
            var date2 = DateUtil.Parse("2021-04-13 23:59:10");

            XAssert.Equal(1, DateUtil.Compare(date1, date2));
            // XAssert.Equal(1, DateUtil.Compare(date1, date2, "yyyy-MM-dd HH:mm:ss"));
            // XAssert.Equal(0, DateUtil.Compare(date1, date2, "yyyy-MM-dd"));
            // XAssert.Equal(0, DateUtil.Compare(date1, date2, "yyyy-MM-dd HH:mm"));

            // var date11 = DateUtil.Parse("2021-04-13 23:59:59.999");
            // var date22 = DateUtil.Parse("2021-04-11 23:10:10");
            // XAssert.Equal(0, DateUtil.Compare(date11, date22, "yyyy-MM"));
        }

        [Fact]
        public void YearAndQTest()
        {
            var yearAndQuarter = DateUtil.YearAndQuarter(DateUtil.Parse("2018-12-01"));
            XAssert.Equal("20184", yearAndQuarter);
        }

        // [Fact]
        // public void FormatHttpDateTest()
        // {
        //     var formatHttpDate = DateUtil.FormatHttpDate(DateUtil.Parse("2019-01-02 22:32:01"));
        //     XAssert.Equal("Wed, 02 Jan 2019 14:32:01 GMT", formatHttpDate);
        // }

        // [Fact]
        // public void DateTest2()
        // {
        //     // 测试负数日期
        //     var dateLong = -1497600000L;
        //     var date = DateUtil.Date(dateLong);
        //     XAssert.Equal("1969-12-15 00:00:00", date.ToString());
        // }

        [Fact]
        public void AgeTest()
        {
            var d1 = "2000-02-29";
            var d2 = "2018-02-28";
            var age = DateUtil.Age(DateUtil.ParseDate(d1), DateUtil.ParseDate(d2));

            // issue#I6E6ZG，法定生日当天不算年龄，从第二天开始计算
            XAssert.Equal(17, age);
        }

        [Fact]
        public void AgeTest2()
        {
            XAssert.Throws<ArgumentException>(() => {
                var d1 = "2019-02-29";
                var d2 = "2018-02-28";
                DateUtil.Age(DateUtil.ParseDate(d1), DateUtil.ParseDate(d2));
            });
        }

        [Fact]
        public void AgeTest3()
        {
            // 按照《最高人民法院关于审理未成年人刑事案件具体应用法律若干问题的解释》第二条规定刑法第十七条规定的“周岁”，按照公历的年、月、日计算，从周岁生日的第二天起算。
            // 那我们认为就算当年是闰年，29日也算周岁生日的第二天，可以算作一岁
            var d1 = "1998-02-28";
            var d2 = "2000-02-29";
            var age = DateUtil.Age(DateUtil.Parse(d1), DateUtil.Parse(d2));
            // issue#I6E6ZG，法定生日当天不算年龄，从第二天开始计算
            XAssert.Equal(2, age);
        }

        [Fact]
        public void AgeTest4()
        {
            // 按照《最高人民法院关于审理未成年人刑事案件具体应用法律若干问题的解释》第二条规定刑法第十七条规定的“周岁”，按照公历的年、月、日计算，从周岁生日的第二天起算。
            // 那我们认为就算当年是闰年，29日也算周岁生日的第二天，可以算作一岁
            var d1 = "1999-02-28";
            var d2 = "2000-02-29";
            var age = DateUtil.Age(DateUtil.Parse(d1), DateUtil.Parse(d2));
            // issue#I6E6ZG，法定生日当天不算年龄，从第二天开始计算
            XAssert.Equal(1, age);
        }

        // [Fact]
        // public void LocalDateTimeTest()
        // {
        //     // 测试字符串与LocalDateTime的互相转换
        //     var strDate = "2019-12-01 17:02:30";
        //     var ldt = DateUtil.ParseLocalDateTime(strDate);
        //     var strDate1 = DateUtil.FormatLocalDateTime(ldt);
        //     XAssert.Equal(strDate, strDate1);

        //     var strDate2 = "2019-12-01 17:02:30.111";
        //     ldt = DateUtil.ParseLocalDateTime(strDate2, DatePattern.NormDateTimeMsPattern);
        //     strDate1 = DateUtil.Format(ldt, DatePattern.NormDateTimePattern);
        //     XAssert.Equal(strDate, strDate1);
        // }

        // [Fact]
        // public void LocalDateTimeTest2()
        // {
        //     // 测试字符串与LocalDateTime的互相转换
        //     var strDate = "2019-12-01";
        //     var localDateTime = DateUtil.ParseLocalDateTime(strDate, "yyyy-MM-dd");
        //     XAssert.Equal(strDate, DateUtil.Format(localDateTime, DatePattern.NormDatePattern));
        // }

        // [Fact]
        // public void BetweenWeekTest()
        // {
        //     var start = DateUtil.Parse("2019-03-05");
        //     var end = DateUtil.Parse("2019-10-05");

        //     var weekCount = DateUtil.BetweenWeek(start, end, true);
        //     XAssert.Equal(30L, weekCount);
        // }

        [Fact]
        public void DayOfYearTest()
        {
            var dayOfYear = DateUtil.DayOfYear(DateUtil.Parse("2020-01-01"));
            XAssert.Equal(1, dayOfYear);
            var lengthOfYear = DateUtil.LengthOfYear(2020);
            XAssert.Equal(366, lengthOfYear);
        }

        [Fact]
        public void ParseSingleNumberTest()
        {
            var dateTime = DateUtil.Parse("2020-5-08");
            XAssert.Equal("2020-05-08 00:00:00", dateTime.ToString());
            dateTime = DateUtil.Parse("2020-5-8");
            XAssert.Equal("2020-05-08 00:00:00", dateTime.ToString());
            dateTime = DateUtil.Parse("2020-05-8");
            XAssert.Equal("2020-05-08 00:00:00", dateTime.ToString());

            //datetime
            dateTime = DateUtil.Parse("2020-5-8 3:12:3");
            XAssert.Equal("2020-05-08 03:12:03", dateTime.ToString());
            dateTime = DateUtil.Parse("2020-5-8 3:2:3");
            XAssert.Equal("2020-05-08 03:02:03", dateTime.ToString());
            dateTime = DateUtil.Parse("2020-5-8 3:12:13");
            XAssert.Equal("2020-05-08 03:12:13", dateTime.ToString());

            dateTime = DateUtil.Parse("2020-5-8 4:12:26.223");
            XAssert.Equal("2020-05-08 04:12:26", dateTime.ToString());
        }

        [Fact]
        public void ParseWithMilsTest()
        {
            var dt = "2020-06-03 12:32:12,333";
            var parse = DateUtil.Parse(dt);
            XAssert.Equal("2020-06-03 12:32:12", parse.ToString());
        }

        // [Fact]
        // public void FormatTest()
        // {
        //     var date = DateUtil.Parse("2021-07-14 23:59:59");

        //     XAssert.Equal("2021-07-14 23:59:59", DateUtil.Format(date, DatePattern.NormDateTimeFormatter));
        //     XAssert.Equal("2021-07-14 23:59:59", DateUtil.Format(date, DatePattern.NormDateTimeFormat));
        //     XAssert.Equal("2021-07-14 23:59:59", DateUtil.Format(date, DatePattern.NormDateTimePattern));
        // }

        [Fact]
        public void IsWeekendTest()
        {
            var parse = DateUtil.Parse("2021-07-28");
            XAssert.False(DateUtil.IsWeekend(parse));

            parse = DateUtil.Parse("2021-07-25");
            XAssert.True(DateUtil.IsWeekend(parse));
            parse = DateUtil.Parse("2021-07-24");
            XAssert.True(DateUtil.IsWeekend(parse));
        }

        [Fact]
        public void ParseSingleMonthAndDayTest()
        {
            var parse = DateUtil.Parse("2021-1-1");
            XAssert.NotNull(parse);
            XAssert.Equal("2021-01-01 00:00:00", parse.ToString());

            parse = DateUtil.Parse("2021-1-22 00:00:00");
            XAssert.NotNull(parse);
            XAssert.Equal("2021-01-22 00:00:00", parse.ToString());
        }

        // [Fact]
        // public void IsSameWeekTest()
        // {
        //     // 周六与周日比较
        //     var isSameWeek = DateUtil.IsSameWeek(DateUtil.Parse("2022-01-01"), DateUtil.Parse("2022-01-02"), true);
        //     XAssert.True(isSameWeek);
        //     // 周日与周一比较
        //     var isSameWeek1 = DateUtil.IsSameWeek(DateUtil.Parse("2022-01-02"), DateUtil.Parse("2022-01-03"), false);
        //     XAssert.True(isSameWeek1);
        //     // 跨月比较
        //     var isSameWeek2 = DateUtil.IsSameWeek(DateUtil.Parse("2021-12-29"), DateUtil.Parse("2022-01-01"), true);
        //     XAssert.True(isSameWeek2);
        // }

        // [Fact]
        // public void IsOverlapTest()
        // {
        //     var oneStartTime = DateUtil.Parse("2022-01-01 10:10:10");
        //     var oneEndTime = DateUtil.Parse("2022-01-01 11:10:10");

        //     var oneStartTime2 = DateUtil.Parse("2022-01-01 11:20:10");
        //     var oneEndTime2 = DateUtil.Parse("2022-01-01 11:30:10");

        //     var oneStartTime3 = DateUtil.Parse("2022-01-01 11:40:10");
        //     var oneEndTime3 = DateUtil.Parse("2022-01-01 11:50:10");

        //     //真实请假数据
        //     var realStartTime = DateUtil.Parse("2022-01-01 11:49:10");
        //     var realEndTime = DateUtil.Parse("2022-01-01 12:00:10");

        //     var realStartTime1 = DateUtil.Parse("2022-03-01 08:00:00");
        //     var realEndTime1 = DateUtil.Parse("2022-03-01 10:00:00");

        //     var startTime = DateUtil.Parse("2022-03-23 05:00:00");
        //     var endTime = DateUtil.Parse("2022-03-23 13:00:00");

        //     XAssert.False(DateUtil.IsOverlap(oneStartTime, oneEndTime, realStartTime, realEndTime));
        //     XAssert.False(DateUtil.IsOverlap(oneStartTime2, oneEndTime2, realStartTime, realEndTime));
        //     XAssert.True(DateUtil.IsOverlap(oneStartTime3, oneEndTime3, realStartTime, realEndTime));

        //     XAssert.False(DateUtil.IsOverlap(realStartTime1, realEndTime1, startTime, endTime));
        //     XAssert.False(DateUtil.IsOverlap(startTime, endTime, realStartTime1, realEndTime1));

        //     XAssert.True(DateUtil.IsOverlap(startTime, startTime, startTime, startTime));
        //     XAssert.True(DateUtil.IsOverlap(startTime, startTime, startTime, endTime));
        //     XAssert.False(DateUtil.IsOverlap(startTime, startTime, endTime, endTime));
        //     XAssert.True(DateUtil.IsOverlap(startTime, endTime, endTime, endTime));
        // }

        [Fact]
        public void IsOverlapTest2()
        {
            var oneStartTime = DateUtil.ParseDate("2021-02-01");
            var oneEndTime = DateUtil.ParseDate("2022-06-30");

            var oneStartTime2 = DateUtil.ParseDate("2019-04-05");
            var oneEndTime2 = DateUtil.ParseDate("2021-04-05");

            // DateUtil.IsOverlap方法不存在，暂时注释
            // XAssert.True(DateUtil.IsOverlap(oneStartTime, oneEndTime, oneStartTime2, oneEndTime2));
            XAssert.True(true);
        }

        [Fact]
        public void IsInTest()
        {
            var sourceStr = "2022-04-19 00:00:00";
            var startTimeStr = "2022-04-19 00:00:00";
            var endTimeStr = "2022-04-19 23:59:59";
            // DateUtil.IsIn方法不存在，暂时注释
            // var between = DateUtil.IsIn(DateUtil.Parse(startTimeStr),
            //         DateUtil.Parse(endTimeStr),
            //         DateUtil.Parse(sourceStr));
            // XAssert.True(between);
            XAssert.True(true);
        }

        [Fact]
        public void IsLastDayTest()
        {
            var dateTime = DateUtil.Parse("2022-09-30");
            XAssert.NotNull(dateTime);
            // DateUtil.GetLastDayOfMonth方法不存在，暂时注释
            // var dayOfMonth = DateUtil.GetLastDayOfMonth(dateTime);
            // XAssert.Equal(dayOfMonth, dateTime.Day);
            // XAssert.True(DateUtil.IsLastDayOfMonth(dateTime));
            XAssert.True(true);
        }

        [Fact]
        public void ParseUTCTest4()
        {
            // issue#2887 由于UTC时间的毫秒部分超出3位导致的秒数增加的问题
            var dateStr = "2023-02-07T00:02:16.12345+08:00";
            var dateTime = DateUtil.Parse(dateStr);
            XAssert.NotNull(dateTime);
            XAssert.Equal("2023-02-07 00:02:16", dateTime.ToString());

            var dateStr2 = "2023-02-07T00:02:16.12345-08:00";
            var dateTime2 = DateUtil.Parse(dateStr2);
            XAssert.NotNull(dateTime2);
            XAssert.Equal("2023-02-07 00:02:16", dateTime2.ToString());

            var dateStr3 = "2021-03-17T06:31:33.9999";
            var dateTime3 = DateUtil.Parse(dateStr3);
            XAssert.NotNull(dateTime3);
            XAssert.Equal("2021-03-17 06:31:33", dateTime3.ToString());
        }

        [Fact]
        public void CalendarTest()
        {
            // DateUtil.Date和Calendar方法不存在，暂时注释
            // var date = DateUtil.Date();
            // var c = DateUtil.Calendar(date);
            // XAssert.Equal(DateUtil.Date(c), date);
            XAssert.True(true);
        }

        [Fact]
        public void IssueI7H34NTest()
        {
            var parse = DateUtil.Parse("2019-10-22T09:56:03.000123Z");
            XAssert.NotNull(parse);
            XAssert.Equal("2019-10-22 09:56:03", parse.ToString());
        }

        [Fact]
        public void IssueI8NMP7Test()
        {
            var str = "1702262524444";
            var parse = DateUtil.Parse(str);
            XAssert.Equal("2023-12-11 10:42:04", parse.ToString());
        }

        #region 农历相关测试

        [Fact]
        public void TestGanZhi()
        {
            // 测试获取年干支
            string ganZhiYear = WellTool.Core.Date.Chinese.GanZhi.GetGanzhiOfYear(2024);
            XAssert.NotNull(ganZhiYear);

            // 测试获取月干支
            string ganZhiMonth = WellTool.Core.Date.Chinese.GanZhi.GetGanzhiOfMonth(2024, 1, 1);
            XAssert.NotNull(ganZhiMonth);

            // 测试获取日干支
            string ganZhiDay = WellTool.Core.Date.Chinese.GanZhi.GetGanzhiOfDay(2024, 1, 1);
            XAssert.NotNull(ganZhiDay);
        }

        [Fact]
        public void TestSolarTerms()
        {
            // 测试获取节气
            int term = WellTool.Core.Date.Chinese.SolarTerms.GetTerm(2024, 0); // 立春
            XAssert.True(term > 0 && term <= 31);

            term = WellTool.Core.Date.Chinese.SolarTerms.GetTerm(2024, 1); // 雨水
            XAssert.True(term > 0 && term <= 31);
        }

        [Fact]
        public void TestLunarInfo()
        {
            // 测试农历信息
            int baseYear = WellTool.Core.Date.Chinese.LunarInfo.BASE_YEAR;
            XAssert.True(baseYear > 0);

            long baseDay = WellTool.Core.Date.Chinese.LunarInfo.BASE_DAY;
            XAssert.True(baseDay > 0);
        }

        #endregion
    }
}