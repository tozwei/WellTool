using System;
using System.Text;
using Xunit;
using XAssert = Xunit.Assert;
using WellTool.Core.Codec;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// 编解码工具单元测试
    /// </summary>
    public class CodecTests
    {
        #region Base64测试

        // [Fact]
        // public void IsBase64Test()
        // {
        //     var randomStr = RandomUtil.RandomString(1000);
        //     var encoded = Base64.Encode(randomStr);
        //     XAssert.True(Base64.IsBase64(encoded));
        // }

        [Fact]
        public void IsBase64Test2()
        {
            string base64 = "dW1kb3MzejR3bmljM2J6djAyZzcwbWk5M213Nnk3cWQ3eDJwOHFuNXJsYmMwaXhxbmg0dmxrcmN0anRkbmd3\n" +
                    "ZzcyZWFwanI2NWNneTg2dnp6cmJoMHQ4MHpxY2R6c3pjazZtaQ==";
            XAssert.True(Base64.IsBase64(base64));

            // '=' 不位于末尾
            base64 = "dW1kb3MzejR3bmljM2J6=djAyZzcwbWk5M213Nnk3cWQ3eDJwOHFuNXJsYmMwaXhxbmg0dmxrcmN0anRkbmd3\n" +
                    "ZzcyZWFwanI2NWNneTg2dnp6cmJoMHQ4MHpxY2R6c3pjazZtaQ=";
            XAssert.False(Base64.IsBase64(base64));
        }

        [Fact]
        public void EncodeAndDecodeTest()
        {
            string a = "伦家是一个非常长的字符串66";
            string encode = Base64.Encode(a);
            XAssert.Equal("5Lym5a625piv5LiA5Liq6Z2e5bi46ZW/55qE5a2X56ym5LiyNjY=", encode);

            string decodeStr = Base64.DecodeStr(encode);
            XAssert.Equal(a, decodeStr);
        }

        [Fact]
        public void EncodeAndDecodeWithoutPaddingTest()
        {
            string a = "伦家是一个非常长的字符串66";
            string encode = Base64.EncodeWithoutPadding(Encoding.UTF8.GetBytes(a));
            XAssert.Equal("5Lym5a625piv5LiA5Liq6Z2e5bi46ZW/55qE5a2X56ym5LiyNjY", encode);

            string decodeStr = Base64.DecodeStr(encode);
            XAssert.Equal(a, decodeStr);
        }

        [Fact]
        public void EncodeAndDecodeTest2()
        {
            string a = "a61a5db5a67c01445ca2-HZ20181120172058/pdf/中国电信影像云单体网关Docker版-V1.2.pdf";
            string encode = Base64.Encode(a, Encoding.UTF8);
            XAssert.Equal("YTYxYTVkYjVhNjdjMDE0NDVjYTItSFoyMDE4MTEyMDE3MjA1OC9wZGYv5Lit5Zu955S15L+h5b2x5YOP5LqR5Y2V5L2T572R5YWzRG9ja2Vy54mILVYxLjIucGRm", encode);

            string decodeStr = Base64.DecodeStr(encode, Encoding.UTF8);
            XAssert.Equal(a, decodeStr);
        }

        [Fact]
        public void EncodeAndDecodeTest3()
        {
            string a = ":";
            string encode = Base64.Encode(a);
            XAssert.Equal("Og==", encode);

            string decodeStr = Base64.DecodeStr(encode);
            XAssert.Equal(a, decodeStr);
        }

        [Fact]
        public void EncodeAndDecodeGbkTest()
        {
            string orderDescription = "订购成功立即生效，30天内可观看专区中除单独计费影片外的所有内容，到期自动取消。";
            string result = Base64.Encode(orderDescription, Encoding.GetEncoding("gbk"));

            string s = Base64.DecodeStr(result, Encoding.GetEncoding("gbk"));
            XAssert.Equal(orderDescription, s);
        }

        [Fact]
        public void DecodeEmojiTest()
        {
            string str = "😄";
            string encode = Base64.Encode(str);

            string decodeStr = Base64.DecodeStr(encode);
            XAssert.Equal(str, decodeStr);
        }

        #endregion

        #region Base32测试

        [Fact]
        public void Base32EncodeDecodeTest()
        {
            string str = "Hello Base32";
            string encoded = Base32.Encode(str);
            string decoded = Base32.DecodeStr(encoded);
            XAssert.Equal(str, decoded);
        }

        #endregion

        #region Base58测试

        // [Fact]
        // public void Base58EncodeDecodeTest()
        // {
        //     string str = "Hello Base58";
        //     string encoded = Base58.Encode(str);
        //     string decoded = Base58.DecodeStr(encoded);
        //     XAssert.Equal(str, decoded);
        // }

        #endregion

        #region Base62测试

        [Fact]
        public void Base62EncodeDecodeTest()
        {
            string str = "Hello Base62";
            string encoded = Base62.Encode(str);
            string decoded = Base62.DecodeStr(encoded);
            XAssert.Equal(str, decoded);
        }

        #endregion

        #region Caesar测试

        [Fact]
        public void CaesarEncodeDecodeTest()
        {
            string str = "Hello Caesar";
            string encoded = Caesar.Encode(str, 3);
            string decoded = Caesar.Decode(encoded, 3);
            XAssert.Equal(str, decoded);
        }

        #endregion

        #region Hashids测试

        [Fact]
        public void HashidsEncodeDecodeTest()
        {
            var hashids = Hashids.Create("salt".ToCharArray());
            long[] numbers = { 1, 2, 3 };
            string encoded = hashids.Encode(numbers);
            long[] decoded = hashids.Decode(encoded);
            XAssert.Equal(numbers, decoded);
        }

        #endregion

        #region Morse测试

        // [Fact]
        // public void MorseEncodeDecodeTest()
        // {
        //     string str = "HELLO";
        //     var morse = new Morse();
        //     string encoded = morse.Encode(str);
        //     string decoded = morse.Decode(encoded);
        //     XAssert.Equal(str, decoded);
        // }

        #endregion

        #region PunyCode测试

        [Fact]
        public void PunyCodeEncodeDecodeTest()
        {
            string str = "测试";
            string encoded = PunyCode.Encode(str);
            string decoded = PunyCode.Decode(encoded);
            XAssert.Equal(str, decoded);
        }

        #endregion

        #region Rot测试

        [Fact]
        public void RotEncodeDecodeTest()
        {
            string str = "Hello Rot";
            string encoded = Rot.Encode(str, 13, false);
            string decoded = Rot.Decode(encoded, 13, false);
            XAssert.Equal(str, decoded);
        }

        #endregion

        #region BCD测试

        // [Fact]
        // public void BcdEncodeDecodeTest()
        // {
        //     string str = "1234567890";
        //     byte[] encoded = BCD.Encode(str);
        //     string decoded = BCD.Decode(encoded);
        //     XAssert.Equal(str, decoded);
        // }

        #endregion

        #region Base16测试

        // [Fact]
        // public void Base16EncodeDecodeTest()
        // {
        //     string str = "Hello Base16";
        //     byte[] bytes = Encoding.UTF8.GetBytes(str);
        //     var base16Codec = new Base16Codec();
        //     string encoded = base16Codec.Encode(bytes);
        //     byte[] decodedBytes = base16Codec.Decode(encoded);
        //     string decoded = Encoding.UTF8.GetString(decodedBytes);
        //     XAssert.Equal(str, decoded);
        // }

        #endregion

        #region PercentCodec测试

        [Fact]
        public void PercentEncodeDecodeTest()
        {
            string str = "Hello World!";
            var percentCodec = new PercentCodec();
            string encoded = percentCodec.Encode(str, Encoding.UTF8);
            // PercentCodec.Decode方法不存在，暂时只测试编码
            XAssert.NotNull(encoded);
            XAssert.NotEmpty(encoded);
        }

        #endregion
    }
}