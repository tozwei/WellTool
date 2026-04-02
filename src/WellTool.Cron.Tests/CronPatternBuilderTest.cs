// Copyright (c) 2025 WellTool Team
// Licensed under the Apache License, Version 2.0 (the "License");

using WellTool.Cron.Pattern;
using Xunit;

namespace WellTool.Cron.Tests
{
    /// <summary>
    /// CronPatternBuilder 测试
    /// </summary>
    public class CronPatternBuilderTest
    {
        /// <summary>
        /// 测试构建匹配所有的表达式
        /// </summary>
        [Fact]
        public void BuildMatchAllTest()
        {
            var build = CronPatternBuilder.Of().Build();
            Assert.Equal("* * * * *", build);

            build = CronPatternBuilder.Of()
                .Set(Part.SECOND, "*")
                .Build();
            Assert.Equal("* * * * * *", build);

            build = CronPatternBuilder.Of()
                .Set(Part.SECOND, "*")
                .Set(Part.YEAR, "*")
                .Build();
            Assert.Equal("* * * * * * *", build);
        }

        /// <summary>
        /// 测试构建范围表达式
        /// </summary>
        [Fact]
        public void BuildRangeTest()
        {
            var build = CronPatternBuilder.Of()
                .Set(Part.SECOND, "*")
                .SetRange(Part.HOUR, 2, 9)
                .Build();
            Assert.Equal("* * 2-9 * * *", build);
        }

        /// <summary>
        /// 测试构建范围表达式时的错误
        /// </summary>
        [Fact]
        public void BuildRangeErrorTest()
        {
            Assert.Throws<CronException>(() =>
            {
                CronPatternBuilder.Of()
                    .Set(Part.SECOND, "*")
                    .SetRange(Part.HOUR, 2, 55)
                    .Build();
            });
        }
    }
}
