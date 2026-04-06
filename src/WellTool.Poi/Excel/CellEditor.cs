using System;

namespace WellTool.Poi.Excel
{
    /// <summary>
    /// 单元格编辑器
    /// </summary>
    public class CellEditor
    {
        private object _value;
        private string _formula;
        private bool _isSetFormula;

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>this</returns>
        public CellEditor SetValue(object value)
        {
            _value = value;
            _isSetFormula = false;
            return this;
        }

        /// <summary>
        /// 设置公式
        /// </summary>
        /// <param name="formula">公式</param>
        /// <returns>this</returns>
        public CellEditor SetFormula(string formula)
        {
            _formula = formula;
            _isSetFormula = true;
            return this;
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <returns>值</returns>
        public object GetValue()
        {
            return _value;
        }

        /// <summary>
        /// 获取公式
        /// </summary>
        /// <returns>公式</returns>
        public string GetFormula()
        {
            return _formula;
        }

        /// <summary>
        /// 是否设置了公式
        /// </summary>
        /// <returns>是否设置公式</returns>
        public bool IsSetFormula()
        {
            return _isSetFormula;
        }
    }
}