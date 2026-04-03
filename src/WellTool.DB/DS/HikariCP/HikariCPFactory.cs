using System.Data;

namespace WellTool.DB.DS.HikariCP
{
    /// <summary>
    /// HikariCP数据源工厂
    /// 
    /// 需要安装 HikariCP 或类似 NuGet 包
    /// 项目地址：https://github.com/brettwooldridge/HikariCP
    /// </summary>
    public class HikariCPFactory : AbstractDSFactory
    {
        /// <summary>
        /// 构造
        /// </summary>
        public HikariCPFactory()
        {
        }

        /// <summary>
        /// 获取数据源
        /// </summary>
        /// <returns>数据源</returns>
        public override IDbDataSource GetDataSource()
        {
            // TODO: 需要集成 HikariCP.NET 或 Microsoft.Data.SqlClient 等实现
            return null;
        }

        /// <summary>
        /// 获取数据源名称
        /// </summary>
        /// <returns>名称</returns>
        public override string GetName()
        {
            return "hikari";
        }
    }
}
