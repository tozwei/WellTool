// Copyright (c) 2025 WellTool Team
// Licensed under the Apache License, Version 2.0 (the "License");

using System.Collections.Generic;
using WellTool.Cron.Pattern;
using WellTool.Cron.Pattern.Matcher;
using WellTool.Cron.Pattern.Parser;
using Xunit;

namespace WellTool.Cron.Tests
{
    /// <summary>
    /// IssueI7SMP7 测试 - 表达式解析问题
    /// </summary>
    public class IssueI7SMP7Test
    {
        /// <summary>
        /// 测试带年部分的表达式解析
        /// </summary>
        [Fact]
        public void ParseTest()
        {
            var parse = PatternParser.Parse("0 0 3 1 1 ? */1");
            Assert.NotNull(parse);
            Assert.Single(parse);
        }
    }
}
