using System;
using System.Drawing;

namespace WellTool.Core.Img;

/// <summary>
/// 图片封装
/// </summary>
public class Img
{
	private readonly Image _image;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="image">图片</param>
	public Img(Image image)
	{
		_image = image ?? throw new ArgumentNullException(nameof(image));
	}

	/// <summary>
	/// 宽
	/// </summary>
	public int Width => _image.Width;

	/// <summary>
	/// 高
	/// </summary>
	public int Height => _image.Height;

	/// <summary>
	/// 获取原图
	/// </summary>
	public Image GetImage() => _image;

	/// <summary>
	/// 缩放
	/// </summary>
	/// <param name="width">宽</param>
	/// <param name="height">高</param>
	/// <returns>新图片</returns>
	public Img Scale(int width, int height)
	{
		var scaledImage = new Bitmap(width, height);
		using (var g = Graphics.FromImage(scaledImage))
		{
			g.DrawImage(_image, 0, 0, width, height);
		}
		return new Img(scaledImage);
	}

	/// <summary>
	/// 裁剪
	/// </summary>
	/// <param name="x">x坐标</param>
	/// <param name="y">y坐标</param>
	/// <param name="width">宽</param>
	/// <param name="height">高</param>
	/// <returns>新图片</returns>
	public Img Cut(int x, int y, int width, int height)
	{
		var rect = new Rectangle(x, y, width, height);
		var croppedImage = new Bitmap(width, height);
		using (var g = Graphics.FromImage(croppedImage))
		{
			g.DrawImage(_image, new Rectangle(0, 0, width, height), rect, GraphicsUnit.Pixel);
		}
		return new Img(croppedImage);
	}

	/// <summary>
	/// 保存到文件
	/// </summary>
	/// <param name="path">路径</param>
	public void Save(string path)
	{
		_image.Save(path);
	}
}
