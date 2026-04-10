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
using WellTool.DB;
using System.IO;

namespace WellTool.DB.Tests;

/// <summary>
/// 图片传输测试
/// </summary>
public class PicTransferTest
{
    /// <summary>
    /// 测试图片传输功能
    /// </summary>
    [Fact]
    public void TestPicTransfer()
    {
        // 测试图片传输功能
        var dataSource = new TestDataSource("Server=localhost;Database=test;", "MockDriver");
        var db = new TestDb(dataSource);

        // 创建测试图片数据（模拟图片的二进制数据）
        var imageData = new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }; // PNG文件头

        // 测试插入图片数据
        var insertSql = "INSERT INTO images (name, data) VALUES (?, ?)";
        var insertResult = db.Execute(insertSql, "test.png", imageData);

        // 由于使用的是模拟连接，这里会返回0，但测试可以验证方法调用是否正常
        Assert.Equal(0, insertResult);

        // 测试查询图片数据
        var querySql = "SELECT data FROM images WHERE name = ?";
        var result = db.Query(querySql, "test.png");

        // 验证方法调用没有异常
        Assert.NotNull(result);
    }

    /// <summary>
    /// 测试图片数据的参数绑定
    /// </summary>
    [Fact]
    public void TestImageDataParameterBinding()
    {
        // 测试图片数据作为参数的绑定
        var dataSource = new TestDataSource("Server=localhost;Database=test;", "MockDriver");
        var db = new TestDb(dataSource);

        // 创建测试图片数据
        var imageData = new byte[1024]; // 1KB的测试数据
        for (int i = 0; i < imageData.Length; i++)
        {
            imageData[i] = (byte)(i % 256);
        }

        // 测试带图片数据参数的SQL执行
        var sql = "INSERT INTO images (id, name, data) VALUES (?, ?, ?)";
        var result = db.Execute(sql, 1, "test_image.jpg", imageData);

        // 由于使用的是模拟连接，这里会返回0
        Assert.Equal(0, result);
    }

    /// <summary>
    /// 测试从文件读取图片数据
    /// </summary>
    [Fact]
    public void TestReadImageFromFile()
    {
        // 测试从文件读取图片数据并准备插入
        var tempFile = Path.Combine(Path.GetTempPath(), "test_image.png");

        try
        {
            // 创建临时测试文件
            var testData = new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };
            File.WriteAllBytes(tempFile, testData);

            // 读取文件内容
            var imageData = File.ReadAllBytes(tempFile);

            // 验证读取成功
            Assert.NotNull(imageData);
            Assert.Equal(testData.Length, imageData.Length);

            // 验证数据正确
            for (int i = 0; i < testData.Length; i++)
            {
                Assert.Equal(testData[i], imageData[i]);
            }
        }
        finally
        {
            // 清理临时文件
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
        }
    }
}
