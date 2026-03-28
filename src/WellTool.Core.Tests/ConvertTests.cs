using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using WellTool.Core.Converter;
using WellTool.Core.Date;
using WellTool.Core.Lang;
using WellTool.Core.Map;
using WellTool.Core.Util;

// 使用别名避免Assert引用歧义
using XAssert = Xunit.Assert;
// 使用别名避免Convert引用歧义
using WellConvert = WellTool.Core.Converter.Convert;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// 类型转换工具单元测试
    /// </summary>
    public class ConvertTests
    {
        [Fact]
        public void ToObjectTest()
        {
            var result = WellConvert.ConvertObject(typeof(object), "aaaa");
            XAssert.Equal("aaaa", result);
        }

        [Fact]
        public void ToStrTest()
        {
            int a = 1;
            long[] b = { 1, 2, 3, 4, 5 };

            XAssert.Equal("[1, 2, 3, 4, 5]", WellConvert.ConvertObject(typeof(string), b));

            string aStr = WellConvert.ToStr(a);
            XAssert.Equal("1", aStr);
            string bStr = WellConvert.ToStr(b);
            XAssert.Equal("[1, 2, 3, 4, 5]", WellConvert.ToStr(b));
        }

        [Fact]
        public void ToStrTest2()
        {
            var result = WellConvert.ConvertObject(typeof(string), "aaaa");
            XAssert.Equal("aaaa", result);
        }

        [Fact]
        public void ToStrTest3()
        {
            char a = 'a';
            var result = WellConvert.ConvertObject(typeof(string), a);
            XAssert.Equal("a", result);
        }

        [Fact]
        public void ToStrTest4()
        {
            // 被当作八进制
            string result = WellConvert.ToStr(001200);
            XAssert.Equal("640", result);
        }

        [Fact]
        public void ToIntTest()
        {
            string a = " 34232";
            int? aInteger = WellConvert.ToInt(a);
            XAssert.Equal(34232, aInteger);
            int aInt = ConverterRegistry.Instance.Convert<int>(a);
            XAssert.Equal(34232, aInt);

            // 带小数测试
            string b = " 34232.00";
            int? bInteger = WellConvert.ToInt(b);
            XAssert.Equal(34232, bInteger);
            int bInt = ConverterRegistry.Instance.Convert<int>(b);
            XAssert.Equal(34232, bInt);

            // boolean测试
            bool c = true;
            int? cInteger = WellConvert.ToInt(c);
            XAssert.Equal(1, cInteger);
            int cInt = ConverterRegistry.Instance.Convert<int>(c);
            XAssert.Equal(1, cInt);

            // boolean测试
            string d = "08";
            int? dInteger = WellConvert.ToInt(d);
            XAssert.Equal(8, dInteger);
            int dInt = ConverterRegistry.Instance.Convert<int>(d);
            XAssert.Equal(8, dInt);
        }

        [Fact]
        public void ToIntTest2()
        {
            var array = new List<string>();
            int? aInt = WellConvert.ConvertQuietly<int>(array, -1);
            XAssert.Equal(-1, aInt);
        }

        [Fact]
        public void ToLongTest()
        {
            string a = " 342324545435435";
            long? aLong = WellConvert.ToLong(a);
            XAssert.Equal(342324545435435L, aLong);
            long aLong2 = ConverterRegistry.Instance.Convert<long>(a);
            XAssert.Equal(342324545435435L, aLong2);

            // 带小数测试
            string b = " 342324545435435.245435435";
            long? bLong = WellConvert.ToLong(b);
            XAssert.Equal(342324545435435L, bLong);
            long bLong2 = ConverterRegistry.Instance.Convert<long>(b);
            XAssert.Equal(342324545435435L, bLong2);

            // boolean测试
            bool c = true;
            long? cLong = WellConvert.ToLong(c);
            XAssert.Equal(1L, cLong);
            long cLong2 = ConverterRegistry.Instance.Convert<long>(c);
            XAssert.Equal(1L, cLong2);

            // boolean测试
            string d = "08";
            long? dLong = WellConvert.ToLong(d);
            XAssert.Equal(8L, dLong);
            long dLong2 = ConverterRegistry.Instance.Convert<long>(d);
            XAssert.Equal(8L, dLong2);
        }

        [Fact]
        public void ToCharTest()
        {
            string str = "aadfdsfs";
            char? c = WellConvert.ToChar(str);
            XAssert.Equal('a', c);

            // 转换失败
            object str2 = "";
            char? c2 = WellConvert.ToChar(str2);
            XAssert.Null(c2);
        }

        [Fact]
        public void ToNumberTest()
        {
            object a = "12.45";
            decimal? number = WellConvert.ToDecimal(a);
            XAssert.Equal(12.45M, number);
        }

        [Fact]
        public void EmptyToNumberTest()
        {
            object a = "";
            decimal? number = WellConvert.ToDecimal(a);
            XAssert.Null(number);
        }

        [Fact]
        public void IntAndByteConvertTest()
        {
            // 测试 int 转 byte
            int int0 = 234;
            byte byte0 = WellConvert.IntToByte(int0);
            XAssert.Equal((byte)234, byte0);

            int int1 = WellConvert.ByteToUnsignedInt(byte0);
            XAssert.Equal(int0, int1);
        }

        [Fact]
        public void IntAndBytesTest()
        {
            // 测试 int 转 byte 数组
            int int2 = 1417;
            byte[] bytesInt = WellConvert.IntToBytes(int2);

            // 测试 byte 数组转 int
            int int3 = WellConvert.BytesToInt(bytesInt);
            XAssert.Equal(int2, int3);
        }

        [Fact]
        public void LongAndBytesTest()
        {
            // 测试 long 转 byte 数组
            long long1 = 2223;

            byte[] bytesLong = WellConvert.LongToBytes(long1);
            long long2 = WellConvert.BytesToLong(bytesLong);

            XAssert.Equal(long1, long2);
        }

        [Fact]
        public void ShortAndBytesTest()
        {
            short short1 = 122;
            byte[] bytes = WellConvert.ShortToBytes(short1);
            short short2 = WellConvert.BytesToShort(bytes);

            XAssert.Equal(short2, short1);
        }

        [Fact]
        public void ToListTest()
        {
            var list = new List<string> { "1", "2" };
            string str = WellConvert.ToStr(list);
            var list2 = WellConvert.ToList<string>(str);
            XAssert.Equal("1", list2[0]);
            XAssert.Equal("2", list2[1]);

            var list3 = WellConvert.ToList<int>(str);
            XAssert.Equal(1, list3[0]);
            XAssert.Equal(2, list3[1]);
        }

        [Fact]
        public void ToListTest2()
        {
            string str = "1,2";
            var list2 = WellConvert.ToList<string>(str);
            XAssert.Equal("1", list2[0]);
            XAssert.Equal("2", list2[1]);

            var list3 = WellConvert.ToList<int>(str);
            XAssert.Equal(1, list3[0]);
            XAssert.Equal(2, list3[1]);
        }

        [Fact]
        public void ToSetTest()
        {
            var result = WellConvert.ConvertObject(typeof(HashSet<int>), "1,2,3") as HashSet<int>;
            XAssert.NotNull(result);
            XAssert.Equal(3, result.Count);
            XAssert.Contains(1, result);
            XAssert.Contains(2, result);
            XAssert.Contains(3, result);
        }

        [Fact]
        public void ToDateTest()
        {
            XAssert.Throws<ConvertException>(() => {
                // 默认转换失败报错而不是返回null
                WellConvert.ConvertObject(typeof(DateTime), "aaaa");
            });
        }

        [Fact]
        public void ToDateTest2()
        {
            var date = WellConvert.ToDate("2021-01");
            XAssert.Null(date);
        }

        [Fact]
        public void ToBigDecimalTest()
        {
            // https://github.com/chinabugotech/hutool/issues/1818
            string str = "33020000210909112800000124";
            decimal? bigDecimal = WellConvert.ToDecimal(str);
            XAssert.Equal(str, bigDecimal.ToString());
        }

        [Fact]
        public void ToFloatTest()
        {
            // https://gitee.com/chinabugotech/hutool/issues/I4M0E4
            string hex2 = "CD0CCB43";
            byte[] value = new byte[] { 0xCD, 0x0C, 0xCB, 0x43 };
            float? f = WellConvert.ToFloat(value);
            XAssert.Equal(406.1F, f);
        }

        [Fact]
        public void FloatToDoubleTest()
        {
            float a = 0.45f;
            double? b = WellConvert.ToDouble(a);
            XAssert.Equal(0.45D, b);
        }

        [Fact]
        public void DoubleToFloatTest()
        {
            double a = 0.45f;
            float? b = WellConvert.ToFloat(a);
            XAssert.Equal((float)a, b);
        }

        [Fact]
        public void LocalDateTimeToLocalDateTest()
        {
            var localDateTime = DateTime.Now;
            var convert = (DateOnly)WellConvert.ConvertObject(typeof(DateOnly), localDateTime);
            XAssert.Equal(DateOnly.FromDateTime(localDateTime), convert);
        }

        [Fact]
        public void ToSBCTest()
        {
            string s = WellConvert.ToSBC(null);
            XAssert.Null(s);
        }

        [Fact]
        public void ToDBCTest()
        {
            string s = WellConvert.ToDBC(null);
            XAssert.Null(s);
        }

        [Fact]
        public void TestChineseMoneyToNumber()
        {
            /*
             * s=陆万柒仟伍佰伍拾陆圆, n=67556
             * s=陆万柒仟伍佰伍拾陆元, n=67556
             * s=叁角, n=0.3
             * s=贰分, n=0.02
             * s=陆万柒仟伍佰伍拾陆元叁角, n=67556.3
             * s=陆万柒仟伍佰伍拾陆元贰分, n=67556.02
             * s=叁角贰分, n=0.32
             * s=陆万柒仟伍佰伍拾陆元叁角贰分, n=67556.32
             */
            XAssert.Equal(0, WellConvert.ChineseMoneyToNumber("陆万柒仟伍佰伍拾陆圆").Value);
            XAssert.Equal(0, WellConvert.ChineseMoneyToNumber("陆万柒仟伍佰伍拾陆元").Value);
            XAssert.Equal(0, WellConvert.ChineseMoneyToNumber("叁角").Value);
            XAssert.Equal(0, WellConvert.ChineseMoneyToNumber("贰分").Value);
            XAssert.Equal(0, WellConvert.ChineseMoneyToNumber("陆万柒仟伍佰伍拾陆元叁角").Value);
            XAssert.Equal(0, WellConvert.ChineseMoneyToNumber("陆万柒仟伍佰伍拾陆元贰分").Value);
            XAssert.Equal(0, WellConvert.ChineseMoneyToNumber("叁角贰分").Value);
            XAssert.Equal(0, WellConvert.ChineseMoneyToNumber("陆万柒仟伍佰伍拾陆元叁角贰分").Value);
        }

        [Fact]
        public void Issue3662Test()
        {
            string s = WellConvert.DigitToChinese(0);
            XAssert.Equal("零元整", s);

            s = WellConvert.DigitToChinese(null);
            XAssert.Equal("零元整", s);
        }

        public enum BuildingType
        {
            PING = 1,
            CUO = 2,
            YUE = 3,
            FUSHI = 4,
            KAIJIAN = 5,
            OTHER = 6
        }

        [Fact]
        public void TestCastUtil()
        {
            // 测试CastUtil的转换功能
            object obj = "123";
            int intValue = CastUtil.CastTo<int>(obj);
            XAssert.Equal(123, intValue);

            obj = 123;
            string strValue = CastUtil.CastTo<string>(obj);
            XAssert.Equal("123", strValue);

            obj = true;
            bool boolValue = CastUtil.CastTo<bool>(obj);
            XAssert.True(boolValue);
        }

        [Fact]
        public void TestConverterRegistry()
        {
            // 测试ConverterRegistry的功能
            var registry = ConverterRegistry.Instance;
            
            // 测试注册和获取转换器
            string str = "123";
            int intValue = registry.Convert<int>(str);
            XAssert.Equal(123, intValue);

            // 测试转换为不同类型
            long longValue = registry.Convert<long>(str);
            XAssert.Equal(123L, longValue);

            bool boolValue = registry.Convert<bool>("true");
            XAssert.True(boolValue);
        }
    }
}