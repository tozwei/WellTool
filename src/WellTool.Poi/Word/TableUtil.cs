using NPOI.XWPF.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace WellTool.Poi.Word
{
    /// <summary>
    /// Word中表格相关工具
    /// </summary>
    public static class TableUtil
    {
        /// <summary>
        /// 创建空表，只有一行
        /// </summary>
        /// <param name="doc"><see cref="XWPFDocument"/></param>
        /// <returns><see cref="XWPFTable"/></returns>
        public static XWPFTable CreateTable(XWPFDocument doc)
        {
            return CreateTable(doc, null);
        }

        /// <summary>
        /// 创建表格并填充数据，默认表格
        /// </summary>
        /// <param name="doc"><see cref="XWPFDocument"/></param>
        /// <param name="data">数据</param>
        /// <returns><see cref="XWPFTable"/></returns>
        public static XWPFTable CreateTable(XWPFDocument doc, IEnumerable data)
        {
            if (doc == null)
            {
                throw new ArgumentNullException(nameof(doc), "XWPFDocument must be not null!");
            }

            var table = doc.CreateTable();
            // 新建table的时候默认会新建一行，此处移除之
            if (table.Rows.Count > 0)
            {
                table.RemoveRow(0);
            }
            return WriteTable(table, data);
        }

        /// <summary>
        /// 为table填充数据
        /// </summary>
        /// <param name="table"><see cref="XWPFTable"/></param>
        /// <param name="data">数据</param>
        /// <returns><see cref="XWPFTable"/></returns>
        public static XWPFTable WriteTable(XWPFTable table, IEnumerable data)
        {
            if (table == null)
            {
                throw new ArgumentNullException(nameof(table), "XWPFTable must be not null!");
            }

            if (data == null)
            {
                // 数据为空，返回空表
                return table;
            }

            bool isFirst = true;
            foreach (var rowData in data)
            {
                var row = table.CreateRow();
                WriteRow(row, rowData, isFirst);
                if (isFirst)
                {
                    isFirst = false;
                }
            }

            return table;
        }

        /// <summary>
        /// 写一行数据
        /// </summary>
        /// <param name="row">行</param>
        /// <param name="rowBean">行数据</param>
        /// <param name="isWriteKeyAsHead">如果为Map或者Bean，是否写标题</param>
        public static void WriteRow(XWPFTableRow row, object rowBean, bool isWriteKeyAsHead)
        {
            if (rowBean is IEnumerable enumerable)
            {
                WriteRow(row, enumerable);
                return;
            }

            IDictionary rowMap = null;
            if (rowBean is IDictionary dictionary)
            {
                rowMap = dictionary;
            }
            else if (rowBean != null && rowBean.GetType().IsClass && rowBean.GetType() != typeof(string))
            {
                rowMap = BeanToMap(rowBean);
            }
            else
            {
                // 其它转为字符串默认输出
                WriteRow(row, new List<object> { rowBean }, isWriteKeyAsHead);
                return;
            }

            WriteRow(row, rowMap, isWriteKeyAsHead);
        }

        /// <summary>
        /// 写行数据
        /// </summary>
        /// <param name="row">行</param>
        /// <param name="rowMap">行数据</param>
        /// <param name="isWriteKeyAsHead">是否写标题</param>
        public static void WriteRow(XWPFTableRow row, IDictionary rowMap, bool isWriteKeyAsHead)
        {
            if (rowMap == null || rowMap.Count == 0)
            {
                return;
            }

            if (isWriteKeyAsHead)
            {
                WriteRow(row, rowMap.Keys);
                // 创建新行
                var table = GetTableFromRow(row);
                if (table != null)
                {
                    row = table.CreateRow();
                }
            }
            WriteRow(row, rowMap.Values);
        }

        /// <summary>
        /// 写行数据
        /// </summary>
        /// <param name="row">行</param>
        /// <param name="rowData">行数据</param>
        public static void WriteRow(XWPFTableRow row, IEnumerable rowData)
        {
            if (rowData == null)
            {
                return;
            }

            int index = 0;
            foreach (var cellData in rowData)
            {
                var cell = GetOrCreateCell(row, index);
                cell.SetText(cellData?.ToString() ?? string.Empty);
                index++;
            }
        }

        /// <summary>
        /// 获取或创建新行<br>
        /// 存在则直接返回，不存在创建新的行
        /// </summary>
        /// <param name="table"><see cref="XWPFTable"/></param>
        /// <param name="index">索引（行号），从0开始</param>
        /// <returns><see cref="XWPFTableRow"/></returns>
        public static XWPFTableRow GetOrCreateRow(XWPFTable table, int index)
        {
            var row = table.GetRow(index);
            if (row == null)
            {
                row = table.CreateRow();
            }
            return row;
        }

        /// <summary>
        /// 获取或创建新单元格<br>
        /// 存在则直接返回，不存在创建新的单元格
        /// </summary>
        /// <param name="row"><see cref="XWPFTableRow"/> 行</param>
        /// <param name="index">索引（列号），从0开始</param>
        /// <returns><see cref="XWPFTableCell"/></returns>
        public static XWPFTableCell GetOrCreateCell(XWPFTableRow row, int index)
        {
            var cell = row.GetCell(index);
            if (cell == null)
            {
                cell = row.CreateCell();
            }
            return cell;
        }

        /// <summary>
        /// 通过反射获取XWPFTableRow所属的表格
        /// </summary>
        /// <param name="row">行对象</param>
        /// <returns>表格对象</returns>
        private static XWPFTable GetTableFromRow(XWPFTableRow row)
        {
            if (row == null)
            {
                return null;
            }

            try
            {
                // 尝试通过反射获取Table属性
                var type = row.GetType();
                var property = type.GetProperty("Table", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (property != null)
                {
                    return (XWPFTable)property.GetValue(row);
                }

                // 尝试通过字段获取
                var field = type.GetField("table", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (field != null)
                {
                    return (XWPFTable)field.GetValue(row);
                }
            }
            catch
            {
                // 忽略反射错误
            }

            return null;
        }

        /// <summary>
        /// 将对象转换为字典
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>字典</returns>
        private static IDictionary BeanToMap(object obj)
        {
            var map = new Dictionary<string, object>();
            if (obj == null)
            {
                return map;
            }

            var type = obj.GetType();
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in properties)
            {
                if (property.CanRead)
                {
                    map[property.Name] = property.GetValue(obj);
                }
            }
            return map;
        }
    }
}