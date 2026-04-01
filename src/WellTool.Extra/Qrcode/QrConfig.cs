using System.Drawing;
using System.Text;

namespace WellTool.Extra.Qrcode
{
    /// <summary>
    /// 二维码设置
    /// </summary>
    public class QrConfig
    {
        private const int Black = unchecked((int)0xFF000000);
        private const int White = unchecked((int)0xFFFFFFFF);

        /// <summary>
        /// 宽度（单位：像素）
        /// </summary>
        protected int Width;

        /// <summary>
        /// 高度（单位：像素）
        /// </summary>
        protected int Height;

        /// <summary>
        /// 前景色（二维码颜色）
        /// </summary>
        protected int? ForeColor = Black;

        /// <summary>
        /// 背景色，默认白色，null表示透明
        /// </summary>
        protected int? BackColor = White;

        /// <summary>
        /// 边距1~4
        /// </summary>
        protected int? Margin = 2;

        /// <summary>
        /// 设置二维码中的信息量，可设置0-40的整数
        /// </summary>
        protected int? QrVersion;

        /// <summary>
        /// 纠错级别
        /// </summary>
        protected ErrorCorrectionLevel ErrorCorrection = ErrorCorrectionLevel.M;

        /// <summary>
        /// 编码
        /// </summary>
        protected Encoding Charset = Encoding.UTF8;

        /// <summary>
        /// 二维码中的Logo
        /// </summary>
        protected Image Img;

        /// <summary>
        /// 二维码中的Logo圆角弧度
        /// </summary>
        protected double Round = 0.3;

        /// <summary>
        /// 二维码中的Logo缩放的比例系数，如5表示长宽最小值的1/5
        /// </summary>
        protected int Ratio = 6;

        /// <summary>
        /// 创建QrConfig
        /// </summary>
        /// <returns>QrConfig</returns>
        public static QrConfig Create()
        {
            return new QrConfig();
        }

        /// <summary>
        /// 构造，默认长宽为300
        /// </summary>
        public QrConfig() : this(300, 300)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="width">宽</param>
        /// <param name="height">长</param>
        public QrConfig(int width, int height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// 获取宽度
        /// </summary>
        /// <returns>宽度</returns>
        public int GetWidth()
        {
            return Width;
        }

        /// <summary>
        /// 设置宽度
        /// </summary>
        /// <param name="width">宽度</param>
        /// <returns>this</returns>
        public QrConfig SetWidth(int width)
        {
            Width = width;
            return this;
        }

        /// <summary>
        /// 获取高度
        /// </summary>
        /// <returns>高度</returns>
        public int GetHeight()
        {
            return Height;
        }

        /// <summary>
        /// 设置高度
        /// </summary>
        /// <param name="height">高度</param>
        /// <returns>this</returns>
        public QrConfig SetHeight(int height)
        {
            Height = height;
            return this;
        }

        /// <summary>
        /// 获取前景色
        /// </summary>
        /// <returns>前景色</returns>
        public int? GetForeColor()
        {
            return ForeColor;
        }

        /// <summary>
        /// 设置前景色
        /// </summary>
        /// <param name="foreColor">前景色</param>
        /// <returns>this</returns>
        public QrConfig SetForeColor(Color foreColor)
        {
            ForeColor = foreColor.ToArgb();
            return this;
        }

        /// <summary>
        /// 获取背景色
        /// </summary>
        /// <returns>背景色</returns>
        public int? GetBackColor()
        {
            return BackColor;
        }

        /// <summary>
        /// 设置背景色
        /// </summary>
        /// <param name="backColor">背景色，null表示透明背景</param>
        /// <returns>this</returns>
        public QrConfig SetBackColor(Color? backColor)
        {
            BackColor = backColor?.ToArgb();
            return this;
        }

        /// <summary>
        /// 获取边距
        /// </summary>
        /// <returns>边距</returns>
        public int? GetMargin()
        {
            return Margin;
        }

        /// <summary>
        /// 设置边距
        /// </summary>
        /// <param name="margin">边距</param>
        /// <returns>this</returns>
        public QrConfig SetMargin(int? margin)
        {
            Margin = margin;
            return this;
        }

        /// <summary>
        /// 设置二维码中的信息量，可设置0-40的整数，二维码图片也会根据qrVersion而变化，0表示根据传入信息自动变化
        /// </summary>
        /// <returns>二维码中的信息量</returns>
        public int? GetQrVersion()
        {
            return QrVersion;
        }

        /// <summary>
        /// 设置二维码中的信息量，可设置0-40的整数，二维码图片也会根据qrVersion而变化，0表示根据传入信息自动变化
        /// </summary>
        /// <param name="qrVersion">二维码中的信息量</param>
        /// <returns>this</returns>
        public QrConfig SetQrVersion(int? qrVersion)
        {
            QrVersion = qrVersion;
            return this;
        }

        /// <summary>
        /// 获取纠错级别
        /// </summary>
        /// <returns>纠错级别</returns>
        public ErrorCorrectionLevel GetErrorCorrection()
        {
            return ErrorCorrection;
        }

        /// <summary>
        /// 设置纠错级别
        /// </summary>
        /// <param name="errorCorrection">纠错级别</param>
        /// <returns>this</returns>
        public QrConfig SetErrorCorrection(ErrorCorrectionLevel errorCorrection)
        {
            ErrorCorrection = errorCorrection;
            return this;
        }

        /// <summary>
        /// 获取编码
        /// </summary>
        /// <returns>编码</returns>
        public Encoding GetCharset()
        {
            return Charset;
        }

        /// <summary>
        /// 设置编码
        /// </summary>
        /// <param name="charset">编码</param>
        /// <returns>this</returns>
        public QrConfig SetCharset(Encoding charset)
        {
            Charset = charset;
            return this;
        }

        /// <summary>
        /// 获取二维码中的Logo
        /// </summary>
        /// <returns>Logo图片</returns>
        public Image GetImg()
        {
            return Img;
        }

        /// <summary>
        /// 设置二维码中的Logo
        /// </summary>
        /// <param name="img">二维码中的Logo</param>
        /// <returns>this</returns>
        public QrConfig SetImg(Image img)
        {
            Img = img;
            return this;
        }

        /// <summary>
        /// 获取二维码中的Logo缩放的比例系数，如5表示长宽最小值的1/5
        /// </summary>
        /// <returns>二维码中的Logo缩放的比例系数，如5表示长宽最小值的1/5</returns>
        public int GetRatio()
        {
            return Ratio;
        }

        /// <summary>
        /// 设置二维码中的Logo缩放的比例系数，如5表示长宽最小值的1/5
        /// </summary>
        /// <param name="ratio">二维码中的Logo缩放的比例系数，如5表示长宽最小值的1/5</param>
        /// <returns>this</returns>
        public QrConfig SetRatio(int ratio)
        {
            Ratio = ratio;
            return this;
        }

        /// <summary>
        /// 获取二维码中的Logo圆角弧度
        /// </summary>
        /// <returns>二维码中的Logo圆角弧度</returns>
        public double GetRound()
        {
            return Round;
        }

        /// <summary>
        /// 设置二维码中的Logo圆角弧度
        /// </summary>
        /// <param name="round">二维码中的Logo圆角弧度</param>
        /// <returns>this</returns>
        public QrConfig SetRound(double round)
        {
            Round = round;
            return this;
        }
    }

    /// <summary>
    /// 纠错级别
    /// </summary>
    public enum ErrorCorrectionLevel
    {
        /// <summary>
        /// 低
        /// </summary>
        L,
        /// <summary>
        /// 中
        /// </summary>
        M,
        /// <summary>
        /// 较高
        /// </summary>
        Q,
        /// <summary>
        /// 高
        /// </summary>
        H
    }
}