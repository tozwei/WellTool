using System;
using System.Data;
using WellTool.Core.Map;

namespace WellTool.DB
{
    /// <summary>
    /// 数据访问层模板
    /// 此模板用于简化对指定表的操作
    /// </summary>
    public class DaoTemplate
    {
        /// <summary>
        /// 表名
        /// </summary>
        protected string _tableName;
        /// <summary>
        /// 主键字段
        /// </summary>
        protected string _primaryKeyField = "id";
        /// <summary>
        /// 数据库对象
        /// </summary>
        protected Db _db;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="tableName">表名</param>
        public DaoTemplate(string tableName)
            : this(tableName, null)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="primaryKeyField">主键字段</param>
        public DaoTemplate(string tableName, string primaryKeyField)
            : this(tableName, primaryKeyField, null)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="primaryKeyField">主键字段</param>
        /// <param name="db">数据库对象</param>
        public DaoTemplate(string tableName, string primaryKeyField, Db db)
        {
            _tableName = tableName;
            if (!string.IsNullOrEmpty(primaryKeyField))
            {
                _primaryKeyField = primaryKeyField;
            }
            _db = db;
        }

        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>插入行数</returns>
        public int Add(Entity entity)
        {
            return _db.Insert(FixEntity(entity));
        }

        /// <summary>
        /// 添加记录并返回自增主键
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>自增主键</returns>
        public long AddForGeneratedKey(Entity entity)
        {
            return _db.InsertForGeneratedKey(FixEntity(entity));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pk">主键值</param>
        /// <returns>删除行数</returns>
        public int Delete(object pk)
        {
            if (pk == null)
            {
                return 0;
            }
            return _db.Delete(Entity.Create(_tableName).Set(_primaryKeyField, pk));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="where">删除条件</param>
        /// <returns>删除行数</returns>
        public int Delete(Entity where)
        {
            if (MapUtil.IsEmpty(where))
            {
                return 0;
            }
            return _db.Delete(FixEntity(where));
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="record">更新的内容</param>
        /// <param name="where">条件</param>
        /// <returns>更新条目数</returns>
        public int Update(Entity record, Entity where)
        {
            if (MapUtil.IsEmpty(record))
            {
                return 0;
            }
            return _db.Update(FixEntity(record), where);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体对象，必须包含主键</param>
        /// <returns>更新行数</returns>
        public int Update(Entity entity)
        {
            if (MapUtil.IsEmpty(entity))
            {
                return 0;
            }
            entity = FixEntity(entity);
            object pk = entity.Get<object>(_primaryKeyField);
            if (pk == null)
            {
                throw new Exception($"Please determine `{_primaryKeyField}` for update");
            }

            var where = Entity.Create(_tableName).Set(_primaryKeyField, pk);
            var record = entity.Clone();
            record.Remove(_primaryKeyField);

            return _db.Update(record, where);
        }

        /// <summary>
        /// 根据主键获取单个记录
        /// </summary>
        /// <param name="pk">主键值</param>
        /// <returns>记录</returns>
        public Entity Get(object pk)
        {
            return Get(_primaryKeyField, pk);
        }

        /// <summary>
        /// 根据某个字段查询单个记录
        /// </summary>
        /// <param name="field">字段名</param>
        /// <param name="value">字段值</param>
        /// <returns>记录</returns>
        public Entity Get(string field, object value)
        {
            return _db.Get(Entity.Create(_tableName).Set(field, value));
        }

        /// <summary>
        /// 根据条件查询单个记录
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns>记录</returns>
        public Entity Get(Entity where)
        {
            return _db.Get(FixEntity(where));
        }

        /// <summary>
        /// 查询当前表的所有记录
        /// </summary>
        /// <returns>记录列表</returns>
        public System.Collections.Generic.List<Entity> FindAll()
        {
            return Find(Entity.Create(_tableName));
        }

        /// <summary>
        /// 根据条件查询记录
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns>记录列表</returns>
        public System.Collections.Generic.List<Entity> Find(Entity where)
        {
            return _db.FindList(FixEntity(where));
        }

        /// <summary>
        /// 统计记录数
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns>数量</returns>
        public long Count(Entity where)
        {
            return _db.Count(FixEntity(where));
        }

        /// <summary>
        /// 指定条件的数据是否存在
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns>是否存在</returns>
        public bool Exists(Entity where)
        {
            return Count(where) > 0;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="page">分页对象</param>
        /// <returns>分页结果</returns>
        public PageResult<Entity> Page(Entity where, WellTool.DB.Sql.Page page)
        {
            return _db.Page(null, FixEntity(where), page);
        }

        /// <summary>
        /// 修正Entity对象
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns>修正后的实体类</returns>
        private Entity FixEntity(Entity entity)
        {
            if (entity == null)
            {
                entity = Entity.Create(_tableName);
            }
            else if (string.IsNullOrEmpty(entity.GetTableName()))
            {
                entity.SetTableName(_tableName);
            }
            return entity;
        }
    }
}
