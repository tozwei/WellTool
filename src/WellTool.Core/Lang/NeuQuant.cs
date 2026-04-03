namespace WellTool.Core.lang;

using System;

/// <summary>
/// NeuQuant 神经网络量化算法
/// 用于颜色量化
/// </summary>
public class NeuQuant
{
	private const int NetSize = 256;
	private const int Prime = 499;
	private const int MinPictureBytes = 3 * Prime;
	private const int MaxNetPos = NetSize - 1;
	private const int NetBiasShift = 4;
	private const int NCycles = 100;
	private const int IntBiasShift = 16;
	private const int IntBias = 1 << IntBiasShift;
	private const int GammaShift = 10;
	private const int Gamma = 1 << GammaShift;
	private const int BetaShift = 10;
	private const int Beta = IntBias >> BetaShift;
	private const int BetaGamma = IntBias << (GammaShift - BetaShift);
	private const int InitRad = NetSize >> 3;
	private const int RadiusBiasShift = 6;
	private const int RadiusBias = 1 << RadiusBiasShift;
	private const int InitRadius = InitRad * RadiusBias;
	private const int RadiusDec = 30;
	private const int AlphaRadBiasShift = AlphaRadBiasShift;
	private const int AlphaRadBias = 1 << AlphaRadBiasShift;
	private const int AlphaDec = 30;

	private readonly byte[] _thePicture;
	private readonly int _lengthCount;
	private readonly int _sampleFac;
	private readonly int[][] _network;
	private readonly int[] _netIndex = new int[256];
	private readonly int[] _bias = new int[NetSize];
	private readonly int[] _freq = new int[NetSize];
	private readonly int[] _radPower = new int[InitRad];

	public NeuQuant(byte[] thePicture, int lengthCount, int sample)
	{
		_thePicture = thePicture;
		_lengthCount = lengthCount;
		_sampleFac = sample;

		_network = new int[NetSize][];
		for (int i = 0; i < NetSize; i++)
		{
			_network[i] = new int[4];
			var p = _network[i];
			p[0] = p[1] = p[2] = (i << (NetBiasShift + 8)) / NetSize;
			_freq[i] = IntBias / NetSize;
			_bias[i] = 0;
		}
	}

	/// <summary>
	/// 学习并返回量化后的调色板
	/// </summary>
	/// <returns>256色的调色板</returns>
	public byte[] Process()
	{
		if (_lengthCount < MinPictureBytes)
			_sampleFac = 1;

		var alphaDec = 30 + ((_sampleFac - 1) / 3);
		var samplePixels = _lengthCount / (3 * _sampleFac);
		var delta = Math.Max(samplePixels / NCycles, 1);
		var alpha = IntBias;
		var radius = InitRadius;

		var rad = InitRad >> RadiusBiasShift;
		if (rad <= 1) rad = 0;
		for (int i = 0; i < rad; i++)
			_radPower[i] = AlphaDec * (((rad * rad - i * i) * RadiusBias) / (rad * rad));

		var step = 3;
		if (_lengthCount < MinPictureBytes)
			step = 3 * ((_lengthCount / MinPictureBytes) + 1);

		var pix = 0;
		for (var i = 0; i < samplePixels;)
		{
			var b = (_thePicture[pix] & 0xFF) << NetBiasShift;
			var g = (_thePicture[pix + 1] & 0xFF) << NetBiasShift;
			var r = (_thePicture[pix + 2] & 0xFF) << NetBiasShift;
			var j = Contest(b, g, r);

			AlterSingle(alpha, j, b, g, r);
			if (rad != 0)
				AlterNeigh(rad, j, b, g, r);

			pix += step;
			if (pix >= _lengthCount)
				pix -= _lengthCount;

			i++;
			if (delta == 0) delta = 1;
			if (i % delta == 0)
			{
				alpha -= alpha / AlphaDec;
				radius -= radius / RadiusDec;
				rad = radius >> RadiusBiasShift;
				if (rad <= 1) rad = 0;
				for (int k = 0; k < rad; k++)
					_radPower[k] = AlphaDec * (((rad * rad - k * k) * RadiusBias) / (rad * rad));
			}
		}

		return BuildColorMap();
	}

	private int Contest(int b, int g, int r)
	{
		var bestD = int.MaxValue;
		var bestBiasD = bestD;
		var bestPos = -1;
		var bestBiasPos = bestPos;

		for (int i = 0; i < NetSize; i++)
		{
			var n = _network[i];
			var dist = Math.Abs(n[0] - b) + Math.Abs(n[1] - g) + Math.Abs(n[2] - r);
			if (dist < bestD)
			{
				bestD = dist;
				bestPos = i;
			}
			var biasDist = dist - ((_bias[i]) >> (IntBiasShift - NetBiasShift));
			if (biasDist < bestBiasD)
			{
				bestBiasD = biasDist;
				bestBiasPos = i;
			}
			var betaFreq = _freq[i] >> BetaShift;
			_freq[i] -= betaFreq;
			_bias[i] += betaFreq << GammaShift;
		}
		_freq[bestPos] += Beta;
		_bias[bestPos] -= BetaGamma;
		return bestBiasPos;
	}

	private void AlterSingle(int alpha, int i, int b, int g, int r)
	{
		var n = _network[i];
		n[0] -= (alpha * (n[0] - b)) / IntBias;
		n[1] -= (alpha * (n[1] - g)) / IntBias;
		n[2] -= (alpha * (n[2] - r)) / IntBias;
	}

	private void AlterNeigh(int rad, int i, int b, int g, int r)
	{
		var lo = Math.Max(i - rad, -1);
		var hi = Math.Min(i + rad, NetSize);

		for (var j = i + 1; j < hi; j++)
		{
			var a = _radPower[j - i];
			var n = _network[j];
			n[0] -= (a * (n[0] - b)) / AlphaRadBias;
			n[1] -= (a * (n[1] - g)) / AlphaRadBias;
			n[2] -= (a * (n[2] - r)) / AlphaRadBias;
		}

		for (var j = lo + 1; j < i; j++)
		{
			var a = _radPower[i - j];
			var n = _network[j];
			n[0] -= (a * (n[0] - b)) / AlphaRadBias;
			n[1] -= (a * (n[1] - g)) / AlphaRadBias;
			n[2] -= (a * (n[2] - r)) / AlphaRadBias;
		}
	}

	private byte[] BuildColorMap()
	{
		var map = new byte[3 * NetSize];
		var index = new int[NetSize];

		for (int i = 0; i < NetSize; i++)
			index[_network[i][3]] = i;

		var k = 0;
		for (int i = 0; i < NetSize; i++)
		{
			var j = index[i];
			map[k++] = (byte)(_network[j][0]);
			map[k++] = (byte)(_network[j][1]);
			map[k++] = (byte)(_network[j][2]);
		}

		return map;
	}
}
