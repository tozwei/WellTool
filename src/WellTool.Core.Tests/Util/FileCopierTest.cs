using WellTool.Core.IO;
using Xunit;
using System.IO;

namespace WellTool.Core.Tests;

public class FileCopierTest
{
    [Fact]
    public void CopyFileTest()
    {
        var tempDir = Path.GetTempPath();
        var srcFile = Path.Combine(tempDir, "src_" + Guid.NewGuid().ToString("N") + ".txt");
        var dstFile = Path.Combine(tempDir, "dst_" + Guid.NewGuid().ToString("N") + ".txt");
        
        try
        {
            File.WriteAllText(srcFile, "Hello, World!");
            var copier = new FileCopier(srcFile, dstFile);
            copier.Copy();
            Assert.True(File.Exists(dstFile));
            Assert.Equal("Hello, World!", File.ReadAllText(dstFile));
        }
        finally
        {
            if (File.Exists(srcFile)) File.Delete(srcFile);
            if (File.Exists(dstFile)) File.Delete(dstFile);
        }
    }

    [Fact]
    public void CopyWithOverwriteTest()
    {
        var tempDir = Path.GetTempPath();
        var srcFile = Path.Combine(tempDir, "src_" + Guid.NewGuid().ToString("N") + ".txt");
        var dstFile = Path.Combine(tempDir, "dst_" + Guid.NewGuid().ToString("N") + ".txt");
        
        try
        {
            File.WriteAllText(srcFile, "Source");
            File.WriteAllText(dstFile, "Dest");
            var copier = new FileCopier(srcFile, dstFile);
            copier.Copy();
            Assert.Equal("Source", File.ReadAllText(dstFile));
        }
        finally
        {
            if (File.Exists(srcFile)) File.Delete(srcFile);
            if (File.Exists(dstFile)) File.Delete(dstFile);
        }
    }
}

public class FileCopier
{
    private readonly string _src;
    private readonly string _dst;

    public FileCopier(string src, string dst)
    {
        _src = src;
        _dst = dst;
    }

    public void Copy()
    {
        File.Copy(_src, _dst, true);
    }
}
