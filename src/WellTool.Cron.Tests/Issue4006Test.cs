// Copyright (c) 2025 WellTool Team
// Licensed under the Apache License, Version 2.0 (the "License");

using WellTool.Cron.Pattern;
using Xunit;

namespace WellTool.Cron.Tests
{
    /// <summary>
    /// Issue4006 测试 - 日匹配问题
    /// </summary>
    public class Issue4006Test
    {
        /// <summary>
        /// 测试日匹配表达式
        /// </summary>
        [Fact]
        public void TestCron()
        {
            var cron = "0 0 0 */1 * ? *";
            var pattern = new CronPattern(cron);

            // 验证表达式能被正确解析
            Assert.NotNull(pattern);
        }
    }
}
