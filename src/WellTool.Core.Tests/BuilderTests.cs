using System;
using System.Collections.Generic;
using Xunit;
using XAssert = Xunit.Assert;
using WellTool.Core.Builder;
using WellTool.Core.Lang;

namespace WellTool.Core.Tests
{
    public class BuilderTests
    {
        [Fact]
        public void Test()
        {
            var box = GenericBuilder<Box>
                .Of(() => new Box())
                .With(b => b.Id = 1024L)
                .With(b => b.Title = "Hello World!")
                .With(b => b.Length = 9)
                .With(b => b.Width = 8)
                .With(b => b.Height = 7)
                .Build();

            XAssert.Equal(1024L, box.Id);
            XAssert.Equal("Hello World!", box.Title);
            XAssert.Equal(9, box.Length);
            XAssert.Equal(8, box.Width);
            XAssert.Equal(7, box.Height);

            // 对象修改
            var boxModified = GenericBuilder<Box>
                .Of(() => box)
                .With(b => b.Title = "Hello Friend!")
                .With(b => b.Length = 3)
                .With(b => b.Width = 4)
                .With(b => b.Height = 5)
                .Build();

            XAssert.Equal(1024L, boxModified.Id);
            XAssert.Equal("Hello Friend!", box.Title);
            XAssert.Equal(3, boxModified.Length);
            XAssert.Equal(4, boxModified.Width);
            XAssert.Equal(5, boxModified.Height);

            // 多参数构造
            var box1 = GenericBuilder<Box>
                .Of(() => new Box(2048L, "Hello Partner!", 222, 333, 444))
                .With(b => b.Alis())
                .Build();

            XAssert.Equal(2048L, box1.Id);
            XAssert.Equal("Hello Partner!", box1.Title);
            XAssert.Equal(222, box1.Length);
            XAssert.Equal(333, box1.Width);
            XAssert.Equal(444, box1.Height);
            XAssert.Equal("TomXin:\"Hello Partner!\"", box1.TitleAlias);
        }

        [Fact]
        public void BuildMapTest()
        {
            //Map创建
            var colorMap = GenericBuilder<Dictionary<string, string>>
                .Of(() => new Dictionary<string, string>())
                .With(m => m.Add("red", "#FF0000"))
                .With(m => m.Add("yellow", "#FFFF00"))
                .With(m => m.Add("blue", "#0000FF"))
                .Build();
            XAssert.Equal("#FF0000", colorMap["red"]);
            XAssert.Equal("#FFFF00", colorMap["yellow"]);
            XAssert.Equal("#0000FF", colorMap["blue"]);
        }

        public class Box
        {
            public long Id { get; set; }
            public string Title { get; set; }
            public int Length { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
            public string TitleAlias { get; set; }

            public Box()
            {}

            public Box(long id, string title, int length, int width, int height)
            {
                Id = id;
                Title = title;
                Length = length;
                Width = width;
                Height = height;
            }

            public void Alis()
            {
                if (StringUtil.IsNotBlank(Title))
                {
                    TitleAlias = "TomXin:\"" + Title + "\"";
                }
            }
        }
    }
}