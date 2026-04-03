using System.Data;

namespace WellTool.DB.DS.DBCP
{
    /// <summary>
    /// DBCP数据源工厂
    /// 
    /// 需要安装 DBCP 或类似 NuGet 包
    /// 项目地址：https://commons.apache.org/proper/commons-dbcp/
    /// </summary>
    public class DBCPFactory : AbstractDSFactory
    {
        /// <summary>
        /// 构造
        /// </summary>
        public DBCPFactory()
        {
        }

        /// <summary>
        /// 获取数据源
        /// </summary>
        /// <returns>数据源</returns>
        public override IDbDataSource GetDataSource()
        {
            // TODO: 需要集成 DBCP 或类似实现
            return null;
        }

        /// <summary>
        /// 获取数据源名称
        /// </summary>
        /// <returns>名称</returns>
        public override string GetName()
        {
            return "dbcp";
        }
    }
}
