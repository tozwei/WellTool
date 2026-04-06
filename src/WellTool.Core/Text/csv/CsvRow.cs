using System;
using System.Collections.Generic;
using System.Linq;

namespace WellTool.Core.Text.Csv
{
    /// <summary>
    /// CSV行
    /// </summary>
    public class CsvRow
    {
        private readonly CsvData _data;
        private readonly List<string> _values;

        /// <summary>
        /// 构造函数
        /// </summary>
        public CsvRow(CsvData data, IEnumerable<string> values)
        {
            _data = data;
            _values = new List<string>(values);
        }

        /// <summary>
        /// 获取或设置值
        /// </summary>
        public string this[int index]
        {
            get => index >= 0 && index < _values.Count ? _values[index] : null;
            set
            {
                while (_values.Count <= index)
                {
                    _values.Add(null);
                }
                _values[index] = value;
            }
        }

        /// <summary>
        /// 获取或设置值
        /// </summary>
        public string this[string header]
        {
            get
            {
                var index = _data.Headers.IndexOf(header);
                return index >= 0 ? this[index] : null;
            }
            set
            {
                var index = _data.Headers.IndexOf(header);
                if (index >= 0)
                {
                    this[index] = value;
                }
            }
        }

        /// <summary>
        /// 列数
        /// </summary>
        public int Count => _values.Count;

        /// <summary>
        /// 获取所有值
        /// </summary>
        public List<string> Values => new List<string>(_values);

        /// <summary>
        /// 转换为数组
        /// </summary>
        public string[] ToArray()
        {
            return _values.ToArray();
        }

        /// <summary>
        /// 转换为指定类型的对象
        /// </summary>
        public T ToObject<T>() where T : new()
        {
            var result = new T();
            var type = typeof(T);
            var properties = type.GetProperties();

            foreach (var prop in properties)
            {
                var header = prop.Name;
                var index = _data.Headers.IndexOf(header);
                if (index >= 0 && index < _values.Count)
                {
                    var value = _values[index];
                    if (!string.IsNullOrEmpty(value))
                    {
                        try
                        {
                            var converted = System.Convert.ChangeType(value, prop.PropertyType);
                            prop.SetValue(result, converted);
                        }
                        catch
                        {
                            // 忽略转换错误
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 获取字典表示
        /// </summary>
        public Dictionary<string, string> ToDictionary()
        {
            var dict = new Dictionary<string, string>();
            for (int i = 0; i < System.Math.Min(_data.Headers.Count, _values.Count); i++)
            {
                dict[_data.Headers[i]] = _values[i];
            }
            return dict;
        }

        public override string ToString()
        {
            return string.Join(",", _values);
        }
    }
}
