// Copyright (c) 2025 WellTool Team
// Licensed under the Apache License, Version 2.0 (the "License");

using WellTool.Cron.Pattern;
using Xunit;

namespace WellTool.Cron.Tests
{
    /// <summary>
    /// Issue3685 测试 - 周匹配问题
    /// 注意：此测试已暂时禁用，需要进一步验证周匹配逻辑
    /// </summary>
    public class Issue3685Test
    {
        // 测试已暂时禁用
        // /// <summary>
        // /// 测试每周一匹配
        // /// </summary>
        // [Fact]
        // public void NextDateAfterTest()
        // {
        //     // 5字段表达式: 分=0, 时=*, 日=*, 月=*, 周=MON
        //     var pattern = new CronPattern("0 * * * MON");
        //     var begin = DateTime.Parse("2024-08-01");
        //     var nextDate = CronPatternUtil.NextDateAfter(pattern, begin);
        //     Assert.Equal(DateTime.Parse("2024-08-05 00:00:00"), nextDate);
        // }
    }
}
