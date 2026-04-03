namespace WellTool.Core.Img.Gif;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

/// <summary>
/// 动态GIF动画生成器
/// </summary>
public class AnimatedGifEncoder
{
    protected int _width;
    protected int _height;
    protected Color? _transparent;
    protected bool _transparentExactMatch;
    protected Color? _background;
    protected int _transIndex;
    protected int _repeat = -1;
    protected int _delay = 0;
    protected bool _started;
    protected Stream? _out;
    protected byte[]? _pixels;
    protected byte[]? _indexedPixels;
    protected int _colorDepth;
    protected byte[]? _colorTab;
    protected bool[] _usedEntry = new bool[256];
    protected int _palSize = 7;
    protected int _dispose = -1;
    protected bool _closeStream;
    protected bool _firstFrame = true;
    protected bool _sizeSet;
    protected int _sample = 10;

    /// <summary>
    /// 设置每一帧的间隔时间
    /// </summary>
    /// <param name="ms">间隔时间，单位毫秒</param>
    public void SetDelay(int ms)
    {
        _delay = (int)Math.Round(ms / 10.0f);
    }

    /// <summary>
    /// 设置 GIF 帧处理方式
    /// </summary>
    /// <param name="code">处理代码</param>
    public void SetDispose(int code)
    {
        if (code >= 0)
        {
            _dispose = code;
        }
    }

    /// <summary>
    /// 设置 GIF 重复播放次数
    /// </summary>
    /// <param name="iter">重复次数，0表示无限循环</param>
    public void SetRepeat(int iter)
    {
        if (iter >= 0)
        {
            _repeat = iter;
        }
    }

    /// <summary>
    /// 设置透明色
    /// </summary>
    /// <param name="c">透明色</param>
    public void SetTransparent(Color c)
    {
        _transparent = c;
    }

    /// <summary>
    /// 开始生成 GIF
    /// </summary>
    /// <param name="fileName">输出文件名</param>
    public bool Start(string fileName)
    {
        try
        {
            _out = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            return Start(_out);
        }
        catch (IOException)
        {
            return false;
        }
    }

    /// <summary>
    /// 开始生成 GIF
    /// </summary>
    /// <param name="os">输出流</param>
    public bool Start(Stream os)
    {
        _out = new BufferedStream(os);
        try
        {
            WriteString("GIF89a");
        }
        catch (IOException)
        {
            return false;
        }
        _started = true;
        return true;
    }

    /// <summary>
    /// 添加帧
    /// </summary>
    /// <param name="image">图像</param>
    public bool AddFrame(Image image)
    {
        if (image == null || !_started)
        {
            return false;
        }

        try
        {
            if (_firstFrame)
            {
                _width = image.Width;
                _height = image.Height;
                WriteLSD();
                WritePalette();
                if (_repeat >= 0)
                {
                    WriteNetscapeExt();
                }
            }

            AnalyzeImage(image);
            WriteGraphicCtrlExt();
            WriteImageDesc();
            if (!_firstFrame)
            {
                WritePalette();
            }
            WritePixels();
            _firstFrame = false;
        }
        catch (IOException)
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// 完成生成
    /// </summary>
    public bool Finish()
    {
        if (!_started)
        {
            return false;
        }

        try
        {
            _out?.WriteByte(0x3b);
            _out?.Flush();
            if (_closeStream)
            {
                _out?.Close();
            }
        }
        catch (IOException)
        {
            return false;
        }

        _started = false;
        return true;
    }

    private void AnalyzeImage(Image image)
    {
        int w = image.Width;
        int h = image.Height;

        using var bitmap = new Bitmap(image);
        var pixelsList = new List<byte>();

        for (int y = 0; y < h; y++)
        {
            for (int x = 0; x < w; x++)
            {
                var pixel = bitmap.GetPixel(x, y);
                pixelsList.Add(pixel.R);
                pixelsList.Add(pixel.G);
                pixelsList.Add(pixel.B);
            }
        }

        _pixels = pixelsList.ToArray();
        _indexedPixels = new byte[w * h];

        // 简单的颜色量化
        var colorMap = new Dictionary<int, int>();
        var colors = new List<Color>();

        for (int i = 0; i < _pixels.Length; i += 3)
        {
            int rgb = (_pixels[i] << 16) | (_pixels[i + 1] << 8) | _pixels[i + 2];

            if (!colorMap.ContainsKey(rgb))
            {
                colorMap[rgb] = colors.Count;
                colors.Add(Color.FromArgb(_pixels[i], _pixels[i + 1], _pixels[i + 2]));

                if (colors.Count >= 256)
                    break;
            }
        }

        // 填充颜色表到256色
        while (colors.Count < 256)
        {
            colors.Add(Color.Black);
        }

        _colorTab = new byte[256 * 3];
        for (int i = 0; i < 256 && i < colors.Count; i++)
        {
            _colorTab[i * 3] = colors[i].R;
            _colorTab[i * 3 + 1] = colors[i].G;
            _colorTab[i * 3 + 2] = colors[i].B;
        }

        // 映射像素到调色板索引
        for (int i = 0; i < _pixels.Length; i += 3)
        {
            int rgb = (_pixels[i] << 16) | (_pixels[i + 1] << 8) | _pixels[i + 2];

            if (colorMap.TryGetValue(rgb, out int index))
            {
                _indexedPixels[i / 3] = (byte)index;
            }
            else
            {
                _indexedPixels[i / 3] = 0;
            }
        }

        _colorDepth = 8;
    }

    private void WriteLSD()
    {
        WriteShort(_width);
        WriteShort(_height);
        _out?.WriteByte((byte)(0x80 | 0x70 | _palSize));
        _out?.WriteByte(0);
        _out?.WriteByte(0);
    }

    private void WriteNetscapeExt()
    {
        _out?.WriteByte(0x21);
        _out?.WriteByte(0xff);
        _out?.WriteByte(11);
        WriteString("NETSCAPE2.0");
        _out?.WriteByte(3);
        _out?.WriteByte(1);
        WriteShort(_repeat);
        _out?.WriteByte(0);
    }

    private void WritePalette()
    {
        if (_colorTab == null) return;
        _out?.Write(_colorTab, 0, _colorTab.Length);
        int n = 3 * 256 - (_colorTab?.Length ?? 0);
        for (int i = 0; i < n; i++)
        {
            _out?.WriteByte(0);
        }
    }

    private void WriteGraphicCtrlExt()
    {
        _out?.WriteByte(0x21);
        _out?.WriteByte(0xf9);
        _out?.WriteByte(4);

        int transp = 0;
        int disp = _dispose > 0 ? _dispose : 0;
        if (_transparent != null)
        {
            transp = 1;
            disp = 2;
        }

        _out?.WriteByte((byte)(0 | (disp << 2) | transp));
        WriteShort(_delay);
        _out?.WriteByte((byte)_transIndex);
        _out?.WriteByte(0);
    }

    private void WriteImageDesc()
    {
        _out?.WriteByte(0x2c);
        WriteShort(0);
        WriteShort(0);
        WriteShort(_width);
        WriteShort(_height);

        if (_firstFrame)
        {
            _out?.WriteByte(0);
        }
        else
        {
            _out?.WriteByte(0x80 | _palSize);
        }
    }

    private void WritePixels()
    {
        var encoder = new LZWEncoder(_width, _height, _indexedPixels, _colorDepth);
        encoder.Encode(_out!);
    }

    private void WriteShort(int value)
    {
        _out?.WriteByte((byte)(value & 0xff));
        _out?.WriteByte((byte)((value >> 8) & 0xff));
    }

    private void WriteString(string s)
    {
        foreach (char c in s)
        {
            _out?.WriteByte((byte)c);
        }
    }
}
