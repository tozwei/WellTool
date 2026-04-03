using System.Data;

namespace WellTool.DB.DS.Tomcat
{
    /// <summary>
    /// Tomcat数据源工厂
    /// 
    /// 需要安装 Tomcat.DataSource 或类似 NuGet 包
    /// </summary>
    public class TomcatDSFactory : AbstractDSFactory
    {
        /// <summary>
        /// 构造
        /// </summary>
        public TomcatDSFactory()
        {
        }

        /// <summary>
        /// 获取数据源
        /// </summary>
        /// <returns>数据源</returns>
        public override IDbDataSource GetDataSource()
        {
            // TODO: 需要集成 Tomcat JDBC Pool 或类似实现
            return null;
        }

        /// <summary>
        /// 获取数据源名称
        /// </summary>
        /// <returns>名称</returns>
        public override string GetName()
        {
            return "tomcat";
        }
    }
}
