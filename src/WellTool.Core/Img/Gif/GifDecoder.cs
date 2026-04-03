namespace WellTool.Core.Img.Gif;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

/// <summary>
/// GIF 文件解析器
/// 
/// @author Kevin Weiner, FM Software
/// </summary>
public class GifDecoder
{
    /// <summary>
    /// 文件读取状态：正常
    /// </summary>
    public const int STATUS_OK = 0;

    /// <summary>
    /// 文件读取状态：解码错误
    /// </summary>
    public const int STATUS_FORMAT_ERROR = 1;

    /// <summary>
    /// 文件读取状态：无法打开文件
    /// </summary>
    public const int STATUS_OPEN_ERROR = 2;

    private Stream? _in;
    private int _status;

    private int _width;
    private int _height;
    private bool _gctFlag;
    private int _gctSize;
    private int _loopCount = 1;

    private int[]? _gct;
    private int[]? _lct;
    private int[]? _act;

    private int _bgIndex;
    private int _bgColor;
    private int _lastBgColor;
    private int _pixelAspect;

    private bool _lctFlag;
    private bool _interlace;
    private int _lctSize;

    private int _ix, _iy, _iw, _ih;
    private Rectangle _lastRect;
    private Bitmap? _image;
    private Bitmap? _lastImage;

    private byte[]? _block;
    private int _blockSize;

    private int _dispose = 0;
    private int _lastDispose = 0;
    private bool _transparency = false;
    private int _delay = 0;
    private int _transIndex;

    /// <summary>
    /// 获取帧数量
    /// </summary>
    public int FrameCount => _frames?.Count ?? 0;

    /// <summary>
    /// 获取播放延迟（毫秒）
    /// </summary>
    public int Delay => _delay;

    /// <summary>
    /// 获取循环次数
    /// </summary>
    public int LoopCount => _loopCount;

    /// <summary>
    /// 获取 GIF 宽度
    /// </summary>
    public int Width => _width;

    /// <summary>
    /// 获取 GIF 高度
    /// </summary>
    public int Height => _height;

    private List<Bitmap>? _frames;

    /// <summary>
    /// 读取 GIF 文件
    /// </summary>
    public int Read(string fileName)
    {
        try
        {
            using var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            return Read(fs);
        }
        catch
        {
            return STATUS_OPEN_ERROR;
        }
    }

    /// <summary>
    /// 读取 GIF 流
    /// </summary>
    public int Read(Stream ins)
    {
        Init();
        _in = ins;
        ReadHeader();
        if (_status == STATUS_OK)
        {
            ReadContents();
            if (_frames == null || _frames.Count == 0)
            {
                _status = STATUS_FORMAT_ERROR;
            }
        }
        return _status;
    }

    /// <summary>
    /// 获取指定帧
    /// </summary>
    public Bitmap? GetFrame(int n)
    {
        return _frames?.ElementAtOrDefault(n);
    }

    /// <summary>
    /// 获取指定帧的延迟
    /// </summary>
    public int GetDelay(int n)
    {
        return _delay;
    }

    private void Init()
    {
        _status = STATUS_OK;
        _frames = new List<Bitmap>();
        _gct = null;
        _lct = null;
    }

    private void ReadHeader()
    {
        var header = new byte[6];
        Read(_block ??= new byte[1], 6);
        header = _block ?? Array.Empty<byte>();

        if (!Verify(header))
        {
            _status = STATUS_FORMAT_ERROR;
            return;
        }

        ReadLSD();
        if (_gctFlag)
        {
            _gct = new int[1 << (_gctSize + 1)];
            ReadColorTable(_gct);
            _bgColor = _gct[_bgIndex];
        }
    }

    private bool Verify(byte[] header)
    {
        return header[0] == 'G' && header[1] == 'I' &&
               header[2] == 'F' && header[3] == '8';
    }

    private void ReadLSD()
    {
        _width = ReadShort();
        _height = ReadShort();

        var packed = ReadByte();
        _gctFlag = (packed & 0x80) != 0;
        _gctSize = 2 << (packed & 7);

        _bgIndex = ReadByte();
        _pixelAspect = ReadByte();
    }

    private void ReadColorTable(int[] colorTable)
    {
        int n = 3 * colorTable.Length;
        byte[] c = new byte[n];
        Read(c, n);
        for (int i = 0; i < n; i += 3)
        {
            colorTable[i / 3] = (c[i] << 16) | (c[i + 1] << 8) | c[i + 2];
        }
    }

    private void ReadContents()
    {
        bool done = false;
        while (!done && _status == STATUS_OK)
        {
            int code = ReadByte();
            switch (code)
            {
                case 0x2C:
                    ReadImageBlock();
                    break;
                case 0x21:
                    ReadExtension();
                    break;
                case 0x3B:
                    done = true;
                    break;
                default:
                    _status = STATUS_FORMAT_ERROR;
                    break;
            }
        }
    }

    private void ReadExtension()
    {
        int label = ReadByte();
        switch (label)
        {
            case 0xF9:
                ReadGraphicControlExt();
                break;
            case 0xFF:
                ReadApplicationExt();
                break;
            default:
                SkipSubBlocks();
                break;
        }
    }

    private void ReadGraphicControlExt()
    {
        ReadByte();
        int packed = ReadByte();
        _dispose = (packed >> 2) & 7;
        _transparency = (packed & 1) != 0;
        _delay = ReadShort() * 10;
        _transIndex = ReadByte();
        ReadByte();
    }

    private void ReadApplicationExt()
    {
        ReadByte();
        var app = new byte[11];
        Read(app, 11);
        if (app[0] == 'N' && app[1] == 'E' && app[2] == 'T')
        {
            SkipSubBlocks();
            _loopCount = ReadShort();
        }
        else
        {
            SkipSubBlocks();
        }
    }

    private void ReadImageBlock()
    {
        _ix = ReadShort();
        _iy = ReadShort();
        _iw = ReadShort();
        _ih = ReadShort();

        int packed = ReadByte();
        _lctFlag = (packed & 0x80) != 0;
        _interlace = (packed & 0x40) != 0;
        _lctSize = 2 << (packed & 7);

        if (_lctFlag)
        {
            _lct = new int[1 << (_lctSize + 1)];
            ReadColorTable(_lct!);
            _act = _lct;
        }
        else
        {
            _act = _gct;
        }

        if (_transparency && _transIndex >= 0 && _act != null)
        {
            _act[_transIndex] = 0;
        }

        DecodeImageData();
        SkipSubBlocks();

        if (_frames != null)
        {
            _frames.Add(_image!);
        }
    }

    private void DecodeImageData()
    {
        var scanline = new byte[_width];
        _image = new Bitmap(_width, _height);
        _act ??= _gct ?? Array.Empty<int>();

        int[] colorMap = _act!;
        var colorArray = new Color[colorMap.Length];

        for (int i = 0; i < colorMap.Length; i++)
        {
            int c = colorMap[i];
            colorArray[i] = Color.FromArgb((c >> 16) & 0xFF, (c >> 8) & 0xFF, c & 0xFF);
        }

        int pass = 0;
        int inc = 8;
        int step = 8;

        for (int y = 0; y < _height; y++)
        {
            int line = y;
            if (_interlace)
            {
                if (pass == 0 && y == 7) pass = 1;
                else if (pass == 1 && y == _ih + 8) { pass = 2; step = 4; }
                else if (pass == 2 && y == _ih + 8 + 4) { pass = 3; step = 2; }
                else if (pass == 3 && y == _ih + 8 + 4 + 2) pass = 4;

                line = y;
                if (pass > 0)
                {
                    line = y;
                    int temp = 8;
                    for (int p = 0; p < pass; p++)
                    {
                        line += step;
                        temp >>= 1;
                    }
                    line = temp;
                }
            }

            for (int x = 0; x < _iw; x++)
            {
                int index = ScanBlock();
                if (index < colorArray.Length)
                {
                    _image.SetPixel(_ix + x, _iy + line, colorArray[index]);
                }
            }
        }
    }

    private int ScanBlock()
    {
        int len;
        int accum = 0;
        int shift = 0;

        while ((len = ReadByte()) > 0)
        {
            while (len > 0)
            {
                int data = _block?[0] ?? 0;
                _block = null;
                accum |= data << shift;
                shift += 8;
                len--;
            }
        }

        return accum;
    }

    private int ReadByte()
    {
        _blockSize = Read();
        if (_blockSize > 0)
        {
            _block = new byte[_blockSize];
            Read(_block, _blockSize);
            return _block[0];
        }
        return 0;
    }

    private int Read()
    {
        return _in?.ReadByte() ?? -1;
    }

    private void Read(byte[] data, int length)
    {
        if (_in != null)
        {
            int totalRead = 0;
            while (totalRead < length)
            {
                int read = _in.Read(data, totalRead, length - totalRead);
                if (read <= 0) break;
                totalRead += read;
            }
        }
    }

    private int ReadShort()
    {
        return ReadByte() | (ReadByte() << 8);
    }

    private void SkipSubBlocks()
    {
        while (ReadByte() > 0)
        {
            Read(_block ??= new byte[1], _blockSize);
        }
    }
}
