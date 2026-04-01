namespace WellTool.DB
{
    /// <summary>
    /// 分页参数
    /// </summary>
    public class Page
    {
        /// <summary>
        /// 页码，从1开始
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 构造
        /// </summary>
        public Page() : this(1, 10)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="pageNumber">页码，从1开始</param>
        /// <param name="pageSize">每页大小</param>
        public Page(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        /// <summary>
        /// 获取起始位置
        /// </summary>
        /// <returns>起始位置</returns>
        public int GetStartIndex()
        {
            return (PageNumber - 1) * PageSize;
        }

        /// <summary>
        /// 获取结束位置
        /// </summary>
        /// <returns>结束位置</returns>
        public int GetEndIndex()
        {
            return PageNumber * PageSize;
        }

        /// <summary>
        /// 下一页
        /// </summary>
        /// <returns>下一页</returns>
        public Page NextPage()
        {
            return new Page(PageNumber + 1, PageSize);
        }

        /// <summary>
        /// 上一页
        /// </summary>
        /// <returns>上一页</returns>
        public Page PreviousPage()
        {
            return new Page(PageNumber > 1 ? PageNumber - 1 : 1, PageSize);
        }
    }
}