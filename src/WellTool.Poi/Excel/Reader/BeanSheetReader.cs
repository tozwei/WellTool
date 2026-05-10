using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using WellTool.Poi.Excel.Cell;

namespace WellTool.Poi.Excel.Reader
{
    /// <summary>
    /// 读取<see cref="ISheet"/>为bean的List列表形式
    /// </summary>
    /// <typeparam name="T">Bean类型</typeparam>
    public class BeanSheetReader<T> : ISheetReader<List<T>>
    {
        private readonly Type _beanType;
        private readonly MapSheetReader _mapSheetReader;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="headerRowIndex">标题所在行，如果标题行在读取的内容行中间，这行做为数据将忽略</param>
        /// <param name="startRowIndex">起始行（包含，从0开始计数）</param>
        /// <param name="endRowIndex">结束行（包含，从0开始计数）</param>
        /// <param name="beanType">每行对应Bean的类型</param>
        public BeanSheetReader(int headerRowIndex, int startRowIndex, int endRowIndex, Type beanType)
        {
            _mapSheetReader = new MapSheetReader(headerRowIndex, startRowIndex, endRowIndex);
            _beanType = beanType;
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="sheet"><see cref="ISheet"/></param>
        /// <returns>读取结果</returns>
        public List<T> Read(ISheet sheet)
        {
            var mapList = _mapSheetReader.Read(sheet);
            if (typeof(IDictionary<string, object>).IsAssignableFrom(_beanType))
            {
                return (List<T>)(object)mapList;
            }

            var beanList = new List<T>(mapList.Count);
            foreach (var map in mapList)
            {
                beanList.Add(MapToBean(map));
            }
            return beanList;
        }

        /// <summary>
        /// 将Map转换为Bean
        /// </summary>
        /// <param name="map">Map数据</param>
        /// <returns>Bean对象</returns>
        private T MapToBean(Dictionary<string, object> map)
        {
            var bean = Activator.CreateInstance<T>();
            var properties = _beanType.GetProperties();
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

        /// <summary>
        /// 设置单元格值处理逻辑<br>
        /// 当Excel中的值并不能满足我们的读取要求时，通过传入一个编辑接口，可以对单元格值自定义，例如对数字和日期类型值转换为字符串等
        /// </summary>
        /// <param name="cellEditor">单元格值处理接口</param>
        public void SetCellEditor(ICellEditor cellEditor)
        {
            _mapSheetReader.SetCellEditor(cellEditor);
        }

        /// <summary>
        /// 设置是否忽略空行
        /// </summary>
        /// <param name="ignoreEmptyRow">是否忽略空行</param>
        public void SetIgnoreEmptyRow(bool ignoreEmptyRow)
        {
            _mapSheetReader.SetIgnoreEmptyRow(ignoreEmptyRow);
        }

        /// <summary>
        /// 设置标题行的别名Map
        /// </summary>
        /// <param name="headerAlias">别名Map</param>
        public void SetHeaderAlias(Dictionary<string, string> headerAlias)
        {
            _mapSheetReader.SetHeaderAlias(headerAlias);
        }

        /// <summary>
        /// 增加标题别名
        /// </summary>
        /// <param name="header">标题</param>
        /// <param name="alias">别名</param>
        public void AddHeaderAlias(string header, string alias)
        {
            _mapSheetReader.AddHeaderAlias(header, alias);
        }
    }
}