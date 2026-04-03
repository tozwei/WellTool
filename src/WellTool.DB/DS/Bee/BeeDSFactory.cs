using System.Data;

namespace WellTool.DB.DS.Bee
{
    /// <summary>
    /// Bee数据源工厂
    /// 
    /// 需要安装 Bee 或类似 NuGet 包
    /// </summary>
    public class BeeDSFactory : AbstractDSFactory
    {
        /// <summary>
        /// 构造
        /// </summary>
        public BeeDSFactory()
        {
        }

        /// <summary>
        /// 获取数据源
        /// </summary>
        /// <returns>数据源</returns>
        public override IDbDataSource GetDataSource()
        {
            // TODO: 需要集成 Bee 或类似实现
            return null;
        }

        /// <summary>
        /// 获取数据源名称
        /// </summary>
        /// <returns>名称</returns>
        public override string GetName()
        {
            return "bee";
        }
    }
}
