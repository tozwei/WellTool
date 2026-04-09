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

namespace WellTool.DB.Tests;

/// <summary>
/// 图片传输测试
/// </summary>
public class PicTransferTest
{
    /// <summary>
    /// 测试图片传输
    /// </summary>
    [Fact]
    public void TestPicTransfer()
    {
        // 测试图片传输功能
        // 使用 SQLite 内存数据库进行测试
        using var connection = new System.Data.SQLite.SQLiteConnection("Data Source=:memory:");
        connection.Open();

        // 创建测试表，包含 BLOB 列用于存储图片
        using var createTableCmd = connection.CreateCommand();
        createTableCmd.CommandText = @"CREATE TABLE TestImages (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Name TEXT,
            Image BLOB
        )";
        createTableCmd.ExecuteNonQuery();

        // 创建测试图片数据（简单的 PNG 图片头和数据）
        var testImageData = new byte[] {
            0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A, // PNG 签名
            0x00, 0x00, 0x00, 0x0D, 0x49, 0x48, 0x44, 0x52, // IHDR 块
            0x00, 0x00, 0x00, 0x10, 0x00, 0x00, 0x00, 0x10, // 16x16 图像
            0x08, 0x06, 0x00, 0x00, 0x00, 0x1F, 0xF3, 0xFF, // 8 位 RGBA
            0x61, 0x00, 0x00, 0x00, 0x04, 0x67, 0x41, 0x4D, // gAMA 块
            0x41, 0x00, 0x00, 0xB1, 0x8F, 0x0B, 0xF1, 0x72, //  gamma 值
            0x52, 0x47, 0x42, 0x41, 0x00, 0x00, 0x00, 0x0C, // sRGB 块
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, // 标准 sRGB
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, // 占位数据
            0x49, 0x45, 0x4E, 0x44, 0xAE, 0xB2, 0x60, 0x82  // IEND 块
        };

        // 插入图片数据
        using var insertCmd = connection.CreateCommand();
        insertCmd.CommandText = "INSERT INTO TestImages (Name, Image) VALUES (@Name, @Image)";
        insertCmd.Parameters.AddWithValue("@Name", "test_image.png");
        insertCmd.Parameters.AddWithValue("@Image", testImageData);
        insertCmd.ExecuteNonQuery();

        // 验证数据已插入
        using var selectCmd = connection.CreateCommand();
        selectCmd.CommandText = "SELECT * FROM TestImages WHERE Id = 1";
        using var reader = selectCmd.ExecuteReader();
        Assert.True(reader.Read());
        Assert.Equal("test_image.png", reader["Name"]);
        var retrievedImageData = (byte[])reader["Image"];
        Assert.NotNull(retrievedImageData);
        Assert.Equal(testImageData.Length, retrievedImageData.Length);

        // 验证图片数据是否一致
        for (int i = 0; i < testImageData.Length; i++)
        {
            Assert.Equal(testImageData[i], retrievedImageData[i]);
        }

        reader.Close();
    }
}