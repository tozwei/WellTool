using System;
using System.Collections.Generic;

namespace WellTool.Json
{
    /// <summary>
    /// JSON 配置项
    /// </summary>
    public class JSONConfig
    {
        /// <summary>
        /// 键排序规则，null 表示不排序，不排序情况下，按照加入顺序排序
        /// </summary>
        private IComparer<string> _keyComparator;

        /// <summary>
        /// 是否忽略转换过程中的异常
        /// </summary>
        private bool _ignoreError;

        /// <summary>
        /// 是否忽略键的大小写
        /// </summary>
        private bool _ignoreCase;

        /// <summary>
        /// 日期格式，null 表示默认的时间戳
        /// </summary>
        private string _dateFormat;

        /// <summary>
        /// 是否忽略 null 值，默认为 true
        /// </summary>
        private bool _ignoreNullValue = true;

        /// <summary>
        /// 是否支持 transient 关键字修饰，如果支持，被修饰的字段将被忽略
        /// </summary>
        private bool _transientSupport = true;

        /// <summary>
        /// 是否去除末尾多余0，例如如果为 true, 5.0 返回 5
        /// </summary>
        private bool _stripTrailingZeros = true;

        /// <summary>
        /// 是否检查重复 key
        /// </summary>
        private bool _checkDuplicate;

        /// <summary>
        /// 是否将 Long 值写出为字符串类型
        /// </summary>
        private bool _writeLongAsString;

        /// <summary>
        /// 创建默认的配置项
        /// </summary>
        /// <returns>JSONConfig</returns>
        public static JSONConfig Create()
        {
            return new JSONConfig();
        }

        /// <summary>
        /// 获取键排序规则
        /// </summary>
        /// <returns>键排序规则，null 表示不排序</returns>
        public IComparer<string> GetKeyComparator()
        {
            return _keyComparator;
        }

        /// <summary>
        /// 设置自然排序，即按照字母顺序排序
        /// </summary>
        /// <returns>this</returns>
        public JSONConfig SetNatureKeyComparator()
        {
            return SetKeyComparator(Comparer<string>.Default);
        }

        /// <summary>
        /// 设置键排序规则
        /// </summary>
        /// <param name="keyComparator">键排序规则</param>
        /// <returns>this</returns>
        public JSONConfig SetKeyComparator(IComparer<string> keyComparator)
        {
            _keyComparator = keyComparator;
            return this;
        }

        /// <summary>
        /// 是否忽略转换过程中的异常
        /// </summary>
        /// <returns>是否忽略转换过程中的异常</returns>
        public bool IsIgnoreError()
        {
            return _ignoreError;
        }

        /// <summary>
        /// 设置是否忽略转换过程中的异常
        /// </summary>
        /// <param name="ignoreError">是否忽略转换过程中的异常</param>
        /// <returns>this</returns>
        public JSONConfig SetIgnoreError(bool ignoreError)
        {
            _ignoreError = ignoreError;
            return this;
        }

        /// <summary>
        /// 是否忽略键的大小写
        /// </summary>
        /// <returns>是否忽略键的大小写</returns>
        public bool IsIgnoreCase()
        {
            return _ignoreCase;
        }

        /// <summary>
        /// 设置是否忽略键的大小写
        /// </summary>
        /// <param name="ignoreCase">是否忽略键的大小写</param>
        /// <returns>this</returns>
        public JSONConfig SetIgnoreCase(bool ignoreCase)
        {
            _ignoreCase = ignoreCase;
            return this;
        }

        /// <summary>
        /// 获取日期格式
        /// </summary>
        /// <returns>日期格式，null 表示默认的时间戳</returns>
        public string GetDateFormat()
        {
            return _dateFormat;
        }

        /// <summary>
        /// 设置日期格式，null 表示默认的时间戳
        /// </summary>
        /// <param name="dateFormat">日期格式</param>
        /// <returns>this</returns>
        public JSONConfig SetDateFormat(string dateFormat)
        {
            _dateFormat = dateFormat;
            return this;
        }

        /// <summary>
        /// 是否忽略 null 值
        /// </summary>
        /// <returns>是否忽略 null 值</returns>
        public bool IsIgnoreNullValue()
        {
            return _ignoreNullValue;
        }

        /// <summary>
        /// 设置是否忽略 null 值
        /// </summary>
        /// <param name="ignoreNullValue">是否忽略 null 值</param>
        /// <returns>this</returns>
        public JSONConfig SetIgnoreNullValue(bool ignoreNullValue)
        {
            _ignoreNullValue = ignoreNullValue;
            return this;
        }

        /// <summary>
        /// 是否支持 transient 关键字修饰
        /// </summary>
        /// <returns>是否支持</returns>
        public bool IsTransientSupport()
        {
            return _transientSupport;
        }

        /// <summary>
        /// 设置是否支持 transient 关键字修饰
        /// </summary>
        /// <param name="transientSupport">是否支持</param>
        /// <returns>this</returns>
        public JSONConfig SetTransientSupport(bool transientSupport)
        {
            _transientSupport = transientSupport;
            return this;
        }

        /// <summary>
        /// 是否去除末尾多余0
        /// </summary>
        /// <returns>是否去除末尾多余0</returns>
        public bool IsStripTrailingZeros()
        {
            return _stripTrailingZeros;
        }

        /// <summary>
        /// 设置是否去除末尾多余0
        /// </summary>
        /// <param name="stripTrailingZeros">是否去除末尾多余0</param>
        /// <returns>this</returns>
        public JSONConfig SetStripTrailingZeros(bool stripTrailingZeros)
        {
            _stripTrailingZeros = stripTrailingZeros;
            return this;
        }

        /// <summary>
        /// 是否检查重复 key
        /// </summary>
        /// <returns>是否检查重复 key</returns>
        public bool IsCheckDuplicate()
        {
            return _checkDuplicate;
        }

        /// <summary>
        /// 设置是否检查重复 key
        /// </summary>
        /// <param name="checkDuplicate">是否检查重复 key</param>
        /// <returns>this</returns>
        public JSONConfig SetCheckDuplicate(bool checkDuplicate)
        {
            _checkDuplicate = checkDuplicate;
            return this;
        }

        /// <summary>
        /// 是否将 Long 值写出为字符串类型
        /// </summary>
        /// <returns>是否将 Long 值写出为字符串类型</returns>
        public bool IsWriteLongAsString()
        {
            return _writeLongAsString;
        }

        /// <summary>
        /// 设置是否将 Long 值写出为字符串类型
        /// </summary>
        /// <param name="writeLongAsString">是否将 Long 值写出为字符串类型</param>
        /// <returns>this</returns>
        public JSONConfig SetWriteLongAsString(bool writeLongAsString)
        {
            _writeLongAsString = writeLongAsString;
            return this;
        }

        /// <summary>
        /// 创建配置副本
        /// </summary>
        /// <returns>配置副本</returns>
        public JSONConfig Copy()
        {
            return new JSONConfig
            {
                _keyComparator = _keyComparator,
                _ignoreError = _ignoreError,
                _ignoreCase = _ignoreCase,
                _dateFormat = _dateFormat,
                _ignoreNullValue = _ignoreNullValue,
                _transientSupport = _transientSupport,
                _stripTrailingZeros = _stripTrailingZeros,
                _checkDuplicate = _checkDuplicate,
                _writeLongAsString = _writeLongAsString
            };
        }
    }
}
