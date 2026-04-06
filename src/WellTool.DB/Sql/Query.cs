using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WellTool.DB.Sql
{
    /// <summary>
    /// 查询对象
    /// </summary>
    public class Query
    {
        /// <summary>
        /// 条件
        /// </summary>
        public string Conditions { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 分页信息
        /// </summary>
        public Page Page { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="conditions">条件</param>
        /// <param name="tableName">表名</param>
        public Query(string conditions, string tableName)
        {
            Conditions = conditions;
            TableName = tableName;
        }

        /// <summary>
        /// 根据实体创建查询对象
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>查询对象</returns>
        public static Query Of(object entity)
        {
            // 默认实现，子类可重写
            return new Query(string.Empty, string.Empty);
        }

        /// <summary>
        /// 获取分页信息
        /// </summary>
        /// <returns>分页信息</returns>
        public Page GetPage()
        {
            return Page;
        }

        /// <summary>
        /// 选择字段
        /// </summary>
        /// <param name="fields">字段列表</param>
        /// <returns>查询对象</returns>
        public Query SelectFields(Collection<string> fields)
        {
            // 默认实现，子类可重写
            return this;
        }

        /// <summary>
        /// 设置分页
        /// </summary>
        /// <param name="page">分页信息</param>
        /// <returns>查询对象</returns>
        public Query SetPage(Page page)
        {
            Page = page;
            return this;
        }
    }

    /// <summary>
    /// 分页信息
    /// </summary>
    public class Page
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pageNumber">页码</param>
        /// <param name="pageSize">每页大小</param>
        public Page(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }

    /// <summary>
    /// 分页结果
    /// </summary>
    /// <typeparam name="T">结果类型</typeparam>
    public class PageResult<T>
    {
        /// <summary>
        /// 数据列表
        /// </summary>
        public List<T> Data { get; set; }

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
        public int TotalPages { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="data">数据列表</param>
        /// <param name="total">总记录数</param>
        /// <param name="pageNumber">页码</param>
        /// <param name="pageSize">每页大小</param>
        public PageResult(List<T> data, long total, int pageNumber, int pageSize)
        {
            Data = data;
            Total = total;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling((double)total / pageSize);
        }
    }
}