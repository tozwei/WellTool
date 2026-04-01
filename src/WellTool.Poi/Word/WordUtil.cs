using System.IO;

namespace WellTool.Poi.Word
{
    /// <summary>
    /// Word工具类
    /// </summary>
    public static class WordUtil
    {
        /// <summary>
        /// 创建Word 07格式的生成器
        /// </summary>
        /// <returns><see cref="Word07Writer"/></returns>
        public static Word07Writer GetWriter()
        {
            return new Word07Writer();
        }

        /// <summary>
        /// 创建Word 07格式的生成器
        /// </summary>
        /// <param name="destFile">目标文件</param>
        /// <returns><see cref="Word07Writer"/></returns>
        public static Word07Writer GetWriter(string destFile)
        {
            return new Word07Writer(destFile);
        }

        /// <summary>
        /// 创建Word 07格式的生成器
        /// </summary>
        /// <param name="destFile">目标文件</param>
        /// <returns><see cref="Word07Writer"/></returns>
        public static Word07Writer GetWriter(FileInfo destFile)
        {
            return new Word07Writer(destFile);
        }
    }
}