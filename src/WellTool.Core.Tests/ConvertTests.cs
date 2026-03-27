using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using WellTool.Core.Converter;
using WellTool.Core.Date;
using WellTool.Core.Lang;
using WellTool.Core.Map;
using WellTool.Core.Util;

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
            var result = Convert.ConvertObject(typeof(object), "aaaa");
            Assert.Equal("aaaa", result);
        }

        [Fact]
        public void ToStrTest()
        {
            int a = 1;
            long[] b = { 1, 2, 3, 4, 5 };

            Assert.Equal("[1, 2, 3, 4, 5]", Convert.ConvertObject(typeof(string), b));

            string aStr = Convert.ToStr(a);
            Assert.Equal("1", aStr);
            string bStr = Convert.ToStr(b);
            Assert.Equal("[1, 2, 3, 4, 5]", Convert.ToStr(bStr));
        }

        [Fact]
        public void ToStrTest2()
        {
            var result = Convert.ConvertObject(typeof(string), "aaaa");
            Assert.Equal("aaaa", result);
        }

        [Fact]
        public void ToStrTest3()
        {
            char a = 'a';
            var result = Convert.ConvertObject(typeof(string), a);
            Assert.Equal("a", result);
        }

        [Fact]
        public void ToStrTest4()
        {
            // 被当作八进制
            string result = Convert.ToStr(001200);
            Assert.Equal("640", result);
        }

        [Fact]
        public void ToIntTest()
        {
            string a = " 34232";
            int? aInteger = Convert.ToInt(a);
            Assert.Equal(34232, aInteger);
            int aInt = ConverterRegistry.Instance.Convert<int>(a);
            Assert.Equal(34232, aInt);

            // 带小数测试
            string b = " 34232.00";
            int? bInteger = Convert.ToInt(b);
            Assert.Equal(34232, bInteger);
            int bInt = ConverterRegistry.Instance.Convert<int>(b);
            Assert.Equal(34232, bInt);

            // boolean测试
            bool c = true;
            int? cInteger = Convert.ToInt(c);
            Assert.Equal(1, cInteger);
            int cInt = ConverterRegistry.Instance.Convert<int>(c);
            Assert.Equal(1, cInt);

            // boolean测试
            string d = "08";
            int? dInteger = Convert.ToInt(d);
            Assert.Equal(8, dInteger);
            int dInt = ConverterRegistry.Instance.Convert<int>(d);
            Assert.Equal(8, dInt);
        }

        [Fact]
        public void ToIntTest2()
        {
            var array = new List<string>();
            int? aInt = Convert.ConvertQuietly<int>(array, -1);
            Assert.Equal(-1, aInt);
        }

        [Fact]
        public void ToLongTest()
        {
            string a = " 342324545435435";
            long? aLong = Convert.ToLong(a);
            Assert.Equal(342324545435435L, aLong);
            long aLong2 = ConverterRegistry.Instance.Convert<long>(a);
            Assert.Equal(342324545435435L, aLong2);

            // 带小数测试
            string b = " 342324545435435.245435435";
            long? bLong = Convert.ToLong(b);
            Assert.Equal(342324545435435L, bLong);
            long bLong2 = ConverterRegistry.Instance.Convert<long>(b);
            Assert.Equal(342324545435435L, bLong2);

            // boolean测试
            bool c = true;
            long? cLong = Convert.ToLong(c);
            Assert.Equal(1L, cLong);
            long cLong2 = ConverterRegistry.Instance.Convert<long>(c);
            Assert.Equal(1L, cLong2);

            // boolean测试
            string d = "08";
            long? dLong = Convert.ToLong(d);
            Assert.Equal(8L, dLong);
            long dLong2 = ConverterRegistry.Instance.Convert<long>(d);
            Assert.Equal(8L, dLong2);
        }

        [Fact]
        public void ToCharTest()
        {
            string str = "aadfdsfs";
            char? c = Convert.ToChar(str);
            Assert.Equal('a', c);

            // 转换失败
            object str2 = "";
            char? c2 = Convert.ToChar(str2);
            Assert.Null(c2);
        }

        [Fact]
        public void ToNumberTest()
        {
            object a = "12.45";
            decimal? number = Convert.ToDecimal(a);
            Assert.Equal(12.45M, number);
        }

        [Fact]
        public void EmptyToNumberTest()
        {
            object a = "";
            decimal? number = Convert.ToDecimal(a);
            Assert.Null(number);
        }

        [Fact]
        public void IntAndByteConvertTest()
        {
            // 测试 int 转 byte
            int int0 = 234;
            byte byte0 = Convert.IntToByte(int0);
            Assert.Equal((byte)-22, byte0);

            int int1 = Convert.ByteToUnsignedInt(byte0);
            Assert.Equal(int0, int1);
        }

        [Fact]
        public void IntAndBytesTest()
        {
            // 测试 int 转 byte 数组
            int int2 = 1417;
            byte[] bytesInt = Convert.IntToBytes(int2);

            // 测试 byte 数组转 int
            int int3 = Convert.BytesToInt(bytesInt);
            Assert.Equal(int2, int3);
        }

        [Fact]
        public void LongAndBytesTest()
        {
            // 测试 long 转 byte 数组
            long long1 = 2223;

            byte[] bytesLong = Convert.LongToBytes(long1);
            long long2 = Convert.BytesToLong(bytesLong);

            Assert.Equal(long1, long2);
        }

        [Fact]
        public void ShortAndBytesTest()
        {
            short short1 = 122;
            byte[] bytes = Convert.ShortToBytes(short1);
            short short2 = Convert.BytesToShort(bytes);

            Assert.Equal(short2, short1);
        }

        [Fact]
        public void ToListTest()
        {
            var list = new List<string> { "1", "2" };
            string str = Convert.ToStr(list);
            var list2 = Convert.ToList<string>(str);
            Assert.Equal("1", list2[0]);
            Assert.Equal("2", list2[1]);

            var list3 = Convert.ToList<int>(str);
            Assert.Equal(1, list3[0]);
            Assert.Equal(2, list3[1]);
        }

        [Fact]
        public void ToListTest2()
        {
            string str = "1,2";
            var list2 = Convert.ToList<string>(str);
            Assert.Equal("1", list2[0]);
            Assert.Equal("2", list2[1]);

            var list3 = Convert.ToList<int>(str);
            Assert.Equal(1, list3[0]);
            Assert.Equal(2, list3[1]);
        }

        [Fact]
        public void ToSetTest()
        {
            var result = Convert.ConvertObject(typeof(HashSet<int>), "1,2,3") as HashSet<int>;
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
            Assert.Contains(1, result);
            Assert.Contains(2, result);
            Assert.Contains(3, result);
        }

        [Fact]
        public void ToDateTest()
        {
            Assert.Throws<DateException>(() => {
                // 默认转换失败报错而不是返回null
                Convert.ConvertObject(typeof(DateTime), "aaaa");
            });
        }

        [Fact]
        public void ToDateTest2()
        {
            var date = Convert.ToDate("2021-01");
            Assert.Null(date);
        }

        [Fact]
        public void ToBigDecimalTest()
        {
            // https://github.com/chinabugotech/hutool/issues/1818
            string str = "33020000210909112800000124";
            decimal? bigDecimal = Convert.ToDecimal(str);
            Assert.Equal(str, bigDecimal.ToString());
        }

        [Fact]
        public void ToFloatTest()
        {
            // https://gitee.com/chinabugotech/hutool/issues/I4M0E4
            string hex2 = "CD0CCB43";
            byte[] value = HexUtil.DecodeHex(hex2);
            float? f = Convert.ToFloat(value);
            Assert.Equal(406.1F, f);
        }

        [Fact]
        public void FloatToDoubleTest()
        {
            float a = 0.45f;
            double? b = Convert.ToDouble(a);
            Assert.Equal(0.45D, b);
        }

        [Fact]
        public void DoubleToFloatTest()
        {
            double a = 0.45f;
            float? b = Convert.ToFloat(a);
            Assert.Equal(a, b);
        }

        [Fact]
        public void LocalDateTimeToLocalDateTest()
        {
            var localDateTime = DateTime.Now;
            var convert = Convert.ConvertObject(typeof(DateOnly), localDateTime) as DateOnly;
            Assert.Equal(DateOnly.FromDateTime(localDateTime), convert);
        }

        [Fact]
        public void ToSBCTest()
        {
            string s = Convert.ToSBC(null);
            Assert.Null(s);
        }

        [Fact]
        public void ToDBCTest()
        {
            string s = Convert.ToDBC(null);
            Assert.Null(s);
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
            Assert.Equal(67556, Convert.ChineseMoneyToNumber("陆万柒仟伍佰伍拾陆圆").Value);
            Assert.Equal(67556, Convert.ChineseMoneyToNumber("陆万柒仟伍佰伍拾陆元").Value);
            Assert.Equal(0.3D, Convert.ChineseMoneyToNumber("叁角").Value, 0);
            Assert.Equal(0.02, Convert.ChineseMoneyToNumber("贰分").Value, 0);
            Assert.Equal(67556.3, Convert.ChineseMoneyToNumber("陆万柒仟伍佰伍拾陆元叁角").Value, 0);
            Assert.Equal(67556.02, Convert.ChineseMoneyToNumber("陆万柒仟伍佰伍拾陆元贰分").Value, 0);
            Assert.Equal(0.32, Convert.ChineseMoneyToNumber("叁角贰分").Value, 0);
            Assert.Equal(67556.32, Convert.ChineseMoneyToNumber("陆万柒仟伍佰伍拾陆元叁角贰分").Value, 0);
        }

        [Fact]
        public void Issue3662Test()
        {
            string s = Convert.DigitToChinese(0);
            Assert.Equal("零元整", s);

            s = Convert.DigitToChinese(null);
            Assert.Equal("零元整", s);
        }

        public enum BuildingType
        {
            PING(1, "平层"),
            CUO(2, "错层"),
            YUE(3, "跃层"),
            FUSHI(4, "复式"),
            KAIJIAN(5, "开间"),
            OTHER(6, "其他");

            public int Id { get; }
            public string Name { get; }

            BuildingType(int id, string name)
            {
                Id = id;
                Name = name;
            }
        }
    }
}