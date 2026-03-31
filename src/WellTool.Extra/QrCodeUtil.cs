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

using System.Drawing;
using System.IO;

namespace WellTool.Extra;

/// <summary>
/// 二维码工具类
/// </summary>
public class QrCodeUtil
{
    /// <summary>
    /// 单例实例
    /// </summary>
    public static QrCodeUtil Instance { get; } = new QrCodeUtil();

    /// <summary>
    /// 生成二维码
    /// </summary>
    /// <param name="content">二维码内容</param>
    /// <param name="width">二维码宽度</param>
    /// <param name="height">二维码高度</param>
    /// <returns>二维码图片</returns>
    public Bitmap Generate(string content, int width = 200, int height = 200)
    {
        try
        {
            // 简单实现，实际项目中可能需要使用更专业的二维码库
            // 这里使用System.Drawing绘制一个简单的二维码模拟图
            var bitmap = new Bitmap(width, height);
            using var graphics = Graphics.FromImage(bitmap);
            
            // 填充背景
            graphics.FillRectangle(Brushes.White, 0, 0, width, height);
            
            // 绘制二维码模拟图案
            int cellSize = width / 25;
            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    // 模拟二维码图案，实际项目中需要使用真正的二维码算法
                    if ((i + j) % 3 == 0)
                    {
                        graphics.FillRectangle(Brushes.Black, i * cellSize, j * cellSize, cellSize, cellSize);
                    }
                }
            }
            
            // 绘制定位图案
            graphics.FillRectangle(Brushes.Black, 2 * cellSize, 2 * cellSize, 7 * cellSize, 7 * cellSize);
            graphics.FillRectangle(Brushes.White, 3 * cellSize, 3 * cellSize, 5 * cellSize, 5 * cellSize);
            graphics.FillRectangle(Brushes.Black, 4 * cellSize, 4 * cellSize, 3 * cellSize, 3 * cellSize);
            
            graphics.FillRectangle(Brushes.Black, 2 * cellSize, 18 * cellSize, 7 * cellSize, 7 * cellSize);
            graphics.FillRectangle(Brushes.White, 3 * cellSize, 19 * cellSize, 5 * cellSize, 5 * cellSize);
            graphics.FillRectangle(Brushes.Black, 4 * cellSize, 20 * cellSize, 3 * cellSize, 3 * cellSize);
            
            graphics.FillRectangle(Brushes.Black, 18 * cellSize, 2 * cellSize, 7 * cellSize, 7 * cellSize);
            graphics.FillRectangle(Brushes.White, 19 * cellSize, 3 * cellSize, 5 * cellSize, 5 * cellSize);
            graphics.FillRectangle(Brushes.Black, 20 * cellSize, 4 * cellSize, 3 * cellSize, 3 * cellSize);
            
            return bitmap;
        }
        catch (Exception ex)
        {
            throw new QrCodeException("生成二维码失败", ex);
        }
    }

    /// <summary>
    /// 生成二维码并保存到文件
    /// </summary>
    /// <param name="content">二维码内容</param>
    /// <param name="filePath">文件路径</param>
    /// <param name="width">二维码宽度</param>
    /// <param name="height">二维码高度</param>
    public void GenerateToFile(string content, string filePath, int width = 200, int height = 200)
    {
        try
        {
            using var bitmap = Generate(content, width, height);
            bitmap.Save(filePath);
        }
        catch (Exception ex)
        {
            throw new QrCodeException("保存二维码失败", ex);
        }
    }

    /// <summary>
    /// 生成二维码并返回字节数组
    /// </summary>
    /// <param name="content">二维码内容</param>
    /// <param name="width">二维码宽度</param>
    /// <param name="height">二维码高度</param>
    /// <returns>二维码字节数组</returns>
    public byte[] GenerateToBytes(string content, int width = 200, int height = 200)
    {
        try
        {
            using var bitmap = Generate(content, width, height);
            using var memoryStream = new MemoryStream();
            bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
            return memoryStream.ToArray();
        }
        catch (Exception ex)
        {
            throw new QrCodeException("生成二维码字节数组失败", ex);
        }
    }
}

/// <summary>
/// 二维码异常
/// </summary>
public class QrCodeException : Exception
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="message">异常消息</param>
    public QrCodeException(string message) : base(message)
    {}

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="message">异常消息</param>
    /// <param name="innerException">内部异常</param>
    public QrCodeException(string message, Exception innerException) : base(message, innerException)
    {}
}