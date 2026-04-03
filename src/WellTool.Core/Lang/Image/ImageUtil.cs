namespace WellTool.Core.lang.image;

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

/// <summary>
/// 剪贴板监视器
/// </summary>
public class ClipboardMonitor
{
	private System.Windows.Forms.Timer _timer;
	private event EventHandler<object>? _clipboardChanged;

	/// <summary>
	/// 剪贴板内容改变事件
	/// </summary>
	public event EventHandler<object>? ClipboardChanged
	{
		add => _clipboardChanged += value;
		remove => _clipboardChanged -= value;
	}

	/// <summary>
	/// 开始监视
	/// </summary>
	/// <param name="interval">检查间隔(毫秒)</param>
	public void Start(int interval = 500)
	{
		_timer = new System.Windows.Forms.Timer { Interval = interval };
		_timer.Tick += OnTimerTick;
		_timer.Start();
	}

	/// <summary>
	/// 停止监视
	/// </summary>
	public void Stop()
	{
		_timer?.Stop();
		_timer?.Dispose();
	}

	private string? _lastContent;

	private void OnTimerTick(object sender, EventArgs e)
	{
		try
		{
			if (System.Windows.Forms.Clipboard.ContainsText())
			{
				var content = System.Windows.Forms.Clipboard.GetText();
				if (content != _lastContent)
				{
					_lastContent = content;
					_clipboardChanged?.Invoke(this, content);
				}
			}
		}
		catch
		{
			// Ignore clipboard access errors
		}
	}
}
