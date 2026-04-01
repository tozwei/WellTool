using System;using System.Collections.Generic;
using WellTool.Core.Util;

namespace WellTool.Poi.Excel.Sax.Handler
{
    /// <summary>
    /// Bean形式的行处理器<br>
    /// 将一行数据转换为Bean，key为指定行，value为当前行对应位置的值
    /// </summary>
    /// <typeparam name="T">Bean类型</typeparam>
    public abstract class BeanRowHandler<T> : AbstractRowHandler<T>
    {
        /// <summary>
        /// 标题所在行（从0开始计数）
        /// </summary>
        private readonly int _headerRowIndex;
        
        /// <summary>
        /// 标题行
        /// </summary>
        protected List<string> HeaderList;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="headerRowIndex">标题所在行（从0开始计数）</param>
        /// <param name="startRowIndex">读取起始行（包含，从0开始计数）</param>
        /// <param name="endRowIndex">读取结束行（包含，从0开始计数）</param>
        /// <param name="beanType">Bean类型</param>
        public BeanRowHandler(int headerRowIndex, int startRowIndex, int endRowIndex, Type beanType)
            : base(startRowIndex, endRowIndex)
        {
            if (headerRowIndex > startRowIndex)
            {
                throw new ArgumentException("Header row must before the start row!");
            }
            _headerRowIndex = headerRowIndex;
            ConvertFunc = (rowList) => MapToBean(IterUtil.ToDictionary(HeaderList, rowList, true), beanType);
        }

        /// <summary>
        /// 处理一行数据
        /// </summary>
        /// <param name="sheetIndex">当前Sheet序号</param>
        /// <param name="rowIndex">当前行号，从0开始计数</param>
        /// <param name="rowCells">行数据，每个object表示一个单元格的值</param>
        public override void Handle(int sheetIndex, long rowIndex, List<object> rowCells)
        {
            if (rowIndex == _headerRowIndex)
            {
                HeaderList = rowCells.ConvertAll(obj => obj?.ToString() ?? string.Empty);
                return;
            }
            base.Handle(sheetIndex, rowIndex, rowCells);
        }

        /// <summary>
        /// 将Map转换为Bean
        /// </summary>
        /// <param name="map">Map数据</param>
        /// <param name="beanType">Bean类型</param>
        /// <returns>Bean对象</returns>
        private T MapToBean(Dictionary<string, object> map, Type beanType)
        {
            var bean = Activator.CreateInstance<T>();
            var properties = beanType.GetProperties();
            foreach (var property in properties)
            {
                if (map.TryGetValue(property.Name, out var value) && value != null)
                {
                    try
                    {
                        var convertedValue = Convert.ChangeType(value, property.PropertyType);
                        property.SetValue(bean, convertedValue);
                    }
                    catch
                    {
                        // 忽略类型转换错误
                    }
                }
            }
            return bean;
        }
    }
}