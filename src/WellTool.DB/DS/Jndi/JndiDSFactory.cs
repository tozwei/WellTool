using System.Data;

namespace WellTool.DB.DS.Jndi
{
    /// <summary>
    /// JNDI数据源工厂
    /// 
    /// 需要安装 JNDI 支持
    /// </summary>
    public class JndiDSFactory : AbstractDSFactory
    {
        /// <summary>
        /// 构造
        /// </summary>
        public JndiDSFactory()
        {
        }

        /// <summary>
        /// 获取数据源
        /// </summary>
        /// <returns>数据源</returns>
        public override IDbDataSource GetDataSource()
        {
            // TODO: 需要实现 JNDI 查找逻辑
            return null;
        }

        /// <summary>
        /// 获取数据源名称
        /// </summary>
        /// <returns>名称</returns>
        public override string GetName()
        {
            return "jndi";
        }
    }
}
