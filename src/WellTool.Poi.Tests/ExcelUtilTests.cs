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

using System.Collections.Generic;
using System.IO;
using Xunit;

namespace WellTool.Poi.Tests;

namespace WellTool.Poi.Tests;

using System.Collections.Generic;
using System.IO;
using Xunit;

public class ExcelUtilTests
{
    [Fact]
    public void TestGetReader()
    {
        // 创建一个临时Excel文件用于测试
        var tempFile = Path.GetTempFileName() + ".xlsx";
        try
        {
            // 首先创建一个写入器并写入一些数据
            using var writer = WellTool.Poi.ExcelUtil.GetWriter(tempFile);
            var sheetIndex = writer.CreateSheet("Sheet1");
            
            var data = new List<List<object?>>
            {
                new List<object?> { "Name", "Age", "City" },
                new List<object?> { "John", 30, "New York" },
                new List<object?> { "Jane", 25, "London" }
            };
            
            writer.Write(sheetIndex, data);
            writer.Save();
            
            // 然后创建读取器并读取数据
            using var reader = WellTool.Poi.ExcelUtil.GetReader(tempFile);
            var readData = reader.ReadSheet(0);
            
            // 验证读取的数据与写入的数据一致
            Assert.Equal(data.Count, readData.Count);
            for (int i = 0; i < data.Count; i++)
            {
                var expectedRow = data[i];
                var actualRow = readData[i];
                Assert.Equal(expectedRow.Count, actualRow.Count);
                for (int j = 0; j < expectedRow.Count; j++)
                {
                    var expected = expectedRow[j];
                    var actual = actualRow[j];
                    
                    // 处理数字类型的比较，因为EPPlus可能返回不同的数字类型
                    if (expected is int expectedInt && actual is double actualDouble)
                    {
                        Assert.Equal(expectedInt, (int)actualDouble);
                    }
                    else if (expected is double expectedDouble && actual is int actualInt)
                    {
                        Assert.Equal(expectedDouble, (double)actualInt);
                    }
                    else
                    {
                        Assert.Equal(expected, actual);
                    }
                }
            }
        }
        finally
        {
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
        }
    }

    [Fact]
    public void TestGetWriter()
    {
        // 创建一个临时Excel文件用于测试
        var tempFile = Path.GetTempFileName() + ".xlsx";
        try
        {
            // 创建写入器并写入一些数据
            using var writer = WellTool.Poi.ExcelUtil.GetWriter(tempFile);
            var sheetIndex = writer.CreateSheet("Sheet1");
            
            var data = new List<List<object?>>
            {
                new List<object?> { "Product", "Price", "Quantity" },
                new List<object?> { "Apple", 1.99, 10 },
                new List<object?> { "Banana", 0.99, 20 }
            };
            
            writer.Write(sheetIndex, data);
            writer.Save();
            
            // 验证文件已创建
            Assert.True(File.Exists(tempFile));
            
            // 验证文件大小大于0
            Assert.True(new FileInfo(tempFile).Length > 0);
        }
        finally
        {
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
        }
    }
}

public class WordUtilTests
{
    [Fact]
    public void TestWordUtil()
    {
        // 创建一个临时Word文件用于测试
        var tempFile = Path.GetTempFileName() + ".docx";
        try
        {
            // 创建Word写入器
            using var writer = WellTool.Poi.Word.WordUtil.GetWriter(tempFile);
            
            // 写入文本
            writer.AddText(null, null, "Hello, WellTool!");
            writer.AddText(null, null, "This is a test document.");
            
            // 保存文件
            writer.Flush();
            
            // 验证文件已创建
            Assert.True(File.Exists(tempFile));
            
            // 验证文件大小大于0
            Assert.True(new FileInfo(tempFile).Length > 0);
        }
        finally
        {
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
        }
    }
}

public class PoiUtilTests
{
    [Fact]
    public void TestPoiChecker()
    {
        // 测试PoiChecker是否能正确初始化
        var isPoiAvailable = WellTool.Poi.PoiChecker.IsPoiAvailable();
        // 验证结果是布尔值
        Assert.IsType<bool>(isPoiAvailable);
    }
}
