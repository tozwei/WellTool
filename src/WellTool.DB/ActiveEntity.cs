namespace WellTool.DB
{
    /// <summary>
    /// 活动实体，继承自Entity，提供了更丰富的数据库操作方法
    /// </summary>
    public class ActiveEntity : Entity
    {
        /// <summary>
        /// 构造
        /// </summary>
        public ActiveEntity() : base()
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="tableName">表名</param>
        public ActiveEntity(string tableName) : base(tableName)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="capacity">初始容量</param>
        public ActiveEntity(string tableName, int capacity) : base(tableName, capacity)
        {
        }

        /// <summary>
        /// 保存当前实体到数据库
        /// </summary>
        /// <returns>影响的行数</returns>
        public int Save()
        {
            return Db.Use().Save(this);
        }

        /// <summary>
        /// 从数据库中删除当前实体
        /// </summary>
        /// <returns>影响的行数</returns>
        public int Remove()
        {
            return Db.Use().Delete(this);
        }

        /// <summary>
        /// 从数据库中更新当前实体
        /// </summary>
        /// <returns>影响的行数</returns>
        public int Update()
        {
            return Db.Use().Update(this);
        }

        /// <summary>
        /// 从数据库中获取当前实体
        /// </summary>
        /// <returns>是否获取成功</returns>
        public bool Load()
        {
            return Db.Use().Find(this) != null;
        }
    }
}