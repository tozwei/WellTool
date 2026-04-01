using System.Data;
using System.Collections.Generic;
using WellTool.DB;

namespace WellTool.DB.Meta
{
    /// <summary>
    /// 数据库元数据工具类
    /// </summary>
    public static class MetaUtil
    {
        /// <summary>
        /// 获取数据库表信息
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="tableName">表名</param>
        /// <returns>表信息</returns>
        public static Table GetTable(IDbConnection connection, string tableName)
        {
            if (connection == null)
            {
                throw new DbRuntimeException("Database connection is null");
            }

            Table table = new Table(tableName);

            // 获取列信息
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = string.Format("SELECT * FROM {0} WHERE 1=0", tableName);
                using (IDataReader reader = command.ExecuteReader())
                {
                    DataTable schemaTable = reader.GetSchemaTable();
                    foreach (DataRow row in schemaTable.Rows)
                    {
                        Column column = new Column
                        {
                            Name = row["ColumnName"].ToString(),
                            Type = row["DataTypeName"].ToString(),
                            Size = Convert.ToInt32(row["ColumnSize"]),
                            DecimalDigits = Convert.ToInt32(row["NumericScale"]),
                            IsNullable = Convert.ToBoolean(row["AllowDBNull"])
                        };
                        table.AddColumn(column);
                    }
                }
            }

            return table;
        }

        /// <summary>
        /// 获取数据库所有表信息
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <returns>表信息列表</returns>
        public static List<Table> GetTables(IDbConnection connection)
        {
            if (connection == null)
            {
                throw new DbRuntimeException("Database connection is null");
            }

            List<Table> tables = new List<Table>();

            // 获取表信息
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT name FROM sys.tables";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string tableName = reader.GetString(0);
                        tables.Add(GetTable(connection, tableName));
                    }
                }
            }

            return tables;
        }

        /// <summary>
        /// 获取数据库所有表名
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <returns>表名列表</returns>
        public static List<string> GetTableNames(IDbConnection connection)
        {
            if (connection == null)
            {
                throw new DbRuntimeException("Database connection is null");
            }

            List<string> tableNames = new List<string>();

            // 获取表名
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "SELECT name FROM sys.tables";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tableNames.Add(reader.GetString(0));
                    }
                }
            }

            return tableNames;
        }
    }
}