using System;
using System.Collections;
using WellTool.Core.Convert;
using WellTool.Core.Lang;

namespace WellTool.Core.Convert.Impl
{
    public class PairConverter : IConverter
    {
        private readonly Type _pairType;
        private readonly Type _keyType;
        private readonly Type _valueType;

        public PairConverter(Type pairType)
            : this(pairType, null, null)
        {
        }

        public PairConverter(Type pairType, Type keyType, Type valueType)
        {
            _pairType = pairType;
            _keyType = keyType;
            _valueType = valueType;
        }

        public object Convert(object value, Type targetType)
        {
            if (value == null)
            {
                return null;
            }

            IDictionary map = null;

            if (value is DictionaryEntry entry)
            {
                map = new Hashtable { { entry.Key, entry.Value } };
            }
            else if (value is IDictionary dict)
            {
                map = dict;
            }
            else if (value is string str)
            {
                map = StrToMap(str);
            }

            if (map != null && map.Count > 0)
            {
                return MapToPair(map);
            }

            throw new ConvertException("Unsupported to convert to Pair");
        }

        private static IDictionary StrToMap(string str)
        {
            int index = str.IndexOf('=');
            if (index > -1)
            {
                return new Hashtable
                {
                    { str.Substring(0, index), str.Substring(index + 1) }
                };
            }
            return null;
        }

        private object MapToPair(IDictionary map)
        {
            object key = null;
            object value = null;

            if (map.Count == 1)
            {
                foreach (DictionaryEntry entry in map)
                {
                    key = entry.Key;
                    value = entry.Value;
                    break;
                }
            }
            else if (map.Count >= 2)
            {
                key = map["key"] ?? map.Keys.Cast<object>().FirstOrDefault();
                value = map["value"] ?? map.Values.Cast<object>().FirstOrDefault();
            }

            return new Pair<object, object>(key, value);
        }

        public Type[] GetSupportedSourceTypes()
        {
            return new[] { typeof(IDictionary), typeof(string), typeof(DictionaryEntry) };
        }

        public Type[] GetSupportedTargetTypes()
        {
            return new[] { typeof(Pair<object, object>) };
        }
    }
}