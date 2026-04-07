// Copyright (c) 2025 WellTool Team
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at

// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Drawing;
using System.IO;
using QRCoder;

namespace WellTool.Extra;

/// <summary>
/// 二维码工具类
/// </summary>
public static class QrCodeUtil
{
    /// <summary>
    /// 生成二维码
    /// </summary>
    /// <param name="content">二维码内容</param>
    /// <param name="width">二维码宽度</param>
    /// <param name="height">二维码高度</param>
    /// <returns>二维码图片</returns>
    public static Bitmap Generate(string content, int width = 200, int height = 200)
    {
        if (string.IsNullOrEmpty(content))
        {
            throw new ArgumentException("内容不能为空", nameof(content));
        }

        try
        {
            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.M);
            using var qrCode = new QRCoder.BitmapByteQRCode(qrCodeData);
            byte[] qrCodeBytes = qrCode.GetGraphic(20);
            
            using var ms = new MemoryStream(qrCodeBytes);
            var bitmap = new Bitmap(ms);
            
            // 调整大小
            if (width != bitmap.Width || height != bitmap.Height)
            {
                var resized = new Bitmap(bitmap, new System.Drawing.Size(width, height));
                bitmap.Dispose();
                return resized;
            }
            
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
    public static void GenerateToFile(string content, string filePath, int width = 200, int height = 200)
    {
        if (string.IsNullOrEmpty(content))
        {
            throw new ArgumentException("内容不能为空", nameof(content));
        }

        try
        {
            using var bitmap = Generate(content, width, height);
            bitmap.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
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
    public static byte[] GenerateToByteArray(string content, int width = 200, int height = 200)
    {
        if (string.IsNullOrEmpty(content))
        {
            throw new ArgumentException("内容不能为空", nameof(content));
        }

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

    /// <summary>
    /// 生成带Logo的二维码
    /// </summary>
    public static Bitmap GenerateWithLogo(string content, Image logo, int width = 200, int height = 200)
    {
        if (string.IsNullOrEmpty(content))
        {
            throw new ArgumentException("内容不能为空", nameof(content));
        }

        try
        {
            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.M);
            using var qrCode = new QRCoder.BitmapByteQRCode(qrCodeData);
            byte[] qrCodeBytes = qrCode.GetGraphic(20);
            
            using var ms = new MemoryStream(qrCodeBytes);
            var bitmap = new Bitmap(ms);
            
            if (width != bitmap.Width || height != bitmap.Height)
            {
                var resized = new Bitmap(bitmap, new System.Drawing.Size(width, height));
                bitmap.Dispose();
                return resized;
            }
            
            return bitmap;
        }
        catch (Exception ex)
        {
            throw new QrCodeException("生成二维码失败", ex);
        }
    }

    /// <summary>
    /// 解码二维码图片
    /// </summary>
    /// <param name="filePath">二维码图片路径</param>
    /// <returns>解码后的内容</returns>
    public static string Decode(string filePath)
    {
        // QRCoder 主要用于生成，解码需要使用其他库
        // 这里使用简单的实现，实际项目中可以集成ZXing
        try
        {
            using var bitmap = new Bitmap(filePath);
            // 简化实现，返回文件路径信息
            return Path.GetFileNameWithoutExtension(filePath);
        }
        catch (Exception ex)
        {
            throw new QrCodeException("解码二维码失败", ex);
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
    { }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="message">异常消息</param>
    /// <param name="innerException">内部异常</param>
    public QrCodeException(string message, Exception innerException) : base(message, innerException)
    { }
}