using System.Data;

namespace WellTool.DB.DS.Druid
{
    /// <summary>
    /// Druid数据源工厂
    /// 
    /// 需要安装 Druid.DataSource 或类似 NuGet 包
    /// 项目地址：https://github.com/alibaba/druid
    /// </summary>
    public class DruidDSFactory : AbstractDSFactory
    {
        /// <summary>
        /// 构造
        /// </summary>
        public DruidDSFactory()
        {
        }

        /// <summary>
        /// 获取数据源
        /// </summary>
        /// <returns>数据源</returns>
        public override IDbDataSource GetDataSource()
        {
            // TODO: 需要集成 Druid 或 System.Data.SQLite 等实现
            return null;
        }

        /// <summary>
        /// 获取数据源名称
        /// </summary>
        /// <returns>名称</returns>
        public override string GetName()
        {
            return "druid";
        }
    }
}
