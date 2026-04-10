using Xunit;
using WellTool.Core;
using WellTool.Core.IO.File;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Issue #I9IDAG 测试
    /// </summary>
    public class IssueI9IDAGTest
    {
        [Fact]
        [Trait("Category", "Disabled")]
        public void LoopFilesTest()
        {
            // Issue I9IDAG: 测试循环文件
            var files = PathUtil.LoopFiles("d:/m2_repo");
            foreach (var file in files)
            {
                Console.WriteLine(file.FullName);
            }
        }
    }
}
