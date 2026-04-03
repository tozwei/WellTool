using System.Data;

namespace WellTool.DB.DS.C3P0
{
    /// <summary>
    /// C3P0数据源工厂
    /// 
    /// 需要安装 C3P0 或类似 NuGet 包
    /// 项目地址：https://github.com/swaldman/c3p0
    /// </summary>
    public class C3P0Factory : AbstractDSFactory
    {
        /// <summary>
        /// 构造
        /// </summary>
        public C3P0Factory()
        {
        }

        /// <summary>
        /// 获取数据源
        /// </summary>
        /// <returns>数据源</returns>
        public override IDbDataSource GetDataSource()
        {
            // TODO: 需要集成 c3p0 或类似实现
            return null;
        }

        /// <summary>
        /// 获取数据源名称
        /// </summary>
        /// <returns>名称</returns>
        public override string GetName()
        {
            return "c3p0";
        }
    }
}
