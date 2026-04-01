using System.Collections.Generic;

namespace WellTool.DB
{
    /// <summary>
    /// 分页结果
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    public class PageResult<T>
    {
        /// <summary>
        /// 数据列表
        /// </summary>
        public List<T> List { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public long Total { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages
        {
            get
            {
                return (int)((Total + PageSize - 1) / PageSize);
            }
        }

        /// <summary>
        /// 构造
        /// </summary>
        public PageResult() : this(new List<T>(), 0, 1, 10)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="list">数据列表</param>
        /// <param name="total">总记录数</param>
        /// <param name="pageNumber">页码</param>
        /// <param name="pageSize">每页大小</param>
        public PageResult(List<T> list, long total, int pageNumber, int pageSize)
        {
            List = list;
            Total = total;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        /// <summary>
        /// 是否有下一页
        /// </summary>
        /// <returns>是否有下一页</returns>
        public bool HasNext()
        {
            return PageNumber < TotalPages;
        }

        /// <summary>
        /// 是否有上一页
        /// </summary>
        /// <returns>是否有上一页</returns>
        public bool HasPrevious()
        {
            return PageNumber > 1;
        }
    }
}