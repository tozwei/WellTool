namespace WellTool.Core.Img.Gif;

using System;
using System.IO;

/// <summary>
/// LZW 编码器，用于 GIF 压缩
/// </summary>
internal class LZWEncoder
{
    private static readonly int EOF = -1;

    private readonly int _imgW;
    private readonly int _imgH;
    private readonly byte[] _pixAry;
    private readonly int _initCodeSize;
    private readonly int _remaining;
    private int _curPixel;

    private static readonly int BITS = 12;
    private static readonly int HSIZE = 5003;

    private int _nBits;
    private int _maxBits = BITS;
    private int _maxCode;
    private int _maxMaxCode = 1 << BITS;

    private readonly int[] _hTab = new int[HSIZE];
    private readonly int[] _codeTab = new int[HSIZE];

    private int _hSize;
    private int _freeEnt;

    private bool _clearFlag;
    private int _gInitBits;
    private int _clearCode;
    private int _eofCode;

    private int _curAccum;
    private int _curBits;

    private readonly int[] _masks = new int[]
    {
        0x0000, 0x0001, 0x0003, 0x0007, 0x000F,
        0x001F, 0x003F, 0x007F, 0x00FF, 0x010F,
        0x01FF, 0x03FF, 0x07FF, 0x0FFF, 0x1FFF,
        0x3FFF, 0x7FFF, 0xFFFF
    };

    private int _aCount;
    private readonly byte[] _accum = new byte[256];

    private readonly int[][] _pixCode = new int[][]
    {
        new int[] { 0, 1, 2, 3, 4, 5, 6, 7 },
        new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 },
        new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 },
        new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 },
        new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 },
        new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 },
        new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 },
        new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25 },
    };

    public LZWEncoder(int width, int height, byte[] pixels, int colorDepth)
    {
        _imgW = width;
        _imgH = height;
        _pixAry = pixels;
        _initCodeSize = Math.Max(2, colorDepth);
        _remaining = _imgW * _imgH;
        _curPixel = 0;
        _hSize = HSIZE;
    }

    public void Encode(Stream outs)
    {
        outs.WriteByte((byte)_initCodeSize);
        _remaining = _imgW * _imgH;
        _curPixel = 0;
        Compress(_initCodeSize + 1, outs);
        outs.WriteByte(0);
    }

    private void Compress(int initBits, Stream outs)
    {
        int fcode;
        int c;
        int ent;
        int disp;
        int hsizeReg;
        int hashTip;

        _gInitBits = initBits;
        _clearFlag = false;
        _freeEnt = (1 << initBits) + 2;
        _eofCode = (1 << initBits) + 1;
        _nBits = initBits;
        _maxCode = MaxCode(_nBits);

        Array.Clear(_hTab, 0, _hTab.Length);
        Array.Clear(_codeTab, 0, _codeTab.Length);

        ent = NextPixel();
        hsizeReg = _hSize;
        ClHash(hsizeReg);

        _clearCode = 1 << (initBits - 1);
        _eofCode = _clearCode + 1;
        _freeEnt++;

        int aCount = 0;
        byte[] accum = new byte[256];

        void Output(int code)
        {
            _curAccum &= _masks[_curBits];
            _curAccum |= code < 0 ? 0 : code << _curBits;
            _curBits += _nBits;

            while (_curBits >= 8)
            {
                accum[aCount++] = (byte)(_curAccum & 0xff);
                if (aCount >= 254)
                {
                    outs.WriteByte((byte)aCount);
                    outs.Write(accum, 0, aCount);
                    aCount = 0;
                }
                _curAccum >>= 8;
                _curBits -= 8;
            }

            if (code >= 0)
            {
                var taboff = code - _clearCode;
                if (taboff >= 0 && taboff < 4096)
                {
                    if (taboff < _pixCode.Length)
                    {
                        _nBits = _pixCode[taboff].Length + _gInitBits;
                    }
                }
            }
        }

        Output(_clearCode);

        while ((c = NextPixel()) != EOF)
        {
            outer_loop:
            fcode = (c << _maxBits) + ent;
            int i = (c << 4) ^ hsizeReg;

            if (_hTab[i] == fcode)
            {
                ent = _codeTab[i];
                continue;
            }
            else if (_hTab[i] >= 0)
            {
                disp = hsizeReg - i;
                if (i == 0)
                    disp = 1;

                do
                {
                    if ((i -= disp) < 0)
                        i += hsizeReg;

                    if (_hTab[i] == fcode)
                    {
                        ent = _codeTab[i];
                        continue;
                    }
                } while (_hTab[i] >= 0);
            }

            Output(ent);
            ent = c;

            if (_freeEnt < _maxMaxCode)
            {
                _codeTab[i] = _freeEnt++;
                _hTab[i] = fcode;
            }
            else
            {
                ClBlock(outs);
            }
        }

        Output(ent);
        Output(_eofCode);

        if (aCount > 0)
        {
            outs.WriteByte((byte)aCount);
            outs.Write(accum, 0, aCount);
        }
    }

    private int NextPixel()
    {
        if (_remaining == 0)
            return EOF;

        --_remaining;
        byte pix = _pixAry[_curPixel++];
        return pix & 0xff;
    }

    private int MaxCode(int nBits)
    {
        return (1 << nBits) - 1;
    }

    private void ClBlock(Stream outs)
    {
        ClHash(_hSize);
        _freeEnt = (1 << _gInitBits) + 2;
        _clearFlag = true;
        Output(_clearCode);
    }

    private void ClHash(int hsize)
    {
        for (int i = 0; i < hsize; ++i)
            _hTab[i] = -1;
    }
}
