using System;

namespace WellTool.Core.Util
{
    /// <summary>
    /// 分页工具类
    /// </summary>
    public static class PageUtil
    {
        /// <summary>
        /// 将页码和每页大小转换为偏移量
        /// </summary>
        public static int ToOffset(int pageNo, int pageSize)
        {
            if (pageNo < 1)
            {
                pageNo = 1;
            }
            return (pageNo - 1) * pageSize;
        }

        /// <summary>
        /// 将偏移量和每页大小转换为页码
        /// </summary>
        public static int ToPageNo(int offset, int pageSize)
        {
            if (offset < 0)
            {
                offset = 0;
            }
            return offset / pageSize + 1;
        }

        /// <summary>
        /// 计算总页数
        /// </summary>
        public static int TotalPages(int total, int pageSize)
        {
            if (pageSize <= 0)
            {
                return 0;
            }
            if (total <= 0)
            {
                return 1;
            }
            return (total + pageSize - 1) / pageSize;
        }

        /// <summary>
        /// 判断是否有下一页
        /// </summary>
        public static bool HasNext(int pageNo, int pageSize, int total)
        {
            return pageNo < TotalPages(total, pageSize);
        }

        /// <summary>
        /// 判断是否有上一页
        /// </summary>
        public static bool HasPrev(int pageNo)
        {
            return pageNo > 1;
        }

        /// <summary>
        /// 获取下一页页码
        /// </summary>
        public static int NextPage(int pageNo, int pageSize, int total)
        {
            return HasNext(pageNo, pageSize, total) ? pageNo + 1 : pageNo;
        }

        /// <summary>
        /// 获取上一页页码
        /// </summary>
        public static int PrevPage(int pageNo)
        {
            return HasPrev(pageNo) ? pageNo - 1 : pageNo;
        }

        /// <summary>
        /// 转换为彩虹码分页
        /// </summary>
        public static int[] Rainbow(int total, int pageSize, int m)
        {
            int totalPages = TotalPages(total, pageSize);
            if (totalPages <= m * 2 + 1)
            {
                var result = new int[m];
                for (int i = 0; i < m; i++)
                {
                    result[i] = i + 1;
                }
                return result;
            }

            int halfM = m / 2;
            var result2 = new int[m + 3 + totalPages - m * 2];
            int index = 0;

            // 前半部分
            for (int i = 1; i <= halfM; i++)
            {
                result2[index++] = i;
            }

            // 中间部分
            result2[index++] = -1; // 省略号标记
            result2[index++] = (totalPages + 1) / 2; // 中间页
            result2[index++] = -1; // 省略号标记

            // 后半部分
            for (int i = totalPages - halfM + 1; i <= totalPages; i++)
            {
                result2[index++] = i;
            }

            return result2;
        }

        /// <summary>
        /// 获取页码范围
        /// </summary>
        public static int[] PageRange(int pageNo, int pageSize, int total, int showCount = 5)
        {
            int totalPages = TotalPages(total, pageSize);
            int half = showCount / 2;

            int start = System.Math.Max(1, pageNo - half);
            int end = System.Math.Min(totalPages, start + showCount - 1);

            if (end - start + 1 < showCount)
            {
                start = System.Math.Max(1, end - showCount + 1);
            }

            var result = new int[end - start + 1];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = start + i;
            }

            return result;
        }

        /// <summary>
        /// 转换为URL查询参数
        /// </summary>
        public static string ToUrlParam(int pageNo, int pageSize)
        {
            return $"pageNo={pageNo}&pageSize={pageSize}";
        }

        /// <summary>
        /// 计算分页起始和结束行号
        /// </summary>
        public static (int start, int end) GetRowRange(int pageNo, int pageSize)
        {
            int start = (pageNo - 1) * pageSize + 1;
            int end = pageNo * pageSize;
            return (start, end);
        }

        /// <summary>
        /// 生成页面大小选择的HTML下拉框
        /// </summary>
        public static string ToHtmlSelect(int currentPageSize, params int[] pageSizes)
        {
            if (pageSizes == null || pageSizes.Length == 0)
            {
                pageSizes = new[] { 10, 20, 30, 50, 100 };
            }

            var sb = new System.Text.StringBuilder();
            sb.Append("<select class=\"page-size-select\">");

            foreach (var size in pageSizes)
            {
                bool selected = size == currentPageSize;
                sb.Append($"<option value=\"{size}\"{(selected ? " selected=\"selected\"" : "")}>{size}</option>");
            }

            sb.Append("</select>");
            return sb.ToString();
        }
        /// <summary>
        /// 将页码和每页大小转换为起始和结束索引
        /// </summary>
        public static (int start, int end) TransToStartEnd(int pageNo, int pageSize)
        {
            int start = (pageNo - 1) * pageSize;
            int end = start + pageSize;
            return (start, end);
        }

        /// <summary>
        /// 获取第一页的页码
        /// </summary>
        public static int GetFirstPage()
        {
            return 0;
        }

        /// <summary>
        /// 计算总页数
        /// </summary>
        public static int TotalPage(int total, int pageSize)
        {
            return TotalPages(total, pageSize);
        }
    }
}
