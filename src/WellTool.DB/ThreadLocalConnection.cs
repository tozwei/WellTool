using System;
using System.Collections.Generic;
using System.Data;

namespace WellTool.DB
{
    /// <summary>
    /// 线程相关的数据库连接持有器
    /// </summary>
    public class ThreadLocalConnection
    {
        private static readonly ThreadLocalConnection _instance = new ThreadLocalConnection();
        private readonly ThreadLocal<GroupedConnection> _threadLocal = new ThreadLocal<GroupedConnection>();

        /// <summary>
        /// 获取实例
        /// </summary>
        public static ThreadLocalConnection Instance => _instance;

        private ThreadLocalConnection()
        {
        }

        /// <summary>
        /// 获取数据源对应的数据库连接
        /// </summary>
        /// <param name="ds">数据源</param>
        /// <returns>连接</returns>
        public IDbConnection Get(IDbDataSource ds)
        {
            var groupedConnection = _threadLocal.Value;
            if (groupedConnection == null)
            {
                groupedConnection = new GroupedConnection();
                _threadLocal.Value = groupedConnection;
            }
            return groupedConnection.Get(ds);
        }

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        /// <param name="ds">数据源</param>
        public void Close(IDbDataSource ds)
        {
            var groupedConnection = _threadLocal.Value;
            if (groupedConnection != null)
            {
                groupedConnection.Close(ds);
                if (groupedConnection.IsEmpty())
                {
                    _threadLocal.Dispose();
                }
            }
        }

        /// <summary>
        /// 分组连接，根据不同的分组获取对应的连接
        /// </summary>
        public class GroupedConnection
        {
            private readonly Dictionary<IDbDataSource, IDbConnection> _connMap = new Dictionary<IDbDataSource, IDbConnection>();

            /// <summary>
            /// 获取连接
            /// </summary>
            /// <param name="ds">数据源</param>
            /// <returns>连接</returns>
            public IDbConnection Get(IDbDataSource ds)
            {
                if (_connMap.TryGetValue(ds, out var conn) && conn.State != ConnectionState.Closed)
                {
                    return conn;
                }
                conn = ds.GetConnection();
                _connMap[ds] = conn;
                return conn;
            }

            /// <summary>
            /// 关闭并移除连接
            /// </summary>
            /// <param name="ds">数据源</param>
            /// <returns>this</returns>
            public GroupedConnection Close(IDbDataSource ds)
            {
                if (_connMap.TryGetValue(ds, out var conn))
                {
                    _connMap.Remove(ds);
                    conn?.Dispose();
                }
                return this;
            }

            /// <summary>
            /// 持有的连接是否为空
            /// </summary>
            public bool IsEmpty() => _connMap.Count == 0;
        }
    }
}
