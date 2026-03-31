// Copyright (c) 2025 WellTool Team
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Xunit;

namespace WellTool.Dfa.Tests;

public class SensitiveUtilTests
{
    [Fact]
    public void TestAddSensitiveWords()
    {
        // 清除现有敏感词
        SensitiveUtil.Clear();
        
        // 添加敏感词
        SensitiveUtil.AddSensitiveWords(new[] { "敏感词1", "敏感词2" });
        
        // 测试是否包含敏感词
        var result = SensitiveUtil.ContainsSensitiveWords("这是一个敏感词1的测试");
        Assert.True(result);
        
        result = SensitiveUtil.ContainsSensitiveWords("这是一个正常的测试");
        Assert.False(result);
    }
    
    [Fact]
    public void TestReplaceSensitiveWords()
    {
        // 清除现有敏感词
        SensitiveUtil.Clear();
        
        // 添加敏感词
        SensitiveUtil.AddSensitiveWords(new[] { "敏感词1", "敏感词2" });
        
        // 测试替换敏感词
        var result = SensitiveUtil.ReplaceSensitiveWords("这是一个敏感词1的测试", '*');
        Assert.Equal("这是一个*****的测试", result);
    }
    
    [Fact]
    public void TestGetSensitiveWords()
    {
        // 清除现有敏感词
        SensitiveUtil.Clear();
        
        // 添加敏感词
        SensitiveUtil.AddSensitiveWords(new[] { "敏感词1", "敏感词2" });
        
        // 测试获取敏感词
        var words = SensitiveUtil.GetSensitiveWords("这是一个敏感词1和敏感词2的测试");
        Assert.Contains("敏感词1", words);
        Assert.Contains("敏感词2", words);
    }
    
    [Fact]
    public void TestClear()
    {
        // 添加敏感词
        SensitiveUtil.AddSensitiveWords(new[] { "敏感词1", "敏感词2" });
        
        // 清除敏感词
        SensitiveUtil.Clear();
        
        // 测试是否已清除
        var result = SensitiveUtil.ContainsSensitiveWords("这是一个敏感词1的测试");
        Assert.False(result);
    }
}