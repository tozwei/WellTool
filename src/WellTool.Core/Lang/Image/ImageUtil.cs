namespace WellTool.Core.Lang.Image;

using System;
using System.Drawing;

/// <summary>
/// 图像选择接口
/// </summary>
public interface IImageSelection
{
	/// <summary>
	/// 获取选中的图像
	/// </summary>
	/// <returns>图像</returns>
	Image GetImage();
}

/// <summary>
/// 默认图像选择实现
/// </summary>
public class ImageSelection : IImageSelection
{
	private readonly Image _image;
	private readonly Rectangle _selection;

	public ImageSelection(Image image) : this(image, new Rectangle(0, 0, image.Width, image.Height))
	{
	}

	public ImageSelection(Image image, Rectangle selection)
	{
		_image = image;
		_selection = selection;
	}

	public Image GetImage()
	{
		var bitmap = new Bitmap(_selection.Width, _selection.Height);
		using (var g = Graphics.FromImage(bitmap))
		{
			g.DrawImage(_image, new Rectangle(0, 0, _selection.Width, _selection.Height), _selection, GraphicsUnit.Pixel);
		}
		return bitmap;
	}
}
