using NPOI.XWPF.UserModel;

namespace WellTool.Poi.Word
{
    /// <summary>
    /// Word中的图片类型
    /// </summary>
    public enum PicType
    {
        /// <summary>
        /// EMF 图片类型
        /// </summary>
        EMF = 2,

        /// <summary>
        /// WMF 图片类型
        /// </summary>
        WMF = 3,

        /// <summary>
        /// PICT 图片类型
        /// </summary>
        PICT = 4,

        /// <summary>
        /// JPEG 图片类型
        /// </summary>
        JPEG = 5,

        /// <summary>
        /// PNG 图片类型
        /// </summary>
        PNG = 6,

        /// <summary>
        /// DIB 图片类型
        /// </summary>
        DIB = 7,

        /// <summary>
        /// GIF 图片类型
        /// </summary>
        GIF = 8,

        /// <summary>
        /// TIFF 图片类型
        /// </summary>
        TIFF = 9,

        /// <summary>
        /// EPS 图片类型
        /// </summary>
        EPS = 10,

        /// <summary>
        /// WPG 图片类型
        /// </summary>
        WPG = 11
    }
}