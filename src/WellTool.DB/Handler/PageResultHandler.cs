using System.Data;
using System.Collections.Generic;
using WellTool.DB;

namespace WellTool.DB.Handler
{
    /// <summary>
    /// 分页结果处理器
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    public class PageResultHandler<T> : RsHandler<PageResult<T>>
    {
        private RsHandler<List<T>> _listHandler;
        private long _total;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="listHandler">列表处理器</param>
        /// <param name="total">总记录数</param>
        public PageResultHandler(RsHandler<List<T>> listHandler, long total)
        {
            _listHandler = listHandler;
            _total = total;
        }

        /// <summary>
        /// 处理结果集
        /// </summary>
        /// <param name="reader">结果集</param>
        /// <returns>处理结果</returns>
        public PageResult<T> Handle(IDataReader reader)
        {
            List<T> list = _listHandler.Handle(reader);
            return new PageResult<T>(list, _total, 1, list.Count);
        }
    }
}